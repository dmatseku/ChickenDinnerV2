using Exiled.API.Interfaces;
using DistanceModificationConfig = ChickenDinnerV2.Modules.DistanceModification.Config;
using AtmosphericStartConfig = ChickenDinnerV2.Modules.AtmosphericStart.Config;
using InfiniteRadioConfig = ChickenDinnerV2.Modules.InfiniteRadio.Config;
using DoNotTriggerTeslaConfig = ChickenDinnerV2.Modules.DoNotTriggerTesla.Config;

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
    }
}
