using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DOL.GS.Effects;
using DOL.GS.Spells;

namespace DOL.GS
{
    // Component for holding persistent effects on the player.
    public class EffectListComponent : IManagedEntity
    {
        private int _lastUpdateEffectsCount;
        private int _usedConcentration;

        public GameLiving Owner { get; }
        public EntityManagerId EntityManagerId { get; set; } = new(EntityManager.EntityType.EffectListComponent, false);
        public Dictionary<eEffect, List<ECSGameEffect>> Effects { get; } = [];
        public object EffectsLock { get; } = new();
        public List<ECSGameSpellEffect> ConcentrationEffects { get; } = new List<ECSGameSpellEffect>(20);
        public object ConcentrationEffectsLock { get; } = new();
        private readonly Dictionary<int, ECSGameEffect> EffectIdToEffect = [];

        public int UsedConcentration => Interlocked.CompareExchange(ref _usedConcentration, 0, 0);

        public EffectListComponent(GameLiving owner)
        {
            Owner = owner;
        }

        public bool AddEffect(ECSGameEffect effect)
        {
            lock (EffectsLock)
            {
                try
                {
                    // Dead owners don't get effects
                    if (!Owner.IsAlive)
                        return false;

                    EntityManager.Add(this);

                    if (effect is ECSGameAbilityEffect)
                    {
                        if (Effects.TryGetValue(effect.EffectType, out List<ECSGameEffect> existingEffects))
                            existingEffects.Add(effect);
                        else
                            Effects.Add(effect.EffectType, [effect]);

                        EffectIdToEffect.TryAdd(effect.Icon, effect);
                        return true;
                    }

                    if (effect is ECSGameSpellEffect newSpellEffect && Effects.TryGetValue(effect.EffectType, out List<ECSGameEffect> existingGameEffects))
                    {
                        ISpellHandler newSpellHandler = newSpellEffect.SpellHandler;
                        Spell newSpell = newSpellHandler.Spell;

                        // RAs use spells with an ID of 0. Differentiating them is tricky and requires some rewriting.
                        // So for now let's prevent overwriting / coexistence altogether.
                        if (newSpell.ID == 0)
                            return false;

                        List<ECSGameSpellEffect> existingEffects = existingGameEffects.Cast<ECSGameSpellEffect>().ToList();

                        // Effects contains this effect already so refresh it
                        if (existingEffects.FirstOrDefault(e => e.SpellHandler.Spell.ID == newSpell.ID || (newSpell.EffectGroup > 0 && e.SpellHandler.Spell.EffectGroup == newSpell.EffectGroup && newSpell.IsPoisonEffect)) != null)
                        {
                            if (newSpellEffect.IsConcentrationEffect() && !newSpellEffect.RenewEffect)
                                return false;
                            for (int i = 0; i < existingEffects.Count; i++)
                            {
                                ECSGameSpellEffect existingEffect = existingEffects[i];
                                ISpellHandler existingSpellHandler = existingEffect.SpellHandler;
                                Spell existingSpell = existingSpellHandler.Spell;

                                // Console.WriteLine($"Effect found! Name: {existingEffect.Name}, Effectiveness: {existingEffect.Effectiveness}, Poison: {existingSpell.IsPoisonEffect}, ID: {existingSpell.ID}, EffectGroup: {existingSpell.EffectGroup}");

                                if ((existingSpell.IsPulsing && effect.SpellHandler.Caster.ActivePulseSpells.ContainsKey(effect.SpellHandler.Spell.SpellType) && existingSpell.ID == newSpell.ID)
                                    || (existingSpell.IsConcentration && existingEffect == newSpellEffect)
                                    || existingSpell.ID == newSpell.ID)
                                {
                                    if (newSpell.IsPoisonEffect && newSpellEffect.EffectType == eEffect.DamageOverTime)
                                    {
                                        existingEffect.ExpireTick = newSpellEffect.ExpireTick;
                                        newSpellEffect.IsBuffActive = true; // Why?
                                    }
                                    else
                                    {
                                        if (newSpell.IsPulsing)
                                            newSpellEffect.RenewEffect = true;

                                        // If the effectiveness changed (for example after resurrection illness expired), we need to stop the effect.
                                        // It will be restarted in 'EffectService.HandlePropertyModification()'.
                                        // Also needed for movement speed debuffs' since their effect decrease over time.
                                        if (existingEffect.Effectiveness != newSpellEffect.Effectiveness ||
                                            existingSpell.SpellType is eSpellType.SpeedDecrease or eSpellType.UnbreakableSpeedDecrease)
                                            existingEffect.OnStopEffect();

                                        newSpellEffect.IsDisabled = existingEffect.IsDisabled;
                                        newSpellEffect.IsBuffActive = existingEffect.IsBuffActive;
                                        newSpellEffect.PreviousPosition = GetAllEffects().IndexOf(existingEffect);
                                        Effects[newSpellEffect.EffectType][i] = newSpellEffect;
                                        EffectIdToEffect[newSpellEffect.Icon] = newSpellEffect;
                                    }

                                    return true;
                                }
                            }
                        }
                        else if (effect.EffectType is eEffect.SavageBuff or eEffect.ArmorAbsorptionBuff)
                        {
                            if (effect.EffectType is eEffect.ArmorAbsorptionBuff)
                            {
                                for (int i = 0; i < existingEffects.Count; i++)
                                {
                                    // Better Effect so disable the current Effect.
                                    if (newSpellEffect.SpellHandler.Spell.Value >
                                        existingEffects[i].SpellHandler.Spell.Value)
                                    {
                                        EffectService.RequestDisableEffect(existingEffects[i]);
                                        Effects[newSpellEffect.EffectType].Add(newSpellEffect);
                                        EffectIdToEffect.TryAdd(newSpellEffect.Icon, newSpellEffect);
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                            // Player doesn't have this buff yet.
                            else if (!existingEffects.Where(e => e.SpellHandler.Spell.SpellType == newSpellEffect.SpellHandler.Spell.SpellType).Any())
                            {
                                Effects[newSpellEffect.EffectType].Add(newSpellEffect);
                                EffectIdToEffect.TryAdd(newSpellEffect.Icon, newSpellEffect);
                                return true;
                            }
                            else
                                return false;
                        }
                        else
                        {
                            bool addEffect = false;
                            // foundIsOverwriteableEffect is a bool for if we find an overwriteable effect when looping over existing effects. Will be used to later to add effects that are not in same effect group.
                            bool foundIsOverwriteableEffect = false;

                            for (int i = 0; i < existingEffects.Count; i++)
                            {
                                ECSGameSpellEffect existingEffect = existingEffects[i];
                                ISpellHandler existingSpellHandler = existingEffect.SpellHandler;
                                Spell existingSpell = existingSpellHandler.Spell;

                                // Check if existingEffect is overwritable by new effect.
                                if (existingSpellHandler.IsOverwritable(newSpellEffect) || newSpellEffect.EffectType == eEffect.MovementSpeedDebuff)
                                {
                                    foundIsOverwriteableEffect = true;

                                    if (effect.EffectType == eEffect.Bladeturn)
                                    {
                                        // PBT should only replace itself.
                                        if (!newSpell.IsPulsing)
                                        {
                                            // Self cast Bladeturns should never be overwritten.
                                            if (existingSpell.Target != eSpellTarget.SELF)
                                            {
                                                EffectService.RequestCancelEffect(existingEffect);
                                                addEffect = true;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (existingEffect.IsDisabled)
                                            continue;

                                        // Special handling for ablative effects.
                                        // We use the remaining amount instead of the spell value. They also can't be added as disabled effects.
                                        // Note: This ignores subclasses of 'AblativeArmorSpellHandler', so right know we only allow one ablative buff regardless of its type.
                                        if (effect.EffectType == eEffect.AblativeArmor &&
                                            existingEffect is AblativeArmorECSGameEffect existingAblativeEffect)
                                        {
                                            // 'Damage' represents the absorption% per hit.
                                            if (newSpell.Value * AblativeArmorSpellHandler.ValidateSpellDamage((int)newSpell.Damage) >
                                                existingAblativeEffect.RemainingValue *  AblativeArmorSpellHandler.ValidateSpellDamage((int)existingSpell.Damage))
                                            {
                                                EffectService.RequestCancelEffect(existingEffect);
                                                addEffect = true;
                                            }

                                            break;
                                        }

                                        if (newSpellEffect.IsBetterThan(existingEffect))
                                        {
                                            // New effect is better than the current effect. Disable or cancel the current effect.
                                            if (newSpell.IsHelpful && (newSpellHandler.Caster != existingSpellHandler.Caster
                                                || newSpellHandler.SpellLine.KeyName == GlobalSpellsLines.Potions_Effects
                                                || existingSpellHandler.SpellLine.KeyName == GlobalSpellsLines.Potions_Effects))
                                                EffectService.RequestDisableEffect(existingEffect);
                                            else
                                                EffectService.RequestCancelEffect(existingEffect);

                                            addEffect = true;
                                            break;
                                        }
                                        else
                                        {
                                            // New effect is not as good as the current effect, but it can be added in a disabled state.
                                            if (newSpell.IsHelpful && (newSpellHandler.Caster != existingSpellHandler.Caster
                                                || newSpellHandler.SpellLine.KeyName == GlobalSpellsLines.Potions_Effects
                                                || existingSpellHandler.SpellLine.KeyName == GlobalSpellsLines.Potions_Effects))
                                            {
                                                addEffect = true;
                                                newSpellEffect.IsDisabled = true;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }

                            // No overwriteable effects found that match new spell effect, so add it!
                            if (!foundIsOverwriteableEffect)
                                addEffect = true;

                            if (addEffect)
                            {
                                Effects[newSpellEffect.EffectType].Add(newSpellEffect);

                                if (effect.EffectType != eEffect.Pulse && effect.Icon != 0)
                                    EffectIdToEffect.TryAdd(newSpellEffect.Icon, newSpellEffect);

                                return true;
                            }
                        }
                        //Console.WriteLine("Effect List contains type: " + effect.EffectType.ToString() + " (" + effect.Owner.Name + ")");
                        return false;
                    }
                    else if (Effects.TryGetValue(effect.EffectType, out List<ECSGameEffect> existingEffects))
                        existingEffects.Add(effect);
                    else
                    {
                        Effects.Add(effect.EffectType, new List<ECSGameEffect> { effect });

                        if (effect.EffectType != eEffect.Pulse && effect.Icon != 0)
                            EffectIdToEffect.TryAdd(effect.Icon, effect);
                    }

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error adding an Effect {e}");
                    return false;
                }
            }
        }

        public void AddUsedConcentration(int amount)
        {
            Interlocked.Add(ref _usedConcentration, amount);
        }

        public List<ECSGameEffect> GetAllEffects()
        {
            lock (EffectsLock)
            {
                var temp = new List<ECSGameEffect>();
                foreach (var effects in Effects.Values.ToList())
                {
                    for (int j = 0; j < effects?.Count; j++)
                    {
                        if (effects[j].EffectType != eEffect.Pulse)
                            temp.Add(effects[j]);
                    }
                }

                return temp.OrderBy(e => e.StartTick).ToList();
            }
        }

        public List<ECSPulseEffect> GetAllPulseEffects()
        {
            lock (EffectsLock)
            {
                var temp = new List<ECSPulseEffect>();
                foreach (var effects in Effects.Values.ToList())
                {
                    for (int j = 0; j < effects?.Count; j++)
                    {
                        if (effects[j].EffectType == eEffect.Pulse)
                            temp.Add((ECSPulseEffect)effects[j]);
                    }
                }

                return temp;
            }
        }

        public List<IConcentrationEffect> GetConcentrationEffects()
        {
            lock (EffectsLock)
            {
                var temp = new List<IConcentrationEffect>();
                var allEffects = Effects.Values.ToList();

                if (allEffects != null)
                {
                    foreach (var effects in allEffects)
                    {
                        for (int j = 0; j < effects?.Count; j++)
                        {
                            if (effects[j] is ECSPulseEffect || effects[j].IsConcentrationEffect())
                                temp.Add(effects[j] as IConcentrationEffect);
                        }
                    }
                }

                return temp;
            }
        }

        public ECSGameSpellEffect GetBestDisabledSpellEffect(eEffect effectType = eEffect.Unknown)
        {
            lock (EffectsLock)
            {
                return GetSpellEffects(effectType)?.Where(x => x.IsDisabled).OrderByDescending(x => x.SpellHandler.Spell.Value).FirstOrDefault();
            }
        }

        public List<ECSGameSpellEffect> GetSpellEffects(eEffect effectType = eEffect.Unknown)
        {
            lock (EffectsLock)
            {
                var temp = new List<ECSGameSpellEffect>();
                foreach (var effects in Effects.Values.ToList())
                {
                    for (int j = 0; j < effects?.Count; j++)
                    {
                        if (effects[j] is ECSGameSpellEffect)
                        {
                            if (effectType != eEffect.Unknown)
                            {
                                if (effects[j].EffectType == effectType)
                                    temp.Add(effects[j] as ECSGameSpellEffect);
                            }
                            else
                                temp.Add(effects[j] as ECSGameSpellEffect);
                        }
                    }
                }

                return temp.OrderBy(e => e.StartTick).ToList();
            }
        }

        public List<ECSGameAbilityEffect> GetAbilityEffects()
        {
            lock (EffectsLock)
            {
                var temp = new List<ECSGameAbilityEffect>();
                foreach (var effects in Effects.Values.ToList())
                {
                    for (int j = 0; j < effects?.Count; j++)
                    {
                        if (effects[j] is ECSGameAbilityEffect)
                            temp.Add(effects[j] as ECSGameAbilityEffect);
                    }
                }

                return temp.OrderBy(e => e.StartTick).ToList();
            }
        }

        public ECSGameEffect TryGetEffectFromEffectId(int effectId)
        {
            EffectIdToEffect.TryGetValue(effectId, out var effect);
            return effect;
        }

        public ref int GetLastUpdateEffectsCount()
        {
            return ref _lastUpdateEffectsCount;
        }

        public bool RemoveEffect(ECSGameEffect effect)
        {
            lock (EffectsLock)
            {
                try
                {
                    if (!Effects.TryGetValue(effect.EffectType, out List<ECSGameEffect> existingEffects))
                        return false;

                    if (effect.CancelEffect)
                    {
                        // Get the effectToRemove from the Effects list. Had issues trying to remove the effect directly from the list if it wasn't the same object.
                        ECSGameEffect effectToRemove = existingEffects.FirstOrDefault(e => e.Name == effect.Name);
                        existingEffects.Remove(effectToRemove);
                        EffectIdToEffect.Remove(effect.Icon);

                        if (existingEffects.Count == 0)
                        {
                            EffectIdToEffect.Remove(effect.Icon);
                            Effects.Remove(effect.EffectType);
                        }
                    }

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error removing an Effect from {effect.Owner}'s EffectList {e}");
                    return false;
                }
            }
        }

        public bool ContainsEffectForEffectType(eEffect effectType)
        {
            try
            {
                if (Effects.ContainsKey(effectType))
                    return true;
                else
                    return false;
            } 
            catch
            {
                //Console.WriteLine($"Error attempting to check effect type");
                return false;
            }
        }

        public void CancelAll()
        {
            foreach (var effects in Effects.Values.ToList())
            {
                for (int j = 0; j < effects.Count; j++)
                    EffectService.RequestCancelEffect(effects[j]);
            }
        }
    }
}
