using ChickenDinnerV2.Core.Interfaces;

namespace ChickenDinnerV2.Modules.ForbidToOpenLockedDoors
{
    public class Config : IModuleConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
