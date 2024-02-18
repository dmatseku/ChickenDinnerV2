using Exiled.API.Interfaces;
using WeaponAdditionsConfig = ChickenDinnerV2.Modules.WeaponAdditions.Config;
using AtmosphericStartConfig = ChickenDinnerV2.Modules.AtmosphericStart.Config;
using PlayerSpawnRulesConfig = ChickenDinnerV2.Modules.PlayerSpawnRules.Config;
using DistanceModificationConfig = ChickenDinnerV2.Modules.DistanceModification.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public AtmosphericStartConfig AtmosphericStart { get; set; } = new AtmosphericStartConfig();

        public PlayerSpawnRulesConfig PlayerSpawnRules {  get; set; } = new PlayerSpawnRulesConfig();

        public WeaponAdditionsConfig WeaponAdditions { get; set; } = new WeaponAdditionsConfig();

        public DistanceModificationConfig DistanceModification { get; set; } = new DistanceModificationConfig();
    }
}
