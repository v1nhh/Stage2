using System.Collections.Generic;
using ItemCabinetModule.ApplicationCore.DTO.Sync.Base;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace ItemCabinetModule.ApplicationCore.DTO.Sync.LiveSync
{
    public class UserLiveSyncEnvelope
    {
        public CTAMUserBaseDTO User { get; set; }

        public List<CTAMUserInPossessionBaseDTO> UserInPossessions { get; set; }

        public List<CTAMUserPersonalItemBaseDTO> UserPersonalItems { get; set; }

        public List<CTAMUser_RoleBaseDTO> UserRoles { get; set; }
    }
}
