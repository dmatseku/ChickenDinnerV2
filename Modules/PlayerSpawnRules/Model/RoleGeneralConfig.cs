using ChickenDinnerV2.Core;
using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChickenDinnerV2.Modules.PlayerSpawnRules.Model
{
    internal static class RoleGeneralConfig
    {
        private static Config PlayerSpawnRulesConfig = Main.Instance.Config.PlayerSpawnRules;

        private static void ApplyHP(Player player, Dictionary<string, string> config)
        {
            string HPString;
            float HP;

            if (config.TryGetValue("HP", out HPString) && float.TryParse(HPString, out HP))
            {
                player.MaxHealth = HP;
                player.Health = HP;
            }
        }

        public static void ApplyGeneralConfig(Player player, RoleTypeId newRole)
        {
            Dictionary<string, string> config;

            if (PlayerSpawnRulesConfig.RoleConfig.TryGetValue(newRole.ToString(), out config))
            {
                ApplyHP(player, config);
            }
        }
    }
}
