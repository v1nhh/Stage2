using CloudAPI.ApplicationCore.Commands.Items;
using CloudAPI.ApplicationCore.Commands.Roles;
using CloudAPI.ApplicationCore.Commands.Users;
using CloudAPI.ApplicationCore.DTO.Import;
using CsvHelper;
using CsvHelper.Configuration;
using CTAM.Core.Exceptions;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using ItemModule.ApplicationCore.Commands.ErrorCodes;
using ItemModule.ApplicationCore.Commands.ItemTypes;
using ItemModule.ApplicationCore.DTO;
using ItemModule.ApplicationCore.DTO.Import;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Import;

namespace CloudAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly ILogger<ImportController> _logger;
        private readonly IMediator _mediator;

        public ImportController(ILogger<ImportController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("errorcodes")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        [RequestSizeLimit(10000000)] 
        public async Task<ActionResult> ImportErrorCodes(IFormFile file)
        {
            try
            {
                _logger.LogInformation($"Import errorcodes called with file {file.FileName}");
                return await ReadCsvToList<ErrorCodeImportDTO, ErrorCodeImportReturnDTO, ImportErrorCodesCommand>(file, new ErrorCodeImportDTOMap());
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("roles")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        [RequestSizeLimit(10000000)] 
        public async Task<ActionResult> ImportRoles(IFormFile file)
        {
            try
            {
                _logger.LogInformation($"Import roles called with file {file.FileName}");
                return await ReadCsvToList<RoleImportDTO, RoleImportReturnDTO, ImportRolesCommand>(file, new RoleImportDTOMap());
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("users")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        [RequestSizeLimit(10000000)] 
        public async Task<ActionResult> ImportUsers(IFormFile file)
        {
            try
            {
                _logger.LogInformation($"Import users called with file {file.FileName}");
                var res = await ReadCsvToList<UserImportDTO, UserImportReturnDTO, ImportUsersCommand>(file, new UserImportDTOMap());
                return res;
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("itemtypes")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        [RequestSizeLimit(10000000)] 
        public async Task<ActionResult> ImportItemTypes(IFormFile file)
        {
            try
            {
                _logger.LogInformation($"Import itemtypes called with file {file.FileName}");
                return await ReadCsvToList<ItemTypeImportDTO, ItemTypeImportReturnDTO, ImportItemTypesCommand>(file, new ItemTypeImportDTOMap());
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        [HttpPost("items")]
        [Authorize(Roles = "MANAGEMENT_WRITE")]
        [RequestSizeLimit(10000000)] 
        public async Task<ActionResult> ImportItems(IFormFile file)
        {
            try
            {
                _logger.LogInformation($"Import items called with file {file.FileName}");
                var res = await ReadCsvToList<ItemImportDTO, ItemImportReturnDTO, ImportItemsCommand>(file, new ItemImportDTOMap());
                return res;
            }
            catch (Exception ex)
            {
                return ex.GetObjectResult(_logger, this);
            }
        }

        private async Task<ActionResult> ReadCsvToList<ImportDTO, ReturnDTO, ImportCommand> (IFormFile file, ClassMap map) 
                                                      where ImportCommand : IRequest<List<ReturnDTO>>, IImportCommand<ImportDTO>, new()
        {
            if (file.Length > 0)
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" };
                List<ReturnDTO> result;

                using (var stream = file.OpenReadStream())
                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap(map);
                    var records = csv.GetRecords<ImportDTO>();
                    var lst = records.ToList();

                    if (lst.Count == 0)
                    {
                        return Ok(new List<ReturnDTO>());
                    }

                    var cmd = new ImportCommand() { InputList = lst, Filename = file.FileName };
                    result = await _mediator.Send(cmd);
                }
                return Ok(result);
            }
            throw new CustomException(System.Net.HttpStatusCode.BadRequest, CloudTranslations.csv_apiExceptions_noFile);
        }
    }
}
