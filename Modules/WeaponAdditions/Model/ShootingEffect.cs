using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using System.Collections.Generic;

namespace ChickenDinnerV2.Modules.WeaponAdditions.Model
{
    internal abstract class ShootingEffect
    {
        protected abstract bool ApplyPreEffect(ShootingEventArgs ev, List<object> data, out float time);

        protected abstract bool ApplyEffect(ShootingEventArgs ev, List<object> data, out float time);

        protected abstract void ApplyPostEffect(ShootingEventArgs ev, List<object> data);

        protected abstract float GetEffectTime();

        public void InitEffect(ShootingEventArgs ev)
        {
            Timing.RunCoroutine(this.ExecuteEffect(ev));
        }

        public IEnumerator<float> ExecuteEffect(ShootingEventArgs ev)
        {
            List<object> data = new List<object>();
            float waitTime;
            bool needToExecuteEffect = this.ApplyPreEffect(ev, data, out waitTime);
            bool needToExecuteAgain = needToExecuteEffect;

            if (!needToExecuteEffect)
            {
                yield return 0;
            }

            while (needToExecuteAgain)
            {
                yield return Timing.WaitForSeconds(waitTime);
                needToExecuteAgain = this.ApplyEffect(ev, data, out waitTime);
            }

            if (needToExecuteEffect)
            {
                this.ApplyPostEffect(ev, data);
            }
        }
    }
}
