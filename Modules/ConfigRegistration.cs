using Exiled.API.Interfaces;
using PlayerSpawnRulesConfig = ChickenDinnerV2.Modules.PlayerSpawnRules.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public PlayerSpawnRulesConfig PlayerSpawnRules {  get; set; } = new PlayerSpawnRulesConfig();
    }
}
