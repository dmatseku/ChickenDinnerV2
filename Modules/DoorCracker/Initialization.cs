using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Modules.DoorCracker.Model;
using Exiled.API.Features;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenDinnerV2.Modules.DoorCracker
{
    internal class Initialization : IInitialize
    {

        public void Initialize()
        {
            Timing.RunCoroutine(ObserveMovement());
        }

        protected IEnumerator<float> ObserveMovement()
        {
            while (true)
            {
                for (int crackerIndex = 0; crackerIndex < ChickenDinnerV2.Modules.DoorCracker.Model.DoorCracker.CrackingDoorsList.Count; crackerIndex++)
                {
                    DoorCrackingManager.CheckMovement(ChickenDinnerV2.Modules.DoorCracker.Model.DoorCracker.CrackingDoorsList[crackerIndex]);
                }

                yield return Timing.WaitForSeconds(1f);
            }
        }
    }
}
