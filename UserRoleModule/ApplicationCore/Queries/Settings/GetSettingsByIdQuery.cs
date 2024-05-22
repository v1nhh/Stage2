using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;

namespace UserRoleModule.ApplicationCore.Queries.Settings
{
    public class GetSettingsByIdQuery : IRequest<CTAMSetting>
    {
        public int ID { get; set; }
    }

    public class GetSettingsByIdQueryHandler : IRequestHandler<GetSettingsByIdQuery, CTAMSetting>
    {
        private readonly MainDbContext _context;

        public GetSettingsByIdQueryHandler(MainDbContext context)
        {
            _context = context;
        }

        public async Task<CTAMSetting> Handle(GetSettingsByIdQuery request, CancellationToken cancellationToken)
        {
            var setting = await _context.CTAMSetting()
                .Where(setting => setting.ID == request.ID)
                .FirstOrDefaultAsync();
            if (setting == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_settingWithIdNotFound,
                                          new Dictionary<string, string> { { "id", request.ID.ToString() } });
            }
            return setting;
        }
    }

}
