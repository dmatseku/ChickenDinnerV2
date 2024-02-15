using Exiled.API.Interfaces;
using InfiniteRadioConfig = ChickenDinnerV2.Modules.InfiniteRadio.Config;

namespace ChickenDinnerV2.Modules
{
    public class ConfigRegistration : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        
        public InfiniteRadioConfig InfiniteRadio {  get; set; } = new InfiniteRadioConfig();
    }
}
