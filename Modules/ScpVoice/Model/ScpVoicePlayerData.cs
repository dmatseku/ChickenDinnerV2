using ChickenDinnerV2.Core.Interfaces;
using Exiled.API.Features;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChickenDinnerV2.Modules.ScpVoice.Model
{
    internal class ScpVoicePlayerData : IPlayerData
    {
        public Player Owner { get; set; }

        public void Created()
        {
            throw new NotImplementedException();
        }

        public void RoleChanged(RoleTypeId newRole)
        {
            throw new NotImplementedException();
        }
    }
}
