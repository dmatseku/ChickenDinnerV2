using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Core;
using Exiled.Events.EventArgs.Player;

namespace ChickenDinnerV2.Modules.PlayerSpawnRules.Events.Player
{
    internal class RoleChanged : IObserver
    {
        protected static Config PlayerSpawnRulesConfig = Main.Instance.Config.PlayerSpawnRules;

        public bool Register()
        {
            if (PlayerSpawnRulesConfig.IsEnabled)
            {
                Exiled.Events.Handlers.Player.Spawned += SetItems;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.Spawned -= SetItems;
        }

        public void SetItems(SpawnedEventArgs ev)
        {
            Model.RoleHandler.HandleNewRole(ev.Player, ev.Player.Role.Type);
        }
    }
}
