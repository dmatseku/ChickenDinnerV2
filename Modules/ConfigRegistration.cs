using Exiled.API.Interfaces;
using ScpVoiceConfig = ChickenDinnerV2.Modules.ScpVoice.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        public ScpVoiceConfig ScpVoice {  get; set; } = new ScpVoiceConfig();
    }
}
