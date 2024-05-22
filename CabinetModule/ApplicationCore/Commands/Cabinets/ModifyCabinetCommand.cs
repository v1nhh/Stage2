using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CabinetModule.ApplicationCore.Commands.Cabinets
{
    public class ModifyCabinetCommand: IRequest<CabinetDTO>
    {
        public string CabinetNumber { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string LocationDescr { get; set; }

        public string Email { get; set; }

        public string CabinetErrorMessage { get; set; }

        public bool? HasSwipeCardAssign { get; set; }

        public string CabinetLanguage { get; set; }

        public LoginMethod? LoginMethod { get; set; }

        public bool? IsActive { get; set; }

        public CabinetType? CabinetType { get; set; }

        public string CabinetConfiguration { get; set; }

        public bool? RemoveConfiguration { get; set; }

        public IEnumerable<CabinetDoorDTO> CabinetDoors { get; set; }
    }

    public class ModifyCabinetHandler : IRequestHandler<ModifyCabinetCommand, CabinetDTO>
    {
        private readonly ILogger<ModifyCabinetHandler> _logger;
        private readonly IMapper _mapper;
        private readonly MainDbContext _context;
        private readonly IManagementLogger _managementLogger;

        public ModifyCabinetHandler(ILogger<ModifyCabinetHandler> logger, IMapper mapper, MainDbContext context, IManagementLogger managementLogger)
        {
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _managementLogger = managementLogger;
        }

        public async Task<CabinetDTO> Handle(ModifyCabinetCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ModifyCabinetHandler is called");

            CheckAllCommandParameters(command);
            var cabinet = await GetCabinetWithConfigurations(command.CabinetNumber);
            await ModifyCabinet(command, cabinet);

            await _context.SaveChangesAsync();

            var modifiedCabinet = await _context.Cabinet()
                .AsNoTracking()
                .Where(c => c.CabinetNumber.Equals(command.CabinetNumber))
                .FirstOrDefaultAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_cabinetModified), ("name", modifiedCabinet.Name));
            return _mapper.Map<CabinetDTO>(modifiedCabinet);
        }

        public void CheckAllCommandParameters(ModifyCabinetCommand command)
        {
            if (command.CabinetNumber == null)
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_emptyNumber);
            }
            if (command.CabinetType == null && command.IsActive == null && command.RemoveConfiguration == null && command.CabinetConfiguration == null
                && command.Name == null && command.Description == null && command.LocationDescr == null && command.Email == null 
                && command.LoginMethod == null && command.CabinetErrorMessage == null && command.HasSwipeCardAssign == null && command.CabinetLanguage == null
                && (command.CabinetDoors == null || command.CabinetDoors.Count() == 0))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_noData,
                                                                 new Dictionary<string, string> { { "cabinetNumber", command.CabinetNumber } });
            }

            if (command.Name != null && string.IsNullOrWhiteSpace(command.Name))
            {
                throw new CustomException(HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_emptyName);
            }
        }

        // Get cabinet with all needed configurations
        public async Task<Entities.Cabinet> GetCabinetWithConfigurations(string cabinetNumber)
        {
            var cabinet = await _context.Cabinet()
                .Where(c => c.CabinetNumber.Equals(cabinetNumber))
                .Include(cabinet => cabinet.CabinetPositions)
                .Include(cabinet => cabinet.CabinetColumns)
                .FirstOrDefaultAsync();
            if (cabinet == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_notFound,
                                          new Dictionary<string, string> { { "cabinetNumber", cabinetNumber } });
            }
            return cabinet;
        }

        // Make changes to the cabinet according to command parameters
        public async Task ModifyCabinet(ModifyCabinetCommand command, Entities.Cabinet cabinet)
        {
            if (command.LoginMethod == LoginMethod.LoginCode_PinCode)
            {
                command.HasSwipeCardAssign = false;
            }
            if (command.Name != null)
            {
                cabinet.Name = command.Name;
            }

            if (command.Description != null)
            {
                cabinet.Description = command.Description;
            }

            if (command.LocationDescr != null)
            {
                cabinet.LocationDescr = command.LocationDescr;
            }

            if (command.Email != null)
            {
                cabinet.Email = command.Email;
            }

            if (command.LoginMethod.HasValue)
            {
                cabinet.LoginMethod = command.LoginMethod.Value;
            }

            if (command.IsActive.HasValue)
            {
                cabinet.IsActive = command.IsActive.Value;
            }

            if (command.HasSwipeCardAssign.HasValue)
            {
                cabinet.HasSwipeCardAssign = command.HasSwipeCardAssign.Value;
            }
            if (command.CabinetType.HasValue)
            {
                if (cabinet.CabinetPositions.Any())
                {
                    throw new CustomException(HttpStatusCode.FailedDependency, CloudTranslations.cabinets_apiExceptions_modifyCabinetTypeWithPositions);
                }
                cabinet.CabinetType = command.CabinetType.Value;
            }
            if (command.RemoveConfiguration.HasValue && command.RemoveConfiguration.Value)
            {
                if (cabinet.IsActive)
                {
                    throw new CustomException(HttpStatusCode.FailedDependency, CloudTranslations.cabinets_apiExceptions_cabinetConfigRemoveWhileActive);
                }
                cabinet.CabinetConfiguration = null;
                _context.RemoveRange(cabinet.CabinetPositions);
                _context.RemoveRange(cabinet.CabinetColumns);
            }
            if (command.CabinetErrorMessage != null)
            {
                cabinet.CabinetErrorMessage = command.CabinetErrorMessage;
            }

            if (command.CabinetLanguage != null) 
            {
                cabinet.CabinetLanguage = command.CabinetLanguage;
            }

            if (command.CabinetConfiguration != null)
            {
                // TODO: Check serializable
                cabinet.CabinetConfiguration = command.CabinetConfiguration;
            }

            if (command.CabinetDoors != null && command.CabinetDoors.Count() > 0)
            {
                var ids = command.CabinetDoors.Select(cd => cd.ID).ToList();
                var dbDoors = await _context.CabinetDoor().Where(cd => ids.Contains(cd.ID)).ToListAsync();
                foreach (var upd in command.CabinetDoors)
                {
                    var db = dbDoors.Where(cd => cd.ID == upd.ID).FirstOrDefault();
                    if (db != null)
                    {
                        _mapper.Map(upd, db);
                    }
                }
            }
        }
    }
}
