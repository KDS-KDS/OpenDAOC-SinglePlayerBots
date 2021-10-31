﻿//edited by loki for current SVN 2018


using DOL.Database;
using DOL.GS.PacketHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOL.GS {
    public class ItemModel : GameNPC {
        public string TempProperty = "ItemModel";
        private int Chance;
        private Random rnd = new Random();

        //placeholder prices
        //500 lowbie stuff
        //2k low toa
        //4k high toa
        //10k dragonsworn
        //20k champion
        private int lowbie = 450;
        private int festive = 1000;
        private int toageneric = 1800;
        private int armorpads = 2500;
        private int artifact = 4800;
        private int epic = 4000;
        private int dragon = 9000;
        private int champion = 18000;
        private int cloakcheap = 9000;
        private int cloakmedium = 18000;
        private int cloakexpensive = 61749;

        public override bool AddToWorld()
        {
            base.AddToWorld();
            return true;
        }

        public override bool Interact(GamePlayer player)
        {
            if (base.Interact(player))
            {
                TurnTo(player, 500);
                InventoryItem item = player.TempProperties.getProperty<InventoryItem>(TempProperty);
                if (item == null)
                {
                    SendReply(player, "Hello there! \n" +
                    "I can offer a variety of aesthetics... for those willing to pay for it.\n" +
                    "Hand me the item and then we can talk prices.");
                }
                else
                {
                    ReceiveItem(player, item);
                }
                return true;
            }
            return false;
        }

        public override bool WhisperReceive(GameLiving source, string str)
        {
            /*
            if (!base.WhisperReceive(source, str)) return false;
            if (!(source is GamePlayer)) return false;
            GamePlayer player = (GamePlayer)source;
            TurnTo(player.X, player.Y);

            InventoryItem item = player.TempProperties.getProperty<InventoryItem>(TempProperty);

            if (item == null)
            {
                SendReply(player, "I need an item to work on!");
                return false;
            }
            int model = item.Model;
            int tmpmodel = int.Parse(str);
            if (tmpmodel != 0) model = tmpmodel;
            SetModel(player, model);
            SendReply(player, "I have changed your item's model, you can now use it.");
            */

            if (!base.WhisperReceive(source, str)) return false;
            if (!(source is GamePlayer)) return false;

            int modelIDToAssign = 0;
            int price = 0;

            GamePlayer player = source as GamePlayer;
            TurnTo(player.X, player.Y);
            InventoryItem item = player.TempProperties.getProperty<InventoryItem>(TempProperty);

            if (item == null)
            {
                SendReply(player, "I need an item to work on!");
                return false;
            }

            switch (str.ToLower())
            {
                #region helms
                case "dragonslayer helm":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = dragon * 2;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4056;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4070;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4063;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4054;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4068;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4061;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4055;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4069;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4062;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4057;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4072;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 4066;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 4053;
                            break;
                    }
                    break;
                case "dragonsworn helm":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = dragon;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3864;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3862;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3863;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3866;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 3865;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 3867;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3861;
                            break;
                    }
                    break;
                case "crown of zahur":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 1839;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 1840;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 1841;
                            break;
                        default:
                            modelIDToAssign = 0;
                            break;
                    }
                    break;
                case "crown of zahur variant":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 1842;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 1843;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 1844;
                            break;
                        default:
                            modelIDToAssign = 0;
                            break;
                    }
                    break;
                case "winged helm":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 2223;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 2225;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 2224;
                            break;
                        default:
                            modelIDToAssign = 0;
                            break;
                    }
                    break;
                case "oceanus helm":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2253;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2289;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2271;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2256;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2292;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2274;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2262;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2280;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Reinforced:
                            modelIDToAssign = 2298;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2265;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2277;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 2301;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2268;
                            break;
                    }
                    break;
                case "stygia helm":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2307;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2343;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2325;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2310;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2346;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2328;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2316;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2334;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Reinforced:
                            modelIDToAssign = 2352;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2313;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2331;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 2355;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2322;
                            break;
                    }
                    break;
                case "volcanus helm":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2361;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2397;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2379;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2364;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2400;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2382;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2370;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2388;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Reinforced:
                            modelIDToAssign = 2406;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2373;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2385;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 2409;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2376;
                            break;
                    }
                    break;
                case "aerus helm":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2415;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2451;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2433;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2418;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2454;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2436;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2424;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2442;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Reinforced:
                            modelIDToAssign = 2460;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2421;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2439;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 2463;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2430;
                            break;
                    }
                    break;
                case "wizard hat":
                    if (item.Item_Type != Slot.HELM || item.Object_Type != (int)eObjectType.Cloth)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = epic;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 1278;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 1279;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 1280;
                            break;
                        default:
                            modelIDToAssign = 0;
                            break;
                    }
                    break;
                case "robin hood hat":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 1281;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 1282;
                            break;
                        default:
                            modelIDToAssign = 0;
                            break;
                    }
                    break;
                case "fur cap":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    modelIDToAssign = 1283;
                    break;
                case "tarboosh":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    modelIDToAssign = 1284;
                    break;
                case "leaf hat":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    modelIDToAssign = 1285;
                    break;
                case "wing hat":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    modelIDToAssign = 1286;
                    break;
                case "jester hat":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    modelIDToAssign = 1287;
                    break;
                case "stag helm":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    modelIDToAssign = 1288;
                    break;
                case "wolf helm":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    modelIDToAssign = 1289;
                    break;
                case "candle hat":
                    if (item.Item_Type != Slot.HELM)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    modelIDToAssign = 4169;
                    break;
                #endregion

                #region torsos
                case "dragonslayer breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = dragon * 2;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4015;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4046;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4099;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3990;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4021;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4074;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4010;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4041;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4094;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3995;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4026;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 4089;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 4000;
                            break;
                    }
                    break;
                case "dragonsworn breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = dragon;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3783;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3758;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3778;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3778;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3773;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3768;
                            break;
                    }
                    break;
                case "good shar breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3018;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2988;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3012;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 2994;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3000;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3006;
                            break;
                    }
                    break;
                case "possessed shar breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3085;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3090;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3095;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3110;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3100;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3105;
                            break;
                    }
                    break;
                case "good inconnu breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3059;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3064;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3116;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3043;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3048;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3053;
                            break;
                    }
                    break;
                case "possessed inconnu breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3069;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3075;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3111;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3028;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3033;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3038;
                            break;
                    }
                    break;
                case "good realm breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2790;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2797;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 2803;
                            break;

                        case eObjectType.Scale:
                        case eObjectType.Chain:
                            modelIDToAssign = 2809;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 2815;
                            break;
                    }
                    break;
                case "possessed realm breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2728;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2735;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 2741;
                            break;

                        case eObjectType.Scale:
                        case eObjectType.Chain:
                            modelIDToAssign = 2747;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 2753;
                            break;
                    }
                    break;
                case "mino breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3631;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3606;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3611;
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 3621;
                            break;
                        case eObjectType.Chain:
                            modelIDToAssign = 3611;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 3616;
                            break;
                    }
                    break;
                case "eirene's chest":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 2226;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 2228;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 2227;
                            break;
                        default:
                            modelIDToAssign = 0;
                            break;
                    }
                    break;
                case "naliah's robe":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 2516;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 2517;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 2518;
                            break;
                        default:
                            modelIDToAssign = 0;
                            break;
                    }
                    break;
                case "guard of valor":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 2475;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 2476;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 2477;
                            break;
                        default:
                            modelIDToAssign = 0;
                            break;
                    }
                    break;
                case "golden scarab vest":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 2187;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 2189;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 2188;
                            break;
                        default:
                            modelIDToAssign = 0;
                            break;
                    }
                    break;
                case "oceanus breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 1619;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 1621;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 1623;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 1640;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 1641;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 1642;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 1848;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 1849;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Reinforced:
                            modelIDToAssign = 1850;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2101;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2102;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1773;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2092;
                            break;
                    }
                    break;
                case "stygia breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2515;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2515;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2515;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2135; //lol leopard print
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2136;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2137;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 1757;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 1758;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Reinforced:
                            modelIDToAssign = 1759;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 1809;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 1810;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1791;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2124;
                            break;
                    }
                    break;
                case "volcanus breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2513;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2513;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2513;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2176;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2178;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2177;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 1780;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 1781;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Reinforced:
                            modelIDToAssign = 1782;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 1694;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 1695;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1714;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 1703;
                            break;
                    }
                    break;
                case "aerus breastplate":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2245;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2246;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2247;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 2144;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 2146;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 2145;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 1798;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 1799;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Reinforced:
                            modelIDToAssign = 1800;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 1736;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 1737;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1738;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 1685;
                            break;
                    }
                    break;
                case "class epic chestpiece":
                    if (item.Item_Type != Slot.TORSO)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = epic;
                    switch ((eCharacterClass)player.CharacterClass.ID)
                    {
                        //alb
                        case eCharacterClass.Armsman:
                            modelIDToAssign = 688;
                            break;
                        case eCharacterClass.Cabalist:
                            modelIDToAssign = 682;
                            break;
                        case eCharacterClass.Cleric:
                            modelIDToAssign = 713;
                            break;
                        case eCharacterClass.Friar:
                            modelIDToAssign = 797;
                            break;
                        case eCharacterClass.Infiltrator:
                            modelIDToAssign = 792;
                            break;
                        case eCharacterClass.Mercenary:
                            modelIDToAssign = 718;
                            break;
                        case eCharacterClass.Minstrel:
                            modelIDToAssign = 3380;
                            break;
                        case eCharacterClass.Necromancer:
                            modelIDToAssign = 1266;
                            break;
                        case eCharacterClass.Paladin:
                            modelIDToAssign = 693;
                            break;
                        case eCharacterClass.Reaver:
                            modelIDToAssign = 1267;
                            break;
                        case eCharacterClass.Scout:
                            modelIDToAssign = 728;
                            break;
                        case eCharacterClass.Sorcerer:
                            modelIDToAssign = 804;
                            break;
                        case eCharacterClass.Theurgist:
                            modelIDToAssign = 733;
                            break;
                        case eCharacterClass.Wizard:
                            modelIDToAssign = 798;
                            break;

                        //mid
                        case eCharacterClass.Berserker:
                            modelIDToAssign = 751;
                            break;
                        case eCharacterClass.Bonedancer:
                            modelIDToAssign = 1187;
                            break;
                        case eCharacterClass.Healer:
                            modelIDToAssign = 698;
                            break;
                        case eCharacterClass.Hunter:
                            modelIDToAssign = 756;
                            break;
                        case eCharacterClass.Runemaster:
                            modelIDToAssign = 703;
                            break;
                        case eCharacterClass.Savage:
                            modelIDToAssign = 1192;
                            break;
                        case eCharacterClass.Shadowblade:
                            modelIDToAssign = 761;
                            break;
                        case eCharacterClass.Shaman:
                            modelIDToAssign = 766;
                            break;
                        case eCharacterClass.Skald:
                            modelIDToAssign = 771;
                            break;
                        case eCharacterClass.Spiritmaster:
                            modelIDToAssign = 799;
                            break;
                        case eCharacterClass.Thane:
                            modelIDToAssign = 3370;
                            break;
                        case eCharacterClass.Warrior:
                            modelIDToAssign = 776;
                            break;

                        //hib
                        case eCharacterClass.Animist:
                            modelIDToAssign = 1186;
                            break;
                        case eCharacterClass.Bard:
                            modelIDToAssign = 734;
                            break;
                        case eCharacterClass.Blademaster:
                            modelIDToAssign = 782;
                            break;
                        case eCharacterClass.Champion:
                            modelIDToAssign = 810;
                            break;
                        case eCharacterClass.Druid:
                            modelIDToAssign = 739;
                            break;
                        case eCharacterClass.Eldritch:
                            modelIDToAssign = 744;
                            break;
                        case eCharacterClass.Enchanter:
                            modelIDToAssign = 781;
                            break;
                        case eCharacterClass.Hero:
                            modelIDToAssign = 708;
                            break;
                        case eCharacterClass.Mentalist:
                            modelIDToAssign = 745;
                            break;
                        case eCharacterClass.Nightshade:
                            modelIDToAssign = 746;
                            break;
                        case eCharacterClass.Ranger:
                            modelIDToAssign = 815;
                            break;
                        case eCharacterClass.Valewalker:
                            modelIDToAssign = 1003;
                            break;
                        case eCharacterClass.Warden:
                            modelIDToAssign = 805;
                            break;
                    }
                    break;
                #endregion

                #region sleeves
                case "dragonslayer sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = dragon * 2;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4017;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4048;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4101;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3992;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4023;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4076;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4012;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4043;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4096;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3997;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4028;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 4091;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 4002;
                            break;
                    }
                    break;
                case "good shar sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3020;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2990;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3014;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 2996;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3002;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3008;
                            break;
                    }
                    break;
                case "possessed shar sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3083;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3088;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3093;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3108;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3098;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3103;
                            break;
                    }
                    break;
                case "good inconnu sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3061;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3066;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3118;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3045;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3050;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3055;
                            break;
                    }
                    break;
                case "possessed inconnu sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3123;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3077;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3113;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3030;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3035;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3040;
                            break;
                    }
                    break;
                case "good realm sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2793;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2799;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 2805;
                            break;

                        case eObjectType.Scale:
                        case eObjectType.Chain:
                            modelIDToAssign = 2811;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 2817;
                            break;
                    }
                    break;
                case "possessed realm sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2731;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2737;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 2743;
                            break;

                        case eObjectType.Scale:
                        case eObjectType.Chain:
                            modelIDToAssign = 2749;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 2755;
                            break;
                    }
                    break;
                case "mino sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3644;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3608;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3628;
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 3623;
                            break;
                        case eObjectType.Chain:
                            modelIDToAssign = 3613;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 3618;
                            break;
                    }
                    break;
                case "foppish sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1732;
                    break;
                case "arms of the wind":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1733;
                    break;
                case "oceanus sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 1625;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 1639;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1847;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 2100;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1770;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2091;
                            break;
                    }
                    break;
                case "stygia sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2152;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2134;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1756;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1808;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1788;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2123;
                            break;
                    }
                    break;
                case "volcanus sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2161;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2175;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1779;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1693;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1711;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 1702;
                            break;
                    }
                    break;
                case "aerus sleeves":
                    if (item.Item_Type != Slot.ARMS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2237;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2143;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1797;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1735;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1747;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 1684;
                            break;
                    }
                    break;
                #endregion

                #region pants
                case "dragonslayer pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = dragon * 2;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4016;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4047;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4100;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3991;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4022;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4075;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4011;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4042;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4095;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3996;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4027;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 4090;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 4001;
                            break;
                    }
                    break;
                case "good shar pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3019;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2989;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3013;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 2995;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3001;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3007;
                            break;
                    }
                    break;
                case "possessed shar pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3082;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3087;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3092;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3107;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3097;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3102;
                            break;
                    }
                    break;
                case "good inconnu pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3060;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3065;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3117;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3044;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3049;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3054;
                            break;
                    }
                    break;
                case "possessed inconnu pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3071;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3076;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3112;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3029;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3034;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3039;
                            break;
                    }
                    break;
                case "good realm pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2792;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2798;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 2804;
                            break;

                        case eObjectType.Scale:
                        case eObjectType.Chain:
                            modelIDToAssign = 2810;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 2816;
                            break;
                    }
                    break;
                case "possessed realm pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2730;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2736;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 2742;
                            break;

                        case eObjectType.Scale:
                        case eObjectType.Chain:
                            modelIDToAssign = 2748;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 2754;
                            break;
                    }
                    break;
                case "mino pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3643;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3607;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3627;
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 3622;
                            break;
                        case eObjectType.Chain:
                            modelIDToAssign = 3612;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 3617;
                            break;
                    }
                    break;
                case "wing's dive":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1767;
                    break;
                case "alvarus' leggings":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1744;
                    break;
                case "oceanus pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 1631;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 1646;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1854;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 2107;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1778;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2098;
                            break;
                    }
                    break;
                case "stygia pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2158;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2141;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1763;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1815;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1796;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2130;
                            break;
                    }
                    break;
                case "volcanus pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2167;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2182;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1786;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1700;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1718;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 1709;
                            break;
                    }
                    break;
                case "aerus pants":
                    if (item.Item_Type != Slot.LEGS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2243;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2150;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1804;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1742;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1754;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 1691;
                            break;
                    }
                    break;
                #endregion

                #region boots
                case "dragonslayer boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = dragon * 2;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4019;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4050;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4103;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3994;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4025;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4078;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4013;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4044;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4097;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3999;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4030;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 4093;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 4004;
                            break;
                    }
                    break;
                case "good shar boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3021;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2992;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3015;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 2998;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3004;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3010;
                            break;
                    }
                    break;
                case "possessed shar boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3084;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3089;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3094;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3109;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3099;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3104;
                            break;
                    }
                    break;
                case "good inconnu boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3062;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3067;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3119;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3046;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3051;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3056;
                            break;
                    }
                    break;
                case "possessed inconnu boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3073;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3078;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3114;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3031;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3036;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3041;
                            break;
                    }
                    break;
                case "good realm boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2795;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2801;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 2807;
                            break;

                        case eObjectType.Scale:
                        case eObjectType.Chain:
                            modelIDToAssign = 2813;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 2819;
                            break;
                    }
                    break;
                case "possessed realm boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2733;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2739;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 2745;
                            break;

                        case eObjectType.Scale:
                        case eObjectType.Chain:
                            modelIDToAssign = 2751;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 2757;
                            break;
                    }
                    break;
                case "mino boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3646;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3610;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3629;
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 3625;
                            break;
                        case eObjectType.Chain:
                            modelIDToAssign = 3615;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 3620;
                            break;
                    }
                    break;
                case "enyalio's boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 2488;
                    break;
                case "flamedancer's boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1731;
                    break;
                case "oceanus boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 1629;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 1643;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1851;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 2104;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1775;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2095;
                            break;
                    }
                    break;
                case "stygia boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2157;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2139;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1761;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1813;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1793;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2127;
                            break;
                    }
                    break;
                case "volcanus boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2166;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2180;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1784;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1698;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1716;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 1706;
                            break;
                    }
                    break;
                case "aerus boots":
                    if (item.Item_Type != Slot.FEET)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2242;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2148;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1802;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1740;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1752;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 1688;
                            break;
                    }
                    break;
                #endregion

                #region gloves
                case "dragonslayer gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = dragon * 2;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4018;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4049;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4102;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Leather:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3993;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4024;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4077;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 4014;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4045;
                                    break;
                                case eRealm.Hibernia:
                                    modelIDToAssign = 4098;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                    modelIDToAssign = 3998;
                                    break;
                                case eRealm.Midgard:
                                    modelIDToAssign = 4029;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 4092;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 4003;
                            break;
                    }
                    break;
                case "good shar gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3022;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2993;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3016;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 2999;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3005;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3011;
                            break;
                    }
                    break;
                case "possessed shar gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3085;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3090;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3095;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3110;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3100;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3105;
                            break;
                    }
                    break;
                case "good inconnu gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3063;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3068;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3120;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3047;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3052;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3057;
                            break;
                    }
                    break;
                case "possessed inconnu gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3074;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3079;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3115;
                            break;

                        case eObjectType.Chain:
                            switch (source.Realm)
                            {
                                case eRealm.Albion:
                                case eRealm.Midgard:
                                    modelIDToAssign = 3032;
                                    break;
                                default:
                                    modelIDToAssign = 0;
                                    break;
                            }
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 3037;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 3042;
                            break;
                    }
                    break;
                case "good realm gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2796;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2802;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 2808;
                            break;

                        case eObjectType.Scale:
                        case eObjectType.Chain:
                            modelIDToAssign = 2814;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 2820;
                            break;
                    }
                    break;
                case "possessed realm gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2734;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2740;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 2746;
                            break;

                        case eObjectType.Scale:
                        case eObjectType.Chain:
                            modelIDToAssign = 2752;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 2758;
                            break;
                    }
                    break;
                case "mino gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = festive;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 3645;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 3609;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 3630;
                            break;

                        case eObjectType.Scale:
                            modelIDToAssign = 3624;
                            break;
                        case eObjectType.Chain:
                            modelIDToAssign = 3614;
                            break;

                        case eObjectType.Plate:
                            modelIDToAssign = 3619;
                            break;
                    }
                    break;
                case "maddening scalars":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1746;
                    break;
                case "sharkskin gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1734;
                    break;
                case "oceanus gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 1620;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 1645;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1853;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 2106;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1776;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2097;
                            break;
                    }
                    break;
                case "stygia gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2248;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2140;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1762;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1814;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1794;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 2129;
                            break;
                    }
                    break;
                case "volcanus gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2249;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2181;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1785;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1699;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1717;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 1708;
                            break;
                    }
                    break;
                case "aerus gloves":
                    if (item.Item_Type != Slot.HANDS)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    switch ((eObjectType)item.Object_Type)
                    {
                        case eObjectType.Cloth:
                            modelIDToAssign = 2250;
                            break;

                        case eObjectType.Leather:
                            modelIDToAssign = 2149;
                            break;

                        case eObjectType.Studded:
                        case eObjectType.Reinforced:
                            modelIDToAssign = 1803;
                            break;

                        case eObjectType.Chain:
                            modelIDToAssign = 1741;
                            break;
                        case eObjectType.Scale:
                            modelIDToAssign = 1753;
                            break;
                        case eObjectType.Plate:
                            modelIDToAssign = 1690;
                            break;
                    }
                    break;
                #endregion

                #region cloaks 

                case "realm cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakexpensive;
                    switch ((eRealm)player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 3800;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 3802;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 3801;
                            break;
                    }
                    break;

                case "dragonslayer cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakexpensive;
                    switch ((eRealm)player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 4105;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 4109;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 4107;
                            break;
                    }
                    break;

                case "dragonsworn cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 3790;
                    break;

                case "valentines cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 3752;
                    break;

                case "winter cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 4115;
                    break;

                case "clean leather cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 3637;
                    break;

                case "corrupt leather cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 3634;
                    break;

                case "cloudsong":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 1727;
                    break;

                case "shades of mist":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 1726;
                    break;

                case "magma cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 1725;
                    break;

                case "stygian cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 1724;
                    break;

                case "aerus cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 1720;
                    break;

                case "oceanus cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 1722;
                    break;

                case "harpy feather cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 1721;
                    break;

                case "healer's embrace":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakmedium;
                    modelIDToAssign = 1723;
                    break;

                case "collared cloak":
                    if (item.Item_Type != Slot.CLOAK)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = cloakcheap;
                    modelIDToAssign = 669;
                    break;

                #endregion

                #region weapons

                #region 1h wep
                case "traitor's dagger 1h":
                    if ((item.Item_Type != Slot.RIGHTHAND && 
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Thrust)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1668;
                    break;
                case "traitor's axe 1h":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Slash)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 3452;
                    break;
                case "croc tooth dagger 1h":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Thrust)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1669;
                    break;
                case "croc tooth axe 1h":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Slash)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 3451;
                    break;
                case "golden spear 1h":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Thrust)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1807;
                    break;
                case "malice axe 1h":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Slash)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 2109;
                    break;
                case "malice hammer 1h":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Crush)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 3447;
                    break;
                case "bruiser hammer 1h":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Crush)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1671;
                    break;
                case "battler hammer 1h":
                    if ((item.Item_Type != Slot.RIGHTHAND ||
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Crush)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 3453;
                    break;
                case "battler sword 1h":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Slash)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 2112;
                    break;
                case "scepter of the meritorious":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Crush)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1672;
                    break;
                case "hilt 1h":
                    if (item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = epic;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 673;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 670;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 674;
                            break;
                    }
                    break;
                case "rolling pin":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Crush)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = epic;
                    modelIDToAssign = 3458;
                    break;
                case "wakazashi":
                    if (item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND &&
                        item.Type_Damage != (int)eDamageType.Thrust &&
                        item.Type_Damage != (int)eDamageType.Slash)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = epic;
                    modelIDToAssign = 2209;
                    break;
                case "turkey leg":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Crush)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = champion;
                    modelIDToAssign = 3454;
                    break;
                case "cleaver":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Slash)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = epic;
                    modelIDToAssign = 654;
                    break;
                case "khopesh":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Slash)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = epic;
                    modelIDToAssign = 2195;
                    break;
                case "stein":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Crush)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = champion;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 3440;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 3443;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 3438;
                            break;
                    }
                    break;
                case "hot metal rod":
                    if ((item.Item_Type != Slot.RIGHTHAND &&
                        item.Item_Type != Slot.LEFTHAND) &&
                        item.Type_Damage != (int)eDamageType.Crush)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = champion;
                    modelIDToAssign = 2984;
                    break;

                //hand to hand
                case "snakecharmer's fist":
                    price = artifact;
                    modelIDToAssign = 2469;
                    break;

                case "scorched fist":
                    price = toageneric;
                    switch ((eDamageType)item.Type_Damage)
                    {
                        case eDamageType.Slash:
                            modelIDToAssign = 3726;
                            break;
                        case eDamageType.Crush:
                            modelIDToAssign = 3728;
                            break;
                        case eDamageType.Thrust:
                            modelIDToAssign = 3730;
                            break;
                    }
                    break;

                case "dragonsworn fist":
                    price = dragon;
                    switch ((eDamageType)item.Type_Damage)
                    {
                        case eDamageType.Slash:
                            modelIDToAssign = 3843;
                            break;
                        case eDamageType.Crush:
                            modelIDToAssign = 3845;
                            break;
                        case eDamageType.Thrust:
                            modelIDToAssign = 3847;
                            break;
                    }
                    break;

                //flex
                case "snakecharmer's whip":
                    price = artifact;
                    modelIDToAssign = 2119;
                    break;

                case "scorched whip":
                    price = toageneric;
                    switch ((eDamageType)item.Type_Damage)
                    {
                        case eDamageType.Slash:
                            modelIDToAssign = 3697;
                            break;
                        case eDamageType.Crush:
                            modelIDToAssign = 3696;
                            break;
                        case eDamageType.Thrust:
                            modelIDToAssign = 3698;
                            break;
                    }
                    break;

                case "dragonsworn whip":
                    price = dragon;
                    switch ((eDamageType)item.Type_Damage)
                    {
                        case eDamageType.Slash:
                            modelIDToAssign = 3814;
                            break;
                        case eDamageType.Crush:
                            modelIDToAssign = 3815;
                            break;
                        case eDamageType.Thrust:
                            modelIDToAssign = 3813;
                            break;
                    }
                    break;


                #endregion

                #region 2h wep

                case "pickaxe":
                    price = epic;
                    modelIDToAssign = 2983;
                    break;

                //axe
                case "malice axe 2h":
                    price = artifact;
                    modelIDToAssign = 2110;
                    break;
                case "scorched axe 2h":
                    price = toageneric;
                    modelIDToAssign = 3705;
                    break;
                case "magma axe 2h":
                    price = toageneric;
                    modelIDToAssign = 2217;
                    break;

                //spears
                case "golden spear 2h":
                    price = artifact;
                    modelIDToAssign = 1662;
                    break;
                case "dragon spear 2h":
                    price = dragon;
                    modelIDToAssign = 3819;
                    break;
                case "scorched spear 2h":
                    price = toageneric;
                    modelIDToAssign = 3714;
                    break;
                case "trident spear 2h":
                    price = toageneric;
                    modelIDToAssign = 2191;
                    break;

                //hammers
                case "bruiser hammer 2h":
                    price = artifact;
                    modelIDToAssign = 2113;
                    break;
                case "battler hammer 2h":
                    price = artifact;
                    modelIDToAssign = 3448;
                    break;
                case "malice hammer 2h":
                    price = artifact;
                    modelIDToAssign = 3449;
                    break;
                case "scorched hammer 2h":
                    price = toageneric;
                    modelIDToAssign = 3704;
                    break;
                case "magma hammer 2h":
                    price = toageneric;
                    modelIDToAssign = 2215;
                    break;

                //swords
                case "battler sword 2h":
                    price = artifact;
                    modelIDToAssign = 1670;
                    break;
                case "scorched sword 2h":
                    price = toageneric;
                    modelIDToAssign = 3701;
                    break;
                case "katana 2h":
                    price = epic;
                    modelIDToAssign = 2208;
                    break;
                case "khopesh 2h":
                    price = epic;
                    modelIDToAssign = 2196;
                    break;
                case "hilt 2h":
                    price = epic;
                    switch (player.Realm)
                    {
                        case eRealm.Albion:
                            modelIDToAssign = 672;
                            break;
                        case eRealm.Midgard:
                            modelIDToAssign = 671;
                            break;
                        case eRealm.Hibernia:
                            modelIDToAssign = 675;
                            break;
                    }
                    break;

                //thrust
                case "scorched thrust 2h":
                    price = toageneric;
                    modelIDToAssign = 3700;
                    break;
                case "dragon thrust 2h":
                    price = dragon;
                    modelIDToAssign = 3817;
                    break;

                //staffs
                case "traldor's oracle":
                    price = artifact;
                    modelIDToAssign = 1659;
                    break;
                case "trident of the gods":
                    price = artifact;
                    modelIDToAssign = 1660;
                    break;
                case "tartaros gift":
                    price = artifact;
                    modelIDToAssign = 1658;
                    break;
                case "dragonsworn staff":
                    price = dragon;
                    modelIDToAssign = 3827;
                    break;
                case "scorched staff":
                    price = toageneric;
                    modelIDToAssign = 3710;
                    break;

                //scythes
                case "dragonsworn scythe":
                    price = artifact;
                    modelIDToAssign = 3825;
                    break;
                case "magma scythe":
                    price = toageneric;
                    modelIDToAssign = 2213;
                    break;
                case "scorched scythe":
                    price = toageneric;
                    modelIDToAssign = 3708;
                    break;
                case "scythe of kings":
                    price = artifact;
                    modelIDToAssign = 3450;
                    break;
                case "snakechamer's scythe":
                    price = artifact;
                    modelIDToAssign = 2111;
                    break;

                //polearms
                case "dragonsworn pole":
                    price = dragon;
                    switch ((eDamageType)item.Type_Damage)
                    {
                        case eDamageType.Slash:
                            modelIDToAssign = 3832;
                            break;
                        case eDamageType.Crush:
                            modelIDToAssign = 3833;
                            break;
                        case eDamageType.Thrust:
                            modelIDToAssign = 3831;
                            break;
                    }
                    break;
                case "pole of kings":
                    price = artifact;
                    modelIDToAssign = 1661;
                    break;
                case "scorched pole":
                    price = toageneric;
                    switch ((eDamageType)item.Type_Damage)
                    {
                        case eDamageType.Slash:
                            modelIDToAssign = 3715;
                            break;
                        case eDamageType.Crush:
                            modelIDToAssign = 3716;
                            break;
                        case eDamageType.Thrust:
                            modelIDToAssign = 3714;
                            break;
                    }
                    break;
                case "golden pole":
                    price = artifact;
                    modelIDToAssign = 1662;
                    break;

                #endregion

                #region class weapons

                case "class epic 1h":
                    price = champion;
                    switch ((eCharacterClass)player.CharacterClass.ID)
                    {
                        //alb
                        case eCharacterClass.Armsman:
                            switch ((eDamageType)item.Type_Damage)
                            {
                                case eDamageType.Thrust:
                                    modelIDToAssign = 3296;
                                    break;
                                case eDamageType.Slash:
                                    modelIDToAssign = 3295;
                                    break;
                                case eDamageType.Crush:
                                    modelIDToAssign = 3294;
                                    break;
                            }
                            break;
                        case eCharacterClass.Cabalist:
                            modelIDToAssign = 3264;
                            break;
                        case eCharacterClass.Cleric:
                            modelIDToAssign = 3282;
                            break;
                        case eCharacterClass.Friar:
                            modelIDToAssign = 3272;
                            break;
                        case eCharacterClass.Infiltrator:
                            switch ((eDamageType)item.Type_Damage)
                            {
                                case eDamageType.Thrust:
                                    modelIDToAssign = 3270;
                                    break;
                                case eDamageType.Slash:
                                    modelIDToAssign = 3269;
                                    break;
                            }
                            break;
                        case eCharacterClass.Mercenary:
                            switch ((eDamageType)item.Type_Damage)
                            {
                                case eDamageType.Thrust:
                                    modelIDToAssign = 3285;
                                    break;
                                case eDamageType.Slash:
                                    modelIDToAssign = 3284;
                                    break;
                                case eDamageType.Crush:
                                    modelIDToAssign = 3283;
                                    break;
                            }
                            break;
                        case eCharacterClass.Minstrel:
                            switch ((eDamageType)item.Type_Damage)
                            {
                                case eDamageType.Thrust:
                                    modelIDToAssign = 3277;
                                    break;
                                case eDamageType.Slash:
                                    modelIDToAssign = 3276;
                                    break;
                            }
                            break;
                        case eCharacterClass.Necromancer:
                            modelIDToAssign = 3268;
                            break;
                        case eCharacterClass.Paladin:
                            switch ((eDamageType)item.Type_Damage)
                            {
                                case eDamageType.Thrust:
                                    modelIDToAssign = 3305;
                                    break;
                                case eDamageType.Slash:
                                    modelIDToAssign = 3304;
                                    break;
                                case eDamageType.Crush:
                                    modelIDToAssign = 3303;
                                    break;
                            }
                            break;
                        case eCharacterClass.Reaver:
                            if ((eObjectType)item.Object_Type == eObjectType.Flexible)
                            {
                                modelIDToAssign = 3292;
                            }
                            else
                            {
                                switch ((eDamageType)item.Type_Damage)
                                {
                                    case eDamageType.Thrust:
                                        modelIDToAssign = 3291;
                                        break;
                                    case eDamageType.Slash:
                                        modelIDToAssign = 3290;
                                        break;
                                    case eDamageType.Crush:
                                        modelIDToAssign = 3289;
                                        break;
                                }
                            }
                            break;
                        case eCharacterClass.Scout:
                            switch ((eDamageType)item.Type_Damage)
                            {
                                case eDamageType.Thrust:
                                    modelIDToAssign = 3274;
                                    break;
                                case eDamageType.Slash:
                                    modelIDToAssign = 3273;
                                    break;
                            }
                            break;
                        case eCharacterClass.Sorcerer:
                            modelIDToAssign = 3265;
                            break;
                        case eCharacterClass.Theurgist:
                            modelIDToAssign = 3266;
                            break;
                        case eCharacterClass.Wizard:
                            modelIDToAssign = 3267;
                            break;

                        //mid
                        case eCharacterClass.Berserker:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Hammer:
                                    modelIDToAssign = 3323;
                                    break;
                                case eObjectType.Axe:
                                    modelIDToAssign = 3321;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3325;
                                    break;
                                case eObjectType.LeftAxe:
                                    modelIDToAssign = 3321;
                                    break;
                            }
                            break;
                        case eCharacterClass.Bonedancer:
                            modelIDToAssign = 3311;
                            break;
                        case eCharacterClass.Healer:
                            modelIDToAssign = 3335;
                            break;
                        case eCharacterClass.Hunter:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Spear:
                                    if ((eDamageType)item.Type_Damage == eDamageType.Thrust)
                                    {
                                        modelIDToAssign = 3319;
                                    }
                                    else
                                    {
                                        modelIDToAssign = 3320;
                                    }
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3317;
                                    break;
                            }
                            break;
                        case eCharacterClass.Runemaster:
                            modelIDToAssign = 3309;
                            break;
                        case eCharacterClass.Savage:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Hammer:
                                    modelIDToAssign = 3329;
                                    break;
                                case eObjectType.Axe:
                                    modelIDToAssign = 3327;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3331;
                                    break;
                                case eObjectType.HandToHand:
                                    modelIDToAssign = 3333;
                                    break;
                            }
                            break;
                        case eCharacterClass.Shadowblade:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Axe:
                                    modelIDToAssign = 3315;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3313;
                                    break;
                            }
                            break;
                        case eCharacterClass.Shaman:
                            modelIDToAssign = 3337;
                            break;
                        case eCharacterClass.Skald:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Hammer:
                                    modelIDToAssign = 3341;
                                    break;
                                case eObjectType.Axe:
                                    modelIDToAssign = 3339;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3343;
                                    break;
                            }
                            break;
                        case eCharacterClass.Spiritmaster:
                            modelIDToAssign = 3310;
                            break;
                        case eCharacterClass.Thane:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Hammer:
                                    modelIDToAssign = 3347;
                                    break;
                                case eObjectType.Axe:
                                    modelIDToAssign = 3345;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3349;
                                    break;
                            }
                            break;
                        case eCharacterClass.Warrior:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Hammer:
                                    modelIDToAssign = 3353;
                                    break;
                                case eObjectType.Axe:
                                    modelIDToAssign = 3351;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3355;
                                    break;
                            }
                            break;

                        //hib
                        case eCharacterClass.Animist:
                            modelIDToAssign = 3229;
                            break;
                        case eCharacterClass.Bard:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blades:
                                    modelIDToAssign = 3235;
                                    break;
                                case eObjectType.Blunt:
                                    modelIDToAssign = 3236;
                                    break;
                            }
                            break;
                        case eCharacterClass.Blademaster:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blades:
                                    modelIDToAssign = 3244;
                                    break;
                                case eObjectType.Blunt:
                                    modelIDToAssign = 3246;
                                    break;
                                case eObjectType.Piercing:
                                    modelIDToAssign = 3245;
                                    break;
                            }
                            break;
                        case eCharacterClass.Champion:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blades:
                                    modelIDToAssign = 3251;
                                    break;
                                case eObjectType.Blunt:
                                    modelIDToAssign = 3253;
                                    break;
                                case eObjectType.Piercing:
                                    modelIDToAssign = 3252;
                                    break;
                            }
                            break;
                        case eCharacterClass.Druid:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blades:
                                    modelIDToAssign = 3247;
                                    break;
                                case eObjectType.Blunt:
                                    modelIDToAssign = 3248;
                                    break;
                            }
                            break;
                        case eCharacterClass.Eldritch:
                            modelIDToAssign = 3226;
                            break;
                        case eCharacterClass.Enchanter:
                            modelIDToAssign = 3227;
                            break;
                        case eCharacterClass.Hero:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blades:
                                    modelIDToAssign = 3256;
                                    break;
                                case eObjectType.Blunt:
                                    modelIDToAssign = 3258;
                                    break;
                                case eObjectType.Piercing:
                                    modelIDToAssign = 3257;
                                    break;
                            }
                            break;
                        case eCharacterClass.Mentalist:
                            modelIDToAssign = 3228;
                            break;
                        case eCharacterClass.Nightshade:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blades:
                                    modelIDToAssign = 3233;
                                    break;
                                case eObjectType.Piercing:
                                    modelIDToAssign = 3234;
                                    break;
                            }
                            break;
                        case eCharacterClass.Ranger:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blades:
                                    modelIDToAssign = 3242;
                                    break;
                                case eObjectType.Piercing:
                                    modelIDToAssign = 3241;
                                    break;
                            }
                            break;
                        case eCharacterClass.Valewalker:
                            modelIDToAssign = 3231;
                            break;
                        case eCharacterClass.Warden:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blades:
                                    modelIDToAssign = 3249;
                                    break;
                                case eObjectType.Blunt:
                                    modelIDToAssign = 3250;
                                    break;
                            }
                            break;
                    }
                    break;

                case "class epic 2h":
                    price = champion;
                    switch ((eCharacterClass)player.CharacterClass.ID)
                    {
                        //alb
                        case eCharacterClass.Armsman:
                            if ((eObjectType)item.Object_Type == eObjectType.PolearmWeapon)
                            {
                                switch ((eDamageType)item.Type_Damage)
                                {
                                    case eDamageType.Thrust:
                                        modelIDToAssign = 3297;
                                        break;
                                    case eDamageType.Slash:
                                        modelIDToAssign = 3297;
                                        break;
                                    case eDamageType.Crush:
                                        modelIDToAssign = 3298;
                                        break;
                                }
                            }
                            else
                            {
                                switch ((eDamageType)item.Type_Damage)
                                {
                                    case eDamageType.Thrust:
                                        modelIDToAssign = 3301;
                                        break;
                                    case eDamageType.Slash:
                                        modelIDToAssign = 3300;
                                        break;
                                    case eDamageType.Crush:
                                        modelIDToAssign = 3302;
                                        break;
                                }
                            }
                            break;
                        case eCharacterClass.Cabalist:
                            modelIDToAssign = 3264;
                            break;
                        case eCharacterClass.Cleric:
                            modelIDToAssign = 3282;
                            break;
                        case eCharacterClass.Friar:
                            modelIDToAssign = 3271;
                            break;
                        case eCharacterClass.Necromancer:
                            modelIDToAssign = 3268;
                            break;
                        case eCharacterClass.Paladin:
                            switch ((eDamageType)item.Type_Damage)
                            {
                                case eDamageType.Thrust:
                                    modelIDToAssign = 3307;
                                    break;
                                case eDamageType.Slash:
                                    modelIDToAssign = 3306;
                                    break;
                                case eDamageType.Crush:
                                    modelIDToAssign = 3308;
                                    break;
                            }
                            break;
                        case eCharacterClass.Sorcerer:
                            modelIDToAssign = 3265;
                            break;
                        case eCharacterClass.Theurgist:
                            modelIDToAssign = 3266;
                            break;
                        case eCharacterClass.Wizard:
                            modelIDToAssign = 3267;
                            break;

                        //mid
                        case eCharacterClass.Berserker:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Hammer:
                                    modelIDToAssign = 3324;
                                    break;
                                case eObjectType.Axe:
                                    modelIDToAssign = 3322;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3326;
                                    break;
                            }
                            break;
                        case eCharacterClass.Bonedancer:
                            modelIDToAssign = 3311;
                            break;
                        case eCharacterClass.Healer:
                            modelIDToAssign = 3335;
                            break;
                        case eCharacterClass.Hunter:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Spear:
                                    if ((eDamageType)item.Type_Damage == eDamageType.Thrust)
                                    {
                                        modelIDToAssign = 3319;
                                    }
                                    else
                                    {
                                        modelIDToAssign = 3320;
                                    }
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3318;
                                    break;
                            }
                            break;
                        case eCharacterClass.Runemaster:
                            modelIDToAssign = 3309;
                            break;
                        case eCharacterClass.Savage:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Hammer:
                                    modelIDToAssign = 3330;
                                    break;
                                case eObjectType.Axe:
                                    modelIDToAssign = 3328;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3332;
                                    break;
                            }
                            break;
                        case eCharacterClass.Shadowblade:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Axe:
                                    modelIDToAssign = 3316;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3314;
                                    break;
                            }
                            break;
                        case eCharacterClass.Shaman:
                            modelIDToAssign = 3338;
                            break;
                        case eCharacterClass.Skald:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Hammer:
                                    modelIDToAssign = 3342;
                                    break;
                                case eObjectType.Axe:
                                    modelIDToAssign = 3340;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3344;
                                    break;
                            }
                            break;
                        case eCharacterClass.Spiritmaster:
                            modelIDToAssign = 3310;
                            break;
                        case eCharacterClass.Thane:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Hammer:
                                    modelIDToAssign = 3348;
                                    break;
                                case eObjectType.Axe:
                                    modelIDToAssign = 3346;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3350;
                                    break;
                            }
                            break;
                        case eCharacterClass.Warrior:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Hammer:
                                    modelIDToAssign = 3354;
                                    break;
                                case eObjectType.Axe:
                                    modelIDToAssign = 3352;
                                    break;
                                case eObjectType.Sword:
                                    modelIDToAssign = 3356;
                                    break;
                            }
                            break;

                        //hib
                        case eCharacterClass.Animist:
                            modelIDToAssign = 3229;
                            break;
                        case eCharacterClass.Champion:
                            switch ((eDamageType)item.Type_Damage)
                            {
                                case eDamageType.Slash:
                                    modelIDToAssign = 3254;
                                    break;
                                case eDamageType.Thrust:
                                    modelIDToAssign = 3254;
                                    break;
                                case eDamageType.Crush:
                                    modelIDToAssign = 3253;
                                    break;
                            }
                            break;
                        case eCharacterClass.Eldritch:
                            modelIDToAssign = 3226;
                            break;
                        case eCharacterClass.Enchanter:
                            modelIDToAssign = 3227;
                            break;
                        case eCharacterClass.Hero:
                            switch ((eObjectType)item.Object_Type)
                            {
                                case eObjectType.Blades:
                                    modelIDToAssign = 3259;
                                    break;
                                case eObjectType.Blunt:
                                    modelIDToAssign = 3260;
                                    break;
                                case eObjectType.Piercing:
                                    modelIDToAssign = 3261;
                                    break;
                                case eObjectType.CelticSpear:
                                    modelIDToAssign = 3263;
                                    break;
                                case eObjectType.LargeWeapons:
                                    //yay weird daoc weapon structure
                                    switch ((eDamageType)item.Type_Damage)
                                    {
                                        case eDamageType.Slash:
                                            modelIDToAssign = 3259;
                                            break;
                                        case eDamageType.Thrust:
                                            modelIDToAssign = 3261;
                                            break;
                                        case eDamageType.Crush:
                                            modelIDToAssign = 3260;
                                            break;
                                    }
                                    break;

                            }
                            break;
                        case eCharacterClass.Mentalist:
                            modelIDToAssign = 3228;
                            break;
                        case eCharacterClass.Valewalker:
                            modelIDToAssign = 3231;
                            break;
                    }
                    break;
                #endregion

                #endregion

                #region shields
                case "aten's shield":
                    price = artifact;
                    modelIDToAssign = 1663;
                    break;
                case "cyclop's eye":
                    price = artifact;
                    modelIDToAssign = 1664;
                    break;
                case "shield of khaos":
                    price = artifact;
                    modelIDToAssign = 1665;
                    break;
                case "oceanus shield":
                    {
                        price = toageneric;
                        if (item.Type_Damage == 1)//small shield
                        {
                            modelIDToAssign = 2192;
                        }
                        else if (item.Type_Damage == 2)
                        {
                            modelIDToAssign = 2193;
                        }
                        else if (item.Type_Damage == 3)
                        {
                            modelIDToAssign = 2194;
                        }
                        break;
                    }
                case "aerus shield":
                    {
                        price = toageneric;
                        if (item.Type_Damage == 1)//small shield
                        {
                            modelIDToAssign = 2210;
                        }
                        else if (item.Type_Damage == 2)
                        {
                            modelIDToAssign = 2211;
                        }
                        else if (item.Type_Damage == 3)
                        {
                            modelIDToAssign = 2212;
                        }
                        break;
                    }
                case "magma shield":
                    {
                        price = toageneric;
                        if (item.Type_Damage == 1)//small shield
                        {
                            modelIDToAssign = 2218;
                        }
                        else if (item.Type_Damage == 2)
                        {
                            modelIDToAssign = 2219;
                        }
                        else if (item.Type_Damage == 3)
                        {
                            modelIDToAssign = 2220;
                        }
                        break;
                    }
                case "minotaur shield":
                    price = toageneric;
                    modelIDToAssign = 3554;
                    break;

                #endregion

                #region ranged weapons/instruments
                //case "dragonslayer harp": probably doesn't work
                //     break;
                case "class epic harp":
                    if (item.Object_Type != (int)eObjectType.Instrument)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = epic;
                    if ((eCharacterClass)player.CharacterClass.ID == eCharacterClass.Bard)
                    {
                        modelIDToAssign = 3239;
                    }
                    else if ((eCharacterClass)player.CharacterClass.ID == eCharacterClass.Minstrel)
                    {
                        modelIDToAssign = 3280;
                    }
                    else
                    {
                        price = 0;
                    }
                    break;
                case "labyrinth harp":
                    if (item.Object_Type != (int)eObjectType.Instrument)
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    modelIDToAssign = 3688;
                    break;
                case "class epic bow":
                    if (item.Object_Type != (int)eObjectType.CompositeBow &&
                        item.Object_Type != (int)eObjectType.Longbow &&
                        item.Object_Type != (int)eObjectType.RecurvedBow
                        )
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = champion;
                    if ((eCharacterClass)player.CharacterClass.ID == eCharacterClass.Scout)
                    {
                        modelIDToAssign = 3275;
                    }
                    else if ((eCharacterClass)player.CharacterClass.ID == eCharacterClass.Hunter)
                    {
                        modelIDToAssign = 3365;
                    }
                    else if ((eCharacterClass)player.CharacterClass.ID == eCharacterClass.Ranger)
                    {
                        modelIDToAssign = 3243;
                    }
                    else
                    {
                        price = 0;
                    }
                    break;
                case "fool's bow":
                    if (item.Object_Type != (int)eObjectType.CompositeBow &&
                        item.Object_Type != (int)eObjectType.Longbow &&
                        item.Object_Type != (int)eObjectType.RecurvedBow &&
                        item.Object_Type != (int)eObjectType.Fired
                        )
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1666;
                    break;
                case "braggart's bow":
                    if (item.Object_Type != (int)eObjectType.CompositeBow &&
                        item.Object_Type != (int)eObjectType.Longbow &&
                        item.Object_Type != (int)eObjectType.RecurvedBow &&
                        item.Object_Type != (int)eObjectType.Fired
                        )
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = artifact;
                    modelIDToAssign = 1667;
                    break;
                case "labyrinth bow":
                    if (item.Object_Type != (int)eObjectType.CompositeBow &&
                        item.Object_Type != (int)eObjectType.Longbow &&
                        item.Object_Type != (int)eObjectType.RecurvedBow &&
                        item.Object_Type != (int)eObjectType.Fired
                        )
                    {
                        SendNotValidMessage(player);
                        break;
                    }
                    price = toageneric;
                    modelIDToAssign = 3706;
                    break;
                #endregion

                #region Armor Pads
                case "armor pad":
                    SendReply(player, "I can offer the following pad types: \n\n" +
                        "[Type 1] \n" +
                        "[Type 2] \n" +
                        "[Type 3] \n" +
                        "[Type 4] \n" +
                        "[Type 5]"
                        );
                    return true;

                case "type 1":
                    price = armorpads;
                    modelIDToAssign = 1;
                    break;
                case "type 2":
                    price = armorpads;
                    modelIDToAssign = 2;
                    break;
                case "type 3":
                    price = armorpads;
                    modelIDToAssign = 3;
                    break;
                case "type 4":
                    price = armorpads;
                    modelIDToAssign = 4;
                    break;
                case "type 5":
                    price = armorpads;
                    modelIDToAssign = 5;
                    break;
                    #endregion

            }

            if (price == armorpads)
            {
                int extens = item.Extension;
                if (modelIDToAssign != extens) extens = modelIDToAssign;
                SetExtension(player, (byte)extens, price);
            }
            else
            {
                int model = item.Model;
                if (modelIDToAssign != 0) model = modelIDToAssign;
                SetModel(player, model, price);
            }

            return true;
        }

        private void SendNotValidMessage(GamePlayer player)
        {
            SendReply(player, "This skin is not valid for this item type. Please try a different combo.");
        }


        public override bool ReceiveItem(GameLiving source, InventoryItem item)
        {
            GamePlayer t = source as GamePlayer;
            if (t == null || item == null) return false;
            if (GetDistanceTo(t) > WorldMgr.INTERACT_DISTANCE)
            {
                t.Out.SendMessage("You are too far away to give anything to " + GetName(0, false) + ".", eChatType.CT_System, eChatLoc.CL_SystemWindow);
                return false;
            }

            switch (item.Item_Type)
            {
                case Slot.HELM:
                    SendReply(t, "A fine piece of headwear. \n" +
                        "I can apply the following skins: \n\n" +
                        "***** Catacombs Models Only ***** \n" +
                          "[Dragonslayer Helm] (" + dragon * 2 + " RPs)\n" +
                          "[Dragonsworn Helm] (" + dragon + " RPs)\n" +
                          "***** End Cata Only ***** \n\n" +

                        "[Crown of Zahur] (" + artifact + " RPs)\n" +
                        "[Crown of Zahur variant] (" + artifact + " RPs)\n" +
                        "[Winged Helm] (" + artifact + " RPs)\n" +
                        "[Oceanus Helm] (" + toageneric + " RPs)\n" +
                        "[Stygia Helm] (" + toageneric + " RPs)\n" +
                        "[Volcanus Helm] (" + toageneric + " RPs)\n" +
                        "[Aerus Helm] (" + toageneric + " RPs)\n" +
                        "[Wizard Hat] (" + epic + " RPs)\n\n" +
                        "Additionally, I have some realm specific headgear available:" +
                        "");
                    switch (source.Realm)
                    {
                        case eRealm.Albion:
                            SendReply(t, "[Robin Hood Hat] (" + festive + " RPs)\n" +
                                "[Tarboosh] (" + festive + " RPs)\n" +
                                "[Jester Hat] (" + festive + " RPs)\n" +
                                "");
                            break;
                        case eRealm.Hibernia:
                            SendReply(t, "[Robin Hood Hat] (" + festive + " RPs)\n" +
                               "[Leaf Hat] (" + festive + " RPs)\n" +
                               "[Stag Helm] (" + festive + " RPs)\n" +
                               "");
                            break;
                        case eRealm.Midgard:
                            SendReply(t, "[Fur Cap] (" + festive + " RPs)\n" +
                               "[Wing Hat] (" + festive + " RPs)\n" +
                               "[Wolf Helm] (" + festive + " RPs)\n" +
                               "");
                            break;
                    }
                    break;

                case Slot.TORSO:
                    SendReply(t, "This looks like it has protected you nicely. \n" +
                        "I can apply the following skins: \n\n" +
                        "***** Catacombs Models Only ***** \n" +
                          "[Dragonslayer Breastplate] (" + dragon * 2 + " RPs)\n" +
                          "[Dragonsworn Breastplate](" + dragon + " RPs)\n" +
                          "[Good Shar Breastplate](" + festive + " RPs)\n" +
                          "[Possessed Shar Breastplate](" + festive + " RPs)\n" +
                          "[Good Inconnu Breastplate](" + festive + " RPs)\n" +
                          "[Possessed Inconnu Breastplate](" + festive + " RPs)\n" +
                          "[Good Realm Breastplate](" + festive + " RPs)\n" +
                          "[Possessed Realm Breastplate](" + festive + " RPs)\n" +
                          "[Mino Breastplate](" + festive + " RPs)\n" +
                          "***** End Cata Only ***** \n\n" +

                        "[Class Epic Chestpiece](" + epic + " RPs)\n" +
                        "[Eirene's Chest](" + artifact + " RPs)\n" +
                        "[Naliah's Robe](" + artifact + " RPs)\n" +
                        "[Guard of Valor](" + artifact + " RPs)\n" +
                        "[Golden Scarab Vest](" + artifact + " RPs)\n" +
                        "[Oceanus Breastplate] (" + toageneric + " RPs)\n" +
                        "[Stygia Breastplate] (" + toageneric + " RPs)\n" +
                        "[Volcanus Breastplate] (" + toageneric + " RPs)\n" +
                        "[Aerus Breastplate] (" + toageneric + " RPs)\n" +

                        "");
                    SendReply(t, "I can also offer you some [armor pad] (" + armorpads + " RPs) options."
                         );
                    break;

                case Slot.ARMS:
                    SendReply(t, "This looks like it has protected you nicely. \n" +
                        "I can apply the following skins: \n\n" +
                        "***** Catacombs Models Only ***** \n" +
                          "[Dragonslayer Sleeves] (" + dragon * 2 + " RPs)\n" +
                          "[Dragonsworn Sleeves](" + dragon + " RPs)\n" +
                          "[Good Shar Sleeves](" + festive + " RPs)\n" +
                          "[Possessed Shar Sleeves](" + festive + " RPs)\n" +
                          "[Good Inconnu Sleeves](" + festive + " RPs)\n" +
                          "[Possessed Inconnu Sleeves](" + festive + " RPs)\n" +
                          "[Good Realm Sleeves](" + festive + " RPs)\n" +
                          "[Possessed Realm Sleeves](" + festive + " RPs)\n" +
                          "[Mino Sleeves](" + festive + " RPs)\n" +
                          "***** End Cata Only ***** \n\n" +
                        "[Foppish Sleeves] (" + artifact + " RPs)\n" +
                        "[Arms of the Wind] (" + artifact + " RPs)\n" +
                        "[Oceanus Sleeves] (" + toageneric + " RPs)\n" +
                        "[Stygia Sleeves] (" + toageneric + " RPs)\n" +
                        "[Volcanus Sleeves] (" + toageneric + " RPs)\n" +
                        "[Aerus Sleeves] (" + toageneric + " RPs)\n" +

                        "");
                    break;

                case Slot.LEGS:
                    SendReply(t, "This looks like it has protected you nicely. \n" +
                        "I can apply the following skins: \n\n" +
                        "***** Catacombs Models Only ***** \n" +
                          "[Dragonslayer Pants] (" + dragon * 2 + " RPs)\n" +
                          "[Dragonsworn Pants](" + dragon + " RPs)\n" +
                          "[Good Shar Pants](" + festive + " RPs)\n" +
                          "[Possessed Shar Pants](" + festive + " RPs)\n" +
                          "[Good Inconnu Pants](" + festive + " RPs)\n" +
                          "[Possessed Inconnu Pants](" + festive + " RPs)\n" +
                          "[Good Realm Pants](" + festive + " RPs)\n" +
                          "[Possessed Realm Pants](" + festive + " RPs)\n" +
                          "[Mino Pants](" + festive + " RPs)\n" +
                          "***** End Cata Only ***** \n\n" +
                        "[Wings Dive] (" + artifact + " RPs)\n" +
                        "[Alvarus' Leggings] (" + artifact + " RPs)\n" +
                        "[Oceanus Pants] (" + toageneric + " RPs)\n" +
                        "[Stygia Pants] (" + toageneric + " RPs)\n" +
                        "[Volcanus Pants] (" + toageneric + " RPs)\n" +
                        "[Aerus Pants] (" + toageneric + " RPs)\n" +

                        "");
                    break;

                case Slot.HANDS:
                    SendReply(t, "This looks like it has protected you nicely. \n" +
                        "I can apply the following skins: \n\n" +
                        "***** Catacombs Models Only ***** \n" +
                          "[Dragonslayer Gloves] (" + dragon * 2 + " RPs)\n" +
                          "[Dragonsworn Gloves](" + dragon + " RPs)\n" +
                          "[Good Shar Gloves](" + festive + " RPs)\n" +
                          "[Possessed Shar Gloves](" + festive + " RPs)\n" +
                          "[Good Inconnu Gloves](" + festive + " RPs)\n" +
                          "[Possessed Inconnu Gloves](" + festive + " RPs)\n" +
                          "[Good Realm Gloves](" + festive + " RPs)\n" +
                          "[Possessed Realm Gloves](" + festive + " RPs)\n" +
                          "[Mino Gloves](" + festive + " RPs)\n" +
                          "***** End Cata Only ***** \n\n" +
                        "[Maddening Scalars] (" + artifact + " RPs)\n" +
                        "[Sharkskin Gloves] (" + artifact + " RPs)\n" +
                        "[Oceanus Gloves] (" + toageneric + " RPs)\n" +
                        "[Stygia Gloves] (" + toageneric + " RPs)\n" +
                        "[Volcanus Gloves] (" + toageneric + " RPs)\n" +
                        "[Aerus Gloves] (" + toageneric + " RPs)\n" +

                        "");
                    SendReply(t, "I can also offer you some [armor pad] (" + armorpads + " RPs) options."
                         );
                    break;

                case Slot.FEET:
                    SendReply(t, "This looks like it has protected you nicely. \n" +
                        "I can apply the following skins: \n\n" +
                        "***** Catacombs Models Only ***** \n" +
                          "[Dragonslayer Boots] (" + dragon * 2 + " RPs)\n" +
                          "[Dragonsworn Boots](" + dragon + " RPs)\n" +
                          "[Good Shar Boots](" + festive + " RPs)\n" +
                          "[Possessed Shar Boots](" + festive + " RPs)\n" +
                          "[Good Inconnu Boots](" + festive + " RPs)\n" +
                          "[Possessed Inconnu Boots](" + festive + " RPs)\n" +
                          "[Good Realm Boots](" + festive + " RPs)\n" +
                          "[Possessed Realm Boots](" + festive + " RPs)\n" +
                          "[Mino Boots](" + festive + " RPs)\n" +
                          "***** End Cata Only ***** \n\n" +
                        "[Enyalio's Boots] (" + artifact + " RPs)\n" +
                        "[Flamedancer's Boots] (" + artifact + " RPs)\n" +
                        "[Oceanus Boots] (" + toageneric + " RPs)\n" +
                        "[Stygia Boots] (" + toageneric + " RPs)\n" +
                        "[Volcanus Boots] (" + toageneric + " RPs)\n" +
                        "[Aerus Boots] (" + toageneric + " RPs)\n" +

                        "");
                    SendReply(t, "I can also offer you some [armor pad] (" + armorpads + " RPs) options."
                         );
                    break;

                case Slot.CLOAK:
                    SendReply(t, "This looks like it has protected you nicely. \n" +
                        "I can apply the following skins: \n\n" +
                        "***** Catacombs Models Only ***** \n" +
                          "[Realm Cloak] (" + cloakexpensive + " RPs)\n" +
                          "[Dragonslayer Cloak] (" + cloakexpensive + " RPs)\n" +
                          "[Dragonsworn Cloak] (" + cloakmedium + " RPs)\n" +
                          "[Valentines Cloak] (" + cloakmedium + " RPs)\n" +
                          "[Winter Cloak] (" + cloakmedium + " RPs)\n" +
                          "[Clean Leather Cloak] (" + cloakmedium + " RPs)\n" +
                          "[Corrupt Leather Cloak] (" + cloakmedium + " RPs)\n" +
                          "***** End Cata Only ***** \n\n" +
                        "[Cloudsong] (" + cloakmedium + " RPs)\n" +
                        "[Shades of Mist] (" + cloakmedium + " RPs)\n" +
                        "[Harpy Feather Cloak] (" + cloakmedium + " RPs)\n" +
                        "[Healer's Embrace] (" + cloakmedium + " RPs)\n" +
                        "[Oceanus Cloak] (" + cloakmedium + " RPs)\n" +
                        "[Magma Cloak] (" + cloakmedium + " RPs)\n" +
                        "[Stygian Cloak] (" + cloakmedium + " RPs)\n" +
                        "[Aerus Cloak] (" + cloakmedium + " RPs)\n" +
                        "[Collared Cloak] (" + cloakcheap + " RPs)\n" +
                        "");
                    break;

                case Slot.RIGHTHAND:
                    SendReply(t, "Ah, I know a highly lethal weapon when I see it. \n" +
                        "I can apply the following skins: \n\n");
                    if ((eObjectType)item.Object_Type == eObjectType.HandToHand)
                    {
                        SendReply(t,
                                    "[Snakecharmer's Fist](" + artifact + " RPs)\n" +
                                    "[Scorched Fist](" + toageneric + " RPs)\n" +
                                    "[Dragonsworn Fist](" + dragon + " RPs)\n" +
                                    "");
                    }
                    if ((eObjectType)item.Object_Type == eObjectType.Flexible)
                    {
                        SendReply(t,
                                    "[Snakecharmer's Whip](" + artifact + " RPs)\n" +
                                    "[Scorched Whip](" + toageneric + " RPs)\n" +
                                    "[Dragonsworn Whip](" + dragon + " RPs)\n" +
                                    "");
                    }
                    else
                    {
                        switch ((eDamageType)item.Type_Damage)
                        {
                            case eDamageType.Thrust:
                                SendReply(t,
                                    "[Traitor's Dagger 1h](" + artifact + " RPs)\n" +
                                    "[Croc Tooth Dagger 1h](" + artifact + " RPs)\n" +
                                    "[Golden Spear 1h](" + artifact + " RPs)\n" +
                                    "[Wakazashi](" + epic + " RPs)\n" +
                                    "");
                                break;

                            case eDamageType.Crush:
                                SendReply(t,
                                    "[Battler Hammer 1h](" + artifact + " RPs)\n" +
                                    "[Malice Hammer 1h](" + artifact + " RPs)\n" +
                                    "[Bruiser Hammer 1h](" + artifact + " RPs)\n" +
                                    "[Scepter of the Meritorious](" + artifact + " RPs)\n" +
                                    "[Rolling Pin](" + epic + " RPs)\n" +
                                    "[Stein](" + epic + " RPs)\n" +
                                    "[Turkey Leg](" + champion + " RPs)\n" +
                                    "");
                                break;

                            case eDamageType.Slash:
                                SendReply(t,
                                    "[Croc Tooth Axe 1h](" + artifact + " RPs)\n" +
                                    "[Traitor's Axe 1h](" + artifact + " RPs)\n" +
                                    "[Malice Axe 1h](" + artifact + " RPs)\n" +
                                    "[Battler Sword 1h](" + artifact + " RPs)\n" +
                                    "[Khopesh](" + epic + " RPs)\n" +
                                    "[Cleaver](" + epic + " RPs)\n" +
                                    "");
                                break;
                        }
                        SendReply(t, "Or, perhaps you'd just prefer a [hilt 1h] (" + dragon + " RPs) \n" +
                                     "");
                    }
                    SendReply(t, "Additionally, I can apply an [class epic 1h] " + champion + " skin. \n");
                    break;


                case Slot.LEFTHAND:
                    if ((eObjectType)item.Object_Type == eObjectType.Shield)
                    {
                        SendReply(t, "A sturdy barricade to ward the blows of your enemies. \n" +
                        "I can apply the following skins: \n\n" +
                        "[Aten's Shield](" + artifact + " RPs)\n" +
                        "[Cyclop's Eye](" + artifact + " RPs)\n" +
                        "[Shield of Khaos](" + artifact + " RPs)\n" +
                        "[Oceanus Shield](" + toageneric + " RPs)\n" +
                        "[Aerus Shield](" + toageneric + " RPs)\n" +
                        "[Magma Shield](" + toageneric + " RPs)\n" +
                        "[Minotaur Shield](" + toageneric + " RPs)\n" +
                        "");
                    }
                    else
                    {
                        goto case Slot.RIGHTHAND;
                    }

                    break;

                case Slot.TWOHAND:
                    SendReply(t, "Ah, I know a highly lethal weapon when I see it. \n" +
                        "I can apply the following skins: \n\n");
                    if ((eObjectType)item.Object_Type == eObjectType.Staff)
                    {
                        SendReply(t,
                                    "[Dragonsworn Staff](" + dragon + " RPs)\n" +
                                    "[Traldor's Oracle](" + artifact + " RPs)\n" +
                                    "[Trident of the Gods](" + artifact + " RPs)\n" +
                                    "[Tartaros Gift](" + artifact + " RPs)\n" +
                                    "[Scorched Staff](" + toageneric + " RPs)\n" +
                                    "");
                    }
                    else if ((eObjectType)item.Object_Type == eObjectType.Scythe)
                    {
                        SendReply(t,
                                    "[Dragonsworn Scythe](" + dragon + " RPs)\n" +
                                    "[Scythe of Kings](" + artifact + " RPs)\n" +
                                    "[Snakechamer's Scythe](" + artifact + " RPs)\n" +
                                    "[Magma Scythe](" + toageneric + " RPs)\n" +
                                    "[Scorched Scythe](" + toageneric + " RPs)\n" +
                                    "");
                    }
                    else if ((eObjectType)item.Object_Type == eObjectType.PolearmWeapon)
                    {
                        SendReply(t,
                                    "[Dragonsworn Pole](" + dragon + " RPs)\n" +
                                    "[Pole of Kings](" + artifact + " RPs)\n" +
                                    "[Golden Pole](" + toageneric + " RPs)\n" +
                                    "[Scorched Pole](" + toageneric + " RPs)\n" +
                                    "");
                    }
                    else if ((eObjectType)item.Object_Type == eObjectType.Spear || (eObjectType)item.Object_Type == eObjectType.CelticSpear)
                    {
                        SendReply(t,
                                    "[Golden Spear 2h](" + artifact + " RPs)\n" +
                                    "[Dragon Spear 2h](" + dragon + " RPs)\n" +
                                    "[Scorched Spear 2h](" + toageneric + " RPs)\n" +
                                    "[Trident Spear 2h](" + toageneric + " RPs)\n" +
                                    "");
                    }
                    else
                    {
                        switch ((eDamageType)item.Type_Damage)
                        {
                            case eDamageType.Thrust:
                                SendReply(t,
                                    "[Scorched Thrust 2h](" + toageneric + " RPs)\n" +
                                    "[Dragon Thrust 2h](" + toageneric + " RPs)\n" +
                                    "[Katana 2h](" + epic + " RPs)\n" +
                                    "[Pickaxe](" + epic + " RPs)\n" +
                                    "");
                                break;

                            case eDamageType.Crush:
                                SendReply(t,
                                    "[Battler Hammer 2h](" + artifact + " RPs)\n" +
                                    "[Malice Hammer 2h](" + artifact + " RPs)\n" +
                                    "[Bruiser Hammer 2h](" + artifact + " RPs)\n" +
                                    "[Scorched Hammer 2h](" + toageneric + " RPs)\n" +
                                    "[Magma Hammer 2h](" + toageneric + " RPs)\n" +
                                    "[Pickaxe](" + epic + " RPs)\n" +
                                    "");
                                break;

                            case eDamageType.Slash:
                                SendReply(t,
                                    "[Malice Axe 2h](" + artifact + " RPs)\n" +
                                    "[Scorched Axe 2h](" + toageneric + " RPs)\n" +
                                    "[Magma Axe 2h](" + toageneric + " RPs)\n" +
                                    "[Battler Sword 2h](" + artifact + " RPs)\n" +
                                    "[Scorched Sword 2h](" + toageneric + " RPs)\n" +
                                    "[Katana 2h](" + epic + " RPs)\n" +
                                    "");
                                break;
                        }
                    }
                    SendReply(t, "Or, perhaps you'd just prefer a [hilt 2h] (" + epic + " RPs) \n" +
                                "Additionally, I can apply an [class epic 2h] (" + champion + " RPs) skin. \n");
                    break;

                case Slot.RANGED:
                    if ((eObjectType)item.Object_Type == eObjectType.Instrument)
                    {
                        SendReply(t, "This looks like it plays beautiful music. \n" +
                        "I can apply the following skins: \n\n" +
                        //"[Dragonslayer Harp](" + dragon + " RPs)\n" + //these too
                        "[Class Epic Harp](" + champion + " RPs)\n" +
                        "[Labyrinth Harp](" + toageneric + " RPs)\n" +
                        "");
                    }
                    else
                    {
                        SendReply(t, "Nothing like bringing death from afar. \n" +
                        "I can apply the following skins: \n\n" +
                        //"[Dragonslayer Bow](" + dragon + " RPs)\n" +
                        "[Class Epic Bow](" + champion + " RPs)\n" +
                        "[Braggart's Bow](" + artifact + " RPs)\n" +
                        "[Fool's Bow](" + artifact + " RPs)\n" +
                        "[Labyrinth Bow](" + toageneric + " RPs)\n" +
                        "");
                    }

                    break;
            }

            SendReply(t, ""
                         );
            t.TempProperties.setProperty(TempProperty, item);
            return false;
        }

        #region setrealmlevel
        public void SetRealmLevel(GamePlayer player, int rps)
        {
            if (player == null)
                return;

            if (rps == 0) { player.RealmLevel = 1; }
            else
            if (rps is >= 25 and < 125) { player.RealmLevel = 2; }
            else
            if (rps is >= 125 and < 350) { player.RealmLevel = 3; }
            else
            if (rps is >= 350 and < 750) { player.RealmLevel = 4; }
            else
            if (rps is >= 750 and < 1375) { player.RealmLevel = 5; }
            else
            if (rps is >= 1375 and < 2275) { player.RealmLevel = 6; }
            else
            if (rps is >= 2275 and < 3500) { player.RealmLevel = 7; }
            else
            if (rps is >= 3500 and < 5100) { player.RealmLevel = 8; }
            else
            if (rps is >= 5100 and < 7125) { player.RealmLevel = 9; }
            else
            //2l0
            if (rps is >= 7125 and < 9625) { player.RealmLevel = 10; }
            else
            if (rps is >= 9625 and < 12650) { player.RealmLevel = 11; }
            else
            if (rps is >= 12650 and < 16250) { player.RealmLevel = 12; }
            else
            if (rps is >= 16250 and < 20475) { player.RealmLevel = 13; }
            else
            if (rps is >= 20475 and < 25375) { player.RealmLevel = 14; }
            else
            if (rps is >= 25375 and < 31000) { player.RealmLevel = 15; }
            else
            if (rps is >= 31000 and < 37400) { player.RealmLevel = 16; }
            else
            if (rps is >= 37400 and < 44625) { player.RealmLevel = 17; }
            else
            if (rps is >= 44625 and < 52725) { player.RealmLevel = 18; }
            else
            if (rps is >= 52725 and < 61750) { player.RealmLevel = 19; }
            else
            //3l0
            if (rps is >= 61750 and < 71750) { player.RealmLevel = 20; }
            else
            if (rps is >= 71750 and < 82775) { player.RealmLevel = 21; }
            else
            if (rps is >= 82775 and < 94875) { player.RealmLevel = 22; }
            else
            if (rps is >= 94875 and < 108100) { player.RealmLevel = 23; }
            else
            if (rps is >= 108100 and < 122500) { player.RealmLevel = 24; }
            else
            if (rps is >= 122500 and < 138125) { player.RealmLevel = 25; }
            else
            if (rps is >= 138125 and < 155025) { player.RealmLevel = 26; }
            else
            if (rps is >= 155025 and < 173250) { player.RealmLevel = 27; }
            else
            if (rps is >= 173250 and < 192850) { player.RealmLevel = 28; }
            else
            if (rps is >= 192850 and < 213875) { player.RealmLevel = 29; }
            else
            //4L0
            if (rps is >= 213875 and < 236375) { player.RealmLevel = 30; }
            else
            if (rps is >= 236375 and < 260400) { player.RealmLevel = 31; }
            else
            if (rps is >= 260400 and < 286000) { player.RealmLevel = 32; }
            else
            if (rps is >= 286000 and < 313225) { player.RealmLevel = 33; }
            else
            if (rps is >= 313225 and < 342125) { player.RealmLevel = 34; }
            else
            if (rps is >= 342125 and < 372750) { player.RealmLevel = 35; }
            else
            if (rps is >= 372750 and < 405150) { player.RealmLevel = 36; }
            else
            if (rps is >= 405150 and < 439375) { player.RealmLevel = 37; }
            else
            if (rps is >= 439375 and < 475475) { player.RealmLevel = 38; }
            else
            if (rps is >= 475475 and < 513500) { player.RealmLevel = 39; }
            else


            if (rps is >= 513500)
            {
                player.RealmPoints = 513500;
                player.RealmLevel = 40;
            }


            player.Out.SendUpdatePlayer();
            player.Out.SendCharStatsUpdate();
            player.Out.SendUpdatePoints();
            player.UpdatePlayerStatus();
        }
        #endregion

        public void SendReply(GamePlayer player, string msg)
        {
            player.Out.SendMessage(msg, eChatType.CT_System, eChatLoc.CL_PopupWindow);
        }

        public void SetModel(GamePlayer player, int number, int price)
        {
            if (price > 0)
            {
                if (player.RealmPoints < price)
                {
                    SendReply(player, "I'm sorry, but you cannot afford my services currently.");
                    return;
                }

                SendReply(player, "Thanks for your donation. " +
                                  "I have changed your item's model, you can now use it. \n\n" +
                                  "I look forward to doing business with you in the future.");

                InventoryItem item = player.TempProperties.getProperty<InventoryItem>(TempProperty);
                player.TempProperties.removeProperty(TempProperty);

                if (item == null || item.OwnerID != player.InternalID || item.OwnerID == null)
                    return;

                player.Inventory.RemoveItem(item);
                ItemUnique unique = new ItemUnique(item.Template);
                unique.Model = number;
                GameServer.Database.AddObject(unique);
                InventoryItem newInventoryItem = GameInventoryItem.Create(unique as ItemTemplate);
                player.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, newInventoryItem);
                player.Out.SendInventoryItemsUpdate(new InventoryItem[] { newInventoryItem });
                // player.RemoveBountyPoints(300);
                player.RealmPoints -= price;
                player.RespecRealm();
                SetRealmLevel(player, (int)player.RealmPoints);
                player.SaveIntoDatabase();
                return;
            }

            SendReply(player, "I'm sorry, I seem to have gotten confused. Please start over. \n" +
                              "If you repeatedly get this message, please file a bug ticket on how you recreate it.");
        }

        public void SetExtension(GamePlayer player, byte number, int price)
        {
            if (price > 0)
            {
                if (player.RealmPoints < price)
                {
                    SendReply(player, "I'm sorry, but you cannot afford my services currently.");
                    return;
                }

                InventoryItem item = player.TempProperties.getProperty<InventoryItem>(TempProperty);
                player.TempProperties.removeProperty(TempProperty);

                if (item == null || item.OwnerID != player.InternalID || item.OwnerID == null)
                    return;

                //only allow pads on valid slots: torso/hand/feet
                if (item.Item_Type != (int)eEquipmentItems.TORSO && item.Item_Type != (int)eEquipmentItems.HAND && item.Item_Type != (int)eEquipmentItems.FEET)
                {
                    SendReply(player, "I'm sorry, but I can only modify the pads on Torso, Hand, and Feet armors.");
                    return;
                }


                player.Inventory.RemoveItem(item);
                ItemUnique unique = new ItemUnique(item.Template);
                unique.Extension = number;
                GameServer.Database.AddObject(unique);
                InventoryItem newInventoryItem = GameInventoryItem.Create(unique as ItemTemplate);
                player.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, newInventoryItem);
                player.Out.SendInventoryItemsUpdate(new InventoryItem[] { newInventoryItem });
                // player.RemoveBountyPoints(300);
                player.RealmPoints -= price;
                player.RespecRealm();
                SetRealmLevel(player, (int)player.RealmPoints);
                player.SaveIntoDatabase();

                SendReply(player, "Thanks for your donation. " +
                                  "I have changed your item's extension, you can now use it. \n\n" +
                                  "I look forward to doing business with you in the future.");

                return;
            }

            SendReply(player, "I'm sorry, I seem to have gotten confused. Please start over. \n" +
                              "If you repeatedly get this message, please file a bug ticket on how you recreate it.");
        }
    }
}
