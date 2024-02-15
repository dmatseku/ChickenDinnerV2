using Exiled.API.Interfaces;
using DistanceModificationConfig = ChickenDinnerV2.Modules.DistanceModification.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public DistanceModificationConfig DistanceModification { get; set; } = new DistanceModificationConfig();
    }
}
