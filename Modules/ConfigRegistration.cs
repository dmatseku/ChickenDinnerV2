using Exiled.API.Interfaces;
using WeaponAdditionsConfig = ChickenDinnerV2.Modules.WeaponAdditions.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public WeaponAdditionsConfig WeaponAdditions { get; set; } = new WeaponAdditionsConfig();
    }
}
