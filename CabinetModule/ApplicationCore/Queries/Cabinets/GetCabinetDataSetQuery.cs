using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using CTAM.Core;
using CTAM.Core.Enums;
using CabinetModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.DTO;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetDataSetQuery : IRequest<CabinetDataSetEnvelope>
    {
        public GetCabinetDataSetQuery( string cabinetNumber)
        {
            this.CabinetNumber = cabinetNumber;
        }

        public string CabinetNumber { get; set; }
    }

    public class GetCabinetDataSetHandler : IRequestHandler<GetCabinetDataSetQuery, CabinetDataSetEnvelope>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetCabinetDataSetHandler> _logger;
        private readonly IMapper _mapper;

        public GetCabinetDataSetHandler(MainDbContext context, ILogger<GetCabinetDataSetHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<CabinetDataSetEnvelope> Handle(GetCabinetDataSetQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetDataSetHandler called");

            CabinetDataSetEnvelope env = new CabinetDataSetEnvelope();

            string cabinetNumber = await GetCabinet(request, env);

            await GetCabinetPositions(env, cabinetNumber, cancellationToken);

            await GetSettings(env, cancellationToken);

            // Verzamelen alle gebruikte rollen in cabinet
            var rolids = new HashSet<int>();
            var qryroles = _context.CTAMRole_Cabinet()
                                .Include(x => x.CTAMRole)
                                    .ThenInclude(x => x.CTAMRole_Permission)
                                        .ThenInclude(x => x.CTAMPermission)
                                .Where(x => x.CabinetNumber.Equals(cabinetNumber));

            foreach (var role in qryroles)
            {
                if (role?.CTAMRole?.CTAMRole_Permission?.Count > 0)
                {
                    rolids.Add(role.CTAMRoleID);
                }
            }

            List<string> userUids = await GetUserUidsAndAssignCTAMUsers(env, rolids);

            await GetRoles(env, cancellationToken);

            await GetPermissions(env, cancellationToken);

            await GetCabinetUI(env);

            return env;
        }

        private async Task GetCabinetUI(CabinetDataSetEnvelope env)
        {
            var cabinetui = await _context.CabinetUI().
                                    AsNoTracking().
                                    FirstOrDefaultAsync();

            env.CabinetUIDTO = _mapper.Map<CabinetUIDTO>(cabinetui);
        }

        private async Task GetRoles(CabinetDataSetEnvelope env, CancellationToken cancellationToken)
        {
            IQueryable<CTAMRole> qryroles = _context.CTAMRole()
                                                .AsQueryable();

            var roles = await qryroles
                                .AsNoTracking()
                                .Include(x => x.CTAMRole_Permission)
                                    .ThenInclude(rp => rp.CTAMPermission)
                                .ToListAsync(cancellationToken);

            env.RolesDTO = _mapper.Map<List<RoleDTO>>(roles);
        }

        private async Task GetPermissions(CabinetDataSetEnvelope env, CancellationToken cancellationToken)
        {
            IQueryable<CTAMPermission> qrypermissions = _context.CTAMPermission();
                                                            // We need all permissions to match roles with permissions in LocalAPI
                                                            //.Where(x => x.CTAMModule == CTAMModule.Cabinet);

            var permissionss = await qrypermissions
                                        .AsNoTracking()
                                        .ToListAsync(cancellationToken);

            env.PermissionsDTO = _mapper.Map<List<PermissionDTO>>(permissionss);
        }



        private async Task<List<string>> GetUserUidsAndAssignCTAMUsers(CabinetDataSetEnvelope env, HashSet<int> rolids)
        {
            var userUids = new List<string>();
            env.UsersDTO = new List<UserDTO>();
            var qryuserroles = _context.CTAMUser_Role()
                                    .Include(x => x.CTAMUser)
                                    .Include(x => x.CTAMRole)
                                        .ThenInclude(x => x.CTAMRole_Permission)
                                            .ThenInclude(x => x.CTAMPermission)
                                .ToListAsync();

            if (rolids.Count > 0)
            {
                foreach (var ctamuser in await qryuserroles)
                {
                    if (rolids.Contains(ctamuser.CTAMRoleID))
                    {
                        if (!userUids.Contains(ctamuser.CTAMUserUID))
                        {
                            env.UsersDTO.Add(_mapper.Map<UserDTO>(ctamuser.CTAMUser));
                            userUids.Add(ctamuser.CTAMUserUID);
                        }
                    }
                }
            }

            return userUids;
        }

        private async Task GetSettings(CabinetDataSetEnvelope env, CancellationToken cancellationToken)
        {
            IQueryable<CTAMSetting> qrysettings = _context.CTAMSetting()
                                                    .Where(x => x.CTAMModule == CTAMModule.Cabinet);

            var settings = await qrysettings
                                    .AsNoTracking()
                                    .ToListAsync(cancellationToken);

            env.SettingsDTO = _mapper.Map<List<SettingDTO>>(settings);
        }

        private async Task GetCabinetPositions(CabinetDataSetEnvelope env, string cabinetNumber, CancellationToken cancellationToken)
        {
            IQueryable<CabinetPosition> qrycabinetpositions = _context.CabinetPosition()
                                                                .Include(pos => pos.Cabinet)
                                                                .Where(x => x.CabinetNumber.Equals(cabinetNumber));

            var cabinetpositions = await qrycabinetpositions
                                    .OrderBy(x => x.PositionNumber)
                                    .AsNoTracking()
                                    .ToListAsync(cancellationToken);

            env.CabinetPositionsDTO = _mapper.Map<List<CabinetPositionDTO>>(cabinetpositions);
        }

        private async Task<string> GetCabinet(GetCabinetDataSetQuery request, CabinetDataSetEnvelope env)
        {
            var cabinet = await _context.Cabinet()
                                .AsNoTracking()
                                .Include(c => c.CTAMRole_Cabinets)
                                    .ThenInclude(cr => cr.CTAMRole)
                                .FirstOrDefaultAsync(cabinet => cabinet.CabinetNumber.Equals(request.CabinetNumber));

            if (cabinet == null)
            {
                throw new KeyNotFoundException($"Requested data for an unknown cabinet '{request.CabinetNumber}'");
            }
            if (!cabinet.IsActive)
            {
                throw new InvalidOperationException($"Requested data for an inactive cabinet '{request.CabinetNumber}'");
            }
            env.CabinetDTO = _mapper.Map<CabinetDTO>(cabinet);

            return cabinet.CabinetNumber;
        }
    }

}
