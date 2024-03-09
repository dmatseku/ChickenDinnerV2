using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Core;
using ChickenDinnerV2.Modules.WeaponAdditions.Model;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenDinnerV2.Modules.WeaponAdditions.Events.Player
{
    internal class Shooting : IObserver
    {
        protected static Config WeaponAdditionsConfig = Main.Instance.Config.WeaponAdditions;

        public bool Register()
        {
            if (!WeaponAdditionsConfig.IsEnabled)
                return false;
            Exiled.Events.Handlers.Player.Shooting += new CustomEventHandler<ShootingEventArgs>(this.HandleShooting);
            return true;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.Shooting -= new CustomEventHandler<ShootingEventArgs>(this.HandleShooting);
        }

        public void HandleShooting(ShootingEventArgs e)
        {
            ShootingEffectsManager.runShootingEffect(e);
        }
    }
}
