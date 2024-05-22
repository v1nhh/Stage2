using CTAM.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using ItemCabinetModule.ApplicationCore.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ItemCabinetModule.ApplicationCore.Commands
{
    public class LinkPersonalItemToUserCommand : IRequest
    {
        public int ItemID { get; set; }
        public string UserUID { get; set; }

        public LinkPersonalItemToUserCommand(int itemID, string userUID)
        {
            ItemID = itemID;
            UserUID = userUID;
        }
    }

    public class LinkPersonalItemToUserHandler : IRequestHandler<LinkPersonalItemToUserCommand>
    {
        private readonly ILogger<LinkPersonalItemToUserHandler> _logger;
        private readonly MainDbContext _context;

        public LinkPersonalItemToUserHandler(ILogger<LinkPersonalItemToUserHandler> logger, MainDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<Unit> Handle(LinkPersonalItemToUserCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation(string.Format("LinkPersonalItemToUserCommand called"));
            var personalItemToAdd = new CTAMUserPersonalItem() { CTAMUserUID = command.UserUID, ItemID = command.ItemID, Status = Enums.UserPersonalItemStatus.OK};

            var user = await _context.CTAMUser().Where(u => u.UID.Equals(command.UserUID)).FirstOrDefaultAsync();
            var personalItemInPossessin = new CTAMUserInPossession() {
                //ID = Guid.NewGuid(),
                ItemID = command.ItemID,
                Status = Enums.UserInPossessionStatus.Picked, 
                CTAMUserUIDOut = user.UID, 
                CTAMUserNameOut = user.Name, 
                CTAMUserEmailOut = user.Email, 
                OutDT = System.DateTime.UtcNow, 
                CreatedDT = System.DateTime.UtcNow,
                CabinetNameOut = "Personal Item Testing Endpoint"};
            await _context.CTAMUserPersonalItem().AddAsync(personalItemToAdd);
            await _context.CTAMUserInPossession().AddAsync(personalItemInPossessin);
            await _context.SaveChangesAsync();
            return new Unit();
        }
    }
}
