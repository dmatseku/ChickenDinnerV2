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
    internal class ChangingRole : IObserver
    {
        protected static Config config = Main.Instance.Config.ScpVoice;

        public bool Register()
        {
            if (config.IsEnabled)
            {
                Exiled.Events.Handlers.Player.Spawned += RemindAboutRole;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.Spawned -= RemindAboutRole;
        }

        public void RemindAboutRole(SpawnedEventArgs ev)
        {
            if (config.SendBroadcastOnRoleChange && ScpVoiceModel.IsRoleAllowed(ev.Player.Role.Type))
            {
                ev.Player.Broadcast((ushort)config.BroadcastDuration, config.Messages["on_role_changed"]);
            }
        }
    }
}
