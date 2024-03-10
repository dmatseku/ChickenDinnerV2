using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Core;
using ChickenDinnerV2.Modules.PlayerSpawnRules.Model;
using Exiled.Events.EventArgs.Server;

namespace ChickenDinnerV2.Modules.PlayerSpawnRules.Events.Server
{
    internal class RespawningTeam : IObserver
    {
        protected static Config PlayerSpawnRulesConfig = Main.Instance.Config.PlayerSpawnRules;

        public bool Register()
        {
            if (PlayerSpawnRulesConfig.IsEnabled)
            {
                Exiled.Events.Handlers.Server.RespawningTeam += ApplyTeamConfig;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Server.RespawningTeam -= ApplyTeamConfig;
        }

        public void ApplyTeamConfig(RespawningTeamEventArgs ev)
        {
            TeamGeneralConfig.ApplyConfig(ev);
        }
    }
}
