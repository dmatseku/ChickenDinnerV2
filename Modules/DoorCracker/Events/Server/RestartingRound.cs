
namespace ChickenDinnerV2.Modules.DoorCracker.Events.Server
{
    internal class RestartingRound
    {
        protected static Config DoorCrackerConfig = ChickenDinnerV2.Core.Main.Instance.Config.DoorCracker;

        public bool Register()
        {
            if (DoorCrackerConfig.IsEnabled)
            {
                Exiled.Events.Handlers.Server.RestartingRound += ClearCracks;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Server.RestartingRound -= ClearCracks;
        }

        public void ClearCracks()
        {
            ChickenDinnerV2.Modules.DoorCracker.Model.DoorCracker.Clear();
        }
    }
}
