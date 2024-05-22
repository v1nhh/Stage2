using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CloudApiModule.ApplicationCore.DataManagers;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using UserRoleModule.ApplicationCore.DTO.Web;
using UserRoleModule.ApplicationCore.Entities;

namespace CloudAPI.ApplicationCore.Commands.Roles
{
    public class CreateRoleWithDependenciesCommand : IRequest<RoleWebDTO>
    {
        public string Description { get; set; }

        public string ValidFromDT { get; set; }

        public string ValidUntilDT { get; set; }

        public IEnumerable<int> AddPermissionIDs { get; set; }

        public IEnumerable<string> AddUserUIDs { get; set; }

        public IEnumerable<int> AddItemTypeIDs { get; set; }

        public Dictionary<string, int> MaxQtyToPickPerItemTypeID { get; set; }

        public IEnumerable<string> AddCabinetNumbers { get; set; }
        
        public IEnumerable<CabinetAccessIntervalCreateDTO> AddCabinetAccessIntervals { get; set; }
    }

    public class CreateRoleWithDependenciesHandler : IRequestHandler<CreateRoleWithDependenciesCommand, RoleWebDTO>
    {
        private readonly ILogger<CreateRoleWithDependenciesHandler> _logger;
        private readonly IMapper _mapper;
        private readonly CloudApiDataManager _cloudManager;

        public CreateRoleWithDependenciesHandler(ILogger<CreateRoleWithDependenciesHandler> logger, IMapper mapper, CloudApiDataManager cloudManager)
        {
            _logger = logger;
            _mapper = mapper;
            _cloudManager = cloudManager;
        }

        public async Task<RoleWebDTO> Handle(CreateRoleWithDependenciesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateRoleWithDependenciesHandler called");
            CTAMRole role;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                role = await _cloudManager.CreateRole(request.Description, request.ValidFromDT, request.ValidUntilDT, request.AddPermissionIDs, request.AddUserUIDs,
                                                      request.AddItemTypeIDs, request.MaxQtyToPickPerItemTypeID, request.AddCabinetNumbers, request.AddCabinetAccessIntervals);
                await _cloudManager.AddMissingCabinetStockAndSave(role.ID);

                scope.Complete();
            }
            return _mapper.Map<RoleWebDTO>(role);
        }
    }

}
