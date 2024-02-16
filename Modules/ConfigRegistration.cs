using Exiled.API.Interfaces;
using VolkodavConfig = ChickenDinnerV2.Modules.Volkodav.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public VolkodavConfig Volkodav { get; set; } = new VolkodavConfig();
    }
}
