using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Modules.DoorCracker.Model;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace ChickenDinnerV2.Modules.DoorCracker.Events.Player
{
    internal class InterruptEvents : IObserver
    {
        protected static Config DoorCrackerConfig = ChickenDinnerV2.Core.Main.Instance.Config.DoorCracker;
        public bool Register()
        {
            if (DoorCrackerConfig.IsEnabled)
            {
                Exiled.Events.Handlers.Player.ChangingRole += ChangingRole;
                Exiled.Events.Handlers.Player.ChangedItem += ChangedItem;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= ChangingRole;
            Exiled.Events.Handlers.Player.ChangedItem -= ChangedItem;
        }
        
        public void ChangingRole(ChangingRoleEventArgs ev)
        {
            DoorCrackingManager.ChangingRole(ev.Player);
        }

        public void ChangedItem(ChangedItemEventArgs ev)
        {
            DoorCrackingManager.CheckItem(ev.Player);
        }
    }
}
