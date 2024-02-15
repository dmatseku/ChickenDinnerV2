using ChickenDinnerV2.Core;
using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Modules.AtmosphericStart.Model.Server;

namespace ChickenDinnerV2.Modules.AtmosphericStart.Events.Server
{
    internal class RoundStarted : IObserver
    {
        protected static Config AtmosphericStartConfig = Main.Instance.Config.AtmosphericStart;

        public bool Register()
        {
            if (Main.Instance.Config.AtmosphericStart.IsEnabled)
            {
                Exiled.Events.Handlers.Server.RoundStarted += ApplyEffects;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= ApplyEffects;
        }

        public void ApplyEffects()
        {
            Effects.applyEffects();
        }
    }
}
