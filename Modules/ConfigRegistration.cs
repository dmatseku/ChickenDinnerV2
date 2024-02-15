using Exiled.API.Interfaces;
using AtmosphericStartConfig = ChickenDinnerV2.Modules.AtmosphericStart.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public AtmosphericStartConfig AtmosphericStart { get; set; } = new AtmosphericStartConfig();
    }
}
