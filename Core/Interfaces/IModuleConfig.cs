
namespace ChickenDinnerV2.Core.Interfaces
{
    public interface IModuleConfig
    {
        bool IsEnabled { get; set; }
        bool Debug { get; set; }
    }
}
