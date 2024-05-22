using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MediatR;
using CTAM.Core;
using CabinetModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Queries.Users;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetAccessibleCabinetsByLoggedInUserQuery : IRequest<List<CabinetWebDTO>>
    {
        public GetAccessibleCabinetsByLoggedInUserQuery()
        {
        }
    }

    public class GetAccessibleCabinetsByLoggedInUserHandler : IRequestHandler<GetAccessibleCabinetsByLoggedInUserQuery, List<CabinetWebDTO>>
    {
        private MainDbContext _context;
        private HttpContext _httpContext;
        private readonly ILogger<GetAccessibleCabinetsByLoggedInUserHandler> _logger;
        private readonly IMediator _mediator;

        public GetAccessibleCabinetsByLoggedInUserHandler(MainDbContext context, ILogger<GetAccessibleCabinetsByLoggedInUserHandler> logger, IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<List<CabinetWebDTO>> Handle(GetAccessibleCabinetsByLoggedInUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAccessibleCabinetsByLoggedInUserHandler called");
            
            var initiatorsEmail = _httpContext.User.Claims?.Where(c => c.Properties.Values.Contains(JwtRegisteredClaimNames.Sub))?.FirstOrDefault()?.Value;
            var user = await _mediator.Send(new GetUserByEmailQuery(initiatorsEmail));
            var roleIds = user.Roles.Select(r => r.ID);
            var cabinetsPerRoleID = await _mediator.Send(new GetCabinetsByRoleIdsQuery(roleIds));
            var distinctCabinets = cabinetsPerRoleID.Values.SelectMany(x => x).GroupBy(x => x.CabinetNumber).Select(x => x.First());

            return distinctCabinets.ToList();
        }
    }

}
