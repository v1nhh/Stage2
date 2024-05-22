using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DataManagers;

namespace CloudAPI.ApplicationCore.Queries.Users
{
    public class GetCTAMSettingQuery: IRequest<string>
    {
        public string Key { get; set; }
    }

    public class GetCTAMSettingHandler : IRequestHandler<GetCTAMSettingQuery, string>
    {
        private readonly UserRoleDataManager _userRoleDataManager;

        public GetCTAMSettingHandler(UserRoleDataManager userRoleDataManager)
        {
            _userRoleDataManager = userRoleDataManager;
        }

        public async Task<string> Handle(GetCTAMSettingQuery request, CancellationToken cancellationToken)
        {
            return await _userRoleDataManager.GetCTAMSetting(request.Key, "");
        }
    }
}
