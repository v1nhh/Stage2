using AutoMapper;
using CTAM.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;

namespace UserRoleModule.ApplicationCore.Utilities
{
    public static class Extensions
    {
        public static async Task<PaginatedResult<ManagementLog, ManagementLogDTO>> Paginate<ManagementLogDTO>(this IQueryable<ManagementLog> source,
                                        int pageSize, int pageNumber, IMapper mapper, Dictionary<string, object> mapperOptions = null)
        {
            return await new PaginatedResult<ManagementLog, ManagementLogDTO>(pageNumber, pageSize).Paginate(source, mapper, null, mapperOptions);
        }

        public static async Task<PaginatedResult<CTAMUser, UserWebDTO>> Paginate<UserWebDTO>(this IQueryable<CTAMUser> source,
                                int pageSize, int pageNumber, IMapper mapper, Func<CTAMUser, IMapper, UserWebDTO> mappingFunc = null)
        {
            return await new PaginatedResult<CTAMUser, UserWebDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }

        public static async Task<PaginatedResult<CTAMRole, RoleWebDTO>> Paginate<RoleWebDTO>(this IQueryable<CTAMRole> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<CTAMRole, IMapper, RoleWebDTO> mappingFunc = null)
        {
            return await new PaginatedResult<CTAMRole, RoleWebDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }

        public static async Task<PaginatedResult<CTAMPermission, PermissionWebDTO>> Paginate<PermissionWebDTO>(this IQueryable<CTAMPermission> source,
                        int pageSize, int pageNumber, IMapper mapper, Func<CTAMPermission, IMapper, PermissionWebDTO> mappingFunc = null)
        {
            return await new PaginatedResult<CTAMPermission, PermissionWebDTO>(pageNumber, pageSize).Paginate(source, mapper, mappingFunc);
        }

        public static async Task<PaginatedResult<RoleWebDTO, RoleWebDTO>> Paginate<RoleWebDTO>(this IQueryable<RoleWebDTO> source,
                        int pageSize, int pageNumber, IMapper mapper)
        {
            return await new PaginatedResult<RoleWebDTO, RoleWebDTO>(pageNumber, pageSize).Paginate(source, mapper);
        }
    }
}