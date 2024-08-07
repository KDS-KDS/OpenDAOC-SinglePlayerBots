using System;
using DOL.AI.Brain;
using DOL.GS.PropertyCalc;

namespace DOL.GS.Scripts
{
	/// <summary>
	/// The critical hit chance calculator. Returns 0 .. 100 chance.
	///
	/// BuffBonusCategory1 unused
	/// BuffBonusCategory2 unused
	/// BuffBonusCategory3 unused
	/// BuffBonusCategory4 for uncapped realm ability bonus
	/// BuffBonusMultCategory1 unused
	///
	/// Crit propability is capped to 50% except for berserk
	/// </summary>
	[PropertyCalculator(eProperty.CriticalMeleeHitChance)]
	public class CriticalMeleeHitChanceCalculator : PropertyCalculator
	{
		public CriticalMeleeHitChanceCalculator() { }

		public override int CalcValue(GameLiving living, eProperty property)
		{
			// No berserk for ranged weapons.
			ECSGameEffect berserk = EffectListService.GetEffectOnTarget(living, eEffect.Berserk);

			if (berserk != null)
				return 100;

			// Base 10% chance of critical for all with melee weapons plus ra bonus.
			int chance = living.BuffBonusCategory4[(int)property] + living.AbilityBonus[(int)property];

			// Summoned or Charmed pet.
			if (living is GameNPC npc && npc.Brain is IControlledBrain petBrain && petBrain.GetLivingOwner() is IGamePlayer player)
			{
				if (npc is NecromancerPet)
					chance += 10;

				chance += player.GetAbility<RealmAbilities.AtlasOF_WildMinionAbility>()?.Amount ?? 0;
			}
			else
				chance += 10;

			// 50% hardcap.
			return Math.Min(chance, 50);
		}
	}
}
