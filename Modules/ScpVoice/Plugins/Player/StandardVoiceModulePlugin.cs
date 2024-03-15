using ChickenDinnerV2.Core.Tools;
using ChickenDinnerV2.Modules.ScpVoice.Model;
using Exiled.API.Features;
using HarmonyLib;
using PlayerRoles.PlayableScps;
using PlayerRoles.Voice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VoiceChat;

namespace ChickenDinnerV2.Modules.ScpVoice.Plugins.Player
{
    [HarmonyPatch(typeof(StandardVoiceModule))]
    internal class StandardVoiceModulePlugin
    {
        protected static Config ScpVoiceConfig = ChickenDinnerV2.Core.Main.Instance.Config.ScpVoice;

        protected static ScpVoicePlayerData GetPlayerData(StandardVoiceModule __instance, bool isValidateSendPrefix = false)
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
        [HarmonyPatch("ProcessSamples")]
        public static bool ProcessSamplesPrefix(StandardVoiceModule __instance, float[] data, int len)
        {
            Log.Warn("hello1");
            ScpVoicePlayerData playerData = GetPlayerData(__instance);
            if (playerData == null)
            {
                Log.Warn("hello2");
                return true;
            }

            Log.Warn("hello3");
            if (__instance.CurrentChannel == VoiceChatChannel.Proximity || playerData.IsProximity)
            {
                Log.Warn("hello4");
                typeof(VoiceModuleBase).GetMethod("ProcessSamples", BindingFlags.Instance | BindingFlags.NonPublic, Type.DefaultBinder, new[] { typeof(float[]), typeof(int) }, null).Invoke(__instance, new object[] { data, len });
                playerData._proximityPlayback.Buffer.Write(data, len);

                return false;
            }

            return true;
        }
    }
}
