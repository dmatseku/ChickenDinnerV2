using Exiled.API.Features.Doors;
using System;
using System.Collections.Generic;
using MEC;
using PlayerRoles;
using Exiled.API.Enums;
using System.Linq;

namespace ChickenDinnerV2.Modules.DoorCracker.Model.Player
{
    internal class CrackingDoor
    {
        protected static Config DoorCrackerConfig = ChickenDinnerV2.Core.Main.Instance.Config.DoorCracker;

        protected static readonly Dictionary<DoorType, RoomType> doorExceptions = new Dictionary<DoorType, RoomType>
        {
            { DoorType.CheckpointEzHczA, RoomType.HczEzCheckpointA },
            { DoorType.CheckpointEzHczB, RoomType.HczEzCheckpointB },
            { DoorType.CheckpointLczA, RoomType.LczCheckpointA },
            { DoorType.CheckpointLczB, RoomType.LczCheckpointB }
        };
        protected static readonly List<RoleTypeId> privilegedRoles = new List<RoleTypeId>
        {
            RoleTypeId.Tutorial,
            RoleTypeId.Overwatch
        };
        protected static readonly Dictionary<RoleTypeId, ItemType> allowedRolesAndCards = new Dictionary<RoleTypeId, ItemType>
        {
            { RoleTypeId.NtfCaptain, ItemType.KeycardContainmentEngineer },
            { RoleTypeId.NtfSpecialist, ItemType.KeycardContainmentEngineer },
            { RoleTypeId.NtfPrivate, ItemType.KeycardContainmentEngineer },
            { RoleTypeId.ChaosConscript, ItemType.KeycardChaosInsurgency },
            { RoleTypeId.ChaosMarauder, ItemType.KeycardChaosInsurgency },
            { RoleTypeId.ChaosRepressor, ItemType.KeycardChaosInsurgency },
            { RoleTypeId.ChaosRifleman, ItemType.KeycardChaosInsurgency }
        };

        protected Door door;
        protected Exiled.API.Features.Player player;
        protected bool isCracking;

        public CrackingDoor(Door door, Exiled.API.Features.Player player)
        {
            this.door = door;
            this.player = player;
            this.isCracking = true;
        }

        public IEnumerator<float> CrackCoroutine()
        {
            if (CheckPrivilegedPlayer(player))
            {
                yield return Timing.WaitForSeconds(0);

                if (player.CurrentItem.Type == ItemType.KeycardChaosInsurgency || player.CurrentItem.Type == ItemType.KeycardContainmentEngineer)
                {
                    InfluenceDoor();
                    isCracking = false;
                }
            }
            else if (isCracking == true && CheckAllowedPlayer() && CheckPlayerPostion())
            {
                alert("Cracking has been started");
                yield return Timing.WaitForSeconds(GetTimeToCrack());

                if (CheckAllowedPlayer() && CheckPlayerPostion())
                {
                    InfluenceDoor();
                    isCracking = false;
                }
            }
            else
            {
                isCracking = false;
                yield return Timing.WaitForSeconds(0);
            }
        }

        //TODO: rewrite to better solution maybe
        protected int GetTimeToCrack()
        {
            if (CheckPrivilegedPlayer(player))
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
                alert("Door has been unlocked");
                door.Unlock();
                door.IsOpen = true;
            }
            else
            {
                alert("Door has been locked");
                door.ChangeLock(DoorLockType.Regular079);
                door.IsOpen = false;
            }
        }

        public bool IsCracking()
        {
            if (isCracking)
            {
                if (!CheckAllowedPlayer() || !CheckPlayerPostion())
                {
                    alert("Cracking has been canceled");
                    isCracking = false;
                }
            }

            return isCracking;
        }

        protected bool CheckPlayerPostion()
        {
            if (CheckPrivilegedPlayer(player))
            {
                return true;
            }
            if (doorExceptions.ContainsKey(door.Type))
            {
                if (doorExceptions[door.Type] != player.CurrentRoom.Type)
                {
                    return false;
                }
                return true;
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

        public static bool CheckPrivilegedPlayer(Exiled.API.Features.Player player)
        {
            return 
                privilegedRoles.Contains(player.Role.Type) && player.CurrentItem != null 
                && (
                    player.CurrentItem.Type == ItemType.KeycardChaosInsurgency || 
                    player.CurrentItem.Type == ItemType.KeycardContainmentEngineer
                );
        }

        protected bool CheckAllowedPlayer()
        {
            return allowedRolesAndCards.ContainsKey(player.Role.Type) && allowedRolesAndCards[player.Role.Type] == player.CurrentItem.Type;
        }

        protected void alert(string message, float duration = 3.0f)
        {
            player.ShowHint("<color=yellow>" + message + "</color>", duration);
        }
    }
}
