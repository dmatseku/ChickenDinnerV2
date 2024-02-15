using ChickenDinnerV2.Core.Interfaces;
using System.ComponentModel;

namespace ChickenDinnerV2.Modules.AtmosphericStart
{
    public class Config : IModuleConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;

        [Description("In seconds")]
        public float Duration { get; set; } = 60;
    }
}
