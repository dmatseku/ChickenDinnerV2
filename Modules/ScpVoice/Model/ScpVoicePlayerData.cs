using ChickenDinnerV2.Core.Interfaces;
using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoiceChat.Playbacks;

namespace ChickenDinnerV2.Modules.ScpVoice.Model
{
    internal class ScpVoicePlayerData : IPlayerData
    {
        public Player Owner { get; set; }

        public SingleBufferPlayback _proximityPlayback { get; set; } = new SingleBufferPlayback();

        public bool IsProximity { get; set; } = false;

        protected void ClearValues()
        {
            _proximityPlayback = new SingleBufferPlayback();
            IsProximity = false;
        }

        public void Created()
        {
            ClearValues();
        }

        public void RoleChange(RoleTypeId newRole)
        {
        }

        public void Spawned()
        {
            if (Owner.Role.Team == Team.SCPs)
            {
                ClearValues();
            }
        }
    }
}
