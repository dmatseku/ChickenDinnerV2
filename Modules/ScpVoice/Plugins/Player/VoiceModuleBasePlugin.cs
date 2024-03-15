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
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using VoiceChat;
using VoiceChat.Networking;
using NorthwoodLib.Pools;

namespace ChickenDinnerV2.Modules.ScpVoice.Plugins.Player
{
    [HarmonyPatch(typeof(VoiceModuleBase))]
    internal class VoiceModuleBasePlugin
    {
        protected static Config ScpVoiceConfig = ChickenDinnerV2.Core.Main.Instance.Config.ScpVoice;

        protected static ScpVoicePlayerData GetPlayerData(VoiceModuleBase __instance, bool isValidateSendPrefix = false)
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

        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        public static void UpdatePrefix(VoiceModuleBase __instance)
        {
            ScpVoicePlayerData data = GetPlayerData(__instance);

            if (data != null)
            {
                data._proximityPlayback.Source.volume = 1.0f;
            }
        }

        public static bool ProcessSamples(GlobalVoiceModuleBase __instance, float[] data, int len)
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
