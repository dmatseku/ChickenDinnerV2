using System;
using Exiled.API.Enums;
using Exiled.API.Features.Doors;
using HarmonyLib;
using Interactables.Interobjects.DoorUtils;
using Exiled.API.Features;
using PlayerRoles;

namespace ChickenDinnerV2.Modules.DistanceModification.Plugins
{
    [HarmonyPatch(typeof(DoorVariant))]
    internal class Scp173
    {

        protected static Config DistanceModificationConfig = ChickenDinnerV2.Core.Main.Instance.Config.DistanceModification;

        [HarmonyPrefix]
        [HarmonyPatch("ServerInteract")]
        public static bool Prefix(DoorVariant __instance, ReferenceHub ply, byte colliderId)
        {
            if (!DistanceModificationConfig.IsEnabled)
            {
                return true;
            }

            Door door = Door.Get(__instance);
            if (door.Type == DoorType.CheckpointEzHczA || door.Type == DoorType.CheckpointEzHczB || door.Type == DoorType.CheckpointLczA || door.Type == DoorType.CheckpointLczB)
            {
                return true;
            }

            Player player = Exiled.API.Features.Player.Get(ply);
            if (player.Role.Type != RoleTypeId.Scp173)
            {
                return true;
            }

            double angle = (int)Math.Round(door.Rotation.eulerAngles.y) % 90;
            double distance = 100;

            if (((int)Math.Round(door.Rotation.eulerAngles.y) / 90) % 2 != 0)
            {
                distance = (door.Position.x - player.Position.x) * ((90 - angle) / 90) + (door.Position.z - player.Position.z) * (angle / 90);
            }
            else
            {
                distance = (door.Position.x - player.Position.x) * (angle / 90) + (door.Position.z - player.Position.z) * ((90 - angle) / 90);
            }

            if (Math.Abs(distance) > DistanceModificationConfig.DoorDistanceTo173Modifier + DistanceModificationConfig.DoorDistanceTo173)
            {
                return false;
            }
            return true;
        }
    }
}
