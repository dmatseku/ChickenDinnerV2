using HarmonyLib;
using InventorySystem.Items.Radio;
using PlayerRoles.PlayableScps;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceChat;

namespace ChickenDinnerV2.Modules.ScpVoice.Plugins.Player
{
    [HarmonyPatch(typeof(StandardScpVoiceModule))]
    internal class StandardScpVoiceModulePlugin
    {
        protected static Config ScpVoiceConfig = ChickenDinnerV2.Core.Main.Instance.Config.ScpVoice;


        [HarmonyPrefix]
        [HarmonyPatch("ProcessInputs")]
        public static bool ProcessInputsPrefix(StandardScpVoiceModule __instance, ref VoiceChatChannel __result, bool primary, bool alt)
        {
            if (primary)
            {
                __result = VoiceChatChannel.ScpChat;
            }
            else if (alt && __instance.IsProximity)
            {
                __result = VoiceChatChannel.Proximity;
            }
            else
            {
                __result = VoiceChatChannel.None;
            }

            return false;
        }
    }
}
