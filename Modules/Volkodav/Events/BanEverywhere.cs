using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Modules.Volkodav.Model;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenDinnerV2.Modules.Volkodav.Events
{
    internal class BanEverywhere : IObserver
    {
        //protected static Config VolkodavConfig = ChickenDinnerV2.Core.Main.Instance.Config.Volkodav;
        public bool Register()
        {
            if (false)//VolkodavConfig.IsEnabled)
            {
                Exiled.Events.Handlers.Player.Joined += OnGameJoined;
                Exiled.Events.Handlers.Player.ChangingRole += OnRoleChanged;
                Exiled.Events.Handlers.Server.RoundStarted += OnRoundStarted;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.Joined -= OnGameJoined;
            Exiled.Events.Handlers.Player.ChangingRole -= OnRoleChanged;
            Exiled.Events.Handlers.Server.RoundStarted -= OnRoundStarted;
        }

        public void OnGameJoined(JoinedEventArgs ev)
        {
            //BanManager.CheckPlayer(ev.Player, wait: true);
        }

        public void OnRoleChanged(ChangingRoleEventArgs ev)
        {
            if (!BanManager.CheckPlayer(ev.Player, false))
            {
                ev.NewRole = PlayerRoles.RoleTypeId.Scp0492;

                if (ev.Player.Role.Type == PlayerRoles.RoleTypeId.Scp0492)
                {
                    ev.IsAllowed = false;
                }
                Log.Warn("hello");
            }
        }

        public void OnRoundStarted()
        {
            //BanManager.CheckAllPlayers();
        }
    }
}
