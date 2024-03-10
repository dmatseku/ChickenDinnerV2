using Exiled.Events.EventArgs.Player;
using System.Collections.Generic;

namespace ChickenDinnerV2.Modules.WeaponAdditions.Model.ShootingEffects
{
    internal class ShockEffect : ShootingEffect
    {
        protected static ShockEffect Instance = null;

        private ShockEffect()
        { }

        public static ShockEffect GetEffect()
        {
            if (Instance == null)
                Instance = new ShockEffect();
            return Instance;
        }

        protected override bool ApplyPreEffect(ShootingEventArgs ev, List<object> data, out float time)
        {
            ev.Firearm.Ammo = 0;
            time = 0f;
            return false;
        }
        protected override bool ApplyEffect(ShootingEventArgs ev, List<object> data, out float time)
        {
            time = 0f;
            return false;
        }

        protected override void ApplyPostEffect(ShootingEventArgs ev, List<object> data)
        {
        }

        protected override float GetEffectTime()
        {
            return 0f;
        }
    }
}
