using ChickenDinnerV2.Core;
using Exiled.Events.EventArgs.Server;
using Respawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenDinnerV2.Modules.PlayerSpawnRules.Model
{
    internal static class TeamGeneralConfig
    {
        private static Config PlayerSpawnRulesConfig = Main.Instance.Config.PlayerSpawnRules;

        public static void ApplyConfig(RespawningTeamEventArgs ev)
        {
            Dictionary<string, string> config;

            if (PlayerSpawnRulesConfig.TeamConfig.TryGetValue(ev.NextKnownTeam.ToString(), out config))
            {
                int teamSize;

                if (int.TryParse(config["max_team_size"], out teamSize))
                {
                    ev.MaximumRespawnAmount = teamSize;
                }
            }
        }
    }
}
