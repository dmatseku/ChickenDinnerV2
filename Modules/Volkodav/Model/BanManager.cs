using CustomPlayerEffects;
using Exiled.API.Features;
using MEC;
using System.Collections.Generic;
using System.Linq;

namespace ChickenDinnerV2.Modules.Volkodav.Model
{
    internal static class BanManager
    {
        private static List<string> BanIps = new List<string>();

        public static bool Ban(string localId)
        {
            Player player = GetPlayer(localId);

            if (player == null)
            {
                return false;
            }

            if (!BanIps.Contains(player.IPAddress))
            {
                BanIps.Add(player.IPAddress);
            }

            CheckPlayer(player);
            return true;
        }
        public static bool Unban(string localId)
        {
            Player player = GetPlayer(localId);

            if (player == null)
            {
                return false;
            }

            BanIps.Remove(player.IPAddress);
            Log.Warn(BanIps.Count.ToString());
            player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.Spectator, PlayerRoles.RoleChangeReason.RemoteAdmin);
            return true;
        }

        private static Player GetPlayer(string localId)
        {
            int id;

            if (int.TryParse(localId, out id))
            {
                return Player.Get(id);
            }
            return null;
        }

        public static bool CheckPlayer(Player player, bool changeRole = true, bool wait = false)
        {
            if (GameCore.RoundStart.RoundStarted && player.ReferenceHub != null && BanIps.Contains(player.IPAddress))
            {
                Timing.RunCoroutine(checkCoroutine(player, changeRole, (wait ? 5 : 0)));
                return false;
            }

            return true;
        }

        private static IEnumerator<float> checkCoroutine(Player player, bool changeRole, float time)
        {
            Log.Warn("hello2");
            yield return Timing.WaitForSeconds(time);

            if (player.Role.Type != PlayerRoles.RoleTypeId.Scp0492 && changeRole)
            {
                player.RoleManager.ServerSetRole(PlayerRoles.RoleTypeId.Scp0492, PlayerRoles.RoleChangeReason.RemoteAdmin);
            }

            foreach (StatusEffectBase allEffect in player.ReferenceHub.playerEffectsController.AllEffects)
                allEffect.Intensity = (byte)0;

            Room shelter = Room.Get(Exiled.API.Enums.RoomType.EzShelter);

            if (shelter != null)
            {
                UnityEngine.Vector3 position = shelter.Position;

                position.Set(position.x, position.y + 2, position.z);
                player.Position = position;
            }
        }

        public static void CheckAllPlayers()
        {
            foreach(Player player in Player.List)
            {
                CheckPlayer(player);
            }
        }
    }
}
