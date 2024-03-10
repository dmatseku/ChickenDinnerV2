using ChickenDinnerV2.Core.Interfaces;
using Exiled.Events.EventArgs.Player;
using PlayerRoles.FirstPersonControl;
using ChickenDinnerV2.Modules.DoorCracker.Model;
using Exiled.API.Features;

namespace ChickenDinnerV2.Modules.DoorCracker.Events.Player
{
    internal class InteractingDoor : IObserver
    {
        protected static Config DoorCrackerConfig = ChickenDinnerV2.Core.Main.Instance.Config.DoorCracker;
        public bool Register()
        {
            if (DoorCrackerConfig.IsEnabled)
            {
                Exiled.Events.Handlers.Player.InteractingDoor += RegisterCrack;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.InteractingDoor -= RegisterCrack;
        }

        public void RegisterCrack(InteractingDoorEventArgs ev)
        {
            if (DoorCrackingConditions.CheckPrivilegedPlayer(ev.Player) || ev.Player.Role.Base is IFpcRole currentRole && currentRole.FpcModule.CurrentMovementState == PlayerMovementState.Sneaking)
            {
                if (DoorCrackingManager.RegisterDoor(ev.Door, ev.Player))
                {
                    ev.IsAllowed = false;
                }
            }
        }
    }
}
