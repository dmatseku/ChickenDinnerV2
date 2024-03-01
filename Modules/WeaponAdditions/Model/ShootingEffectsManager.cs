using ChickenDinnerV2.Core;
using ChickenDinnerV2.Modules.WeaponAdditions.Model.ShootingEffects;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;

namespace ChickenDinnerV2.Modules.WeaponAdditions.Model
{
    internal static class ShootingEffectsManager
    {
        private static Config WeaponAdditionsConfig = Main.Instance.Config.WeaponAdditions;

        private static Dictionary<string, ShootingEffect> shootingEffects = new Dictionary<string, ShootingEffect>()
        {
            { "shock", ShockEffect.GetEffect() }
        };

        public static void runEffect(ShotEventArgs ev)
        {
            string key;
            ShootingEffect shootingEffect;

            if (!WeaponAdditionsConfig.WeaponEffects.TryGetValue(ev.Firearm.Type.ToString(), out key) || !shootingEffects.TryGetValue(key, out shootingEffect))
                return;

            shootingEffect.InitEffect(ev);
        }
    }
}
