using AutoMapper;
using CTAM.Core;
using CTAM.Core.Utilities;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Utilities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemModule.ApplicationCore.Queries.ErrorCodes
{
    public class GetAllErrorCodesQuery : IRequest<PaginatedResult<ErrorCode, ErrorCodeDTO>>
    {
        public int Page { get; private set; }
        public int PageLimit { get; private set; }
        public ErrorCodeColumn? SortedBy { get; private set; }
        public bool SortDescending { get; private set; }
        public string FilterQuery { get; private set; }

        public GetAllErrorCodesQuery(int page, int pageLimit, ErrorCodeColumn? sortedBy, bool sortDescending, string filterQuery)
        {
            Page = page;
            PageLimit = pageLimit;
            SortedBy = sortedBy;
            SortDescending = sortDescending;
            FilterQuery = filterQuery;
        }
    }

    public class GetAllErrorCodesHandler : IRequestHandler<GetAllErrorCodesQuery, PaginatedResult<ErrorCode, ErrorCodeDTO>>
    {
        private MainDbContext _context;
        private readonly ILogger<GetAllErrorCodesHandler> _logger;
        private IMapper _mapper;

        public GetAllErrorCodesHandler(MainDbContext context, ILogger<GetAllErrorCodesHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<ErrorCode, ErrorCodeDTO>> Handle(GetAllErrorCodesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllErrorCodesHandler called");

            bool desc = request.SortDescending;

            var errorCodes = _context.ErrorCode().AsNoTracking();

            switch (request.SortedBy)
            {
                case ErrorCodeColumn.Id:
                    errorCodes = desc ? errorCodes.OrderByDescending(x => x.ID) : errorCodes.OrderBy(x => x.ID);
                    break;
                case ErrorCodeColumn.Description:
                    errorCodes = desc ? errorCodes.OrderByDescending(x => x.Description) : errorCodes.OrderBy(x => x.Description);
                    break;
                case ErrorCodeColumn.Code:
                    errorCodes = desc ? errorCodes.OrderByDescending(x => x.Code) : errorCodes.OrderBy(x => x.Code);
                    break;
                case ErrorCodeColumn.CreateDT:
                    errorCodes = desc ? errorCodes.OrderByDescending(x => x.CreateDT) : errorCodes.OrderBy(x => x.CreateDT);
                    break;
                case ErrorCodeColumn.UpdateDT:
                    errorCodes = desc ? errorCodes.OrderByDescending(x => x.UpdateDT) : errorCodes.OrderBy(x => x.UpdateDT);
                    break;
            }

            if (!string.IsNullOrEmpty(request.FilterQuery))
            {
                errorCodes = errorCodes.Where(x => EF.Functions.Like(x.Description, $"%{request.FilterQuery}%") ||
                                                   EF.Functions.Like(x.Code, $"%{request.FilterQuery}%")
                );
            }

            return await errorCodes.Paginate<ErrorCodeDTO>(request.PageLimit, request.Page, _mapper);
        }
    }

}
