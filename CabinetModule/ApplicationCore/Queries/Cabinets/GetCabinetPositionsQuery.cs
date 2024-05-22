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
using CabinetModule.ApplicationCore.DTO;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetPositionsQuery : IRequest<List<CabinetPositionDTO>>
    {
        public string CabinetNumber { get; set; }

        public GetCabinetPositionsQuery(string cabinetNumber)
        {
            CabinetNumber = cabinetNumber;
        }
    }

    public class GetCabinetPositionsHandler : IRequestHandler<GetCabinetPositionsQuery, List<CabinetPositionDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<GetCabinetPositionsQuery> _logger;
        private readonly IMapper _mapper;


        public GetCabinetPositionsHandler(MainDbContext context, ILogger<GetCabinetPositionsQuery> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<CabinetPositionDTO>> Handle(GetCabinetPositionsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetCabinetPositionsHandler called");
            try
            {
                var result = await _context.Cabinet().AsNoTracking()
                    .Where(cabinet => cabinet.CabinetNumber.Equals(request.CabinetNumber))
                        .Include(cabinet => cabinet.CabinetPositions)
                    .FirstOrDefaultAsync();

                return _mapper.Map<List<CabinetPositionDTO>>(result.CabinetPositions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
