﻿using DOL.GS.PacketHandler;
using DOL.Language;

namespace DOL.GS
{
    public class BerserkECSGameEffect : ECSGameAbilityEffect
    {
        public BerserkECSGameEffect(ECSGameEffectInitParams initParams)
            : base(initParams)
        {
            EffectType = eEffect.Berserk;
            EffectService.RequestStartEffect(this);
        }

        protected ushort m_startModel = 0;

        public override ushort Icon
        { get { return 479; } }

        public override string Name
        {
            get
            {
                if (OwnerPlayer != null)
                    return LanguageMgr.GetTranslation(OwnerPlayer.Client, "Effects.BerserkEffect.Name");
                else
                    return "Berserk";
            }
        }

        public override bool HasPositiveEffect
        { get { return true; } }

        public override void OnStartEffect()
        {
            m_startModel = Owner.Model;

            if (OwnerPlayer != null)
            {
                // "You go into a berserker frenzy!"
                OwnerPlayer.Out.SendMessage(LanguageMgr.GetTranslation(OwnerPlayer.Client, "Effects.BerserkEffect.StartFrenzy"), eChatType.CT_System, eChatLoc.CL_SystemWindow);
                // "{0} goes into a berserker frenzy!"
                Message.SystemToArea(OwnerPlayer, LanguageMgr.GetTranslation(OwnerPlayer.Client, "Effects.BerserkEffect.AreaStartFrenzy", OwnerPlayer.GetName(0, true)), eChatType.CT_System, OwnerPlayer);
            }

            if (Owner.Race == (int)eRace.Dwarf)
                Owner.Model = 2032;
            else
                Owner.Model = 582;

            Owner.Emote(eEmote.MidgardFrenzy);
        }

        public override void OnStopEffect()
        {
            Owner.Model = m_startModel;

            // there is no animation on end of the effect
            if (OwnerPlayer != null)
            {
                // "Your berserker frenzy ends."
                OwnerPlayer.Out.SendMessage(LanguageMgr.GetTranslation(OwnerPlayer.Client, "Effects.BerserkEffect.EndFrenzy"), eChatType.CT_System, eChatLoc.CL_SystemWindow);
                // "{0}'s berserker frenzy ends."
                Message.SystemToArea(OwnerPlayer, LanguageMgr.GetTranslation(OwnerPlayer.Client, "Effects.BerserkEffect.AreaEndFrenzy", OwnerPlayer.GetName(0, true)), eChatType.CT_System, OwnerPlayer);
            }
        }
    }
}