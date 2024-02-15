using Exiled.API.Interfaces;
using DoNotTriggerTeslaConfig = ChickenDinnerV2.Modules.DoNotTriggerTesla.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public DoNotTriggerTeslaConfig DoNotTriggerTesla { get; set; } = new DoNotTriggerTeslaConfig();
    }
}
