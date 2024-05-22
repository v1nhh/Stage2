using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserRoleModule.ApplicationCore.DataManagers;
using UserRoleModule.ApplicationCore.DTO.Sync;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace CabinetModule.ApplicationCore.Queries.Sync
{
    public class GetUserRoleCriticalDataForCabinetQuery : IRequest<UserRoleCriticalDataEnvelope>
    {
        public string CabinetNumber { get; set; }
        public GetUserRoleCriticalDataForCabinetQuery(string cabinetNumber)
        {
            CabinetNumber = cabinetNumber;
        }
    }

    public class GetUserRoleCriticalDataForCabinetHandler : IRequestHandler<GetUserRoleCriticalDataForCabinetQuery, UserRoleCriticalDataEnvelope>
    {
        private readonly ILogger<GetUserRoleCriticalDataForCabinetHandler> _logger;
        private readonly UserRoleDataManager _userRoleManager;
        private readonly MainDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ITenantContext _tenantContext;
        private readonly ITenantService _tenantService;

        public GetUserRoleCriticalDataForCabinetHandler(UserRoleDataManager userRoleManager, IMapper mapper, ILogger<GetUserRoleCriticalDataForCabinetHandler> logger, MainDbContext dbContext, ITenantContext tenantContext, ITenantService tenantService)
        {
            _userRoleManager = userRoleManager;
            _logger = logger;
            _mapper = mapper;
            _dbContext = dbContext;
            _tenantContext = tenantContext;
            _tenantService = tenantService;
        }

        public async Task<UserRoleCriticalDataEnvelope> Handle(GetUserRoleCriticalDataForCabinetQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUserRoleCriticalDataHandler called");
            UserRoleCriticalDataEnvelope env = new UserRoleCriticalDataEnvelope();
            var licensedPermissions = _tenantService.GetLicensePermissionsForTenant(_tenantContext.TenantId);

            //CTAMSetting
            env.CTAMSettingData = _mapper.Map(
                    await _userRoleManager.GetAllCTAMSettings().ToListAsync(),
                    new List<CTAMSettingBaseDTO>()
                );

            //CTAMPermission
            env.CTAMPermissionData = _mapper.Map(
                    await _userRoleManager.GetAllCabinetCTAMPermissions(licensedPermissions).ToListAsync(),
                    new List<CTAMPermissionBaseDTO>()
                );

            var roles = await _dbContext.Cabinet()
                        .Where(cabinet => cabinet.CabinetNumber.Equals(request.CabinetNumber))
                        .Include(cabinet => cabinet.CTAMRole_Cabinets)
                        .ThenInclude(roleCabinet => roleCabinet.CTAMRole)
                        .SelectMany(cabinet => cabinet.CTAMRole_Cabinets.Select(roleCabinet => roleCabinet.CTAMRole))
                        .ToListAsync();
            //CTAMRole
            env.CTAMRoleData = _mapper.Map(
                    roles,
                    new List<CTAMRoleBaseDTO>()
                );

            var roleIDs = roles.Select(role => role.ID).ToList();
            //CTAMUser_Role
            env.CTAMUser_RoleData = _mapper.Map(
                    await _userRoleManager.GetAllCTAMUser_Roles()
                        .Where(userRole => roleIDs.Contains(userRole.CTAMRoleID))
                        .ToListAsync(),
                    new List<CTAMUser_RoleBaseDTO>()
                );

            //CTAMUser
            var userIDs = env.CTAMUser_RoleData.Select(userRole => userRole.CTAMUserUID).Distinct().ToList();
            List<CTAMUserBaseDTO> userBaseDTOs = new List<CTAMUserBaseDTO>();
            foreach (var chunk in userIDs.Partition(15000))
            {
                var chunkUserBaseDTOs = _mapper.Map(
                    await _userRoleManager.GetAllCTAMUsers()
                        .Where(user => chunk.Contains(user.UID))
                        .ToListAsync(),
                    new List<CTAMUserBaseDTO>()
                );
                userBaseDTOs.AddRange(chunkUserBaseDTOs);
            }
            env.CTAMUserData = userBaseDTOs;
            
            //CTAMRole_Permission
            env.CTAMRole_PermissionData = _mapper.Map(
                    await _userRoleManager.GetAllCabinetCTAMRole_Permissions(licensedPermissions)
                        .Where(rolePermission => roleIDs.Contains(rolePermission.CTAMRoleID))
                        .ToListAsync(),
                    new List<CTAMRole_PermissionBaseDTO>()
                );

            return env;
        }
    }
}
