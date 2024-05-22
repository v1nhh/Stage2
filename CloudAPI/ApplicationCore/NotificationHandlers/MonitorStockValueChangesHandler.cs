using CloudAPI.ApplicationCore.Commands.ItemCabinet;
using CloudAPI.Infrastructure;
using CTAM.Core;
using ItemCabinetModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.ApplicationCore.NotificationHandlers
{
    public class MonitorStockValueChangesHandler : INotificationHandler<EntitiesChangedNotification>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<MonitorStockValueChangesHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IManagementLogger _managementLogger;


        private readonly HashSet<Type> _stockEntity = new HashSet<Type>
        {
            typeof(CabinetStock)
        };

        public MonitorStockValueChangesHandler(MainDbContext context, ILogger<MonitorStockValueChangesHandler> logger, IMediator mediator, IManagementLogger managementLogger)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
            _managementLogger = managementLogger;

        }

        public async Task Handle(EntitiesChangedNotification notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("MonitorStockValueChangesHandler called");
            if (notification.EntitiesData.Count() == 0)
            {
                return;
            }
 
            var changedEntity = notification.EntitiesData
                .Where(entityData => _stockEntity.Contains(entityData.Entity.GetType()))
                .Select(entityData => new { Entity = entityData.Entity, OriginalEntity = entityData.OriginalEntity })
                .ToList();

            if (changedEntity.Count() == 0)
            {
                return;
            }

            foreach (var c in changedEntity)
            {
                CabinetStock cur = (CabinetStock)c.Entity;
                CabinetStock org = (CabinetStock)c.OriginalEntity;
                if ((cur.ActualStock != org?.ActualStock) && (cur.MinimalStock>0))  // only when actual stock changes and minimal stock level is set
                {
                    var itemType = await _context.ItemType().AsNoTracking().FirstOrDefaultAsync(itemtype => itemtype.ID == cur.ItemTypeID);

                    if (cur.ActualStock < cur.MinimalStock)
                    {
                        // stock below minimal stock level
                        await _mediator.Send(new StockBelowMinimumCommand()
                        {
                            CabinetNumber = cur.CabinetNumber,
                            ItemTypeID = cur.ItemTypeID
                        });
                    }
                    if (cur.ActualStock == cur.MinimalStock)
                    {
                        // Stock returned at minimal stock level
                        await _mediator.Send(new StockAtMinimalStockLevelCommand()
                        {
                            CabinetNumber = cur.CabinetNumber,
                            ItemTypeID = cur.ItemTypeID
                        });
                    }
                }
            }

            return;

        }

    }
}
