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
    public class GetSettingsByKeyQuery : IRequest<List<CTAMSetting>>
    {
        public string Key { get; set; }
    }

    public class GetSettingsHandler : IRequestHandler<GetSettingsByKeyQuery, List<CTAMSetting>>
    {
        private readonly MainDbContext _context;

        public GetSettingsHandler(MainDbContext context)
        {
            _context = context;
        }

        public async Task<List<CTAMSetting>> Handle(GetSettingsByKeyQuery request, CancellationToken cancellationToken)
        {
            if (request.Key == null)
            {
                return await _context.CTAMSetting().ToListAsync();
            }
            else
            {
                var setting = await _context.CTAMSetting()
                    .Where(setting => setting.ParName == request.Key)
                    .ToListAsync();
                if (setting == null)
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.users_apiExceptions_settingWithKeyNotFound,
                                              new Dictionary<string, string> { { "key", request.Key } });
                }
                return setting;
            }
        }
    }

}
