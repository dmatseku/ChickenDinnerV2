using ChickenDinnerV2.Core.Interfaces;
using System.ComponentModel;

namespace ChickenDinnerV2.Modules.ObserversCounter
{
    public class Config : IModuleConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;


        [Description("count of observers that will lock the scp")]
        public int ObserversCount { get; set; } = 3;
    }
}
