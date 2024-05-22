using CabinetModule.ApplicationCore.Commands.Sync;
using CloudAPI.ApplicationCore.Commands.Cabinet;
using CloudAPI.ApplicationCore.Commands.Sync;
using CloudAPI.ApplicationCore.DTO;
using CloudAPI.ApplicationCore.Interfaces;
using CloudAPI.ApplicationCore.Queries;
using CTAM.Core.Exceptions;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "CABINET_ACTIONS,MANAGEMENT_READ")]
    public class SyncController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SyncController> _logger;
        private readonly ITenantContext _tenantContext;
        private readonly IFullSyncQueue _fullSyncQueue;

        public SyncController(ILogger<SyncController> logger, IMediator mediator, ITenantContext tenantContext, IFullSyncQueue fullSyncQueue)
        {
            _logger = logger;
            _mediator = mediator;
            _tenantContext = tenantContext;
            _fullSyncQueue = fullSyncQueue;
        }

        [HttpGet]
        public async Task<ActionResult<CriticalDataEnvelope>> GetCriticalData(string cabinetNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(cabinetNumber))
                {
                    throw new ArgumentException("Cabinet number cannot be null or empty.");
                }

                var query = new GetCriticalDataQuery(cabinetNumber);
                var result = await _mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // POST: api/Sync/C1/critical
        [HttpPost("{cabinet_number}/critical")]
        public async Task<ActionResult> SyncCriticalCabinetData(string cabinet_number, SyncCriticalDataCommand syncData)
        {
            try
            {
                if (!cabinet_number.Equals(syncData.CabinetNumber))
                {
                    throw new CustomException(System.Net.HttpStatusCode.BadRequest, CloudTranslations.sync_apiExceptions_notSameCabinetNumberOfCriticalData);
                }
                await _mediator.Send(syncData);
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // POST: api/Sync/C1/logs
        [HttpPost("{cabinet_number}/cabinetactions")]
        public async Task<ActionResult> SyncCabinetActions(string cabinet_number, SyncCabinetActionsCommand syncData)
        {
            if (!cabinet_number.Equals(syncData.CabinetNumber))
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, CloudTranslations.sync_apiExceptions_notSameCabinetNumberOfCriticalData);
            }
            try
            {
                await _mediator.Send(syncData);
                return Ok(syncData);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // POST: api/Sync/C1/logs
        [HttpPost("{cabinet_number}/cabinetlogs")]
        public async Task<ActionResult> SyncCabinetLogs(string cabinet_number, SyncCabinetLogsCommand syncData)
        {
            try
            {
                if (!cabinet_number.Equals(syncData.CabinetNumber))
                {
                    throw new CustomException(System.Net.HttpStatusCode.BadRequest, CloudTranslations.sync_apiExceptions_notSameCabinetNumberOfCriticalData);
                }
                await _mediator.Send(syncData);
                return Ok(syncData);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // POST: api/Sync/C1/status
        [HttpPost("{cabinet_number}/status")]
        public async Task<ActionResult> SyncCabinetStatus(string cabinet_number, SyncCabinetStatusCommand syncStatusCommand)
        {
            try
            {
                if (!cabinet_number.Equals(syncStatusCommand.CabinetNumber))
                {
                    throw new CustomException(System.Net.HttpStatusCode.BadRequest, CloudTranslations.sync_apiExceptions_notSameCabinetNumberOfCriticalData);
                }
                await _mediator.Send(syncStatusCommand);
                return Ok();
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("{cabinet_number}/enqueuefullsync")]
        public async Task<ActionResult> EnqueueForFullSync(string cabinet_number)
        {
            try
            {
                await _mediator.Send(new EnqueueForFullSyncCommand() { CabinetNumbers = new List<string>() { cabinet_number }, TenantID = _tenantContext.TenantId });
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetMostInnerException().Message);
                return BadRequest();
            }
        }

        [HttpPost("{cabinet_number}/continuefullsync")]
        public ActionResult ContinueFullSync(string cabinet_number)
        {
            try
            {
                _fullSyncQueue.IsWaitingToDequeue = false;
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetMostInnerException().Message);
                return BadRequest();
            }
        }
    }
}