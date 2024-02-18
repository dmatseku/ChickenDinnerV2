using Exiled.API.Interfaces;
using WeaponAdditionsConfig = ChickenDinnerV2.Modules.WeaponAdditions.Config;
using AtmosphericStartConfig = ChickenDinnerV2.Modules.AtmosphericStart.Config;
using PlayerSpawnRulesConfig = ChickenDinnerV2.Modules.PlayerSpawnRules.Config;
using DistanceModificationConfig = ChickenDinnerV2.Modules.DistanceModification.Config;
using DoNotTriggerTeslaConfig = ChickenDinnerV2.Modules.DoNotTriggerTesla.Config;
using DoorCrackerConfig = ChickenDinnerV2.Modules.DoorCracker.Config;
using ForbidToOpenLockedDoorsConfig = ChickenDinnerV2.Modules.ForbidToOpenLockedDoors.Config;
using InfiniteRadioConfig = ChickenDinnerV2.Modules.InfiniteRadio.Config;

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

        public DoNotTriggerTeslaConfig DoNotTriggerTesla { get; set; } = new DoNotTriggerTeslaConfig();

        public DoorCrackerConfig DoorCracker { get; set; } = new DoorCrackerConfig();

        public ForbidToOpenLockedDoorsConfig ForbidToOpenLockedDoors { get; set; } = new ForbidToOpenLockedDoorsConfig();
        
        public InfiniteRadioConfig InfiniteRadio {  get; set; } = new InfiniteRadioConfig();
    }
}
