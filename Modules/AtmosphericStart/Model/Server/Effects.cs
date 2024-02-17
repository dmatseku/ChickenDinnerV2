using ChickenDinnerV2.Core;
using Interactables.Interobjects.DoorUtils;
using MEC;
using PluginAPI.Core;
using PluginAPI.Core.Zones;
using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Log = Exiled.API.Features.Log;
using MapGeneration;
using PluginAPI.Core.Doors;
using Exiled.API.Features.Doors;

namespace ChickenDinnerV2.Modules.AtmosphericStart.Model.Server
{
    internal static class Effects
    {
        private static Config AtmosphericStartConfig = Main.Instance.Config.AtmosphericStart;

        private static List<RoomName> scpRooms = new List<RoomName>
        {
            MapGeneration.RoomName.Lcz173,
            MapGeneration.RoomName.Hcz049,
            MapGeneration.RoomName.Hcz096,
            MapGeneration.RoomName.Hcz106,
            MapGeneration.RoomName.Hcz939
        };

        private static List<string> scpGates = new List<string>
        {
            "173_CONNECTOR",
            "173_GATE",
            "173_BOTTOM"
        };

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
                            door.ServerChangeLock(DoorLockReason.AdminCommand, true);
                        }
                        door.NetworkTargetState = true;
                    }
                }
                if (scpRooms.Contains(room.Identifier.Name))
                {
                    Room exiledRoom = Room.Get(room.Identifier); 

                    foreach (Door door in exiledRoom.Doors)
                    {
                        door.Lock(9999999, Exiled.API.Enums.DoorLockType.AdminCommand);
                        door.IsOpen = false;
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
                if (scpRooms.Contains(room.Identifier.Name))
                {
                    Room exiledRoom = Room.Get(room.Identifier);

                    foreach (Door door in exiledRoom.Doors)
                    {
                        door.Unlock();
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
