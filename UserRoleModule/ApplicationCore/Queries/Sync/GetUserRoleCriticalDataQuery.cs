using AutoMapper;
using CTAM.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DataManagers;
using UserRoleModule.ApplicationCore.DTO.Sync;
using UserRoleModule.ApplicationCore.DTO.Sync.Base;

namespace UserRoleModule.ApplicationCore.Queries.Sync
{
    public class GetUserRoleCriticalDataQuery : IRequest<UserRoleCriticalDataEnvelope>
    {
        public GetUserRoleCriticalDataQuery()
        {
        }

    }

    public class GetUserRoleCriticalDataHandler : IRequestHandler<GetUserRoleCriticalDataQuery, UserRoleCriticalDataEnvelope>
    {
        private readonly ILogger<GetUserRoleCriticalDataHandler> _logger;
        private readonly UserRoleDataManager _userRoleManager;
        private readonly IMapper _mapper;
        private readonly ITenantContext _tenantContext;
        private readonly ITenantService _tenantService;

        public GetUserRoleCriticalDataHandler(UserRoleDataManager userRoleManager, IMapper mapper, ILogger<GetUserRoleCriticalDataHandler> logger, ITenantContext tenantContext, ITenantService tenantService)
        {
            _userRoleManager = userRoleManager;
            _logger = logger;
            _mapper = mapper;
            _tenantContext = tenantContext;
            _tenantService = tenantService;
        }

        public async Task<UserRoleCriticalDataEnvelope> Handle(GetUserRoleCriticalDataQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetUserRoleCriticalDataHandler called");
            UserRoleCriticalDataEnvelope env = new UserRoleCriticalDataEnvelope();
            var licensedPermissions = _tenantService.GetLicensePermissionsForTenant(_tenantContext.TenantId);

            //CTAMUser_Role
            env.CTAMUser_RoleData = _mapper.Map(
                    await _userRoleManager.GetAllCTAMUser_Roles().ToListAsync(),
                    new List<CTAMUser_RoleBaseDTO>()
                );

            //CTAMPermission
            env.CTAMPermissionData = _mapper.Map(
                    await _userRoleManager.GetAllCabinetCTAMPermissions(licensedPermissions).ToListAsync(),
                    new List<CTAMPermissionBaseDTO>()
                );

            //CTAMRole
            env.CTAMRoleData = _mapper.Map(
                    await _userRoleManager.GetAllCTAMRoles().ToListAsync(),
                    new List<CTAMRoleBaseDTO>()
                );

            //CTAMSetting
            env.CTAMSettingData = _mapper.Map(
                    await _userRoleManager.GetAllCTAMSettings().ToListAsync(),
                    new List<CTAMSettingBaseDTO>()
                );

            //CTAMUser    
            env.CTAMUserData = _mapper.Map(
                    await _userRoleManager.GetAllCTAMUsers().ToListAsync(),
                    new List<CTAMUserBaseDTO>()
                );

            //CTAMRole_Permission
            env.CTAMRole_PermissionData = _mapper.Map(
                    await _userRoleManager.GetAllCabinetCTAMRole_Permissions(licensedPermissions).ToListAsync(),
                    new List<CTAMRole_PermissionBaseDTO>()
                );

            return env;
        }
    }

}
