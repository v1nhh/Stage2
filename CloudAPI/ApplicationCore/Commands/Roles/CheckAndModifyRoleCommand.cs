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
    public class CheckAndModifyRoleCommand: IRequest<RoleWebDTO>
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public string ValidFromDT { get; set; }

        public string ValidUntilDT { get; set; }

        // Permissions
        public IEnumerable<int> AddPermissionIDs { get; set; }

        public IEnumerable<int> RemovePermissionIDs { get; set; }

        // Users
        public IEnumerable<string> AddUserUIDs { get; set; }

        public IEnumerable<string> RemoveUserUIDs { get; set; }

        // ItemTypes
        public IEnumerable<int> AddItemTypeIDs { get; set; }

        public IEnumerable<int> RemoveItemTypeIDs { get; set; }

        public Dictionary<string, int> MaxQtyToPickPerItemTypeID { get; set; }

        // Cabinets
        public IEnumerable<string> AddCabinetNumbers { get; set; }

        public IEnumerable<string> RemoveCabinetNumbers { get; set; }

        // CabinetAccessIntervals
        public IEnumerable<CabinetAccessIntervalCreateDTO> AddCabinetAccessIntervals { get; set; }

        public IEnumerable<int> RemoveCabinetAccessIntervals { get; set; }
    }

    public class CheckAndModifyRoleHandler : IRequestHandler<CheckAndModifyRoleCommand, RoleWebDTO>
    {
        private readonly ILogger<CheckAndModifyRoleHandler> _logger;
        private readonly IMapper _mapper;
        private readonly CloudApiDataManager _cloudManager;

        public CheckAndModifyRoleHandler(ILogger<CheckAndModifyRoleHandler> logger, IMapper mapper, CloudApiDataManager cloudManager)
        {
            _logger = logger;
            _mapper = mapper;
            _cloudManager = cloudManager;
        }

        public async Task<RoleWebDTO> Handle(CheckAndModifyRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CheckAndModifyRoleHandler called");
            CTAMRole role;
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Check dependencies, throws if dependencychecks fails
                await _cloudManager.CheckAndDeleteCabinetStock(request.ID, request.RemoveItemTypeIDs, request.RemoveCabinetNumbers, false);
                role = await _cloudManager.ModifyRole(request.ID, request.Description, request.ValidFromDT, request.ValidUntilDT, 
                                                          request.AddPermissionIDs, request.RemovePermissionIDs, request.AddUserUIDs, request.RemoveUserUIDs);
                await _cloudManager.ModifyItemTypesForRoleAndSave(role, request.AddItemTypeIDs, request.RemoveItemTypeIDs, request.MaxQtyToPickPerItemTypeID);
                await _cloudManager.ModifyCabinetsForRoleAndSave(role, request.AddCabinetNumbers, request.RemoveCabinetNumbers);
                await _cloudManager.ModifyRoleCabinetAccessIntervals(role, request.AddCabinetAccessIntervals, request.RemoveCabinetAccessIntervals);
                await _cloudManager.AddMissingCabinetStockAndSave(role.ID);
                scope.Complete();
            }
            return _mapper.Map<RoleWebDTO>(role);
        }
    }
}
