using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CommunicationModule.ApplicationCore.Commands;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core;
using MediatR;
using UserRoleModule.ApplicationCore.DataManagers;
using UserRoleModule.ApplicationCore.Interfaces;
using UserRoleModule.ApplicationCore.Constants;
using CTAMSharedLibrary.Resources;

namespace CloudAPI.ApplicationCore.Commands.ItemCabinet
{
    public class StockBelowMinimumCommand : IRequest
    {
        public string CabinetNumber { get; set; }
        public int ItemTypeID { get; set; }
    }

    public class StockBelowMinimumHandler : IRequestHandler<StockBelowMinimumCommand>
    {
        private readonly ILogger<StockBelowMinimumHandler> _logger;
        private readonly IMediator _mediator;
        private readonly MainDbContext _context;
        private readonly UserRoleDataManager _userRoleDataManager;
        private readonly IManagementLogger _managementLogger;

        public StockBelowMinimumHandler(ILogger<StockBelowMinimumHandler> logger, IMediator mediator, MainDbContext context, UserRoleDataManager userRoleDataManager, IManagementLogger managementLogger)
        {
            
            _logger = logger;
            _mediator = mediator;
            _context = context;
            _userRoleDataManager = userRoleDataManager;
            _managementLogger = managementLogger;
        }

        public async Task<Unit> Handle(StockBelowMinimumCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation("StockBelowMinimumHandler called");

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
            .FirstOrDefaultAsync();

            var languageCode = await _userRoleDataManager.GetCTAMSetting(CTAMSettingKeys.EmailDefaultLanguage, "en-US");

            //Send email Stock below minimal.
            await _mediator.Send(new SendEmailFromTemplateCommand()
            {
                MailTemplateName = DefaultEmailTemplate.StockBelowMinimum.GetName(),
                LanguageCode = languageCode,
                MailTo = cabinetEmail,
                EmailValues = new Dictionary<string, string>()
                    {
                        {"itemTypeDescription", itemTypeDescription},
                        {"cabinetName", cabinetName},
                        {"cabinetLocationDescr", cabinetLocation },
                        {"cabinetNumber", command.CabinetNumber },
                        {"actualStock", $"{actualStock}" },
                        {"minimalStock", $"{minimalStock}" },
                    },
            });

            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_emailMinimalStockWarning),
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
