using CommunicationModule.ApplicationCore.Commands;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.DataManagers;
using UserRoleModule.ApplicationCore.Interfaces;
using CTAMSharedLibrary.Resources;

namespace CloudAPI.ApplicationCore.Commands.ItemCabinet
{
    public class StockAtMinimalStockLevelCommand : IRequest
    {
        public string CabinetNumber { get; set; }
        public int ItemTypeID { get; set; }
    }

    public class StockAtMinimalStockLevelHandler : IRequestHandler<StockAtMinimalStockLevelCommand>
    {
        private readonly ILogger<StockAtMinimalStockLevelHandler> _logger;
        private readonly IMediator _mediator;
        private readonly MainDbContext _context;
        private readonly UserRoleDataManager _userRoleDataManager;
        private readonly IManagementLogger _managementLogger;

        public StockAtMinimalStockLevelHandler(ILogger<StockAtMinimalStockLevelHandler> logger, IMediator mediator, MainDbContext context, UserRoleDataManager userRoleDataManager, IManagementLogger managementLogger)
        {

            _logger = logger;
            _mediator = mediator;
            _context = context;
            _userRoleDataManager = userRoleDataManager;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(StockAtMinimalStockLevelCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("StockAtMinimalStockLevelHandler called");

            //Retrieve required information.
            var cabinetData = await _context.Cabinet()
                                .AsNoTracking()
                                .Where(c => c.CabinetNumber.Equals(command.CabinetNumber))
                                .Select(c => new { Name = c.Name, Email = c.Email, LocationDescr = c.LocationDescr })
                                .FirstOrDefaultAsync();

            var cabinetName = cabinetData.Name;
            var cabinetLocation = cabinetData.LocationDescr;
            var cabinetEmail = cabinetData.Email;

            var stockData = await _context.CabinetStock()
                            .AsNoTracking()
                            .Where(s => s.CabinetNumber.Equals(command.CabinetNumber) && s.ItemTypeID == command.ItemTypeID)
                            .Select(s => new { MinimalStock = s.MinimalStock, ActualStock = s.ActualStock })
                            .FirstOrDefaultAsync();

            var actualStock = stockData.ActualStock;
            var minimalStock = stockData.MinimalStock;

            var itemTypeDescription = await _context.ItemType()
                                            .AsNoTracking()
                                            .Where(i => i.ID == command.ItemTypeID)
                                            .Select(i => i.Description)
                                            .SingleOrDefaultAsync();

            var languageCode = await _userRoleDataManager.GetCTAMSetting(CTAMSettingKeys.EmailDefaultLanguage, "en-US");

            await _mediator.Send(new SendEmailFromTemplateCommand()
            {
                MailTemplateName = DefaultEmailTemplate.StockAtMinimalStockLevel.GetName(),
                LanguageCode = languageCode,
                MailTo = cabinetEmail,
                EmailValues = new Dictionary<string, string>()
                    {
                        {"itemTypeDescription", itemTypeDescription},
                        {"cabinetName", cabinetName},
                        {"cabinetLocationDescr", cabinetLocation },
                        {"cabinetNumber", command.CabinetNumber },
                        {"minimalStock", $"{minimalStock}" },
                    },
            });

            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailMinimalStockGoodAgain),
                ("cabinetNumber", command.CabinetNumber), 
                ("name", cabinetName), 
                ("location", cabinetLocation), 
                ("actualStock", actualStock.ToString()), 
                ("minimalStock", minimalStock.ToString()), 
                ("itemTypeDescription", itemTypeDescription), 
                ("email", cabinetEmail));
            return new Unit();
        }

    }
}
