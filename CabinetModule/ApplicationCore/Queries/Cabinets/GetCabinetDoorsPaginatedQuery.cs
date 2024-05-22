using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Utilities;
using CTAM.Core;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetDoorsPaginatedQuery : IRequest<PaginatedResult<CabinetDoor, CabinetDoorDTO>>
    {
        public string CabinetNumber { get; set; }
    }

    public class GetCabinetDoorsPaginatedHandler : IRequestHandler<GetCabinetDoorsPaginatedQuery, PaginatedResult<CabinetDoor, CabinetDoorDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetCabinetDoorsPaginatedHandler> _logger;
        private readonly IMapper _mapper;

        public GetCabinetDoorsPaginatedHandler(MainDbContext context, ILogger<GetCabinetDoorsPaginatedHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CabinetDoor, CabinetDoorDTO>> Handle(GetCabinetDoorsPaginatedQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetDoorsPaginatedHandler called");
            var doors = _context.CabinetDoor()
                .AsNoTracking()
                .Where(cd => cd.CabinetNumber == request.CabinetNumber);

            var result = await doors.Paginate<CabinetDoorDTO>(50, 0, _mapper);

            return result; // totals and limit not set for doors, only forced in PaginatedResult frame so it can be easily shown in UI tab
        }
    }

}
