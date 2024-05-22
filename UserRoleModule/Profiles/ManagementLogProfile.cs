using AutoMapper;
using CTAMSharedLibrary.Resources;
using CTAMSharedLibrary.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UserRoleModule.ApplicationCore.DTO;
using UserRoleModule.ApplicationCore.Entities;

namespace UserRoleModule.ApplicationCore.Profiles
{
    public class ManagementLogProfile : Profile
    {
        private string GetLogString(string logResourcePath, string parameters, ResolutionContext context)
        {
            // In case of a rollback. Backwards compatibility for Migration: 20231016120948_AddManagementLogParametersColumn
            if (parameters == null)
            {
                parameters = "[]";
            }

            var resource = CloudTranslations.ResourceManager.GetString(logResourcePath);
            if (string.IsNullOrEmpty(resource))
            {
                return logResourcePath;
            }

            var translation = string.Empty;

            // Handle modified CTAMUser logs
            if (logResourcePath.Equals(TranslationUtils.GetResourceName(nameof(CloudTranslations.managementLog_editedUser))))
            {
                var prefixForChanges = TranslationUtils.GetTranslation(resource, TranslationUtils.Deserialize(parameters));
                var changes = ConvertChangesModifiedUserString(TranslationUtils.Deserialize(parameters).ToList());
                translation = string.Concat(prefixForChanges, changes);
                return translation;
            }

            // Handle modified Item logs
            if (logResourcePath.Equals(TranslationUtils.GetResourceName(nameof(CloudTranslations.managementLog_editedItem))))
            {
                var prefixForChanges = TranslationUtils.GetTranslation(resource, TranslationUtils.Deserialize(parameters));
                var changes = ConvertChangesModifiedItemString(TranslationUtils.Deserialize(parameters).ToList());
                translation = string.Concat(prefixForChanges, changes);
                return translation;
            }

            // Handle modified ErrorCode logs
            if (logResourcePath.Equals(TranslationUtils.GetResourceName(nameof(CloudTranslations.managementLog_editedErrorCode))))
            {
                var prefixForChanges = TranslationUtils.GetTranslation(resource, TranslationUtils.Deserialize(parameters));
                var changes = ConvertChangesModifiedErrorCodeString(TranslationUtils.Deserialize(parameters).ToList());
                translation = string.Concat(prefixForChanges, changes);
                return translation;
            }

            // Handle modified ItemType logs
            if (logResourcePath.Equals(TranslationUtils.GetResourceName(nameof(CloudTranslations.managementLog_editedItemType))))
            {
                var prefixForChanges = TranslationUtils.GetTranslation(resource, TranslationUtils.Deserialize(parameters));
                var changes = ConvertChangesModifiedItemTypeString(TranslationUtils.Deserialize(parameters).ToList());
                translation = string.Concat(prefixForChanges, changes);
                return translation;
            }

            // Handle modified Role logs
            if (logResourcePath.Equals(TranslationUtils.GetResourceName(nameof(CloudTranslations.managementLog_editedRole))))
            {
                var prefixForChanges = TranslationUtils.GetTranslation(resource, TranslationUtils.Deserialize(parameters));
                var changes = ConvertChangesModifiedRoleString(TranslationUtils.Deserialize(parameters).ToList(), context);
                translation = string.Concat(prefixForChanges, changes);
                return translation;
            }

            // Handle created Role logs
            if (logResourcePath.Equals(TranslationUtils.GetResourceName(nameof(CloudTranslations.managementLog_createdRole))))
            {
                var prefixForChanges = TranslationUtils.GetTranslation(resource, TranslationUtils.Deserialize(parameters));
                var changes = ConvertChangesCreatedRoleString(TranslationUtils.Deserialize(parameters).ToList(), context);
                translation = string.Concat(prefixForChanges, changes);
                return translation;
            }

            // Handle added/removed permission to/from role logs
            if (logResourcePath.Equals(TranslationUtils.GetResourceName(nameof(CloudTranslations.managementLog_deletedPermissionsFromRole)))
                    || logResourcePath.Equals(TranslationUtils.GetResourceName(nameof(CloudTranslations.managementLog_addedPermissionsToRole))))
            {
                var keyValues = TranslationUtils.Deserialize(parameters).ToList();
                var indexPermissions = keyValues.FindIndex(item => item.key.Equals("permissions"));

                keyValues[indexPermissions] = (keyValues[indexPermissions].key, GetPermissionTranslations(keyValues[indexPermissions].value));
                translation = TranslationUtils.GetTranslation(resource, keyValues.ToArray());
                return translation;
            }

            translation = TranslationUtils.GetTranslation(resource, TranslationUtils.Deserialize(parameters));
            return translation;
        }

        public ManagementLogProfile()
        {
            CreateMap<ManagementLog, ManagementLogDTO>()
                .ForMember(dto => dto.Log, opt => opt.MapFrom((src, dest, destMember, context) => GetLogString(src.LogResourcePath, src.Parameters, context)))
                .ForMember(dto => dto.LogDT, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LogDT, DateTimeKind.Utc)));
        }

        private static string GetPermissionTranslations(string permissionsString)
        {
            var permissions = permissionsString.Split(',').Select(s => s.Trim());
            var translatedPermissions = permissions.Select(pd => CloudTranslations.ResourceManager.GetString($"permission.{pd.ToLower()}"));
            var translatedPermissionsString = string.Join(", ", translatedPermissions);
            return translatedPermissionsString;
        }

        private static string ConvertChangesCreatedRoleString(List<(string key, string value)> changes, ResolutionContext context)
        {
            var userTimezone = context.Options.Items.ContainsKey("User-Timezone") ? context.Options.Items["User-Timezone"].ToString() : null;
            TimeZoneInfo timeZoneInfo = !string.IsNullOrEmpty(userTimezone) ? TimeZoneInfo.FindSystemTimeZoneById(userTimezone) : TimeZoneInfo.Utc;

            var changesTranslation = new List<string>();

            if (changes.Any(item => item.key.Equals("newDescription")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_createdRole_description, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newValidFromDT")))
            {
                var indexNewValidFromDT = changes.FindIndex(item => item.key.Equals("newValidFromDT"));

                var newValidFromDT = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(changes[indexNewValidFromDT].value), timeZoneInfo).ToString();

                changes[indexNewValidFromDT] = (changes[indexNewValidFromDT].key, newValidFromDT);
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_createdRole_validFromDT, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newValidUntilDT")))
            {
                var indexNewValidUntilDT = changes.FindIndex(item => item.key.Equals("newValidUntilDT"));

                var newValidUntilDT = TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(changes[indexNewValidUntilDT].value), timeZoneInfo).ToString();

                changes[indexNewValidUntilDT] = (changes[indexNewValidUntilDT].key, newValidUntilDT);
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_createdRole_validUntilDT, changes.ToArray()));
            }

            var result = string.Join(", ", changesTranslation.Select(line => $"{line}"));
            return result;
        }

        private static string ConvertChangesModifiedRoleString(List<(string key, string value)> changes, ResolutionContext context)
        {
            var userTimezone = context.Options.Items.ContainsKey("User-Timezone") ? context.Options.Items["User-Timezone"].ToString() : null;
            TimeZoneInfo timeZoneInfo = !string.IsNullOrEmpty(userTimezone) ? TimeZoneInfo.FindSystemTimeZoneById(userTimezone) : TimeZoneInfo.Utc;

            var changesTranslation = new List<string>();

            if (changes.Any(item => item.key.Equals("newDescription")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedRole_description, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newValidFromDT")))
            {
                var indexOldValidFromDT = changes.FindIndex(item => item.key.Equals("oldValidFromDT"));
                var indexNewValidFromDT = changes.FindIndex(item => item.key.Equals("newValidFromDT"));

                
                var oldValidFromDT = changes[indexOldValidFromDT].value.Equals(string.Empty) ? CloudTranslations.general_none : (TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(changes[indexOldValidFromDT].value), timeZoneInfo)).ToString();
                var newValidFromDT = changes[indexNewValidFromDT].value.Equals(string.Empty) ? CloudTranslations.general_none : (TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(changes[indexNewValidFromDT].value), timeZoneInfo)).ToString();

                changes[indexOldValidFromDT] = (changes[indexOldValidFromDT].key, oldValidFromDT);
                changes[indexNewValidFromDT] = (changes[indexNewValidFromDT].key, newValidFromDT);
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedRole_validFromDT, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newValidUntilDT")))
            {
                var indexOldValidUntilDT = changes.FindIndex(item => item.key.Equals("oldValidUntilDT"));
                var indexNewValidUntilDT = changes.FindIndex(item => item.key.Equals("newValidUntilDT"));

                var oldValidUntilDT = changes[indexOldValidUntilDT].value.Equals(string.Empty) ? CloudTranslations.general_none : (TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(changes[indexOldValidUntilDT].value), timeZoneInfo)).ToString();
                var newValidUntilDT = changes[indexNewValidUntilDT].value.Equals(string.Empty) ? CloudTranslations.general_none : (TimeZoneInfo.ConvertTimeFromUtc(DateTime.Parse(changes[indexNewValidUntilDT].value), timeZoneInfo)).ToString();

                changes[indexOldValidUntilDT] = (changes[indexOldValidUntilDT].key, oldValidUntilDT);
                changes[indexNewValidUntilDT] = (changes[indexNewValidUntilDT].key, newValidUntilDT);
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedRole_validUntilDT, changes.ToArray()));
            }

            var result = string.Join(", ", changesTranslation.Select(line => $"{line}"));
            return result;
        }

        private static string ConvertChangesModifiedItemTypeString(List<(string key, string value)> changes)
        {
            var changesTranslation = new List<string>();

            if (changes.Any(item => item.key.Equals("newDescription")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedErrorCode_description, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newTagType")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItemType_tagType, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newMaxLendingTimeInMins")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItemType_maxLendingTimeInMins, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newDepth")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItemType_depth, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newWidth")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItemType_width, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newHeight")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItemType_height, changes.ToArray()));
            }
            var result = string.Join(", ", changesTranslation.Select(line => $"{line}"));
            return result;
        }


        private static string ConvertChangesModifiedErrorCodeString(List<(string key, string value)> changes)
        {
            var changesTranslation = new List<string>();

            if (changes.Any(item => item.key.Equals("newDescription")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedErrorCode_description, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newCode")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedErrorCode_code, changes.ToArray()));
            }

            var result = string.Join(", ", changesTranslation.Select(line => $"{line}"));
            return result;
        }

        private static string ConvertChangesModifiedItemString(List<(string key, string value)> changes)
        {
            var changesTranslation = new List<string>();

            if (changes.Any(item => item.key.Equals("newDescription")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItem_description, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newBarcode")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItem_barcode, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newTagnumber")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItem_tagnumber, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newExternalReferenceID")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItem_externalReferenceID, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newMaxLendingTimeInMins")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItem_maxLendingTimeInMins, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newStatus")))
            {
                var indexOldStatus = changes.FindIndex(item => item.key.Equals("oldStatus"));
                var indexNewStatus = changes.FindIndex(item => item.key.Equals("newStatus"));
                changes[indexOldStatus] = (changes[indexOldStatus].key, GetItemStatusTranslation(int.Parse(changes[indexOldStatus].value)));
                changes[indexNewStatus] = (changes[indexNewStatus].key, GetItemStatusTranslation(int.Parse(changes[indexNewStatus].value)));

                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedItem_status, changes.ToArray()));
            }

            var result = string.Join(", ", changesTranslation.Select(line => $"{line}"));
            return result;
        }

        private static string ConvertChangesModifiedUserString(List<(string key, string value)> changes)
        {
            var changesTranslation = new List<string>();

            if (changes.Any(item => item.key.Equals("newEmail")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_email, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newName")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_name, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newCardCode")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_cardCode, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newLoginCode")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_loginCode, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newPhoneNumber")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_phoneNumber, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newLanguageCode")))
            {
                var indexOldLanguageCode = changes.FindIndex(item => item.key.Equals("oldLanguageCode"));
                var indexNewLanguageCode = changes.FindIndex(item => item.key.Equals("newLanguageCode"));
                changes[indexOldLanguageCode] = (changes[indexOldLanguageCode].key, new CultureInfo(changes[indexOldLanguageCode].value).DisplayName);
                changes[indexNewLanguageCode] = (changes[indexNewLanguageCode].key, new CultureInfo(changes[indexNewLanguageCode].value).DisplayName);
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_languageCode, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("newPinCode")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_pinCode, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("addedRoleDescriptions")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_addedRoles, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("removedRoleDescriptions")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_removedRoles, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("addedPersonalItemDescriptions")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_addedPersonalItems, changes.ToArray()));
            }

            if (changes.Any(item => item.key.Equals("removedPersonalItemDescriptions")))
            {
                changesTranslation.Add(TranslationUtils.GetTranslation(CloudTranslations.managementLog_editedUser_removedPersonalItems, changes.ToArray()));
            }
            var result = string.Join(", ", changesTranslation.Select(line => $"{line}"));
            return result;
        }

        private static string GetItemStatusTranslation(int itemStatus)
        {
            switch(itemStatus)
            {
                case 0:
                    return CloudTranslations.item_status_initial;
                case 1:
                    return CloudTranslations.item_status_active;
                case 2:
                    return CloudTranslations.item_status_notActive;
                case 3:
                    return CloudTranslations.item_status_defect;
                case 4:
                    return CloudTranslations.item_status_blockedForService;
                case 5:
                    return CloudTranslations.item_status_beyondRepair;
                case 6:
                    return CloudTranslations.item_status_inRepair;
                default: 
                    return string.Empty;
            }
        }
    }
}
