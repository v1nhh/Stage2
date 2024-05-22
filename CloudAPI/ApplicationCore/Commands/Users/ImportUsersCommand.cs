using AutoMapper;
using CTAM.Core;
using CTAM.Core.Interfaces;
using CTAM.Core.Utilities;
using CTAMSharedLibrary.Resources;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.DTO.Import;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Interfaces;

namespace CloudAPI.ApplicationCore.Commands.Users
{
    public class ImportUsersCommand : IRequest<List<UserImportReturnDTO>>, IImportCommand<UserImportDTO>
    {
        public List<UserImportDTO> InputList { get; set; }
        public string Filename { get; set; }
    }

    public class ImportUsersHandler : IRequestHandler<ImportUsersCommand, List<UserImportReturnDTO>>
    {
        private readonly ILogger<ImportUsersHandler> _logger;
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;
        private readonly IManagementLogger _managementLogger;

        public ImportUsersHandler(MainDbContext context, ILogger<ImportUsersHandler> logger, IMapper mapper, IManagementLogger managementLogger)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _managementLogger = managementLogger;
            if (!_context.Database.ProviderName.Equals("Microsoft.EntityFrameworkCore.InMemory"))
            {
                _context.Database.SetCommandTimeout(180);
            }
        }

        public async Task<List<UserImportReturnDTO>> Handle(ImportUsersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("ImportUsersHandler called");
            var impUsers = _mapper.Map<List<UserImportReturnDTO>>(request.InputList); // Create room for ErrorMessage
            var invalids = impUsers.Where(u => (string.IsNullOrWhiteSpace(u.UID) || u.UID.Length > 50) ||
                                                (string.IsNullOrWhiteSpace(u.Name) || u.Name.Length > 250) ||
                                                (string.IsNullOrWhiteSpace(u.Email) || u.Email.Length > 250) || !IsValidEmail(u.Email) ||
                                                (!string.IsNullOrEmpty(u.CardCode) && u.CardCode.Length > 50) ||
                                                (!string.IsNullOrEmpty(u.CardCode) && u.CardCode.Trim().Length == 0))
                                        .Select(ec => { ec.ErrorMessage = "Field restrictions"; return ec; })
                                        .ToList();
            var validImports = impUsers.Except(invalids).ToList();

            // Check double UID's in import list
            var doubleUIDs = validImports.GroupBy(u => u.UID).Where(g => g.Count() > 1);
            foreach (var dbl in doubleUIDs)
            {
                var extraImportsUID = dbl.Take(dbl.Count() - 1).Select(u => { u.ErrorMessage = "UID not unique in import"; return u; }).ToList();
                invalids.AddRange(extraImportsUID);
                validImports = validImports.Except(extraImportsUID).ToList();
            }

            // Check double Email in import list
            var doubleEmails = validImports.GroupBy(u => u.Email).Where(g => g.Count() > 1);
            foreach (var dbl in doubleEmails)
            {
                var extraImports = dbl.Take(dbl.Count() - 1).Select(u => { u.ErrorMessage = "Email not unique in import"; return u; }).ToList();
                invalids.AddRange(extraImports);
                validImports = validImports.Except(extraImports).ToList();
            }

            // Check double CardCode in import list
            var doubleCardCode = validImports.GroupBy(u => u.CardCode).Where(g => g.Count() > 1 && !string.IsNullOrEmpty(g.Key));
            foreach (var dbl in doubleCardCode)
            {
                var extraImports = dbl.Take(dbl.Count() - 1).Select(u => { u.ErrorMessage = "CardCode not unique in import"; return u; }).ToList();
                invalids.AddRange(extraImports);
                validImports = validImports.Except(extraImports).ToList();
            }

            // Check for already existing Email in database
            var uniqueEmails = validImports.Select(u => u.Email).ToList();
            var dbExistingEmail = new List<CTAMUser>();

            // Partition the list of uniqueEmails to avoid:
            // "Internal error: An expression services limit has been reached. Please look for potentially complex expressions in your query, and try to simplify them."
            foreach (var chunk in uniqueEmails.Partition(15000))
            {
                dbExistingEmail.AddRange(await _context.CTAMUser().Where(u => chunk.Contains(u.Email)).ToListAsync());
            }

            foreach (var dbu in dbExistingEmail)
            {
                var impDbl = validImports.Where(u => u.Email.Equals(dbu.Email, StringComparison.InvariantCultureIgnoreCase))
                                                .Select(u => { u.ErrorMessage = "Email not unique in database"; return u; })
                                                .FirstOrDefault();
                if (impDbl.UID != dbu.UID) // If the db UID differs with the import, it's an error
                {
                    invalids.Add(impDbl);
                    validImports.Remove(impDbl);
                }
            }

            // Check for already existing CardCode in database
            var uniqueCardCodes = validImports.Where(u => !string.IsNullOrEmpty(u.CardCode)).Select(u => u.CardCode).ToList();
            var dbExistingCardCodes = new List<CTAMUser>();
            // Partition the list of uniqueCardCodes to avoid:
            // "Internal error: An expression services limit has been reached. Please look for potentially complex expressions in your query, and try to simplify them."
            foreach (var chunk in uniqueCardCodes.Partition(15000))
            {
                dbExistingCardCodes.AddRange(await _context.CTAMUser().Where(u => chunk.Contains(u.CardCode)).ToListAsync());
            }

            foreach (var dbu in dbExistingCardCodes)
            {
                var impDbl = validImports.Where(u => u.CardCode.Equals(dbu.CardCode, StringComparison.InvariantCultureIgnoreCase))
                                                .Select(u => { u.ErrorMessage = "CardCode not unique in database"; return u; })
                                                .FirstOrDefault();
                if (impDbl.UID != dbu.UID) // If the db UID differs with the import, it's an error
                {
                    invalids.Add(impDbl);
                    validImports.Remove(impDbl);
                }
            }

            var uniqueUIDs = validImports.Select(u => u.UID).ToList();

            // Find already existing Users in database
            var dbExisting = new List<CTAMUser>();
            // Partition the list of uniqueUIDs to avoid:
            // "Internal error: An expression services limit has been reached. Please look for potentially complex expressions in your query, and try to simplify them."
            foreach (var chunk in uniqueUIDs.Partition(15000))
            {
                dbExisting.AddRange(await _context.CTAMUser().Where(u => chunk.Contains(u.UID)).ToListAsync());
            }
            var dictByUID = validImports.GroupBy(u => u.UID)
                                        .ToDictionary(g => g.Key, g => g.Last());
            int totalChanged = 0;
            int totalUnchanged = 0;
            foreach (var dbu in dbExisting)
            {
                bool changed = false;
                if (dbu.Name != dictByUID[dbu.UID].Name)
                {
                    dbu.Name = dictByUID[dbu.UID].Name;
                    changed = true;
                }

                if (dbu.Email != dictByUID[dbu.UID].Email)
                {
                    dbu.Email = dictByUID[dbu.UID].Email;
                    changed = true;
                }

                if (!string.IsNullOrWhiteSpace(dictByUID[dbu.UID].CardCode))
                {
                    if (dbu.CardCode != dictByUID[dbu.UID].CardCode)
                    {
                        dbu.CardCode = dictByUID[dbu.UID].CardCode;
                        changed = true;
                    }
                }

                if (changed)
                {
                    totalChanged++;
                }
                else
                {
                    totalUnchanged++;
                }
            }

            var dbExistingUIDs = dbExisting.Select(u => u.UID).ToList();
            var newUsers = validImports.Where(imp => !dbExistingUIDs.Contains(imp.UID));
            var newCTAMUsers = _mapper.Map<List<CTAMUser>>(newUsers);
            AddLoginCodesAndPincodes(newCTAMUsers);
            _context.CTAMUser().AddRange(newCTAMUsers);

            await _context.SaveChangesAsync();
            await _managementLogger.LogInfo(nameof(CloudTranslations.managementLog_importedUsers),
                ("fileName", request.Filename),
                ("newUsers", newUsers.Count().ToString()),
                ("changedUsers", totalChanged.ToString()),
                ("unchangedUsers", totalUnchanged.ToString()),
                ("invalidUsers", invalids.Count().ToString()));
            return invalids;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private void AddLoginCodesAndPincodes(List<CTAMUser> users)
        {
            var random = new Random();
            foreach (var user in users)
            {
                user.PinCode = random.Next(0, 99999).ToString("000000");
                user.LoginCode = user.UID; // Uforce number
            }
        }
    }
}
