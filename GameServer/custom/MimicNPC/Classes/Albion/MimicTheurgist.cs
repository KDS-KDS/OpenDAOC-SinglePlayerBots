﻿using System;
using System.Reflection;
using DOL.GS;
using DOL.GS.Scripts;
using DOL.Database;
using log4net;
using DOL.GS.Realm;
using System.Collections.Generic;
using DOL.GS.PlayerClass;

namespace DOL.GS.Scripts
{
	public class MimicTheurgist : MimicNPC
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public MimicTheurgist(GameLiving owner, byte level = 0, Point3D position = null) : base(owner, new ClassTheurgist(), level, position)
		{
			MimicSpec = new TheurgistSpec();

			DistributeSkillPoints();
            SetMeleeWeapon(MimicSpec.WeaponTypeOne);
            SetArmor(eObjectType.Cloth);
			SetJewelry();

			//foreach (InventoryItem item in Inventory.EquippedItems)
			//{
			//	if (item == null)
			//		return;

			//	if (item.Quality < 90)
			//	{
			//		item.Quality = Util.Random(90, 100);
			//	}

			//	log.Debug("Name: " + item.Name);
			//	log.Debug("Slot: " + Enum.GetName(typeof(eInventorySlot), item.SlotPosition));
			//	log.Debug("DPS_AF: " + item.DPS_AF);
			//	log.Debug("SPD_ABS: " + item.SPD_ABS);
			//}

			SwitchWeapon(eActiveWeaponSlot.TwoHanded);

			RefreshSpecDependantSkills(false);
			SetCasterSpells();
		}
	}

    public class TheurgistSpec : MimicSpec
    {
        public TheurgistSpec()
        {
            SpecName = "TheurgistSpec";

            WeaponTypeOne = "Staff";

            int randVariance = Util.Random(2);

            switch (randVariance)
            {
                case 0:
                Add("Earth Magic", 28, 0.1f);
                Add("Cold Magic", 20, 0.0f);
				Add("Wind Magic", 45, 1.0f);
                break;

                case 1:
                Add("Earth Magic", 4, 0.0f);
                Add("Cold Magic", 50, 1.0f);
                Add("Wind Magic", 20, 0.1f);
                break;

                case 2:
                Add("Earth Magic", 50, 1.0f);
                Add("Cold Magic", 4, 0.0f);
                Add("Wind Magic", 20, 0.1f);
                break;
            }
        }
    }
}