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

        private static List<Dictionary<string, string>> getRoleItemsList(string role)
        {
            if (PlayerSpawnRulesConfig.RoleItems.ContainsKey(role))
            {
                List<List<Dictionary<string, string>>> customRoles = PlayerSpawnRulesConfig.RoleItems[role];
                int customRoleIndex = rand.Next(customRoles.Count);

                return customRoles.ElementAt(customRoleIndex);
            }

            return null;
        }

        public static void SetItemsToRole(Player player, RoleTypeId newRole)
        {
            List<Dictionary<string, string>> itemsList = getRoleItemsList(newRole.ToString());

            if (itemsList != null)
            {
                player.ClearInventory();

                foreach (Dictionary<string, string> itemRow in itemsList)
                {
                    ItemType item;
                    int count;
                    int chance;

                    if (itemRow.ContainsKey("Item") && Enum.TryParse(itemRow["Item"], out item) &&
                        itemRow.ContainsKey("Count") && int.TryParse(itemRow["Count"], out count) &&
                        itemRow.ContainsKey("Chance") && int.TryParse(itemRow["Chance"], out chance))
                    {
                        if (chance > 100)
                        {
                            chance = 100;
                        }

                        if (chance >= rand.Next(101))
                        {
                            player.AddItem(item, count);
                        }
                    }
                }
            }
        }
    }
}
