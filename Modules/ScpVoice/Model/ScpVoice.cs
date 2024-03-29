using ChickenDinnerV2.Core;
using ChickenDinnerV2.Core.Tools;
using Exiled.API.Features;
using PlayerRoles;
using PlayerRoles.Voice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VoiceChat;
using VoiceChat.Networking;

namespace ChickenDinnerV2.Modules.ScpVoice.Model
{
    internal static class ScpVoice
    {
        private static Config ScpVoiceConfig = Main.Instance.Config.ScpVoice;

        public static bool IsRoleAllowed(RoleTypeId role)
        {
            return ScpVoiceConfig.AllowedRoles.Contains(role);
        }

        private static float getSpeakDistance()
        {
            return ScpVoiceConfig.Distance;
        }

        private static void sendHint(Player player, bool proximityEnabled)
        {
            player.ShowHint(ScpVoiceConfig.Messages[proximityEnabled ? "proximity_enabled" : "proximity_disabled"], 4f);
        }

        public static bool ToggleVoice(Player player)
        {
            if (player.IsNoclipPermitted || !IsRoleAllowed(player.Role.Type))
            {
                return true;
            }

            ScpVoicePlayerData playerData = PlayerDataBase.Get<ScpVoicePlayerData>(player);

            playerData.IsProximity = !playerData.IsProximity;
            sendHint(player, playerData.IsProximity);
            return false;
        }

        private static HashSet<Player> GetListenersList(Player player, VoiceMessage msg)
        {
            IReadOnlyCollection<Player> players = Player.List;
            HashSet<Player> listeners = new HashSet<Player>();


            foreach (Player spectator in player.CurrentSpectatingPlayers)
            {
                listeners.Add(spectator);
            }
            foreach (Player listener in players)
            {
                if (listener == player)
                {
                    continue;
                }

                if (listener.Role.Type != RoleTypeId.Spectator && listener.Role.Base is IVoiceRole voiceRole)
                {
                    if (Vector3.Distance(msg.Speaker.transform.position, listener.Transform.position) <= getSpeakDistance() && voiceRole.VoiceModule.ValidateReceive(msg.Speaker, VoiceChatChannel.Proximity) != VoiceChatChannel.None)
                    {
                        listeners.Add(listener);

                        foreach (Player spectator in listener.CurrentSpectatingPlayers)
                        {
                            listeners.Add(spectator);
                        }
                    }
                }
            }

            return listeners;
        }

        private static void SendProximityMessage(Player player, VoiceMessage msg)
        {
            HashSet<Player> listeners = GetListenersList(player, msg);

            msg.Channel = VoiceChatChannel.Proximity;

            foreach (Player listener in listeners)
            {
                listener.ReferenceHub.connectionToClient.Send(msg);
            }
        }

        public static bool Talk(Player player, VoiceMessage msg)
        {
            ScpVoicePlayerData playerData = PlayerDataBase.Get<ScpVoicePlayerData>(player);

            if (msg.Channel != VoiceChat.VoiceChatChannel.ScpChat || !IsRoleAllowed(player.Role.Type) || !playerData.IsProximity)
            {
                return true;
            }

            SendProximityMessage(player, msg);

            return false;
        }
    }
}
