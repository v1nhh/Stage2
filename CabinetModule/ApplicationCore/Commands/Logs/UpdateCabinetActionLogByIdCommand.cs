using System.Threading.Tasks;
using System;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using MediatR;
using AutoMapper;
using CTAM.Core;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.DTO;

namespace CabinetModule.ApplicationCore.Commands.Logs
{
    public class UpdateCabinetActionLogByIdCommand : IRequest<CabinetActionDTO>
    {
        public Guid ID { get; set; }

        public CabinetAction CabinetAction { get; set; }
    }

    public class UpdateCabinetActionLogByIdHandler : IRequestHandler<UpdateCabinetActionLogByIdCommand, CabinetActionDTO>
    {
        private readonly ILogger<UpdateCabinetLogByIdHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public UpdateCabinetActionLogByIdHandler(MainDbContext context, ILogger<UpdateCabinetLogByIdHandler> logger, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<CabinetActionDTO> Handle(UpdateCabinetActionLogByIdCommand request, CancellationToken cancellationToken)
        {
            var cabinetAction = await _context.CabinetAction().SingleOrDefaultAsync( x => x.ID == request.ID);
            if (cabinetAction == null)
            {
                throw new Exception("UpdateCabinetActionLogByIdHandler encountered null response from CabinetAction table");
            }

            if (request.CabinetAction.ActionDT != cabinetAction.ActionDT)
            {
                cabinetAction.ActionDT = request.CabinetAction.ActionDT;
            }

            if (request.CabinetAction.Action >= 0)
            {
                cabinetAction.Action = request.CabinetAction.Action;
            }

            //if (request.CTAMUserUID != null)
            //{
            //    cabinetAction.CTAMUserUID = request.CTAMUserUID;
            //}

            //if (request.ItemID > 0)
            //{
            //    cabinetAction.ItemID = request.ItemID;
            //}

            if (!string.IsNullOrEmpty(request.CabinetAction.CabinetNumber))
            {
                cabinetAction.CabinetNumber = request.CabinetAction.CabinetNumber;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var cabinetActionExists = _context.CabinetAction().Any(e => e.ID == request.ID);
                if (!cabinetActionExists)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return _mapper.Map<CabinetActionDTO>(cabinetAction);
        }
    }

}
