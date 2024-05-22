using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ItemModule.ApplicationCore.Commands.ItemDetails
{
    public class RemoveItemDetailCommand : IRequest
    {
        public int ID { get; set; }
    }

    public class RemoveItemDetailHandler : IRequestHandler<RemoveItemDetailCommand>
    {
        private readonly MainDbContext _context;

        public RemoveItemDetailHandler(MainDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(RemoveItemDetailCommand request, CancellationToken cancellationToken)
        {
            var itemdetail = await _context.ItemDetail().Include(id => id.Item).FirstOrDefaultAsync(itemdetail => itemdetail.ID == request.ID);
            if (itemdetail == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.items_apiExceptions_itemDetailNotFound,
                                          new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }

            _context.ItemDetail().Remove(itemdetail);
            itemdetail.Item.NrOfSubItems--;
            await _context.SaveChangesAsync();

            return new Unit();
        }
    }

}
