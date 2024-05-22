using System.Collections.Generic;
using UserRoleModule.ApplicationCore.DTO;

namespace CabinetModule.ApplicationCore.DTO
{
    public class CabinetDataSetEnvelope
    {
        public CabinetDataSetEnvelope()
        {
        }

        public CabinetDTO CabinetDTO { get; set; }

        public List<SettingDTO> SettingsDTO { get; set; }

        public List<UserDTO> UsersDTO { get; set; }

        public List<RoleDTO> RolesDTO { get; set; }

        public List<PermissionDTO> PermissionsDTO { get; set; }

        public List<CabinetPositionDTO> CabinetPositionsDTO { get; set; }

        public CabinetUIDTO CabinetUIDTO { get; set; }
    }
}
