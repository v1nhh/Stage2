using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CTAM.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UserRoleModule.ApplicationCore.DataManagers
{
    public class UserRoleDataManager
    {
        private readonly ILogger<UserRoleDataManager> _logger;
        private readonly MainDbContext _context;

        public UserRoleDataManager(MainDbContext context, ILogger<UserRoleDataManager> logger)
        {
            _logger = logger;
            _context = context;
        }

        public IQueryable<Entities.CTAMUser_Role> GetAllCTAMUser_Roles()
        {
            return _context.CTAMUser_Role()
                .AsNoTracking();
        }

        public IQueryable<Entities.CTAMUser_Role> GetCTAMUser_RolesByUserUID(string userUID)
        {
            return _context.CTAMUser_Role()
                .AsNoTracking()
                .Include(ur => ur.CTAMRole)
                .Include(ur => ur.CTAMUser)
                .Where(ur => ur.CTAMUserUID.Equals(userUID));
        }

        public IQueryable<Entities.CTAMUser_Role> GetCTAMUser_RolesByRoleID(int roleID)
        {
            return _context.CTAMUser_Role()
                .AsNoTracking()
                .Include(ur => ur.CTAMRole)
                .Include(ur => ur.CTAMUser)
                .Where(ur => ur.CTAMRoleID.Equals(roleID));
        }

        public IQueryable<Entities.CTAMPermission> GetAllCabinetCTAMPermissions(List<string> licensedPermissions)
        {
            return _context.CTAMPermission()
                .AsNoTracking()
                .Where(p => p.CTAMModule.Equals(CTAM.Core.Enums.CTAMModule.Cabinet) && licensedPermissions.Contains(p.Description));
        }

        public IQueryable<Entities.CTAMRole> GetAllCTAMRoles()
        {
            return _context.CTAMRole()
                .AsNoTracking();
        }

        public IQueryable<Entities.CTAMRole> GetCTAMRoleByID(int roleID)
        {
            return _context.CTAMRole()
                .AsNoTracking()
                .Where(r => r.ID.Equals(roleID));
        }

        public IQueryable<Entities.CTAMSetting> GetAllCTAMSettings()
        {
            return _context.CTAMSetting()
                .AsNoTracking();
        }

        public IQueryable<Entities.CTAMUser> GetAllCTAMUsers()
        {
            return _context.CTAMUser()
                  .AsNoTracking();
        }

        public IQueryable<Entities.CTAMUser> GetCTAMUserByUID(string userUID)
        {
            return _context.CTAMUser()
                  .Include(u => u.CTAMUser_Roles)
                  .AsNoTracking()
                  .Where(u => u.UID.Equals(userUID));
        }

        public IQueryable<Entities.CTAMRole_Permission> GetCTAMRole_PermissionsByRoleID(int roleID)
        {
            return _context.CTAMRole_Permission()
               .Where(rp => rp.CTAMRoleID.Equals(roleID))
               .AsNoTracking();
        }

        public IQueryable<Entities.CTAMRole_Permission> GetAllCabinetCTAMRole_Permissions(List<string> licensedPermissions)
        {
            return _context.CTAMRole_Permission()
                .AsNoTracking()
                .Where(rp => rp.CTAMPermission.CTAMModule.Equals(CTAM.Core.Enums.CTAMModule.Cabinet) && licensedPermissions.Contains(rp.CTAMPermission.Description)); ;
        }

        public async Task<string> GetCTAMSetting(string settingName, string defaultValue = null)
        {
            var settingValue = await _context.CTAMSetting()
                .AsNoTracking()
                .Where(s => s.ParName.Equals(settingName))
                .Select(s => s.ParValue)
                .FirstOrDefaultAsync();

            return settingValue ?? defaultValue;
        }
        public async Task<bool> GetCTAMSetting(string settingName, bool defaultValue = false)
        {
            string parValue = await GetCTAMSetting(settingName, "");

            return Boolean.TryParse(parValue, out bool result) && result;
        }
    }
}
