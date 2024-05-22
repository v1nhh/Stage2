using AutoMapper;
using CabinetModule.ApplicationCore.Entities;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.DTO.Web;
using ItemCabinetModule.ApplicationCore.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemCabinetModule.ApplicationCore.Queries.Cabinets
{
    public class GetCabinetPositionContentQuery : IRequest<PaginatedResult<CabinetPosition, CabinetPositionsDetailsEnvelope>>
    {
        public string CabinetNumber { get; private set; }
        public int Page { get; private set; }
        public int PageLimit { get; private set; }
        public Enums.CabinetPositionDetailColumn? SortedBy { get; private set; }
        public bool SortDescending { get; private set; }
        public string FilterQuery { get; private set; }

        public GetCabinetPositionContentQuery(string cabinetNumber, int pageLimit, int page, Enums.CabinetPositionDetailColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            CabinetNumber = cabinetNumber;
            PageLimit = pageLimit;
            Page = page;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetCabinetPositionContentHandler : IRequestHandler<GetCabinetPositionContentQuery, PaginatedResult<CabinetPosition, CabinetPositionsDetailsEnvelope>>
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public GetCabinetPositionContentHandler(MainDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<CabinetPosition, CabinetPositionsDetailsEnvelope>> Handle(GetCabinetPositionContentQuery request, CancellationToken cancellationToken)
        {
            bool desc = request.SortDescending;

            var cabinetPositions = _context.CabinetPosition()
                .AsNoTracking()
                .Include(cabPos => cabPos.CabinetCellType)
                .Where(cabPos => cabPos.CabinetNumber.Equals(request.CabinetNumber));

            switch (request.SortedBy)
            {
                case Enums.CabinetPositionDetailColumn.PositionNumber:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.PositionNumber) : cabinetPositions.OrderBy(x => x.PositionNumber);
                    break;
                case Enums.CabinetPositionDetailColumn.PositionAlias:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.PositionAlias) : cabinetPositions.OrderBy(x => x.PositionAlias);
                    break;
                case Enums.CabinetPositionDetailColumn.PositionType:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.PositionType) : cabinetPositions.OrderBy(x => x.PositionType);
                    break;
                case Enums.CabinetPositionDetailColumn.CabinetCellType:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.CabinetCellType) : cabinetPositions.OrderBy(x => x.CabinetCellType);
                    break;
                case Enums.CabinetPositionDetailColumn.BladeNo:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.BladeNo) : cabinetPositions.OrderBy(x => x.BladeNo);
                    break;
                case Enums.CabinetPositionDetailColumn.BladePosNo:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.BladePosNo) : cabinetPositions.OrderBy(x => x.BladePosNo);
                    break;
                case Enums.CabinetPositionDetailColumn.CabinetDoorID:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.CabinetDoorID) : cabinetPositions.OrderBy(x => x.CabinetDoorID);
                    break;
                case Enums.CabinetPositionDetailColumn.CreateDT:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.CreateDT) : cabinetPositions.OrderBy(x => x.CreateDT);
                    break;
                case Enums.CabinetPositionDetailColumn.UpdateDT:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.UpdateDT) : cabinetPositions.OrderBy(x => x.UpdateDT);
                    break;
                case Enums.CabinetPositionDetailColumn.MaxNrOfItems:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.MaxNrOfItems) : cabinetPositions.OrderBy(x => x.MaxNrOfItems);
                    break;
                case Enums.CabinetPositionDetailColumn.IsAllocated:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.IsAllocated) : cabinetPositions.OrderBy(x => x.IsAllocated);
                    break;
                case Enums.CabinetPositionDetailColumn.Status:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.Status) : cabinetPositions.OrderBy(x => x.Status);
                    break;
                case Enums.CabinetPositionDetailColumn.SpecCode:
                    cabinetPositions = desc ? cabinetPositions.OrderByDescending(x => x.CabinetCellType.SpecCode) : cabinetPositions.OrderBy(x => x.CabinetCellType.SpecCode);
                    break;
                default:
                    cabinetPositions = cabinetPositions.OrderBy(x => x.BladeNo).ThenBy(x => x.BladePosNo);
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                cabinetPositions = cabinetPositions.Where(x => EF.Functions.Like(x.PositionAlias, $"%{request.FilterQuery}%") ||
                                                               EF.Functions.Like(x.CabinetCellType.SpecCode, $"%{request.FilterQuery}%"));
            }

            var page = await cabinetPositions.Paginate<CabinetPositionsDetailsEnvelope>(request.PageLimit, request.Page, _mapper);
            var pageIds = page.Objects.Select(p => p.CabinetPosition.ID);

            var cabPosContent = await _context.CabinetPositionContent()
                .AsNoTracking()
                .Include(cabPosContent => cabPosContent.CabinetPosition)
                .Where(cabPosContent => pageIds.Contains(cabPosContent.CabinetPositionID))
                .ToListAsync();

            foreach( var env in page.Objects)
            {
                env.Items = cabPosContent
                    .Where(cabPosContent => cabPosContent.CabinetPositionID.Equals(env.CabinetPosition.ID))
                    .Select(cabPos => cabPos.ItemID)
                    .ToList();
            };

            return page;
        }
    }

}
