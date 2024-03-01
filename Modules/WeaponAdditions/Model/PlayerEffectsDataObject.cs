using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChickenDinnerV2.Core;
using ChickenDinnerV2.Core.Interfaces;
using Exiled.API.Features;

namespace ChickenDinnerV2.Modules.WeaponAdditions.Model
{
    internal class PlayerEffectsDataObject : IPlayerData
    {
        public Player Owner { get; set; }

        public int ShockCurrentHit { get; set; } = 0;

        public int ShockHitCount { get; private set; } = 1;


        private void UpdateShockHitCount(string roleShockHitConfig)
        {
            if (Main.Instance.Config.WeaponAdditions.EffectsConfig["shock"].ContainsKey(roleShockHitConfig))
            {
                ShockHitCount = Convert.ToInt32(Main.Instance.Config.WeaponAdditions.EffectsConfig["shock"][roleShockHitConfig]);
            }
            else
            {
                ShockHitCount = 1;
            }
        }
        public void Created()
        {
            UpdateShockHitCount(Owner.Role.Type.ToString() + "_hit_count");
        }

        public void RoleChanged()
        {
            ShockCurrentHit = 0;
            UpdateShockHitCount(Owner.Role.Type.ToString() + "_hit_count");
        }
    }
}
