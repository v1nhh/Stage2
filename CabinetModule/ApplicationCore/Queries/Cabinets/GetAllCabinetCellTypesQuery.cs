using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetAllCabinetCellTypesQuery : IRequest<List<CabinetCellTypeDTO>>
    {
    }

    public class GetAllCabinetCellTypesHandler : IRequestHandler<GetAllCabinetCellTypesQuery, List<CabinetCellTypeDTO>>
    {
        private MainDbContext _context;
        private IMapper _mapper;

        public GetAllCabinetCellTypesHandler(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<CabinetCellTypeDTO>> Handle(GetAllCabinetCellTypesQuery request, CancellationToken cancellationToken)
        {
            var cabinetCellTypes = await _context.CabinetCellType().AsNoTracking()
                .ToListAsync();
            return _mapper.Map<List<CabinetCellTypeDTO>>(cabinetCellTypes.OrderBy(cct => cct.ShortDescr).ToList());
        }
    }

}
