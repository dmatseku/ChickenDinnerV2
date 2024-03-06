using ChickenDinnerV2.Core;
using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChickenDinnerV2.Modules.PlayerSpawnRules.Model
{
    internal static class RoleHandler
    {
        public static void HandleNewRole(Player player, RoleTypeId newRole)
        {
            RoleGeneralConfig.ApplyGeneralConfig(player, newRole);
            RoleItemsConfig.SetItemsToRole(player, newRole);
        }
    }
}
