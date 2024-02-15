using Exiled.API.Interfaces;
using ObserversCounterConfig = ChickenDinnerV2.Modules.ObserversCounter.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public ObserversCounterConfig ObserversCounter { get; set; } = new ObserversCounterConfig();
    }
}
