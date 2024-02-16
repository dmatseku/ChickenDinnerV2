using ChickenDinnerV2.Core;
using ChickenDinnerV2.Modules.WeaponAdditions.Model.ShootingEffects;
using Exiled.API.Features;
using Exiled.API.Features.Items;
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

        public static void runEffect(Firearm weapon, Player shooter, Player target)
        {
            string key;
            ShootingEffect shootingEffect;

            if (!ShootingEffectsManager.WeaponAdditionsConfig.WeaponEffects.TryGetValue(weapon.Type.ToString(), out key) || !ShootingEffectsManager.shootingEffects.TryGetValue(key, out shootingEffect))
                return;

            shootingEffect.InitEffect(weapon, shooter, target);
        }
    }
}
