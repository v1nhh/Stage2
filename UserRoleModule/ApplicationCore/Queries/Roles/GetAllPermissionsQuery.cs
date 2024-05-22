using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using MediatR;
using UserRoleModule.ApplicationCore.DTO;
using CTAM.Core;

namespace UserRoleModule.ApplicationCore.Queries.Roles
{
    public class GetAllPermissionsQuery : IRequest<List<PermissionDTO>>
    {
    }

    public class GetAllPermissionsHandler: IRequestHandler<GetAllPermissionsQuery, List<PermissionDTO>>
    {
        private MainDbContext _context;
        private IMapper _mapper;

        public GetAllPermissionsHandler(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PermissionDTO>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _context.CTAMPermission().AsNoTracking().ToListAsync();
            return _mapper.Map<List<PermissionDTO>>(permissions);
        }
    }

}
