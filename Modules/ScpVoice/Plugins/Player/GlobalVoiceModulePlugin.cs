using ChickenDinnerV2.Core.Tools;
using ChickenDinnerV2.Modules.ScpVoice.Model;
using Exiled.API.Features;
using HarmonyLib;
using PlayerRoles.PlayableScps;
using PlayerRoles.Voice;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.Remoting.Metadata;
using VoiceChat;
using Interactables.Interobjects.DoorUtils;
using NorthwoodLib.Pools;
using PlayerRoles;

namespace ChickenDinnerV2.Modules.ScpVoice.Plugins.Player
{
    [HarmonyPatch(typeof(GlobalVoiceModuleBase))]
    internal class GlobalVoiceModuleBasePlugin
    {
        protected static Config ScpVoiceConfig = ChickenDinnerV2.Core.Main.Instance.Config.ScpVoice;

        protected static ScpVoicePlayerData GetPlayerData(GlobalVoiceModuleBase __instance, bool isValidateSendPrefix = false)
        {
            ReferenceHub owner = (ReferenceHub)typeof(VoiceModuleBase).GetField("_owner", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);

            if (owner == null || __instance.GetType() != typeof(StandardScpVoiceModule))
            {
                return null;
            }

            Exiled.API.Features.Player player = Exiled.API.Features.Player.Get(owner);

            if (player == null)
            {
                return null;
            }

            return PlayerDataBase.Get<ScpVoicePlayerData>(player);
        }

        [HarmonyPrefix]
        [HarmonyPatch("ProcessInputs")]
        public static bool ProcessInputsPrefix(GlobalVoiceModuleBase __instance, ref VoiceChatChannel __result, bool primary, bool alt)
        {
            ScpVoicePlayerData data = GetPlayerData(__instance);
            if (data == null)
            {
                return true;
            }

            if (primary && data.IsProximity)
            {
                __result = VoiceChatChannel.Proximity;

                return false;
            }

            return true;
        }

        [HarmonyPrefix]
        [HarmonyPatch("ValidateSend")]
        public static bool ValidateSendPrefix(GlobalVoiceModuleBase __instance, ref VoiceChatChannel __result, VoiceChatChannel channel)
        {
            ScpVoicePlayerData playerData = GetPlayerData(__instance, true);
            if (playerData == null)
            {
                return true;
            }

            if (playerData.IsProximity)
            {
                __result = VoiceChatChannel.Proximity;

                return false;
            }

            return true;
        }

        /*[HarmonyPrefix]
        [HarmonyPatch("ValidateReceive")]
        public static bool ValidateReceivePrefix(GlobalVoiceModuleBase __instance, ref VoiceChatChannel __result, ReferenceHub speaker, VoiceChatChannel channel)
        {
            ScpVoicePlayerData playerData = GetPlayerData(__instance, true);
            if (playerData == null)
            {
                return true;
            }

            if (playerData.IsProximity)
            {
                __result = VoiceChatChannel.Proximity;

                return false;
            }

            return true;
        }*/

        [HarmonyPrefix]
        [HarmonyPatch("IsSpeaking", MethodType.Getter)]
        public static bool IsSpeakingPrefix(GlobalVoiceModuleBase __instance, ref bool __result)
        {
            Log.Warn("hello5");
            ScpVoicePlayerData playerData = GetPlayerData(__instance);
            if (playerData == null)
            {
                return true;
            }

            Log.Warn("hello6");
            if (playerData.IsProximity)
            {
                Log.Warn("hello7");
                __result = __instance.IsSpeaking || playerData._proximityPlayback.MaxSamples > 0;

                return false;
            }

            return true;
        }
    }
}
