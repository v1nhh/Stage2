using System.Collections.Generic;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.LiveSync
{
    public class UserRoleLiveSyncEnvelope
    {
        public CTAMUser_RoleBaseDTO UserRole { get; set; }

        public CTAMUserBaseDTO User { get; set; }

        public List<CTAMUserInPossessionBaseDTO> UserInPossessions { get; set; }

        public List<CTAMUserPersonalItemBaseDTO> UserPersonalItems { get; set; }
    }
}
