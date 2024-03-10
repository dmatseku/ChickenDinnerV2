using ChickenDinnerV2.Core;
using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Modules.WeaponAdditions.Model;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.Features;

namespace ChickenDinnerV2.Modules.WeaponAdditions.Events.Player
{
    internal class Shot : IObserver
    {
        protected static Config WeaponAdditionsConfig = Main.Instance.Config.WeaponAdditions;

        public bool Register()
        {
            if (!WeaponAdditionsConfig.IsEnabled)
                return false;
            Exiled.Events.Handlers.Player.Shot += new CustomEventHandler<ShotEventArgs>(this.HandleShot);
            return true;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.Shot -= new CustomEventHandler<ShotEventArgs>(this.HandleShot);
        }

        public void HandleShot(ShotEventArgs e)
        {
            ShootingEffectsManager.runShotEffect(e);
        }
    }
}