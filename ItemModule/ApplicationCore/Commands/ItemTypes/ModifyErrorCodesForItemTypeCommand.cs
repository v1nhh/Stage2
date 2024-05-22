using CTAM.Core;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ItemModule.ApplicationCore.Commands.ItemTypes
{
    public class ModifyErrorCodesForItemTypeCommand : IRequest
    {
        public int ItemTypeID { get; set; }

        public IEnumerable<int> AddErrorCodeIDs { get; set; }

        public IEnumerable<int> RemoveErrorCodeIDs { get; set; }

    }

    public class ModifyErrorCodesForItemTypeHandler : IRequestHandler<ModifyErrorCodesForItemTypeCommand>
    {
        private readonly ILogger<ModifyErrorCodesForItemTypeHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMediator _mediator;


        public ModifyErrorCodesForItemTypeHandler(ILogger<ModifyErrorCodesForItemTypeHandler> logger, MainDbContext context, IMediator mediator)
        {
            _logger = logger;
            _context = context;
            _mediator = mediator;
        }


        public async Task<Unit> Handle(ModifyErrorCodesForItemTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ModifyErrorCodesForItemTypeHandler called");
            await ModifyErrorCodes(request);
            await _context.SaveChangesAsync();
            
            return new Unit();
        }

        private async Task ModifyErrorCodes(ModifyErrorCodesForItemTypeCommand request)
        {
            if(request.AddErrorCodeIDs != null && request.AddErrorCodeIDs.Any())
            {
                _logger.LogInformation($"ItemTypeID= {request.ItemTypeID} > Adding error codes: '{string.Join(", ", request.AddErrorCodeIDs)}");
                var errorCodesToAdd = request.AddErrorCodeIDs.Select(errorCodeID => new ItemType_ErrorCode()
                {
                    ItemTypeID = request.ItemTypeID,
                    ErrorCodeID = errorCodeID
                }).ToArray();
                await _context.ItemType_ErrorCode().AddRangeAsync(errorCodesToAdd);
            }
            if (request.RemoveErrorCodeIDs != null && request.RemoveErrorCodeIDs.Any())
            {
                _logger.LogInformation($"ItemTypeID={request.ItemTypeID} > Removing error codes: '{string.Join(", ", request.RemoveErrorCodeIDs)}");
                var errorCodesToRemove = await _context.ItemType_ErrorCode()
                    .Where(ie => request.ItemTypeID == ie.ItemTypeID && request.RemoveErrorCodeIDs.Contains(ie.ErrorCodeID))
                    .ToArrayAsync();
                _context.ItemType_ErrorCode().RemoveRange(errorCodesToRemove);
            }
        }
    }
}
