using Exiled.API.Features;
using PlayerRoles;

namespace ChickenDinnerV2.Core.Interfaces
{
    internal interface IPlayerData
    {
        Player Owner { get; set; }

        void Created();
        void RoleChanged(RoleTypeId newRole);
    }
}
