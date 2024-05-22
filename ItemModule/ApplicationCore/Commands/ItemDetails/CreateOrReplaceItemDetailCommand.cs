using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ItemModule.ApplicationCore.Commands.ItemDetails
{
    public class CreateOrReplaceItemDetailCommand : IRequest<ItemDetailDTO>
    {
        public int? ID { get; set; }

        public int ItemID { get; set; }

        public string Description { get; set; }

        public string FreeText1 { get; set; }

        public string FreeText2 { get; set; }

        public string FreeText3 { get; set; }

        public string FreeText4 { get; set; }

        public string FreeText5 { get; set; }
    }

    public class CreateOrReplaceItemDetailHandler : IRequestHandler<CreateOrReplaceItemDetailCommand, ItemDetailDTO>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<CreateOrReplaceItemDetailHandler> _logger;
        private readonly IMapper _mapper;

        public CreateOrReplaceItemDetailHandler(MainDbContext context, ILogger<CreateOrReplaceItemDetailHandler> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ItemDetailDTO> Handle(CreateOrReplaceItemDetailCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateOrReplaceItemDetailHandler called");

            var item = await _context.Item().AsNoTracking().FirstOrDefaultAsync(item => item.ID == request.ItemID);
            if (item == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.items_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }

            var itemDetail = await _context.ItemDetail().FirstOrDefaultAsync(itemDetail => itemDetail.ID == request.ID);
            if (itemDetail == null)
            {
                itemDetail = await CreateItemDetail(request);
                item.NrOfSubItems++;
            }
            else
            {
                ReplaceItemDetail(request, itemDetail);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<ItemDetailDTO>(itemDetail);
        }

        private void ReplaceItemDetail(CreateOrReplaceItemDetailCommand request, ItemDetail itemdetail)
        {
            itemdetail.ItemID = request.ItemID;
            itemdetail.Description = request.Description;
            itemdetail.FreeText1 = request.FreeText1;
            itemdetail.FreeText2 = request.FreeText2;
            itemdetail.FreeText3 = request.FreeText3;
            itemdetail.FreeText4 = request.FreeText4;
            itemdetail.FreeText5 = request.FreeText5;
        }

        private async Task<ItemDetail> CreateItemDetail(CreateOrReplaceItemDetailCommand request)
        {
            var newItemDetail = new ItemDetail()
            {
                ItemID = request.ItemID,
                Description = request.Description,
                FreeText1 = request.FreeText1,
                FreeText2 = request.FreeText2,
                FreeText3 = request.FreeText3,
                FreeText4 = request.FreeText4,
                FreeText5 = request.FreeText5,
            };
            await _context.ItemDetail().AddAsync(newItemDetail);

            return newItemDetail;
        }
    }

}
