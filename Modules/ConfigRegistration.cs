using Exiled.API.Interfaces;
using HandcuffTeammatesConfig = ChickenDinnerV2.Modules.HandcuffTeammates.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public HandcuffTeammatesConfig HandcuffTeammates { get; set; } = new HandcuffTeammatesConfig();
    }
}
