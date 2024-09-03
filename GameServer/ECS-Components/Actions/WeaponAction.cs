﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DOL.Database;
using DOL.GS.PacketHandler;
using DOL.GS.Scripts;
using DOL.GS.Spells;
using DOL.GS.Styles;
using static DOL.GS.GameObject;

namespace DOL.GS
{
    public class WeaponAction
    {
        protected readonly GameLiving m_owner;
        protected readonly GameObject m_target;
        protected readonly DbInventoryItem m_attackWeapon;
        protected readonly DbInventoryItem m_leftWeapon;
        protected readonly double m_effectiveness;
        protected readonly int m_interval;
        protected readonly Style m_combatStyle;
        protected readonly eRangedAttackType m_RangedAttackType;

        // The ranged attack type at the time the shot was released.
        public eRangedAttackType RangedAttackType => m_RangedAttackType;

        public bool AttackFinished { get; set; }
        public eActiveWeaponSlot ActiveWeaponSlot { get; }

        public WeaponAction(GameLiving owner, GameObject target, DbInventoryItem attackWeapon, DbInventoryItem leftWeapon, double effectiveness, int interval, Style combatStyle)
        {
            m_owner = owner;
            m_target = target;
            m_attackWeapon = attackWeapon;
            m_leftWeapon = leftWeapon;
            m_effectiveness = effectiveness;
            m_interval = interval;
            m_combatStyle = combatStyle;
            ActiveWeaponSlot = owner.ActiveWeaponSlot;
        }

        public WeaponAction(GameLiving owner, GameObject target, DbInventoryItem attackWeapon, double effectiveness, int interval, eRangedAttackType rangedAttackType)
        {
            m_owner = owner;
            m_target = target;
            m_attackWeapon = attackWeapon;
            m_effectiveness = effectiveness;
            m_interval = interval;
            m_RangedAttackType = rangedAttackType;
            ActiveWeaponSlot = owner.ActiveWeaponSlot;
        }

        public virtual void Execute()
        {
            AttackFinished = true;

            // Crash fix since its apparently possible to get here with a null target.
            if (m_target == null)
                return;

            Style style = m_combatStyle;
            int leftHandSwingCount = 0;
            AttackData mainHandAD = null;
            AttackData leftHandAD = null;
            DbInventoryItem mainWeapon = m_attackWeapon;
            DbInventoryItem leftWeapon = m_leftWeapon;
            double leftHandEffectiveness = m_effectiveness;
            double mainHandEffectiveness = m_effectiveness;

            // GameNPC can dual swing even with no weapon.
            if (m_owner is GameNPC && m_owner is not MimicNPC && m_owner.attackComponent.CanUseLefthandedWeapon)
                leftHandSwingCount = m_owner.attackComponent.CalculateLeftHandSwingCount();
            else if (m_owner.attackComponent.CanUseLefthandedWeapon &&
                     leftWeapon != null &&
                     leftWeapon.Object_Type != (int)eObjectType.Shield &&
                     mainWeapon != null &&
                     mainWeapon.Hand != 1)
                leftHandSwingCount = m_owner.attackComponent.CalculateLeftHandSwingCount();

            // CMH
            // 1.89
            //- Pets will no longer continue to attack a character after the character has stealthed.
            // 1.88
            //- Monsters, pets and Non-Player Characters (NPCs) will now halt their pursuit when the character being chased stealths.
            /*
            if (owner is GameNPC
                && m_target is GamePlayer
                && ((GamePlayer)m_target).IsStealthed
                && !(owner is GameSummonedPet))
            {
                // note due to the 2 lines above all npcs stop attacking
                GameNPC npc = (GameNPC)owner;
                npc.attackComponent.NPCStopAttack();
                npc.TargetObject = null;
                //Stop(); // stop the full tick timer? looks like other code is doing this

                // target death caused this below, so I'm replicating it
                if (npc.ActiveWeaponSlot != eActiveWeaponSlot.Distance &&
                    npc.Inventory != null &&
                    npc.Inventory.GetItem(eInventorySlot.DistanceWeapon) != null)
                    npc.SwitchWeapon(eActiveWeaponSlot.Distance);
                return;
            }*/

            bool usingOH = false;
            m_owner.attackComponent.UsedHandOnLastDualWieldAttack = 0;

            if (leftHandSwingCount > 0)
            {
                if ((m_owner is GameNPC && m_owner is not MimicNPC) ||
                    mainWeapon.Object_Type == (int)eObjectType.HandToHand || 
                    leftWeapon?.Object_Type == (int)eObjectType.HandToHand || 
                    mainWeapon.Object_Type == (int)eObjectType.TwoHandedWeapon || 
                    mainWeapon.Object_Type == (int)eObjectType.Thrown ||
                    mainWeapon.SlotPosition == Slot.RANGED)
                    usingOH = false;
                else
                {
                    usingOH = true;
                    m_owner.attackComponent.UsedHandOnLastDualWieldAttack = 2;
                }

                // Both hands are used for attack.
                mainHandAD = m_owner.attackComponent.MakeAttack(this, m_target, mainWeapon, style, mainHandEffectiveness, m_interval, usingOH);

                if (style == null)
                    mainHandAD.AnimationId = -2; // Virtual code for both weapons swing animation.
            }
            else if (mainWeapon != null)
            {
                if (m_owner is GameNPC && m_owner is not MimicNPC ||
                    mainWeapon.Item_Type == Slot.TWOHAND ||
                    mainWeapon.SlotPosition == Slot.RANGED)
                    usingOH = false;
                else if (leftWeapon != null && leftWeapon.Object_Type != (int)eObjectType.Shield)
                    usingOH = true;

                // One of two hands is used for attack if no style, treated as a main hand attack.
                if (usingOH && style == null && Util.Chance(50))
                {
                    m_owner.attackComponent.UsedHandOnLastDualWieldAttack = 1;
                    mainWeapon = leftWeapon;
                    mainHandAD = m_owner.attackComponent.MakeAttack(this, m_target, mainWeapon, style, mainHandEffectiveness, m_interval, false);
                    mainHandAD.AnimationId = -1; // Virtual code for left weapons swing animation.
                }
                else
                    mainHandAD = m_owner.attackComponent.MakeAttack(this, m_target, mainWeapon, style, mainHandEffectiveness, m_interval, false);
            }
            else
                mainHandAD = m_owner.attackComponent.MakeAttack(this, m_target, mainWeapon, style, mainHandEffectiveness, m_interval, false);

            m_owner.attackComponent.attackAction.LastAttackData = mainHandAD;

            if (mainHandAD.Target == null ||
                mainHandAD.AttackResult == eAttackResult.OutOfRange ||
                mainHandAD.AttackResult == eAttackResult.TargetNotVisible ||
                mainHandAD.AttackResult == eAttackResult.NotAllowed_ServerRules ||
                mainHandAD.AttackResult == eAttackResult.TargetDead)
            {
                return;
            }

            // Notify the target of our attack (sends damage messages, should be before damage)
            mainHandAD.Target.OnAttackedByEnemy(mainHandAD);

            // Check if Reflex Attack RA should apply. This is checked once here and cached since it is used multiple times below (every swing triggers Reflex Attack).
            bool targetHasReflexAttackRA = false;
            IGamePlayer targetPlayer = mainHandAD.Target as IGamePlayer;

            if (targetPlayer != null && targetPlayer.EffectListComponent.ContainsEffectForEffectType(eEffect.ReflexAttack))
                targetHasReflexAttackRA = true;

            // Reflex Attack - Mainhand.
            if (targetHasReflexAttackRA)
                HandleReflexAttack(m_owner, mainHandAD.Target, mainHandAD.AttackResult, m_interval);

            // Deal damage and start effect.
            if (mainHandAD.AttackResult is eAttackResult.HitUnstyled or eAttackResult.HitStyle)
            {
                m_owner.DealDamage(mainHandAD);

                if (mainHandAD.IsMeleeAttack)
                {
                    m_owner.CheckWeaponMagicalEffect(mainHandAD, mainWeapon);
                    HandleDamageAdd(m_owner, mainHandAD);

                    //[Atlas - Takii] Reflex Attack NF Implementation commented out.
                    //if (mainHandAD.Target is GameLiving)
                    //{
                    //    GameLiving living = mainHandAD.Target as GameLiving;

                    //    RealmAbilities.L3RAPropertyEnhancer ra = living.GetAbility<RealmAbilities.ReflexAttackAbility>();
                    //    if (ra != null && Util.Chance(ra.Amount))
                    //    {
                    //        AttackData ReflexAttackAD = living.attackComponent.LivingMakeAttack(owner, living.ActiveWeapon, null, 1, m_interruptDuration, false, true);
                    //        living.DealDamage(ReflexAttackAD);
                    //        living.SendAttackingCombatMessages(ReflexAttackAD);
                    //    }
                    //}
                }
            }

            //CMH
            // 1.89:
            // - Characters who are attacked by stealthed archers will now target the attacking archer if the attacked player does not already have a target.
            if (mainHandAD.Attacker.IsStealthed
                && mainHandAD.AttackType == AttackData.eAttackType.Ranged
                && (mainHandAD.AttackResult == eAttackResult.HitUnstyled || mainHandAD.AttackResult == eAttackResult.HitStyle))
            {
                if (mainHandAD.Target.TargetObject == null)
                    targetPlayer?.Out.SendChangeTarget(mainHandAD.Attacker);
            }

            HandleDamageShields(mainHandAD);

            // Now left hand damage.
            if (leftHandSwingCount > 0 && mainWeapon.SlotPosition != Slot.RANGED)
            {
                switch (mainHandAD.AttackResult)
                {
                    case eAttackResult.HitStyle:
                    case eAttackResult.HitUnstyled:
                    case eAttackResult.Missed:
                    case eAttackResult.Blocked:
                    case eAttackResult.Evaded:
                    case eAttackResult.Fumbled: // Takii - Fumble should not prevent Offhand attack.
                    case eAttackResult.Parried:
                        for (int i = 0; i < leftHandSwingCount; i++)
                        {
                            if (m_target is GameLiving living && (living.IsAlive == false || living.ObjectState != eObjectState.Active))
                                break;

                            // Savage swings - main, left, main, left.
                            if (i % 2 == 0)
                                leftHandAD = m_owner.attackComponent.MakeAttack(this, m_target, leftWeapon, null, leftHandEffectiveness, m_interval, usingOH);
                            else
                                leftHandAD = m_owner.attackComponent.MakeAttack(this, m_target, mainWeapon, null, leftHandEffectiveness, m_interval, usingOH);

                        // Notify the target of our attack (sends damage messages, should be before damage).
                        leftHandAD.Target?.OnAttackedByEnemy(leftHandAD);

                            // Deal damage and start the effect if any.
                            if (leftHandAD.AttackResult is eAttackResult.HitUnstyled or eAttackResult.HitStyle)
                            {
                                m_owner.DealDamage(leftHandAD);
                                if (leftHandAD.IsMeleeAttack)
                                {
                                    m_owner.CheckWeaponMagicalEffect(leftHandAD, leftWeapon);
                                    HandleDamageAdd(m_owner, leftHandAD);
                                }
                            }

                            HandleDamageShields(leftHandAD);

                            // Reflex Attack - Offhand.
                            if (targetHasReflexAttackRA)
                                HandleReflexAttack(m_owner, leftHandAD.Target, leftHandAD.AttackResult, m_interval);
                        }

                    break;
                }
            }

            if (mainHandAD.AttackType == AttackData.eAttackType.Ranged)
            {
                m_owner.CheckWeaponMagicalEffect(mainHandAD, mainWeapon);
                HandleDamageAdd(m_owner, mainHandAD);
            }

            switch (mainHandAD.AttackResult)
            {
                case eAttackResult.NoTarget:
                case eAttackResult.TargetDead:
                    {
                        m_owner.OnTargetDeadOrNoTarget();
                        return;
                    }
                case eAttackResult.NotAllowed_ServerRules:
                case eAttackResult.NoValidTarget:
                    {
                        m_owner.attackComponent.StopAttack();
                        return;
                    }
                case eAttackResult.OutOfRange:
                break;
            }

            // Unstealth before attack animation.
            if (m_owner is IGamePlayer playerOwner)
                playerOwner.Stealth(false);

            // Show the animation.
            if (mainHandAD.AttackResult != eAttackResult.HitUnstyled && mainHandAD.AttackResult != eAttackResult.HitStyle && leftHandAD != null)
                ShowAttackAnimation(leftHandAD, leftWeapon);
            else
                ShowAttackAnimation(mainHandAD, mainWeapon);

            // Start style effect after any damage.
            if (mainHandAD.StyleEffects.Count > 0 && mainHandAD.AttackResult == eAttackResult.HitStyle)
            {
                foreach (ISpellHandler proc in mainHandAD.StyleEffects)
                    proc.StartSpell(mainHandAD.Target);
            }

            if (leftHandAD != null && leftHandAD.StyleEffects.Count > 0 && leftHandAD.AttackResult == eAttackResult.HitStyle)
            {
                foreach (ISpellHandler proc in leftHandAD.StyleEffects)
                    proc.StartSpell(leftHandAD.Target);
            }

            // Mobs' heading isn't updated after they start attacking, so we update it after they swing.
            if (m_owner is GameNPC npcOwner)
            {
                npcOwner.TurnTo(mainHandAD.Target);
                npcOwner.UpdateNPCEquipmentAppearance();
            }

            return;
        }

        public int Execute(ECSGameTimer timer)
        {
            Execute();
            return 0;
        }

        private static void HandleDamageAdd(GameLiving owner, AttackData ad)
        {
            List<ECSGameSpellEffect> damageAddEffects = owner.effectListComponent.GetSpellEffects(eEffect.DamageAdd);

            if (damageAddEffects == null)
                return;

            /// [Atlas - Takii] This could probably be optimized a bit by doing the split below between "affected/unaffected by stacking"
            /// when the effect is applied in the EffectListComponent instead of every time we swing our weapon?
            List<ECSGameSpellEffect> damageAddsUnaffectedByStacking = [];

            // 1 - Apply the DmgAdds that are unaffected by stacking (usually RA-based DmgAdds, EffectGroup 99999) first regardless of their damage.
            foreach (ECSGameSpellEffect damageAdd in damageAddEffects)
            {
                if (damageAdd.SpellHandler.Spell.EffectGroup == 99999)
                {
                    damageAddsUnaffectedByStacking.Add(damageAdd);
                    (damageAdd.SpellHandler as DamageAddSpellHandler).Handle(ad, 1);
                }
            }

            // 2 - Apply regular damage adds. We only start reducing to 50% effectiveness if there is more than one regular damage add being applied.
            // "Unaffected by stacking" dmg adds also dont reduce subsequent damage adds; they are effectively outside of the stacking mechanism.
            int numRegularDmgAddsApplied = 0;

            foreach (ECSGameSpellEffect damageAdd in damageAddEffects.Except(damageAddsUnaffectedByStacking).OrderByDescending(e => e.SpellHandler.Spell.Damage))
            {
                if (damageAdd.IsBuffActive)
                {
                    (damageAdd.SpellHandler as DamageAddSpellHandler).Handle(ad, numRegularDmgAddsApplied > 0 ? 0.5 : 1.0);
                    numRegularDmgAddsApplied++;
                }
            }
        }

        private static void HandleDamageShields(AttackData ad)
        {
            List<ECSGameSpellEffect> damagShieldEffects = ad.Target.effectListComponent.GetSpellEffects(eEffect.FocusShield);

            if (damagShieldEffects == null)
                return;

            foreach (ECSGameSpellEffect damageShield in damagShieldEffects)
                (damageShield.SpellHandler as DamageShieldSpellHandler).Handle(ad, 1);
        }

        private static void HandleReflexAttack(GameLiving attacker, GameLiving target, eAttackResult attackResult, int interval)
        {
            // Create an attack where the target hits the attacker back.
            // Triggers if we actually took a swing at the target, regardless of whether or not we hit.
            switch (attackResult)
            {
                case eAttackResult.HitStyle:
                case eAttackResult.HitUnstyled:
                case eAttackResult.Missed:
                case eAttackResult.Blocked:
                case eAttackResult.Evaded:
                case eAttackResult.Parried:
                    AttackData ReflexAttackAD = target.attackComponent.LivingMakeAttack(null, attacker, target.ActiveWeapon, null, 1, interval, false, true);
                    target.DealDamage(ReflexAttackAD);

                // If we get hit by Reflex Attack (it can miss), send a "you were hit" message to the attacker manually
                // since it will not be done automatically as this attack is not processed by regular attacking code.
                if (ReflexAttackAD.AttackResult == eAttackResult.HitUnstyled)
                {
                    GamePlayer playerAttacker = attacker as GamePlayer;
                    playerAttacker?.Out.SendMessage(target.Name + " counter-attacks you for " + ReflexAttackAD.Damage + " damage.", eChatType.CT_Damaged, eChatLoc.CL_SystemWindow);
                }

                break;

                case eAttackResult.NotAllowed_ServerRules:
                case eAttackResult.NoTarget:
                case eAttackResult.TargetDead:
                case eAttackResult.OutOfRange:
                case eAttackResult.NoValidTarget:
                case eAttackResult.TargetNotVisible:
                case eAttackResult.Fumbled:
                case eAttackResult.Bodyguarded:
                case eAttackResult.Phaseshift:
                case eAttackResult.Grappled:
                default:
                break;
            }
        }

        public virtual void ShowAttackAnimation(AttackData ad, DbInventoryItem weapon)
        {
            bool showAnimation = false;

            switch (ad.AttackResult)
            {
                case eAttackResult.HitUnstyled:
                case eAttackResult.HitStyle:
                case eAttackResult.Evaded:
                case eAttackResult.Parried:
                case eAttackResult.Missed:
                case eAttackResult.Blocked:
                case eAttackResult.Fumbled:
                showAnimation = true;
                break;
            }

            if (!showAnimation)
                return;

            GameLiving defender = ad.Target;

            if (showAnimation)
            {
                // http://dolserver.sourceforge.net/forum/showthread.php?s=&threadid=836
                byte resultByte = 0;
                int attackersWeapon = (weapon == null) ? 0 : weapon.Model;
                int defendersWeapon = 0;

                switch (ad.AttackResult)
                {
                    case eAttackResult.Missed:
                    resultByte = 0;
                    break;

                    case eAttackResult.Evaded:
                    resultByte = 3;
                    break;

                    case eAttackResult.Fumbled:
                    resultByte = 4;
                    break;

                    case eAttackResult.HitUnstyled:
                    resultByte = 10;
                    break;

                    case eAttackResult.HitStyle:
                    resultByte = 11;
                    break;

                    case eAttackResult.Parried:
                    resultByte = 1;

                    if (defender.ActiveWeapon != null)
                        defendersWeapon = defender.ActiveWeapon.Model;

                    break;

                    case eAttackResult.Blocked:
                    resultByte = 2;

                    if (defender.Inventory != null)
                    {
                        DbInventoryItem lefthand = defender.Inventory.GetItem(eInventorySlot.LeftHandWeapon);

                        if (lefthand != null && lefthand.Object_Type == (int)eObjectType.Shield)
                            defendersWeapon = lefthand.Model;
                    }

                    break;
                }

                IEnumerable visiblePlayers = defender.GetPlayersInRadius(WorldMgr.VISIBILITY_DISTANCE);

                if (visiblePlayers == null)
                    return;

                foreach (GamePlayer player in visiblePlayers)
                {
                    if (player == null)
                        return;

                    int animationId;

                    switch (ad.AnimationId)
                    {
                        case -1:
                        animationId = player.Out.OneDualWeaponHit;
                        break;

                        case -2:
                        animationId = player.Out.BothDualWeaponHit;
                        break;

                        default:
                        animationId = ad.AnimationId;
                        break;
                    }

                    // It only affects the attacker's client, but for some reason, the attack animation doesn't play when the defender is different than the actually selected target.
                    // The lack of feedback makes fighting Spiritmasters very awkward because of the intercept mechanic. So until this get figured out, we'll instead play the hit animation on the attacker's selected target.
                    // Ranged attacks can be delayed (which makes the selected target unreliable) and don't seem to be affect by this anyway, so they must be ignored.
                    GameObject animationTarget = player != m_owner || ActiveWeaponSlot == eActiveWeaponSlot.Distance || m_target == defender ? defender : m_target;
                    player.Out.SendCombatAnimation(m_owner, animationTarget,
                                                   (ushort) attackersWeapon, (ushort) defendersWeapon,
                                                   animationId, 0, resultByte, defender.HealthPercent);
                }
            }
        }
    }
}