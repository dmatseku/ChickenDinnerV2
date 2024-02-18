using ChickenDinnerV2.Core;
using ChickenDinnerV2.Core.Interfaces;
using InventorySystem.Disarming;
using Exiled.Events.EventArgs.Player;
using System;

namespace ChickenDinnerV2.Modules.DoNotTriggerTesla.Events.Player
{
    internal class PreventTriggeringTesla : IObserver
    {
        protected static Config config = Main.Instance.Config.DoNotTriggerTesla;

        public bool Register()
        {
            if (Main.Instance.Config.DoNotTriggerTesla.IsEnabled)
            {
                Exiled.Events.Handlers.Player.TriggeringTesla += CheckRoles;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.TriggeringTesla -= CheckRoles;
        }

        public void CheckRoles(TriggeringTeslaEventArgs ev)
        {
            string role = ev.Player.Role.Type.ToString();
            if (config.TeslaWhitelist.ContainsKey(role))
            {
                int state = config.TeslaWhitelist[role];
                bool bstate = Convert.ToBoolean(state);
                bool actualState = ev.Player.IsCuffed || DisarmedPlayers.IsDisarmed(ev.Player.Inventory);

                if (state == -1 || bstate == actualState)
                {
                    ev.IsTriggerable = false;
                }
            }
        }
    }
}
