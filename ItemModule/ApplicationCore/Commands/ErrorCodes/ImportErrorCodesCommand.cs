using AutoMapper;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAMSharedLibrary.Resources;
using ItemModule.ApplicationCore.DTO.Import;
using ItemModule.ApplicationCore.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Interfaces;

namespace ItemModule.ApplicationCore.Commands.ErrorCodes
{
    public class ImportErrorCodesCommand : IRequest<List<ErrorCodeImportReturnDTO>>, IImportCommand<ErrorCodeImportDTO>
    {
        public List<ErrorCodeImportDTO> InputList { get; set; }
        public string Filename { get; set; }
    }

    public class ImportErrorCodesHandler : IRequestHandler<ImportErrorCodesCommand, List<ErrorCodeImportReturnDTO>>
    {
        private readonly MainDbContext _context;
        private readonly ILogger<ImportErrorCodesHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public ImportErrorCodesHandler(MainDbContext context, ILogger<ImportErrorCodesHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _managementLogger = managementLogger;
        }

        public async Task<List<ErrorCodeImportReturnDTO>> Handle(ImportErrorCodesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ImportErrorCodesHandler called");
            var impErrorCodes = _mapper.Map<List<ErrorCodeImportReturnDTO>>(request.InputList); // Create room for ErrorMessage
            var invalids = impErrorCodes.Where(ec => (string.IsNullOrEmpty(ec.Code) || ec.Code.Length > 10) ||
                                                     (string.IsNullOrEmpty(ec.Description) || ec.Description.Length > 250))
                                        .Select(ec => { ec.ErrorMessage = "Field restrictions"; return ec; })
                                        .ToList();
            var validImports = impErrorCodes.Except(invalids).ToList();

            // Check double descriptions in import list
            var doubleDescriptions = validImports.GroupBy(ec => ec.Description).Where(g => g.Count() > 1);
            foreach(var dbl in doubleDescriptions)
            {
                var extraImportsDescr = dbl.Skip(1).Select(ec => { ec.ErrorMessage = "Description not unique in import"; return ec; }).ToList();
                invalids.AddRange(extraImportsDescr);
                validImports = validImports.Except(extraImportsDescr).ToList();
            }
            var uniqueDescriptions = validImports.Select(ec => ec.Description).ToList();

            // Check for already existing descriptions in database
            var dbExistingDescr = await _context.ErrorCode().Where(ec => uniqueDescriptions.Contains(ec.Description)).ToListAsync();
            int totalChanged = 0, totalUnchanged = 0;
            foreach (var dbec in dbExistingDescr)
            {
                var impDblDesc = validImports.Where(ec => ec.Description.Equals(dbec.Description, StringComparison.InvariantCultureIgnoreCase))
                                             .Select(ec => { ec.ErrorMessage = "Description not unique in database"; return ec; })
                                             .FirstOrDefault();
                if (impDblDesc.Code != dbec.Code) // If the db entry is exactly the same as the import, just skip, otherwise it's an error
                {
                    invalids.Add(impDblDesc);
                }

                if (impDblDesc.Description == dbec.Description) // If only case differs it should still be changed
                {
                    totalUnchanged++;
                    validImports.Remove(impDblDesc);
                }
            }

            var dictByCode = validImports.GroupBy(ec => ec.Code)
                                         .ToDictionary(g => g.Key, g => g.Last()); // With double entries, take the last

            var uniqueImportCodes = dictByCode.Keys;
            var dbErrorCodes = await _context.ErrorCode().Where(ec => uniqueImportCodes.Contains(ec.Code)).ToListAsync();
            var dbCodes = dbErrorCodes.Select(ec => ec.Code);
            foreach(var ec in dbErrorCodes)
            {
                if (ec.Description != dictByCode[ec.Code].Description)
                {
                    ec.Description = dictByCode[ec.Code].Description;
                    totalChanged++;
                }
                else
                {
                    totalUnchanged++;
                }
            }

            var newErrorCodes = dictByCode.Values.Where(imp => !dbCodes.Contains(imp.Code)).ToList();
            _context.ErrorCode().AddRange(_mapper.Map<List<ErrorCode>>(newErrorCodes));

            await _context.SaveChangesAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_importedErrorCodes),
                ("fileName", request.Filename), 
                ("newErrorCodes", newErrorCodes.Count().ToString()),
                ("changedErrorCodes", totalChanged.ToString()),
                ("unchangedErrorCodes", totalUnchanged.ToString()),
                ("invalidErrorCodes", invalids.Count().ToString()));
            return invalids;
        }
    }

}
