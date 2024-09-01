﻿/*
 * DAWN OF LIGHT - The first free open source DAoC server emulator
 * 
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA  02111-1307, USA.
 *
 */
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using DOL.Language;
using DOL.GS.PacketHandler;
using DOL.Database;
using DOL.GS.Spells;
using log4net;
using System.Linq;

namespace DOL.GS
{
    /// <summary>
    /// This class represents a relic in a players inventory
    /// </summary>
    public class ItemChargeGem : GameInventoryItem
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ItemChargeGem()
            : base()
        {
        }
        public ItemChargeGem(DbItemTemplate template)
            : base(template)
        {
        }

        public ItemChargeGem(DbItemUnique template)
            : base(template)
        {
        }

        public ItemChargeGem(DbInventoryItem item)
            : base(item)
        {
            OwnerID = item.OwnerID;
            ObjectId = item.ObjectId;
        }

        public override bool Combine(GamePlayer player, DbInventoryItem targetItem)
        {

            if (true)
            {
                List<(eProperty, int)> targetItemProperties = new()
                {
                    ((eProperty)targetItem.Bonus1Type, targetItem.Bonus1),
                    ((eProperty)targetItem.Bonus2Type, targetItem.Bonus2),
                    ((eProperty)targetItem.Bonus3Type, targetItem.Bonus3),
                    ((eProperty)targetItem.Bonus4Type, targetItem.Bonus4),
                    ((eProperty)targetItem.Bonus5Type, targetItem.Bonus5),
                    ((eProperty)targetItem.Bonus6Type, targetItem.Bonus6),
                    ((eProperty)targetItem.Bonus7Type, targetItem.Bonus7),
                    ((eProperty)targetItem.Bonus8Type, targetItem.Bonus8),
                    ((eProperty)targetItem.Bonus9Type, targetItem.Bonus9),
                    ((eProperty)targetItem.Bonus10Type, targetItem.Bonus10),
                };

                var targetItemProcSockets = targetItemProperties.Where(A => A.Item1 == eProperty.Socket_Charge).ToList();
                if (targetItemProcSockets.Count == 0)
                {
                    player.Out.SendMessage($"{targetItem.Name} has no charge sockets!", eChatType.CT_Advise, eChatLoc.CL_ChatWindow);
                    return false;
                }

                DbItemUnique unique = new DbItemUnique(targetItem.Template);

                if (this.SpellID == 0)
                {
                    player.Out.SendMessage($"{this.Name} has no charges!", eChatType.CT_Advise, eChatLoc.CL_ChatWindow);
                    return false;

                }

                unique.SpellID = this.SpellID;
                unique.MaxCharges = 100;

                if (unique.Bonus1Type == (int)eProperty.Socket_Charge)
                {
                    unique.Bonus1Type = (int)0;
                }
                else if (unique.Bonus2Type == (int)eProperty.Socket_Charge)
                {
                    unique.Bonus2Type = (int)0;
                }
                else if (unique.Bonus3Type == (int)eProperty.Socket_Charge)
                {
                    unique.Bonus3Type = (int)0;
                }
                else if (unique.Bonus4Type == (int)eProperty.Socket_Charge)
                {
                    unique.Bonus4Type = (int)0;
                }
                else if (unique.Bonus5Type == (int)eProperty.Socket_Charge)
                {
                    unique.Bonus5Type = (int)0;
                }
                else if (unique.Bonus6Type == (int)eProperty.Socket_Charge)
                {
                    unique.Bonus6Type = (int)0;
                }
                else if (unique.Bonus7Type == (int)eProperty.Socket_Charge)
                {
                    unique.Bonus7Type = (int)0;
                }
                else if (unique.Bonus8Type == (int)eProperty.Socket_Charge)
                {
                    unique.Bonus8Type = (int)0;
                }
                else if (unique.Bonus9Type == (int)eProperty.Socket_Charge)
                {
                    unique.Bonus9Type = (int)0;
                }
                else if (unique.Bonus10Type == (int)eProperty.Socket_Charge)
                {
                    unique.Bonus10Type = (int)0;
                }

                GameServer.Database.AddObject(unique);
                player.Inventory.RemoveItem(targetItem);
                player.Inventory.RemoveCountFromStack(this, 1);

                DbInventoryItem newInventoryItem = GameInventoryItem.Create(unique as DbItemTemplate);
                if (targetItem.IsCrafted)
                    newInventoryItem.IsCrafted = true;
                if (targetItem.Creator != "")
                    newInventoryItem.Creator = targetItem.Creator;


                newInventoryItem.Count = 1;

                player.Inventory.AddItem(eInventorySlot.FirstEmptyBackpack, newInventoryItem);
                player.Out.SendInventoryItemsUpdate(new DbInventoryItem[] { newInventoryItem });

                player.SaveIntoDatabase();

                player.Out.SendMessage($"Your {targetItem.Name} has been upgraded!", eChatType.CT_Advise, eChatLoc.CL_ChatWindow);




                return true;
            }
            return false;
        }
    }
}
