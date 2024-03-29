using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.Events.EventArgs.Player;
using ScpVoiceModel = ChickenDinnerV2.Modules.ScpVoice.Model.ScpVoice;

namespace ChickenDinnerV2.Modules.ScpVoice.Event
{
    internal class TogglingNoClip : IObserver
    {
        protected static Config config = Main.Instance.Config.ScpVoice;

        public bool Register()
        {
            if (config.IsEnabled)
            {
                Exiled.Events.Handlers.Player.TogglingNoClip += ToggleNoClip;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.TogglingNoClip -= ToggleNoClip;
        }

        public void ToggleNoClip(TogglingNoClipEventArgs ev)
        {
            ev.IsAllowed = ScpVoiceModel.ToggleVoice(ev.Player) ? ev.IsAllowed : false;
        }
    }
}
