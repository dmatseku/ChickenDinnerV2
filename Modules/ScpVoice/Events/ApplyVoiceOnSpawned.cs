using ChickenDinnerV2.Core.Interfaces;
using ChickenDinnerV2.Core;
using Exiled.Events.EventArgs.Player;
using PlayerRoles.PlayableScps;
using PlayerRoles.FirstPersonControl;
using System.Reflection;
using ChickenDinnerV2.Modules.ScpVoice.Model;
using Exiled.API.Features;

namespace ChickenDinnerV2.Modules.ScpVoice.Events
{
    internal class ApplyVoiceOnSpawned : IObserver
    {
        protected static Config ScpVoiceConfig = Main.Instance.Config.ScpVoice;

        public bool Register()
        {
            if (ScpVoiceConfig.IsEnabled)
            {
                Exiled.Events.Handlers.Player.Spawned += changeVoice;
                return true;
            }
            return false;
        }

        public void Unregister()
        {
            Exiled.Events.Handlers.Player.Spawned -= changeVoice;
        }

        public void changeVoice(SpawnedEventArgs ev)
        {
            if (ev.Player.VoiceModule.GetType() == typeof(StandardScpVoiceModule) && ev.Player.ReferenceHub.roleManager.CurrentRole is FpcStandardRoleBase voiceRole)
            {
                ScpProximityVoiceModule castedVoice = voiceRole.VoiceModule as ScpProximityVoiceModule;

                if (castedVoice != null)
                {
                    var field = typeof(FpcStandardRoleBase).GetField("<VoiceModule>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);
                    field.SetValue(voiceRole, castedVoice);
                }
                Log.Warn("true");
            }

            if (ev.Player.ReferenceHub.roleManager.CurrentRole is FpcStandardRoleBase voiceRole1)
            {
                Log.Warn(voiceRole1.VoiceModule.GetType().ToString());
            }
        }
    }
}
