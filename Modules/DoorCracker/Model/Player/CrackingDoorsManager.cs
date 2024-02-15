using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using MEC;

namespace ChickenDinnerV2.Modules.DoorCracker.Model.Player
{
    internal class CrackingDoorsManager
    {
        protected static CrackingDoorsManager instance = null;
        
        
        protected static readonly List<string> blackList = new List<string>()
        {
            "079_FIRST",
            "079_SECOND"
        };

        protected Dictionary<Door, CrackingDoor> doors = new Dictionary<Door, CrackingDoor>();

        protected bool coroutineIsRunning = true;

        protected CrackingDoorsManager()
        {
            Timing.RunCoroutine(CheckDoorsCoroutine());
        }

        ~CrackingDoorsManager()
        {
            coroutineIsRunning = false;
        }

        public static CrackingDoorsManager Instance()
        {
            if (instance == null)
            {
                instance = new CrackingDoorsManager();
            }
            return instance;
        }

        public void Clear()
        {
            doors = new Dictionary<Door, CrackingDoor>();
        }

        public void RegisterDoor(Exiled.API.Features.Player player, Door door)
        {
            if (player == null || door == null || blackList.Contains(door.Name))
            {
                player.ShowHint("<color=yellow>Too complicated</color>", 3);
                return;
            }

            lock (doors)
            {
                if (!checkDoorExistance(door))
                {
                    return;
                }

                addDoor(door, player);
            }
        }

        protected bool checkDoorExistance(Door door)
        {
            if (doors.ContainsKey(door))
            {
                if (!doors[door].IsCracking())
                {
                    removeDoor(door);
                    return true;
                }
                return false;
            }
            return true;
        }

        protected IEnumerator<float> CheckDoorsCoroutine()
        {
            while (coroutineIsRunning)
            {
                yield return Timing.WaitForSeconds(5f);

                try
                {
                    lock (doors)
                    {
                        for (int doorsIterator = 0; doorsIterator < doors.Count; doorsIterator++)
                        {
                            KeyValuePair<Door, CrackingDoor> item = doors.ElementAt(doorsIterator);


                            if (!item.Value.IsCracking())
                            {
                                removeDoor(item.Key);
                            }
                        }
                    }
                }
                catch (Exception E)
                {
                    Log.Warn(E.Message);
                    break;
                }
            }
        }

        protected void addDoor(Door door, Exiled.API.Features.Player player)
        {
            CrackingDoor newDoor = new CrackingDoor(door, player);

            doors.Add(door, newDoor);
            Timing.RunCoroutine(newDoor.CrackCoroutine());
        }

        protected void removeDoor(Door door)
        {
            doors.Remove(door);
        }
    }
}
