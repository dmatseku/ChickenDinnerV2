using ChickenDinnerV2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using PlayerRoles;
using System.Text.RegularExpressions;

namespace ChickenDinnerV2.Modules.PlayerSpawnRules.Model
{
    internal static class SpawnOrder
    {
        private static Config PlayerSpawnRulesConfig = Main.Instance.Config.PlayerSpawnRules;

        private static Random rand = new Random(Guid.NewGuid().GetHashCode());

        private static List<int> getPlayerList(int count)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < count; i++)
            {
                result.Add(i);
            }
            return result;
        }

        private static RoleTypeId? GetRoleTypeId(string role, List<RoleTypeId> busyUniqRoles)
        {
            List<string> roles = new Regex(@"\s*,\s*").Split(role).ToList();
            RoleTypeId result;
            
            while (roles.Count > 0)
            {
                int RoleId = rand.Next(roles.Count);

                if (Enum.TryParse(roles[RoleId], out result))
                {
                    if (!roles[RoleId].Contains("Scp") || !busyUniqRoles.Contains(result))
                    {
                        if (roles[RoleId].Contains("Scp"))
                        {
                            busyUniqRoles.Add(result);
                        }
                        return result;
                    }
                }
                roles.RemoveAt(RoleId);
            }

            return null;
        }

        public static void Apply()
        {
            IReadOnlyCollection<Player> PlayerList = Player.List;
            List<int> ids = getPlayerList(PlayerList.Count);
            int SpawnOrderId = 0;
            List<RoleTypeId> busyUniqRoles = new List<RoleTypeId>();

            while (ids.Count > 0)
            {
                int playerId = ids[rand.Next(ids.Count)];
                Log.Warn("id: " + playerId.ToString() + "; ids count: " + ids.Count.ToString());
                RoleTypeId? roleTypeId = GetRoleTypeId(PlayerSpawnRulesConfig.spawnOrder[SpawnOrderId], busyUniqRoles);

                if (roleTypeId != null)
                {
                    PlayerList.ElementAt(playerId).RoleManager.InitializeNewRole((RoleTypeId)roleTypeId, RoleChangeReason.RoundStart);
                }

                ids.Remove(playerId);
                SpawnOrderId++;
                if (SpawnOrderId >= PlayerSpawnRulesConfig.spawnOrder.Count)
                {
                    SpawnOrderId = 0;
                }
            }
        }
    }
}
