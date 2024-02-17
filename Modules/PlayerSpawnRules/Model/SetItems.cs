using ChickenDinnerV2.Core;
using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChickenDinnerV2.Modules.PlayerSpawnRules.Model
{
    internal static class SetItems
    {
        private static Config PlayerSpawnRulesConfig = Main.Instance.Config.PlayerSpawnRules;

        private static Random rand = new Random(Guid.NewGuid().GetHashCode());

        private static KeyValuePair<string, Dictionary<string, int>>? getRoleItemsList(string role)
        {
            if (PlayerSpawnRulesConfig.RoleItems.ContainsKey(role))
            {
                Dictionary<string, Dictionary<string, int>> customRoles = PlayerSpawnRulesConfig.RoleItems[role];
                int customRoleIndex = rand.Next(customRoles.Count);

                return customRoles.ElementAt(customRoleIndex);
            }

            return null;
        }

        public static void SetItemsToRole(Player player, RoleTypeId newRole)
        {
            KeyValuePair<string, Dictionary<string, int>>? itemsList = getRoleItemsList(newRole.ToString());

            if (itemsList != null)
            {
                player.ClearInventory();
                player.RoleManager.name = ((KeyValuePair<string, Dictionary<string, int>>)itemsList).Key;
                Dictionary<string, int> list = ((KeyValuePair<string, Dictionary<string, int>>)itemsList).Value;

                foreach (KeyValuePair<string, int> itemRow in list)
                {
                    ItemType item;

                    if (Enum.TryParse(itemRow.Key, out item))
                    {
                        player.AddItem(item, itemRow.Value);
                    }
                }
            }
        }
    }
}
