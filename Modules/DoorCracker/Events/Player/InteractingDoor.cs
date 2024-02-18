using ChickenDinnerV2.Core.Interfaces;
using Exiled.Events.EventArgs.Player;
using PlayerRoles.FirstPersonControl;
using ChickenDinnerV2.Modules.DoorCracker.Model.Player;

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
            if (CrackingDoor.CheckPrivilegedPlayer(ev.Player))
            {
                CrackingDoorsManager.Instance().RegisterDoor(ev.Player, ev.Door);
                ev.IsAllowed = false;
            }
            else if (ev.Player.ReferenceHub.roleManager.CurrentRole is IFpcRole currentRole && currentRole.FpcModule.CurrentMovementState == PlayerMovementState.Sneaking)
            {
                CrackingDoorsManager.Instance().RegisterDoor(ev.Player, ev.Door);
                ev.IsAllowed = false;
            }
        }
    }
}
