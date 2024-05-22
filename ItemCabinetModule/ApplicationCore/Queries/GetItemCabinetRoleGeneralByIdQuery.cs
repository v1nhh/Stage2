using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Web;

namespace ItemCabinetModule.ApplicationCore.Queries
{
    public class GetItemCabinetRoleGeneralByIdQuery : IRequest<RoleWebDTO>
    {
        public int RoleID { get; set; }

        public GetItemCabinetRoleGeneralByIdQuery(int roleId)
        {
            RoleID = roleId;
        }
    }

    public class GetItemCabinetRoleGeneralIdHandler : IRequestHandler<GetItemCabinetRoleGeneralByIdQuery, RoleWebDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetItemCabinetRoleGeneralIdHandler> _logger;
        private readonly IMapper _mapper;

        public GetItemCabinetRoleGeneralIdHandler(MainDbContext context, ILogger<GetItemCabinetRoleGeneralIdHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<RoleWebDTO> Handle(GetItemCabinetRoleGeneralByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetItemCabinetRoleGeneralIdHandler called");
            var result = await _context.CTAMRole().AsNoTracking()
                .Where(role => role.ID == request.RoleID)
                .SingleOrDefaultAsync();
            
            if (result == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.roles_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "id", request.RoleID.ToString() } });
            }
            return _mapper.Map<RoleWebDTO>(result);
        }
    }

}
