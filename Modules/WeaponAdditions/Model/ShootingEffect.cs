using Exiled.API.Features;
using Exiled.API.Features.Items;
using MEC;
using System.Collections.Generic;

namespace ChickenDinnerV2.Modules.WeaponAdditions.Model
{
    public abstract class ShootingEffect
    {
        protected List<Player> players;

        protected abstract bool ApplyPreEffect(Firearm weapon, Player shooter, Player target, List<object> data, out float time);

        protected abstract bool ApplyEffect(Firearm weapon, Player shooter, Player target, List<object> data, out float time);

        protected abstract void ApplyPostEffect(Firearm weapon, Player shooter, Player target, List<object> data);

        protected abstract float GetEffectTime();

        public void InitEffect(Firearm weapon, Player shooter, Player target)
        {
            lock (this.players)
            {
                if (this.players.Contains(target))
                {
                    return;
                }
                this.players.Add(target);
                Timing.RunCoroutine(this.ExecuteEffect(weapon, shooter, target));
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

        public IEnumerator<float> ExecuteEffect(Firearm weapon, Player shooter, Player target)
        {
            List<object> data = new List<object>();
            float waitTime;
            bool needToExecuteEffect = this.ApplyPreEffect(weapon, shooter, target, data, out waitTime);
            bool needToExecuteAgain = needToExecuteEffect;

            if (!needToExecuteEffect)
            {
                yield return 0;
            }

            while (needToExecuteAgain)
            {
                yield return Timing.WaitForSeconds(waitTime);
                needToExecuteAgain = this.ApplyEffect(weapon, shooter, target, data, out waitTime);
            }

            if (needToExecuteEffect)
            {
                this.ApplyPostEffect(weapon, shooter, target, data);
            }
            this.DestroyEffect(target);
        }
    }
}