using CabinetModule.ApplicationCore.Commands.Cabinets;
using CabinetModule.ApplicationCore.DTO;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CabinetModule.ApplicationCore.Queries.Cabinets;
using CabinetModule.ApplicationCore.Utilities;
using CTAM.Core.Utilities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace CabinetModule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CabinetsController : ControllerBase
    {
        private readonly ILogger<CabinetsController> _logger;
        private readonly IMediator _mediator;

        public CabinetsController(ILogger<CabinetsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<PaginatedResult<Cabinet, CabinetDTO>>> GetAllCabinets(
                                                [FromQuery] int page_limit = 50, [FromQuery] int page = 0,
                                                [FromQuery] string sortedBy = "", [FromQuery] bool sortDescending = false,
                                                [FromQuery] string filterQuery = "")
        {
            try
            {
                CabinetDisplayColumn? sortCol = null;
                if (Enum.TryParse<CabinetDisplayColumn>(sortedBy, out CabinetDisplayColumn col))
                {
                    sortCol = col;
                }
                var query = new GetAllCabinetsQuery(page_limit, page, sortCol, sortDescending, filterQuery);
                var cabinets = await _mediator.Send(query);
                return Ok(cabinets);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("{cabinetNumber}/general")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<CabinetDTO>> GetCabinetGeneral(string cabinetNumber)
        {
            try
            {
                var cabinet = await _mediator.Send(new GetCabinetGeneralQuery(cabinetNumber));
                return Ok(cabinet);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("{cabinetNumber}")]
        [Authorize(Roles = "MANAGEMENT_READ,MANAGEMENT_WRITE,CABINET_ACTIONS")]
        public async Task<ActionResult<CabinetDTO>> GetCabinetByCabinetNumber(string cabinetNumber)
        {
            _logger.LogInformation($"CabinetController: GetCabinetByCabinetNumber({cabinetNumber}) is called");
            try
            {
                var cabinet = await _mediator.Send(new GetCabinetByCabinetNumberQuery(cabinetNumber));
                return Ok(cabinet);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("config/ui")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<CabinetUIDTO>> GetConfigUI()
        {
            try
            {
                var result = await _mediator.Send(new GetCabinetUIConfigQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("config/ui/logo-white")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<CabinetUIDTO>> UploadLogoWhite([FromForm] IFormFile logo)
        {
            try
            {
                var result = await SaveLogo(logo, (string base64) => new UpdateCabinetUIConfigCommand { LogoWhite = base64 });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("config/ui/logo-black")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<CabinetUIDTO>> UploadLogoBlack([FromForm] IFormFile logo)
        {
            try
            {
                var result = await SaveLogo(logo, (string base64) => new UpdateCabinetUIConfigCommand { LogoBlack = base64 });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        private async Task<CabinetUIDTO> SaveLogo([FromForm] IFormFile logo, Func<string, UpdateCabinetUIConfigCommand> createUpdateCommand)
        {
            var types = new HashSet<string> { "image/png", "image/jpeg", "image/svg+xml" };
            var contentType = logo.ContentType;
            if (!types.Contains(contentType))
            {
                throw new ArgumentException("Logo must be of type: " + String.Join(", ", types));
            }

            _logger.LogInformation($"ContentType: {logo.ContentType}");
            _logger.LogInformation($"Filename: {logo.FileName}");
        
            Stream stream = logo.OpenReadStream();
            _logger.LogInformation(logo.FileName);
            byte[] bytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                bytes = memoryStream.ToArray();
            }
            string logoBase64 = Convert.ToBase64String(bytes);
            string completeLogo = $"data:{contentType};base64,{logoBase64}";
            var command = createUpdateCommand.Invoke(completeLogo);
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPatch("config/ui")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult<CabinetUIDTO>> UpdateConfig(UpdateCabinetUIConfigCommand updateCabinetUIConfig)
        {
            try
            {
                var result = await _mediator.Send(updateCabinetUIConfig);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("accessible")]
        [Authorize(Roles = "MANAGEMENT_READ")]
        public async Task<ActionResult<CabinetDTO>> GetCabinetsForLoggedInUser()
        {
            _logger.LogInformation($"CabinetController: GetCabinetsForLoggedInUser is called");
            try
            {
                var cabinets = await _mediator.Send(new GetAccessibleCabinetsByLoggedInUserQuery());
                return Ok(cabinets);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("{cabinetNumber}/doors")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<PaginatedResult<CabinetDoor, CabinetDoorDTO>>> GetCabinetDoors(string cabinetNumber)
        {
            _logger.LogInformation($"CabinetController: GetCabinetDoors({cabinetNumber}) is called");
            try
            {
                var cabinets = await _mediator.Send(new GetCabinetDoorsPaginatedQuery
                {
                    CabinetNumber = cabinetNumber,
                });
                return Ok(cabinets);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("{cabinetNumber}/showdoors")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult<bool>> ShowCabinetDoors(string cabinetNumber)
        {
            _logger.LogInformation($"CabinetController: ShowCabinetDoors({cabinetNumber}) is called");
            try
            {
                var show = await _mediator.Send(new ShowCabinetDoorsQuery
                {
                    CabinetNumber = cabinetNumber,
                });
                return Ok(show);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.GetMostInnerException().Message);
                return StatusCode((int)HttpStatusCode.BadRequest, new
                {
                    innerExceptionMessage = ex.GetMostInnerException().Message
                });
            }
        }

        // POST /api/cabinets
        [HttpPost]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public async Task<ActionResult> CreateCabinet(CreateCabinetCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("cabinetcelltypes")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult> GetAllCabinetCellTypes()
        {
            Console.WriteLine("CabinetController: GetAllCabinetCellTypes is called");
            try
            {
                var cabinetCellTypes = await _mediator.Send(new GetAllCabinetCellTypesQuery());
                return Ok(cabinetCellTypes);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("accessinterval/{id}")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult> GetCabinetAccessInterval(int id)
        {
            try
            {
                var cabinetAccessInterval = await _mediator.Send(new GetCabinetAccessIntervalByIdQuery(id));
                return Ok(cabinetAccessInterval);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        // POST /api/cabinets
        [HttpPut("{cabinetNumber}/properties")]
        [Authorize(Roles = "MANAGEMENT_READ,CABINET_ACTIONS")]
        public async Task<ActionResult> SetProperties(string cabinetNumber, SetCabinetPropertiesCommand command)
        {
            try
            {
                if (cabinetNumber != command.CabinetNumber)
                {
                    throw new Exception("Provided cabinet numbers are not equal");
                }
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpGet("cabinetNumber/generate")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        public ActionResult GetGeneratedCabinetNumber()
        {
            try
            {
                var result = Kcid.NewKcid();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }
    }
}
