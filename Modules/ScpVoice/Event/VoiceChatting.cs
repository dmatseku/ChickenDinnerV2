using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Core;
using Exiled.Events.EventArgs.Player;
using InventorySystem.Disarming;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScpVoiceModel = ChickenDinnerV2.Modules.ScpVoice.Model.ScpVoice;

namespace ChickenDinnerV2.Modules.ScpVoice.Event
{
    internal class VoiceChatting : IObserver
    {
        protected static Config config = Main.Instance.Config.ScpVoice;

        public bool Register()
        {
            if (config.IsEnabled)
            {
                Exiled.Events.Handlers.Player.VoiceChatting += VoiceChattingHandler;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.VoiceChatting -= VoiceChattingHandler;
        }

        public void VoiceChattingHandler(VoiceChattingEventArgs ev)
        {
            ev.IsAllowed = ScpVoiceModel.Talk(ev.Player, ev.VoiceMessage) ? ev.IsAllowed : false;
        }
    }
}
