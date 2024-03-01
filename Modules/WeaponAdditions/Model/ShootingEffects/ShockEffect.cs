using ChickenDinnerV2.Core;
using ChickenDinnerV2.Core.Tools;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Player;
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

        private bool isEnoughShots(Player target)
        {
            PlayerEffectsDataObject playerDataObject = PlayerDataBase.Get<PlayerEffectsDataObject>(target);

            if (playerDataObject.ShockCurrentHit < playerDataObject.ShockHitCount)
            {
                playerDataObject.ShockCurrentHit++;
                return false;
            }
            playerDataObject.ShockCurrentHit = 0;
            return true;
        }

        protected override bool ApplyPreEffect(ShotEventArgs ev, List<object> data, out float time)
        {
            Player target = ev.Target;

            ev.Firearm.Ammo = 0;
            ev.Damage = 1;
            time = 0;

            if (!isEnoughShots(target))
            {
                return false;
            }

            CustomReasonDamageHandler damageHandler = new CustomReasonDamageHandler("Sleep");
            if (target == null)
                return false;

            data.Add((object)Ragdoll.CreateAndSpawn(target.Role.Type, target.Nickname, (DamageHandlerBase)damageHandler, target.Position, target.Rotation));

            target.DropItem(target.CurrentItem);
            Timing.RunCoroutine(this.teleport(target));

            target.EnableEffect(EffectType.AmnesiaItems, byte.MaxValue);
            target.EnableEffect(EffectType.AmnesiaVision, byte.MaxValue);
            target.EnableEffect(EffectType.Ensnared, byte.MaxValue);
            target.EnableEffect(EffectType.Invisible, byte.MaxValue);

            time = this.GetEffectTime();
            return true;
        }

        protected override bool ApplyEffect(ShotEventArgs ev, List<object> data, out float time)
        {
            time = 0;
            return false;
        }

        protected override void ApplyPostEffect(ShotEventArgs ev, List<object> data)
        {
            Player target = ev.Target;

            if (target == null)
                return;

            target.DisableEffect(EffectType.AmnesiaItems);
            target.DisableEffect(EffectType.AmnesiaVision);
            target.DisableEffect(EffectType.Ensnared);
            target.DisableEffect(EffectType.Invisible);

            Ragdoll ragdoll = (Ragdoll)data[0];
            target.Position = new Vector3(ragdoll.Position.x, ragdoll.Position.y + 1f, ragdoll.Position.z);
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
