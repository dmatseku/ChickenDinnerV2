using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using System.Collections.Generic;

namespace ChickenDinnerV2.Modules.WeaponAdditions.Model
{
    public abstract class ShootingEffect
    {
        protected List<Player> players;

        protected abstract bool ApplyPreEffect(ShotEventArgs ev, List<object> data, out float time);

        protected abstract bool ApplyEffect(ShotEventArgs ev, List<object> data, out float time);

        protected abstract void ApplyPostEffect(ShotEventArgs ev, List<object> data);

        protected abstract float GetEffectTime();

        public void InitEffect(ShotEventArgs ev)
        {
            lock (this.players)
            {
                if (this.players.Contains(ev.Target))
                {
                    return;
                }
                this.players.Add(ev.Target);
                Timing.RunCoroutine(this.ExecuteEffect(ev));
            }
        }

        protected void DestroyEffect(Player player)
        {
            lock (this.players)
            {
                if (!this.players.Contains(player))
                {
                    return;
                }
                this.players.Remove(player);
            }
        }

        public IEnumerator<float> ExecuteEffect(ShotEventArgs ev)
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
            this.DestroyEffect(ev.Target);
        }
    }
}