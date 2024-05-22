using AutoMapper;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO;

namespace UserRoleModule.ApplicationCore.Commands.Sync
{
    public class SyncCriticalUserDataCommand : IRequest
    {
        public List<UserAndCardCodeDTO> UserAndCardCodeDTOs { get; set; }

        public SyncCriticalUserDataCommand(List<UserAndCardCodeDTO> userAndCardCodeDTOs)
        {
            UserAndCardCodeDTOs = userAndCardCodeDTOs;
        }
    }

    public class SyncCriticalUserDataHandler : IRequestHandler<SyncCriticalUserDataCommand>
    {
        private readonly ILogger<SyncCriticalUserDataHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public SyncCriticalUserDataHandler(MainDbContext context, ILogger<SyncCriticalUserDataHandler> logger, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(SyncCriticalUserDataCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SyncCriticalUserDataHandler called");
            try
            {
                if (request != null && request.UserAndCardCodeDTOs != null && request.UserAndCardCodeDTOs.Count > 0)
                {
                    await SyncUsersAndCardCode(request);
                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                _logger.LogWarning("User data is not synced!");
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, CloudTranslations.sync_apiExceptions_userDataIsNotSynced);
            }
            return new Unit();
        }

        public async Task SyncUsersAndCardCode(SyncCriticalUserDataCommand request)
        {
            foreach (var user in request.UserAndCardCodeDTOs)
            {
                var foundUser = await _context.CTAMUser().Where(u => u.UID.Equals(user.UID)).FirstOrDefaultAsync();
                if (foundUser != null)
                {
                    foundUser.CardCode = user.CardCode;
                }
            }
        }
    }

}
