using Exiled.API.Interfaces;
using DistanceModificationConfig = ChickenDinnerV2.Modules.DistanceModification.Config;
using AtmosphericStartConfig = ChickenDinnerV2.Modules.AtmosphericStart.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public DistanceModificationConfig DistanceModification { get; set; } = new DistanceModificationConfig();
        
        public AtmosphericStartConfig AtmosphericStart { get; set; } = new AtmosphericStartConfig();
    }
}
