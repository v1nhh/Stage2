using AutoMapper;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Exceptions;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;
using UserRoleModule.ApplicationCore.Utilities;


namespace CabinetModule.ApplicationCore.Commands.Cabinets
{
    public class CreateCabinetCommand : IRequest<CabinetDTO>
    {
        public string CabinetNumber { get; set; }

        public string Name { get; set; }

        public CabinetType CabinetType { get; set; }

        public string Description { get; set; }

        public LoginMethod LoginMethod { get; set; }

        public string LocationDescr { get; set; }

        public string Email { get; set; }

        public string CabinetErrorMessage { get; set; }

        public bool HasSwipeCardAssign { get; set; }

        public string CabinetLanguage { get; set; }

        public int NumberOfPositionsKeyCop { get; set; }

        public int NumberOfCentralDoors { get; set; }

        public int CabinetCellTypeIDKeyCop { get; set; }

        public int NumberOfPositionsLocker { get; set; }

        public int CabinetCellTypeIDLocker { get; set; }
    }

    public class CreateCabinetHandler : IRequestHandler<CreateCabinetCommand, CabinetDTO>
    {
        private readonly ILogger<CreateCabinetHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public CreateCabinetHandler(ILogger<CreateCabinetHandler> logger, MainDbContext context, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<CabinetDTO> Handle(CreateCabinetCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("CreateCabinetHandler called");

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_emptyName);
            }

            var cabinetNumber = await _context.Cabinet().AsNoTracking().FirstOrDefaultAsync(c => c.CabinetNumber == request.CabinetNumber);
            if (cabinetNumber != null)
            {
                throw new CustomException(System.Net.HttpStatusCode.FailedDependency, CloudTranslations.cabinets_apiExceptions_duplicateCabinetNumber, 
                                          new Dictionary<string, string> { { "cabinetNumber", request.CabinetNumber } });
            }

            // Valid CabinetCellType?
            if (request.NumberOfPositionsKeyCop > 0 || request.CabinetCellTypeIDKeyCop > 0)
            {
                var cabinetCellType = await _context.CabinetCellType().AsNoTracking().FirstOrDefaultAsync(cct => cct.ID == request.CabinetCellTypeIDKeyCop);
                if (cabinetCellType == null)
                {
                    throw new CustomException(System.Net.HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_cabinetCellTypeKeyCopNotFound,
                                              new Dictionary<string, string> { { "id", request.CabinetCellTypeIDKeyCop.ToString() } });
                }
                else if (request.NumberOfPositionsKeyCop <= 0)
                {
                    throw new CustomException(System.Net.HttpStatusCode.BadRequest, CloudTranslations.cabinets_apiExceptions_cabinetNumberOfPositionsKeyCopNotFound);

                }
            }
            if (request.NumberOfPositionsLocker > 0 || request.CabinetCellTypeIDLocker > 0)
            {
                var cabinetCellType = await _context.CabinetCellType().AsNoTracking().FirstOrDefaultAsync(cct => cct.ID == request.CabinetCellTypeIDLocker);
                if (cabinetCellType == null)
                {
                    throw new CustomException(System.Net.HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_cabinetCellTypeLockerNotFound,
                                              new Dictionary<string, string> { { "id", request.CabinetCellTypeIDLocker.ToString() } });
                }
                else if (request.NumberOfPositionsLocker <= 0)
                {
                    throw new CustomException(System.Net.HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_cabinetNumberOfPositionsLockerNotFound);

                }
            }
            var cab = await _context.Cabinet().AsNoTracking().FirstOrDefaultAsync(c => c.Name == request.Name);
            if (cab != null)
            {
                throw new CustomException(System.Net.HttpStatusCode.FailedDependency, CloudTranslations.cabinets_apiExceptions_duplicateName,
                                          new Dictionary<string, string> { { "name", request.Name } });
            }

            if (!String.IsNullOrEmpty(request.Email) && !ValidateEmail.IsValidEmail(request.Email))
            {
                throw new CustomException(System.Net.HttpStatusCode.NotFound, CloudTranslations.cabinets_apiExceptions_invalidEmail,
                                            new Dictionary<string, string> { { "email", request.Email } });
            }

            Cabinet newCabinet = await CreateCabinet(request);
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_cabinetCreated), ("cabinetNumber", newCabinet.Name));


            List<CabinetDoor> newCabinetDoors = new List<CabinetDoor>();
            
            if (request.NumberOfCentralDoors > 0)
            {
                CreateCabinetDoors(newCabinet.CabinetNumber, request.NumberOfCentralDoors, newCabinetDoors);
                await _context.CabinetDoor().AddRangeAsync(newCabinetDoors);
            }

            CreateCabinetPositions(request, newCabinet, newCabinetDoors);
            await _context.SaveChangesAsync();

            Entities.Cabinet cabinet = await _context.Cabinet().AsNoTracking()
                .FirstOrDefaultAsync(cabinet => cabinet.CabinetNumber == newCabinet.CabinetNumber);

            return _mapper.Map<CabinetDTO>(cabinet);
        }

        private void CreateCabinetPositions(CreateCabinetCommand request, Cabinet newCabinet, List<CabinetDoor> newCabinetDoors)
        {
            if (request.CabinetType == CabinetType.Locker)
            {
                request.NumberOfPositionsKeyCop = 0;
            }
            if (request.CabinetType == CabinetType.KeyConductor)

            {
                request.NumberOfPositionsLocker = 0;
            }

            int celltypeID = 0;
            CabinetDoor door = newCabinetDoors.FirstOrDefault();
            PositionType posType = PositionType.Keycop;
            int blade = 0;
            int positionOnBlade = 1;

            // Create CabinetPosition records
            for (int i = 0; i < (request.NumberOfPositionsKeyCop + request.NumberOfPositionsLocker); i++)
            {
                if (i < request.NumberOfPositionsKeyCop)
                {
                    // KeyCops
                    celltypeID = request.CabinetCellTypeIDKeyCop;
                    posType = PositionType.Keycop;
                    if ((i % 12) == 0)
                    {
                        blade++;
                        positionOnBlade = 1;
                    }
                }
                else
                {
                    // Lockers
                    celltypeID = request.CabinetCellTypeIDLocker;
                    door = null;
                    posType = PositionType.Locker;
                    if (((i - request.NumberOfPositionsKeyCop) % 10) == 0)
                    {
                        blade++;
                        positionOnBlade = 1;
                    }
                }
                // Nieuw CabonetPosition record
                var newCP = new CabinetPosition()
                {
                    Cabinet = newCabinet,
                    CabinetCellTypeID = celltypeID,
                    PositionAlias = $"{blade}.{positionOnBlade}",
                    PositionNumber = i + 1,
                    BladeNo = blade,
                    BladePosNo = positionOnBlade,
                    PositionType = posType,
                    CabinetDoor = door,
                    MaxNrOfItems = 1,
                    IsAllocated = false,
                    Status = CabinetPositionStatus.OK
                };
                positionOnBlade += 1;

                _context.CabinetPosition().Add(newCP);
            }
        }

        private async Task<Cabinet> CreateCabinet(CreateCabinetCommand request)
        {
            _logger.LogInformation($"CreateCabinet called! Cabinet name: {request.Name}");
            if (request.LoginMethod == LoginMethod.LoginCode_PinCode)
            {
                request.HasSwipeCardAssign = false;
            }
            var newCabinet = new Entities.Cabinet()
            {
                CabinetNumber = request.CabinetNumber,
                Name = request.Name,
                CabinetType = request.CabinetType,
                Description = request.Description,
                LoginMethod = request.LoginMethod,
                LocationDescr = request.LocationDescr,
                Email = request.Email,
                CabinetErrorMessage = request.CabinetErrorMessage,
                HasSwipeCardAssign = request.HasSwipeCardAssign,
                CabinetLanguage = request.CabinetLanguage,
                IsActive = false,
                Status = CabinetStatus.Initial
            };
            await _context.Cabinet().AddAsync(newCabinet);

            return newCabinet;
        }

        private void CreateCabinetDoors(string cabinetNumber, int numberOfDoors, List<CabinetDoor> cabinetDoors)
        {
            _logger.LogInformation($"CreateCabinetDoor called! Cabinet: {cabinetNumber}, number: {numberOfDoors}");
            for (int i = 1; i <= numberOfDoors; i++)
            {
                var newCabinetDoor = new CabinetDoor()
                {
                    Alias = $"{i}",
                    ClosedLevel = false,
                    GPIOPortDoorControl = 16,
                    GPIOPortDoorState = 24,
                    MaxOpenDuration = 30000,
                    UnlockDuration = 2000,
                    UnlockLevel = true,
                    CabinetNumber = cabinetNumber,
                };

                cabinetDoors.Add(newCabinetDoor);
            }
        }

        // voor het aanmaken van de configuratie json van de hardware api 
        private class CabinetConfiguration
        {
            public int nrblades { get; set; }
            public string kctype { get; set; }
            public bool uselfreader { get; set; }
            public bool usehfreader { get; set; }
            public List<PositionConfiguration> positions { get; set; }
        }

        private class PositionConfiguration
        {
            public int id { get; set; }
            public int bladeaddr { get; set; }
            public int bladeposno { get; set; }
            public string alias { get; set; }
            public string positiontype { get; set; }
            public int doorid { get; set; }
            public int nodelf { get; set; }
            public int nodehf { get; set; }
        }
    }

}
