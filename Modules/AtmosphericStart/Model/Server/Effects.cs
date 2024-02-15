using ChickenDinnerV2.Core;
using Interactables.Interobjects.DoorUtils;
using MEC;
using PluginAPI.Core;
using PluginAPI.Core.Zones;
using System;
using System.Collections.Generic;

namespace ChickenDinnerV2.Modules.AtmosphericStart.Model.Server
{
    internal static class Effects
    {
        private static Config AtmosphericStartConfig = Main.Instance.Config.AtmosphericStart;

        private static readonly List<Action<FacilityRoom>> effects = new List<Action<FacilityRoom>>
        {
            (room) =>
            {
                
                if (room.Identifier.Name == MapGeneration.RoomName.LczClassDSpawn)
                {
                    foreach (DoorVariant door in DoorVariant.DoorsByRoom[room.Identifier])
                    {
                        if (door.name.Contains("Prison"))
                        {
                            door.ServerChangeLock(DoorLockReason.Regular079, true);
                        }
                        door.NetworkTargetState = true;
                    }
                }
            },
        };

        private static readonly List<Action<FacilityRoom>> clearEffects = new List<Action<FacilityRoom>>
        {
            (room) =>
            {
                if (room.Identifier.Name == MapGeneration.RoomName.LczClassDSpawn)
                {
                    foreach (DoorVariant door in DoorVariant.DoorsByRoom[room.Identifier])
                    {
                        door.NetworkTargetState = false;
                    }
                }
            },
        };

        private static void applyColorEffect(RoomLightController roomLight)
        {
            if (roomLight.Room.Zone != MapGeneration.FacilityZone.Surface)
            {
                roomLight.NetworkOverrideColor = new UnityEngine.Color(0.9f, 0, 0, 1f);
            }
        }

        private static void clearColorEffect(RoomLightController roomLight)
        {
            if (roomLight.Room.Zone != MapGeneration.FacilityZone.Surface)
            {
                roomLight.NetworkOverrideColor = UnityEngine.Color.clear;
            }
        }


        public static void applyEffects()
        {
            List<FacilityRoom> rooms = Facility.Rooms;

            foreach (FacilityRoom room in rooms)
            {
                foreach (var action in effects)
                {
                    action(room);
                }
            }
            foreach (RoomLightController roomLight in RoomLightController.Instances)
            {
                applyColorEffect(roomLight);
            }

            Timing.RunCoroutine(cancelEffects());
        }

        private static IEnumerator<float> cancelEffects()
        {
            List<FacilityRoom> rooms = Facility.Rooms;

            yield return Timing.WaitForSeconds(AtmosphericStartConfig.Duration);

            foreach (FacilityRoom room in rooms)
            {
                foreach (var action in clearEffects)
                {
                    action(room);
                }
            }
            foreach (RoomLightController roomLight in RoomLightController.Instances)
            {
                clearColorEffect(roomLight);
            }
        }
    }
}
