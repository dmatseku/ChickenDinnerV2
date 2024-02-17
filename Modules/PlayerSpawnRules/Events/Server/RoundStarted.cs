using ChickenDinnerV2.Core;
using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Modules.PlayerSpawnRules.Model;
using Exiled.API.Features;
using Exiled.Events.Handlers;

namespace ChickenDinnerV2.Modules.PlayerSpawnRules.Events.Server
{
    internal class RoundStarted
    {
        protected static Config PlayerSpawnRulesConfig = Main.Instance.Config.PlayerSpawnRules;

        public bool Register()
        {
            if (PlayerSpawnRulesConfig.IsEnabled)
            {
                Exiled.Events.Handlers.Server.RoundStarted += ApplySpawnOrder;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= ApplySpawnOrder;
        }

        public void ApplySpawnOrder()
        {
            Log.Warn("hello");
            bool isRoundLocked = Round.IsLocked;
            Round.IsLocked = true;
            SpawnOrder.Apply();
            Round.IsLocked = isRoundLocked;
        }
    }
}
