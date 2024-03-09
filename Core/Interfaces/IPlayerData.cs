using Exiled.API.Features;
using Exiled.API.Features.Roles;
using PlayerRoles;

namespace ChickenDinnerV2.Core.Interfaces
{
    internal interface IPlayerData
    {
        Player Owner { get; set; }

        void Created();
        void RoleChange(RoleTypeId newRole);
        void Spawned();
    }
}
