using ChickenDinnerV2.Core;
using ChickenDinnerV2.Modules.WeaponAdditions.Model.ShotEffects;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;

namespace ChickenDinnerV2.Modules.WeaponAdditions.Model
{
    internal static class ShootingEffectsManager
    {
        private static Config WeaponAdditionsConfig = Main.Instance.Config.WeaponAdditions;

        private static Dictionary<string, ShotEffect> shotEffects = new Dictionary<string, ShotEffect>()
        {
            { "shock", ChickenDinnerV2.Modules.WeaponAdditions.Model.ShotEffects.ShockEffect.GetEffect() }
        };

        private static Dictionary<string, ShootingEffect> shootingEffects = new Dictionary<string, ShootingEffect>()
        {
            { "shock", ChickenDinnerV2.Modules.WeaponAdditions.Model.ShootingEffects.ShockEffect.GetEffect() }
        };

        public static void runShotEffect(ShotEventArgs ev)
        {
            string key;
            ShotEffect shotEffect;

            if (!WeaponAdditionsConfig.WeaponEffects.TryGetValue(ev.Firearm.Type.ToString(), out key) || !shotEffects.TryGetValue(key, out shotEffect))
                return;

            shotEffect.InitEffect(ev);
        }

        public static void runShootingEffect(ShootingEventArgs ev)
        {
            string key;
            ShootingEffect shootingEffect;

            if (!WeaponAdditionsConfig.WeaponEffects.TryGetValue(ev.Firearm.Type.ToString(), out key) || !shootingEffects.TryGetValue(key, out shootingEffect))
                return;

            shootingEffect.InitEffect(ev);
        }
    }
}
