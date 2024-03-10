using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using PlayerRoles;
using System;
using System.Collections.Generic;
using ExiledPlayer = Exiled.API.Features.Player;

namespace ChickenDinnerV2.Modules.DoorCracker.Model
{
    internal static class DoorCrackingConditions
    {
        private static readonly List<RoleTypeId> privilegedRoles = new List<RoleTypeId>
        {
            RoleTypeId.Tutorial,
            RoleTypeId.Overwatch
        };

        private static readonly Dictionary<RoleTypeId, ItemType> allowedRolesAndCards = new Dictionary<RoleTypeId, ItemType>
        {
            { RoleTypeId.NtfCaptain, ItemType.KeycardContainmentEngineer },
            { RoleTypeId.NtfSpecialist, ItemType.KeycardContainmentEngineer },
            { RoleTypeId.NtfPrivate, ItemType.KeycardContainmentEngineer },
            { RoleTypeId.ChaosConscript, ItemType.KeycardChaosInsurgency },
            { RoleTypeId.ChaosMarauder, ItemType.KeycardChaosInsurgency },
            { RoleTypeId.ChaosRepressor, ItemType.KeycardChaosInsurgency },
            { RoleTypeId.ChaosRifleman, ItemType.KeycardChaosInsurgency }
        };

        private static readonly Dictionary<DoorType, RoomType> doorExceptions = new Dictionary<DoorType, RoomType>
        {
            { DoorType.CheckpointEzHczA, RoomType.HczEzCheckpointA },
            { DoorType.CheckpointEzHczB, RoomType.HczEzCheckpointB },
            { DoorType.CheckpointLczA, RoomType.LczCheckpointA },
            { DoorType.CheckpointLczB, RoomType.LczCheckpointB }
        };

        private static readonly List<string> blackList = new List<string>()
        {
            "079_FIRST",
            "079_SECOND"
        };

        public static bool CheckPrivilegedPlayer(ExiledPlayer player)
        {
            return privilegedRoles.Contains(player.Role.Type) && player.CurrentItem != null && (player.CurrentItem.Type == ItemType.KeycardChaosInsurgency || player.CurrentItem.Type == ItemType.KeycardContainmentEngineer);
        }

        public static bool CheckAllowedPlayer(ExiledPlayer player)
        {
            return allowedRolesAndCards.ContainsKey(player.Role.Type) && player.CurrentItem != null && allowedRolesAndCards[player.Role.Type] == player.CurrentItem.Type;
        }

        public static bool CheckPlayerPostion(ExiledPlayer player, Door door)
        {
            if (CheckPrivilegedPlayer(player))
            {
                return true;
            }

            if (doorExceptions.ContainsKey(door.Type))
            {
                return doorExceptions[door.Type] == player.CurrentRoom.Type;
            }

            double angle = (int)Math.Round(door.Rotation.eulerAngles.y) % 90;
            double distance;

            if (((int)Math.Round(door.Rotation.eulerAngles.y) / 90) % 2 != 0)
            {
                distance = (door.Position.x - player.Position.x) * ((90 - angle) / 90) + (door.Position.z - player.Position.z) * (angle / 90);
            }
            else
            {
                distance = (door.Position.x - player.Position.x) * (angle / 90) + (door.Position.z - player.Position.z) * ((90 - angle) / 90);
            }

            if (Math.Abs(distance) > 3.4f)
            {
                return false;
            }
            return true;
        }

        public static bool CheckDoorBlackList(Door door)
        {
            return blackList.Contains(door.Name);
        }

        public static bool CheckConditions(Door door, ExiledPlayer player)
        {
            if (CheckPrivilegedPlayer(player))
                return true;
            if (CheckDoorBlackList(door))
                return false;
            if (!CheckAllowedPlayer(player))
                return false;
            if (!CheckPlayerPostion(player, door))
                return false;
            return true;
        }
    }
}
