using System;
using DOL.AI.Brain;
using DOL.Events;
using DOL.GS.Effects;
using DOL.GS.Scripts;

namespace DOL.GS.Spells
{
	/// <summary>
	/// Spell handler to summon a bonedancer pet.
	/// </summary>
	/// <author>IST</author>
	[SpellHandler("SummonJuggernaut")]
	public class SummonJuggernaut : SummonSimulacrum
	{
		public SummonJuggernaut(GameLiving caster, Spell spell, SpellLine line) : base(caster, spell, line) { }

		protected override IControlledBrain GetPetBrain(GameLiving owner)
		{
			return new JuggernautBrain(owner);
		}
		
		protected override void OnNpcReleaseCommand(DOLEvent e, object sender, EventArgs arguments)
		{
			if (e != GameLivingEvent.PetReleased || sender is not GameNPC gameNpc)
				return;

			if (gameNpc.Brain is not JuggernautBrain juggernautBrain)
				return;

			var player = juggernautBrain.Owner as IGamePlayer;

			if (player == null)
				return;

			AtlasOF_JuggernautECSEffect effect = (AtlasOF_JuggernautECSEffect)EffectListService.GetEffectOnTarget((GameLiving)player, eEffect.Juggernaut);
			effect?.Cancel(false);

			base.OnNpcReleaseCommand(e, sender, arguments);
		}
	}
}
