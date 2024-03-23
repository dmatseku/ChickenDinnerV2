using ChickenDinnerV2.Core;
using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChickenDinnerV2.Modules.PlayerSpawnRules.Model
{
    internal static class RoleItemsConfig
    {
        private static Config PlayerSpawnRulesConfig = Main.Instance.Config.PlayerSpawnRules;

        private static Random rand = new Random(Guid.NewGuid().GetHashCode());

        private static List<Dictionary<string, string>> getRoleItemsList(string role)
        {
            List<List<Dictionary<string, string>>> customRoles;

            if (PlayerSpawnRulesConfig.RoleItems.TryGetValue(role, out customRoles))
            {
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
                    double chance;

                    if (itemRow.ContainsKey("Item") && Enum.TryParse(itemRow["Item"], out item) &&
                        itemRow.ContainsKey("Count") && int.TryParse(itemRow["Count"], out count) &&
                        itemRow.ContainsKey("Chance") && double.TryParse(itemRow["Chance"], out chance))
                    {
                        if (chance > 100)
                        {
                            chance = 100.0d;
                        }
                        double dchance = rand.NextDouble();
                        double preparedChance = chance / 100.0d;
                        if (preparedChance > dchance)
                        {
                            player.AddItem(item, count);
                        }
                        else if (player.Role.Team == Team.ChaosInsurgency || player.Role.Team == Team.FoundationForces)
                        {
                            Log.Warn("~~~~~~~~~~~~~~");
                            Log.Warn("Role: " + newRole.ToString());
                            Log.Warn("Item: " + item.ToString());
                            Log.Warn("Chance: " + chance.ToString());
                            Log.Warn("Prepared chance: " + preparedChance.ToString());
                            Log.Warn("Result chance: " + dchance.ToString());
                            Log.Warn("Result: " + (preparedChance > dchance).ToString());
                        }
                    }
                }
            }
        }
    }
}
