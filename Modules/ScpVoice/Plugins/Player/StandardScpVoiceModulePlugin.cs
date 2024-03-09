using ChickenDinnerV2.Core.Tools;
using ChickenDinnerV2.Modules.ScpVoice.Model;
using Exiled.API.Features;
using HarmonyLib;
using PlayerRoles.PlayableScps;
using PlayerRoles.Voice;
using System.Reflection;
using System.Runtime.Remoting.Metadata;
using VoiceChat;

namespace ChickenDinnerV2.Modules.ScpVoice.Plugins.Player
{
    [HarmonyPatch(typeof(GlobalVoiceModuleBase))]
    internal class GlobalVoiceModuleBasePlugin
    {
        protected static Config ScpVoiceConfig = ChickenDinnerV2.Core.Main.Instance.Config.ScpVoice;


        protected static ScpVoicePlayerData GetPlayerData(GlobalVoiceModuleBase __instance)
        {
            ReferenceHub owner = (ReferenceHub)typeof(GlobalVoiceModuleBase).GetField("_owner", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);

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

            if (primary)
            {
                Log.Warn("returned ScpChat");
                __result = VoiceChatChannel.ScpChat;
            }
            else if (alt && data.IsProximity)
            {
                Log.Warn("returned proximity");
                __result = VoiceChatChannel.Proximity;
            }
            else
            {
                Log.Warn("returned None");
                __result = VoiceChatChannel.None;
            }

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("ProcessSamples")]
        public static bool ProcessSamplesPrefix(GlobalVoiceModuleBase __instance, float[] data, int len)
        {
            ScpVoicePlayerData playerData = GetPlayerData(__instance);
            if (playerData == null)
            {
                return true;
            }

            if (__instance.CurrentChannel == VoiceChatChannel.Proximity)
            {
                playerData._proximityPlayback.Buffer.Write(data, len);
            }
            else
            {
                Log.Warn("ProcessSamples not Proximity");
                return true;
            }
            Log.Warn("ProcessSamples Proximity");

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("ValidateSend")]
        public static bool ValidateSendPrefix(GlobalVoiceModuleBase __instance, ref VoiceChatChannel __result, VoiceChatChannel channel)
        {
            ScpVoicePlayerData playerData = GetPlayerData(__instance);
            if (playerData == null)
            {
                return true;
            }

            __result = (channel != VoiceChatChannel.Proximity || !playerData.IsProximity ? (channel == VoiceChatChannel.ScpChat ? 1 : 0) : 1) == 0 ? VoiceChatChannel.None : channel;

            return false;
        }

        [HarmonyPrefix]
        [HarmonyPatch("IsSpeaking", MethodType.Getter)]
        public static bool IsSpeakingPrefix(GlobalVoiceModuleBase __instance, ref bool __result)
        {
            ScpVoicePlayerData playerData = GetPlayerData(__instance);
            if (playerData == null)
            {
                return true;
            }

            __result = __instance.IsSpeaking || playerData._proximityPlayback.MaxSamples > 0;

            return false;
        }
    }
}
