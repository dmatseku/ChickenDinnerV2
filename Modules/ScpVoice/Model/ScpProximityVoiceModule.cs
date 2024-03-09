using PlayerRoles.PlayableScps;
using UnityEngine;
using VoiceChat;
using VoiceChat.Playbacks;

namespace ChickenDinnerV2.Modules.ScpVoice.Model
{
    internal class ScpProximityVoiceModule : StandardScpVoiceModule
    {
        [SerializeField]
        private SingleBufferPlayback _proximityPlayback;

        public override bool IsSpeaking => base.IsSpeaking || this._proximityPlayback.MaxSamples > 0;

        public bool IsProximity { get; set; } = false;

        protected override VoiceChatChannel ProcessInputs(bool primary, bool alt)
        {
            if (primary)
                return this.PrimaryChannel;
            return alt && this.IsProximity ? VoiceChatChannel.Proximity : VoiceChatChannel.None;
        }

        protected override void ProcessSamples(float[] data, int len)
        {
            if (this.CurrentChannel == VoiceChatChannel.Proximity)
                this._proximityPlayback.Buffer.Write(data, len);
            else
                base.ProcessSamples(data, len);
        }

        public override VoiceChatChannel ValidateSend(VoiceChatChannel channel)
        {
            return (channel != VoiceChatChannel.Proximity || !this.IsProximity ? (channel == this.PrimaryChannel ? 1 : 0) : 1) == 0 ? VoiceChatChannel.None : channel;
        }

        public override void ResetObject()
        {
            base.ResetObject();
        }
    }
}
