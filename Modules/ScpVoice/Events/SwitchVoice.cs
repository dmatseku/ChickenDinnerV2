using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Core;
using Exiled.Events.EventArgs.Player;
using PlayerRoles.PlayableScps;
using PlayerRoles.FirstPersonControl;
using System.Reflection;
using ChickenDinnerV2.Modules.ScpVoice.Model;
using Exiled.API.Features;
using ChickenDinnerV2.Core.Tools;

namespace ChickenDinnerV2.Modules.ScpVoice.Events
{
    internal class SwitchVoice : IObserver
    {
        protected static Config ScpVoiceConfig = Main.Instance.Config.ScpVoice;

        public bool Register()
        {
            if (ScpVoiceConfig.IsEnabled)
            {
                Exiled.Events.Handlers.Player.TogglingNoClip += changeVoice;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.TogglingNoClip -= changeVoice;
        }

        public void changeVoice(TogglingNoClipEventArgs ev)
        {
            if (!ev.IsAllowed)
            {
                ScpVoicePlayerData player = PlayerDataBase.Get<ScpVoicePlayerData>(ev.Player);

                player.IsProximity = !player.IsProximity;
                Log.Warn("Switched");
            }
        }
    }
}
