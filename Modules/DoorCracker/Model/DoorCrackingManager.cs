using Exiled.API.Features.Doors;
using ExiledPlayer = Exiled.API.Features.Player;
using Exiled.API.Features;

namespace ChickenDinnerV2.Modules.DoorCracker.Model
{
    internal static class DoorCrackingManager
    {
        public static bool RegisterDoor(Door door, ExiledPlayer player)
        {
            lock (DoorCracker.CrackingDoorsList)
            {
                DoorCracker instance = DoorCracker.GetCrackerByDoor(door);

                if (DoorCrackingConditions.CheckConditions(door, player) && instance == null)
                {
                    instance = new DoorCracker(door, player);

                    instance.Invoke();
                    return true;
                }
                else if (instance != null)
                {
                    if (instance.player != player)
                    {
                        instance.Interrupt();
                    }
                    else
                    {
                        instance.NotifyAlreadyCracking();
                    }
                }
            }
            return false;
        }

        public static void CheckMovement(DoorCracker doorCracker)
        {
            if (!DoorCrackingConditions.CheckPlayerPostion(doorCracker.player, doorCracker.door))
            {
                doorCracker.Interrupt();
            }
        }

        public static void CheckItem(ExiledPlayer player)
        {
            DoorCracker crackerInstance = DoorCracker.GetCrackerByPlayer(player);

            if (crackerInstance != null && !DoorCrackingConditions.CheckAllowedPlayer(player))
            {
                crackerInstance.Interrupt();
            }
        }

        public static void ChangingRole(ExiledPlayer player)
        {
            DoorCracker crackerInstance = DoorCracker.GetCrackerByPlayer(player);

            if (crackerInstance != null)
            {
                crackerInstance.Interrupt();
            }
        }
    }
}
