using Exiled.API.Interfaces;
using DoorCrackerConfig = ChickenDinnerV2.Modules.DoorCracker.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public DoorCrackerConfig DoorCracker { get; set; } = new DoorCrackerConfig();
    }
}
