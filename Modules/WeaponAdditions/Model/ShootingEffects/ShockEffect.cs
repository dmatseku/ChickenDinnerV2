using ChickenDinnerV2.Core;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using MEC;
using PlayerStatsSystem;
using System.Collections.Generic;
using UnityEngine;

namespace ChickenDinnerV2.Modules.WeaponAdditions.Model.ShootingEffects
{
    public class ShockEffect : ShootingEffect
    {
        private static Config WeaponAdditionsConfig = Main.Instance.Config.WeaponAdditions;

        protected static ShockEffect Instance = (ShockEffect)null;

        private ShockEffect() => this.players = new List<Player>();

        public static ShockEffect GetEffect()
        {
            if (Instance == null)
                Instance = new ShockEffect();
            return Instance;
        }

        public IEnumerator<float> teleport(Player target)
        {
            yield return Timing.WaitForSeconds(1f);
            target.Teleport(new Vector3(181.5f, 1004f, -28.7f));
        }

        protected override float ApplyPreEffect(
          Firearm weapon,
          Player shooter,
          Player target,
          List<object> data)
        {
            CustomReasonDamageHandler damageHandler = new CustomReasonDamageHandler("Sleep");
            weapon.Ammo = (byte)0;
            if (target == null)
                return 0.0f;

            data.Add((object)Ragdoll.CreateAndSpawn(target.Role.Type, target.Nickname, (DamageHandlerBase)damageHandler, target.Position, target.Rotation));

            target.DropItem(target.CurrentItem);
            Timing.RunCoroutine(this.teleport(target));

            target.EnableEffect(EffectType.AmnesiaItems, byte.MaxValue);
            target.EnableEffect(EffectType.AmnesiaVision, byte.MaxValue);
            target.EnableEffect(EffectType.Ensnared, byte.MaxValue);
            target.EnableEffect(EffectType.Invisible, byte.MaxValue);

            return this.GetEffectTime();
        }

        protected override float ApplyEffect(
          Firearm weapon,
          Player shooter,
          Player target,
          List<object> data)
        {
            return 0.0f;
        }

        protected override void ApplyPostEffect(
          Firearm weapon,
          Player shooter,
          Player target,
          List<object> data)
        {
            if (target == null)
                return;

            target.DisableEffect(EffectType.AmnesiaItems);
            target.DisableEffect(EffectType.AmnesiaVision);
            target.DisableEffect(EffectType.Ensnared);
            target.DisableEffect(EffectType.Invisible);

            Ragdoll ragdoll = (Ragdoll)data[0];
            target.Position = new Vector3(ragdoll.Position.x, ragdoll.Position.y, ragdoll.Position.z);
            ragdoll.Destroy();

            target.EnableEffect(EffectType.Deafened, byte.MaxValue, float.Parse(WeaponAdditionsConfig.EffectsConfig["shock"]["post_effects_time"]));
            target.EnableEffect(EffectType.Concussed, byte.MaxValue, float.Parse(WeaponAdditionsConfig.EffectsConfig["shock"]["post_effects_time"]));
            target.EnableEffect(EffectType.Disabled, byte.MaxValue, float.Parse(WeaponAdditionsConfig.EffectsConfig["shock"]["post_effects_time"]));
        }

        protected override float GetEffectTime()
        {
            return Random.Range(
                float.Parse(WeaponAdditionsConfig.EffectsConfig["shock"]["effect_min_time"]),
                float.Parse(WeaponAdditionsConfig.EffectsConfig["shock"]["effect_max_time"])
            );
        }
    }
}
