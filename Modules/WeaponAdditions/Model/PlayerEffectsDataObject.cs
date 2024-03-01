using System;
using ChickenDinnerV2.Core;
using ChickenDinnerV2.Core.Interfaces;
using Exiled.API.Features;
using PlayerRoles;

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
                ShockHitCount = Convert.ToInt32(Main.Instance.Config.WeaponAdditions.EffectsConfig["shock"][roleShockHitConfig]) - 1;
            }
            else
            {
                ShockHitCount = 0;
            }
        }
        public void Created()
        {
            UpdateShockHitCount(Owner.Role.Type.ToString() + "_hit_count");
        }

        public void RoleChanged(RoleTypeId newRole)
        {
            ShockCurrentHit = 0;
            UpdateShockHitCount(newRole.ToString() + "_hit_count");
        }
    }
}
