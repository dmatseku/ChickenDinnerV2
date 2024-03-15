using ChickenDinnerV2.Core.Tools;
using ChickenDinnerV2.Modules.ScpVoice.Model;
using HarmonyLib;
using PlayerRoles.PlayableScps;
using PlayerRoles.Voice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VoiceChat.Playbacks;

namespace ChickenDinnerV2.Modules.ScpVoice.Plugins.Player
{
    [HarmonyPatch(typeof(SingleBufferPlayback))]
    internal class SingleBufferPlaybackPlugin
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
    }
}
