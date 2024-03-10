using Exiled.API.Enums;
using Exiled.API.Features.Doors;
using System.Collections.Generic;
using ExiledPlayer = Exiled.API.Features.Player;
using Utils.NonAllocLINQ;
using MEC;
using Exiled.API.Features;

namespace ChickenDinnerV2.Modules.DoorCracker.Model
{
    internal class DoorCracker
    {
        protected static Config DoorCrackerConfig = ChickenDinnerV2.Core.Main.Instance.Config.DoorCracker;

        public static List<DoorCracker> CrackingDoorsList { get; protected set; } = new List<DoorCracker>();

        public static DoorCracker GetCrackerByDoor(Door door)
        {
            return CrackingDoorsList.FirstOrDefault((x) => x.door == door, null);
        }

        public static DoorCracker GetCrackerByPlayer(ExiledPlayer player)
        {
            return CrackingDoorsList.FirstOrDefault((x) => x.player == player, null);
        }

        public static void Clear()
        {
            CrackingDoorsList = new List<DoorCracker>();
        }

        public ExiledPlayer player { get; protected set; }

        public Door door { get; protected set; }

        public DoorCracker(Door door, ExiledPlayer player)
        {  
            this.door = door;
            this.player = player;

            CrackingDoorsList.Add(this);
        }

        ~DoorCracker()
        {
            lock (CrackingDoorsList)
            {
                if (CrackingDoorsList.Contains(this))
                {
                    CrackingDoorsList.Remove(this);
                }
            }
        }

        public void Invoke()
        {
            Timing.RunCoroutine(Crack());
        }

        protected IEnumerator<float> Crack()
        {
            Alert("Cracking has been started", "yellow");
            yield return Timing.WaitForSeconds(GetTimeToCrack());

            lock (CrackingDoorsList)
            {
                if (CrackingDoorsList.Contains(this))
                {
                    if (DoorCrackingConditions.CheckConditions(door, player))
                    {
                        InfluenceDoor();
                    }
                    else
                    {
                        Interrupt();
                    }
                    CrackingDoorsList.Remove(this);
                }
            }
        }

        //TODO: rewrite to better solution maybe
        protected int GetTimeToCrack()
        {
            if (DoorCrackingConditions.CheckPrivilegedPlayer(player))
            {
                return 0;
            }

            int time = DoorCrackerConfig.SimpleDoor;

            if (door.IsGate)
            {
                time = DoorCrackerConfig.Gate;
                if (door.RequiredPermissions.RequiredPermissions != Interactables.Interobjects.DoorUtils.KeycardPermissions.None)
                {
                    time = DoorCrackerConfig.KeycardGate;
                }
            }
            else if (door.RequiredPermissions.RequiredPermissions != Interactables.Interobjects.DoorUtils.KeycardPermissions.None)
            {
                time = DoorCrackerConfig.KeycardDoor;
            }

            return time;
        }

        protected void InfluenceDoor()
        {
            if (door.IsLocked)
            {
                Alert("Door has been unlocked", "green");
                door.Unlock();
                door.IsOpen = true;
            }
            else
            {
                Alert("Door has been locked", "green");
                door.ChangeLock(DoorLockType.Regular079);
                door.IsOpen = false;
            }
        }

        public void Interrupt()
        {
            lock (CrackingDoorsList)
            {
                if (CrackingDoorsList.Contains(this))
                {
                    Alert("Cracking has been interrupted", "red");
                    CrackingDoorsList.Remove(this);
                }
            }
        }

        public void NotifyAlreadyCracking()
        {
            Alert("Already cracking", "yellow");
        }

        protected void Alert(string message, string color, float duration = 1.5f)
        {
            player.ShowHint("<color=" + color + ">" + message + "</color>", duration);
        }
    }
}
