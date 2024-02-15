using Exiled.API.Interfaces;
using ForbidToOpenLockedDoorsConfig = ChickenDinnerV2.Modules.ForbidToOpenLockedDoors.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public ForbidToOpenLockedDoorsConfig ForbidToOpenLockedDoors { get; set; } = new ForbidToOpenLockedDoorsConfig();
    }
}
