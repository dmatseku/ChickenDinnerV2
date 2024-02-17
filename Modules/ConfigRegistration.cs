using Exiled.API.Interfaces;
using DistanceModificationConfig = ChickenDinnerV2.Modules.DistanceModification.Config;
using AtmosphericStartConfig = ChickenDinnerV2.Modules.AtmosphericStart.Config;
using InfiniteRadioConfig = ChickenDinnerV2.Modules.InfiniteRadio.Config;
using DoNotTriggerTeslaConfig = ChickenDinnerV2.Modules.DoNotTriggerTesla.Config;
using ObserversCounterConfig = ChickenDinnerV2.Modules.ObserversCounter.Config;
using DoorCrackerConfig = ChickenDinnerV2.Modules.DoorCracker.Config;
using ForbidToOpenLockedDoorsConfig = ChickenDinnerV2.Modules.ForbidToOpenLockedDoors.Config;
using WeaponAdditionsConfig = ChickenDinnerV2.Modules.WeaponAdditions.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public DistanceModificationConfig DistanceModification { get; set; } = new DistanceModificationConfig();
        
        public AtmosphericStartConfig AtmosphericStart { get; set; } = new AtmosphericStartConfig();
        
        public InfiniteRadioConfig InfiniteRadio {  get; set; } = new InfiniteRadioConfig();

        public DoNotTriggerTeslaConfig DoNotTriggerTesla { get; set; } = new DoNotTriggerTeslaConfig();

        public ObserversCounterConfig ObserversCounter { get; set; } = new ObserversCounterConfig();

        public DoorCrackerConfig DoorCracker { get; set; } = new DoorCrackerConfig();

        public ForbidToOpenLockedDoorsConfig ForbidToOpenLockedDoors { get; set; } = new ForbidToOpenLockedDoorsConfig();

        public WeaponAdditionsConfig WeaponAdditions { get; set; } = new WeaponAdditionsConfig();
    }
}
