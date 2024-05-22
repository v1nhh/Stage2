using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CommunicationModule.ApplicationCore.Entities;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core.Enums;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;

namespace CTAMSeeder
{

    public class DevelopmentData : ISeedData
    {
        public TypeOfData SeederDataType => TypeOfData.DevelopmentData;
        public List<CTAMUser> Users => new List<CTAMUser>()
        {
            new CTAMUser() { Name = "Gijs", PhoneNumber = "+31610000000", UID = "gijs_123", Password = "1234", LoginCode = "000001", PinCode = "123456", CardCode = "11111", CreateDT = DateTime.UtcNow, Email = "gijs@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Ed", PhoneNumber = "+31610000001", UID = "ed_123", Password = "1234", LoginCode = "000002", PinCode = "123456", CardCode = "22222", CreateDT = DateTime.UtcNow, Email = "ed@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Lieuwe", PhoneNumber = "+31610000006", UID = "lieuwe_123", Password = "1234", LoginCode = "000007", PinCode = "123456", CardCode = "77777", CreateDT = DateTime.UtcNow, Email = "lieuwe@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Adam", PhoneNumber = "+31610000069", UID = "adam_420", Password = "0420", LoginCode = "000010", PinCode = "123456", CardCode = "69696", CreateDT = DateTime.UtcNow, Email = "adam@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Faysal", PhoneNumber = "+31610000002", UID = "faysal_123", Password = "1234", LoginCode = "000003", PinCode = "123456", CardCode = "33333", CreateDT = DateTime.UtcNow, Email = "faysal@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Samuel", PhoneNumber = "+31610000003", UID = "samuel_123", Password = "1234", LoginCode = "000004", PinCode = "123456", CardCode = "44444", CreateDT = DateTime.UtcNow, Email = "samuel@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Kamran", PhoneNumber = "+31610000004", UID = "kamran_123", Password = "1234", LoginCode = "000005", PinCode = "123456", CardCode = "55555", CreateDT = DateTime.UtcNow, Email = "kamran@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Jay", PhoneNumber = "+31610000005", UID = "jay_123", Password = "1234", LoginCode = "000006", PinCode = "123456", CardCode = "666666", CreateDT = DateTime.UtcNow, Email = "jay@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Sam", PhoneNumber = "+31610000007", UID = "sam_123", Password = "1234", LoginCode = "000008", PinCode = "123456", CardCode = "88888", CreateDT = DateTime.UtcNow, Email = "sam@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Ferhat", PhoneNumber = "+31610000009", UID = "ferhat_123", Password = "1234", LoginCode = "000009", PinCode = "123456", CardCode = "99999", CreateDT = DateTime.UtcNow, Email = "ferhat@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Agent005", PhoneNumber = "+31610001005", UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", Password = "1234", LoginCode = "001005", PinCode = "123456", CardCode = "00005", CreateDT = DateTime.UtcNow, Email = "agent005@politie.nl", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Agent006", PhoneNumber = "+31610001006", UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", Password = "1234", LoginCode = "001006", PinCode = "123456", CardCode = "00006", CreateDT = DateTime.UtcNow, Email = "agent006@politie.nl", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Agent007", PhoneNumber = "+31610001007", UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", Password = "1234", LoginCode = "001007", PinCode = "123456", CardCode = "00007", CreateDT = DateTime.UtcNow, Email = "james@politie.nl", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Tlb001", PhoneNumber = "+31610001008", UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7008", Password = "1234", LoginCode = "001008", PinCode = "123456", CardCode = "00008", CreateDT = DateTime.UtcNow, Email = "beheer001@politie.nl", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "Rupsje Nooitgenoeg", PhoneNumber = "+31610001015", UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", Password = "1234", LoginCode = "001010", PinCode = "123456", CardCode = "00010", CreateDT = DateTime.UtcNow, Email = "nooitgenoeg@bezos.com", LanguageCode = "en-US", UpdateDT = DateTime.UtcNow },
        };

        public List<CTAMUser_Role> UserRoles => new List<CTAMUser_Role>()
        {
            new CTAMUser_Role() { CTAMUserUID = "adam_420", CTAMRoleID = 1, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "gijs_123", CTAMRoleID = 6, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "gijs_123", CTAMRoleID = 2, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "gijs_123", CTAMRoleID = 3, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "gijs_123", CTAMRoleID = 12, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "gijs_123", CTAMRoleID = 13, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "gijs_123", CTAMRoleID = 14, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "ed_123", CTAMRoleID = 1, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "ed_123", CTAMRoleID = 8, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "ed_123", CTAMRoleID = 2, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "ed_123", CTAMRoleID = 5, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "faysal_123", CTAMRoleID = 6, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "samuel_123", CTAMRoleID = 8, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "samuel_123", CTAMRoleID = 2, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "samuel_123", CTAMRoleID = 5, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "kamran_123", CTAMRoleID = 6, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "kamran_123", CTAMRoleID = 2, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "kamran_123", CTAMRoleID = 3, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "jay_123", CTAMRoleID = 6, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "sam_123", CTAMRoleID = 8, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "lieuwe_123", CTAMRoleID = 6, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "lieuwe_123", CTAMRoleID = 2, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "lieuwe_123", CTAMRoleID = 3, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "ferhat_123", CTAMRoleID = 11, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CTAMRoleID = 6, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CTAMRoleID = 6, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CTAMRoleID = 6, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7008", CTAMRoleID = 3, CreateDT = DateTime.UtcNow},
            // Rupsje Nooitgenoeg, IBK Nieuw-Vennep, IBK Den Haag
            new CTAMUser_Role() { CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMRoleID = 6, CreateDT = DateTime.UtcNow},
            new CTAMUser_Role() { CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMRoleID = 8, CreateDT = DateTime.UtcNow},
        };

        public List<CTAMRole> Roles => new List<CTAMRole>()
        {
            new CTAMRole() { ID = 1, Description = "Admin", CreateDT = DateTime.UtcNow, ValidFromDT = DateTime.UtcNow, ValidUntilDT = DateTime.MaxValue },
            new CTAMRole() { ID = 2, Description = "Functioneel beheerder", CreateDT = DateTime.UtcNow, ValidFromDT = DateTime.UtcNow, ValidUntilDT = DateTime.MaxValue, },
            new CTAMRole() { ID = 3, Description = "TLB Nieuw-Vennep", CreateDT = DateTime.UtcNow,  },
            new CTAMRole() { ID = 4, Description = "TLB Amsterdam", CreateDT = DateTime.UtcNow, },
            new CTAMRole() { ID = 5, Description = "TLB Den Haag", CreateDT = DateTime.UtcNow, },
            new CTAMRole() { ID = 6, Description = "IBK Nieuw-Vennep", CreateDT = DateTime.UtcNow },
            new CTAMRole() { ID = 7, Description = "IBK Amsterdam", CreateDT = DateTime.UtcNow },
            new CTAMRole() { ID = 8, Description = "IBK Den Haag", CreateDT = DateTime.UtcNow },
            new CTAMRole() { ID = 9, Description = "TLB KC Tilburg", CreateDT = DateTime.UtcNow },
            new CTAMRole() { ID = 10, Description = "IBK KC Tilburg", CreateDT = DateTime.UtcNow },
            new CTAMRole() { ID = 11, Description = "IBK Nieuw-Vennep middag ploeg", CreateDT = DateTime.UtcNow },
            new CTAMRole() { ID = 12, Description = "ALL for Combilocker Breda", CreateDT = DateTime.UtcNow },
            new CTAMRole() { ID = 13, Description = "TLB DEV", CreateDT = DateTime.UtcNow },
            new CTAMRole() { ID = 14, Description = "IBK DEV", CreateDT = DateTime.UtcNow },
        };

        public List<CTAMRole_Permission> RolePermissions => new List<CTAMRole_Permission>()
        {
            // Admin, alles
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            //new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 10, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 11, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 12, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 13, CreateDT = DateTime.UtcNow, },
            // Functioneel beheerder,          Replace, Remove, Add, +Read/write/delete
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 10, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 11, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 12, CreateDT = DateTime.UtcNow, },

            // TLB Nieuw-Vennep,               Repair, Replace, Remove, Add
            new CTAMRole_Permission() { CTAMRoleID = 3, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 3, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 3, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 3, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
            // TLB Amsterdam,                  Swap, Return, Borrow, Repair, Replace, Remove, Add
            new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            //new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
            // TLB Den Haag,                  Swap, Return, Borrow, Repair, Replace, Remove, Add
            new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            //new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
            // IBK Nieuw-Vennep,              Swap, Return, Borrow
            new CTAMRole_Permission() { CTAMRoleID = 6, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 6, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            //new CTAMRole_Permission() { CTAMRoleID = 6, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 6, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
            // IBK Amsterdam,                 Swap, Return, Borrow
            new CTAMRole_Permission() { CTAMRoleID = 7, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 7, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            //new CTAMRole_Permission() { CTAMRoleID = 7, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 7, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
            // IBK Den Haag,                  Swap, Return, Borrow
            new CTAMRole_Permission() { CTAMRoleID = 8, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 8, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            //new CTAMRole_Permission() { CTAMRoleID = 8, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 8, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
            // TLB KC Tilburg,                Swap, Return, Borrow, Repair, Replace, Remove, Add
            new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            //new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
            // IBK KC Tilburg,                Swap, Return, Borrow
            new CTAMRole_Permission() { CTAMRoleID = 10, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 10, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            //new CTAMRole_Permission() { CTAMRoleID = 10, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 10, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
            // IBK Nieuw-Vennep middag ploeg, Swap, Return, Borrow
            new CTAMRole_Permission() { CTAMRoleID = 11, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 11, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            //new CTAMRole_Permission() { CTAMRoleID = 11, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 11, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
            // ALL for Combilocker Nieuw-Vennep, Alles
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            //new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 10, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 11, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 12, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 12, CTAMPermissionID = 13, CreateDT = DateTime.UtcNow, },

            // TLB DEV,               Repair, Replace, Remove, Add, admin
            new CTAMRole_Permission() { CTAMRoleID = 13, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 13, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 13, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 13, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 13, CTAMPermissionID = 13, CreateDT = DateTime.UtcNow, },
            // IBK DEV,              Swap, Return, Pickup, Borrow
            new CTAMRole_Permission() { CTAMRoleID = 14, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 14, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
            // new CTAMRole_Permission() { CTAMRoleID = 14, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 14, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
        };

        public List<CTAMSetting> CTAMSetting => new List<CTAMSetting>()
        {
            new CTAMSetting() { ID = 12, ParName = "show_ibk_configtab", ParValue = "true", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Management },
            new CTAMSetting() { ID = 13, ParName = "SendgridAPIKey", ParValue = "", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Management },
            new CTAMSetting() { ID = 14, ParName = "MailFromAddress", ParValue = "", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Management },
            new CTAMSetting() { ID = 15, ParName = "SmtpHost", ParValue = "", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Management },
            new CTAMSetting() { ID = 16, ParName = "SmtpPort", ParValue = "", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Management },
            new CTAMSetting() { ID = 17, ParName = "SmtpUsername", ParValue = "", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Management },
            new CTAMSetting() { ID = 18, ParName = "SmtpPassword", ParValue = "", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Management },
        };

        public List<Cabinet> Cabinets => new List<Cabinet>()
        {
            new Cabinet() { Name = "IBK Nieuw Vennep", CabinetNumber = "210309081254", CabinetType= CabinetType.Locker, Description = "Lockerkast Nieuw-Vennep", Email = "supportnieuwvennep@nautaconnect.com", LocationDescr = "Nieuw-Vennep", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Online, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
            new Cabinet() { Name = "IBK Amsterdam", CabinetNumber = "210103102111", CabinetType=CabinetType.Locker, Description = "Lockerkast Amsterdam", Email = "supportamsterdam@nautaconnect.com", LocationDescr = "Amsterdam", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Online, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
            new Cabinet() { Name = "IBK Den Haag", CabinetNumber = "200214160401", CabinetType = CabinetType.Locker, Description = "Lockerkast Den Haag", Email = "supportdenhaag@nautaconnect.com", LocationDescr = "Den Haag", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Online, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
            new Cabinet() { Name = "KeyConductor Tilburg", CabinetNumber = "190404194045", CabinetType = CabinetType.KeyConductor, Description = "KeyConductor Tilburg", Email = "supporttilburg@nautaconnect.com", LocationDescr = "Tilburg", CabinetLanguage = "nl-NL", IsActive = false, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Offline, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
            new Cabinet() { Name = "Combilocker Breda", CabinetNumber = "201515205156", CabinetType = CabinetType.CombiLocker, Description = "Combilocker Breda", Email = "supportbreda@nautaconnect.com", LocationDescr = "Breda", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Online, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
            new Cabinet() { Name = "IBK DEV 1", CabinetNumber = "999999999991", CabinetType= CabinetType.Locker, Description = "Lockerkast IBK DEV 1", Email = "ibkdev1@nautaconnect.com", LocationDescr = "Nieuw-Vennep", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Initial, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
            new Cabinet() { Name = "IBK DEV 2", CabinetNumber = "999999999992", CabinetType= CabinetType.Locker, Description = "Lockerkast IBK DEV 2", Email = "ibkdev2@nautaconnect.com", LocationDescr = "Nieuw-Vennep", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Initial, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
            new Cabinet() { Name = "IBK DEV 3", CabinetNumber = "999999999993", CabinetType= CabinetType.Locker, Description = "Lockerkast IBK DEV 3", Email = "ibkdev3@nautaconnect.com", LocationDescr = "Nieuw-Vennep", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Initial, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
            new Cabinet() { Name = "IBK DEV 4", CabinetNumber = "999999999994", CabinetType= CabinetType.Locker, Description = "Lockerkast IBK DEV 4", Email = "ibkdev4@nautaconnect.com", LocationDescr = "Nieuw-Vennep", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Initial, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
            new Cabinet() { Name = "IBK DEV 5", CabinetNumber = "999999999995", CabinetType= CabinetType.Locker, Description = "Lockerkast IBK DEV 5", Email = "ibkdev5@nautaconnect.com", LocationDescr = "Nieuw-Vennep", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Initial, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode }
        };

        public List<CTAMRole_Cabinet> RoleCabinets => new List<CTAMRole_Cabinet>()
        {
            new CTAMRole_Cabinet() { CabinetNumber = "210309081254", CTAMRoleID = 3, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "210309081254", CTAMRoleID = 6, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "210309081254", CTAMRoleID = 11, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "210103102111", CTAMRoleID = 4, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "210103102111", CTAMRoleID = 7, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "200214160401", CTAMRoleID = 5, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "200214160401", CTAMRoleID = 8, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "190404194045", CTAMRoleID = 9, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "190404194045", CTAMRoleID = 10, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "201515205156", CTAMRoleID = 12, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "999999999991", CTAMRoleID = 13, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "999999999992", CTAMRoleID = 13, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "999999999993", CTAMRoleID = 13, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "999999999994", CTAMRoleID = 13, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "999999999995", CTAMRoleID = 13, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "999999999991", CTAMRoleID = 14, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "999999999992", CTAMRoleID = 14, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "999999999993", CTAMRoleID = 14, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "999999999994", CTAMRoleID = 14, CreateDT = DateTime.UtcNow},
            new CTAMRole_Cabinet() { CabinetNumber = "999999999995", CTAMRoleID = 14, CreateDT = DateTime.UtcNow}
        };

        public List<CabinetAction> CabinetActions => new List<CabinetAction>()
        {
            new CabinetAction() { ID = Guid.NewGuid(), CabinetNumber = "210309081254", CabinetName = "IBK Nieuw Vennep", PositionAlias = "1.1", Action = CabinetActionStatus.PickUp, TakeItemDescription = "Portofoon 1", TakeItemID = 1, ActionDT = DateTime.UtcNow.AddDays(-1), UpdateDT = DateTime.UtcNow.AddDays(-1), CTAMUserUID = "gijs_123", CTAMUserName = "Gijs", CTAMUserEmail = "gijs@nautaconnect.com", LogResourcePath = "cabinetActionLog.pickup" }
        };

        public List<CabinetColumn> CabinetColumns => new List<CabinetColumn>()
        {
            new CabinetColumn() { ID = 1, CabinetNumber = "210309081254", Depth = 30, Height = 30, Width = 30, IsTemplate = false, ColumnNumber = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new CabinetColumn() { ID = 2, CabinetNumber = "210309081254", Depth = 15, Height = 15, Width = 15, IsTemplate = false, ColumnNumber = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow}
        };

        public List<CabinetCell> CabinetCells => new List<CabinetCell>()
        {
            new CabinetCell() { ID = 1, CabinetCellTypeID = 1, CabinetColumnID = 1, X = 1, Y = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new CabinetCell() { ID = 2, CabinetCellTypeID = 1, CabinetColumnID = 1, X = 1, Y = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow}
        };

        public List<CabinetDoor> CabinetDoor => new List<CabinetDoor>()
        {
            new CabinetDoor() { ID = 1, Alias = "Centrale deur 1", CabinetNumber = "190404194045", Status = CabinetDoorStatus.OK, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
            new CabinetDoor() { ID = 2, Alias = "Centrale deur 1", CabinetNumber = "201515205156", Status = CabinetDoorStatus.OK, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow }
        };

        public List<CabinetPosition> CabinetPositions => new List<CabinetPosition>()
        {

            new CabinetPosition() { ID = 1, CabinetNumber = "210309081254", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 2, CabinetNumber = "210309081254", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 3, CabinetNumber = "210309081254", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 4, CabinetNumber = "210309081254", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 5, CabinetNumber = "210309081254", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 6, CabinetNumber = "210309081254", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.6", PositionNumber = 6, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 6, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 7, CabinetNumber = "210309081254", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.7", PositionNumber = 7, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 7, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 8, CabinetNumber = "210309081254", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.8", PositionNumber = 8, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 8, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 9, CabinetNumber = "210309081254", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.9", PositionNumber = 9, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 9, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 10, CabinetNumber = "210309081254", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.10", PositionNumber = 10, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 10, MaxNrOfItems = 1 },

            new CabinetPosition() { ID = 11, CabinetNumber = "210103102111", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 12, CabinetNumber = "210103102111", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 13, CabinetNumber = "210103102111", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 14, CabinetNumber = "210103102111", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },

            new CabinetPosition() { ID = 15, CabinetNumber = "200214160401", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 16, CabinetNumber = "200214160401", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 17, CabinetNumber = "200214160401", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 18, CabinetNumber = "200214160401", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 19, CabinetNumber = "200214160401", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },

            new CabinetPosition() { ID = 20, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 21, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 22, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 23, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 24, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 25, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.6", PositionNumber = 6, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 6, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 26, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.7", PositionNumber = 7, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 7, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 27, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.8", PositionNumber = 8, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 8, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 28, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.9", PositionNumber = 9, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 9, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 29, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.10", PositionNumber = 10, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 10, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 30, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.11", PositionNumber = 11, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 11, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 31, CabinetNumber = "190404194045", CabinetCellTypeID = 1, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.12", PositionNumber = 12, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 12, MaxNrOfItems = 1 },
            // Combilocker Breda KeyCop postitions
            new CabinetPosition() { ID = 32, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 33, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 34, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 35, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 36, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 37, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.6", PositionNumber = 6, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 6, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 38, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.7", PositionNumber = 7, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 7, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 39, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.8", PositionNumber = 8, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 8, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 40, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.9", PositionNumber = 9, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 9, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 41, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.10", PositionNumber = 10, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 10, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 42, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.11", PositionNumber = 11, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 11, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 43, CabinetNumber = "201515205156", CabinetCellTypeID = 1, CabinetDoorID = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.12", PositionNumber = 12, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 12, MaxNrOfItems = 1 },
            // Combilocker Breda Locker positions
            new CabinetPosition() { ID = 44, CabinetNumber = "201515205156", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "2.1", PositionNumber = 13, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 2, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 45, CabinetNumber = "201515205156", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "2.2", PositionNumber = 14, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 2, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 46, CabinetNumber = "201515205156", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "2.3", PositionNumber = 15, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 2, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 47, CabinetNumber = "201515205156", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "2.4", PositionNumber = 16, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 2, BladePosNo = 4, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 48, CabinetNumber = "201515205156", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "2.5", PositionNumber = 17, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 2, BladePosNo = 5, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 49, CabinetNumber = "201515205156", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "2.6", PositionNumber = 18, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 2, BladePosNo = 6, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 50, CabinetNumber = "201515205156", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "2.7", PositionNumber = 19, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 2, BladePosNo = 7, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 51, CabinetNumber = "201515205156", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "2.8", PositionNumber = 20, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 2, BladePosNo = 8, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 52, CabinetNumber = "201515205156", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "2.9", PositionNumber = 21, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 2, BladePosNo = 9, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 53, CabinetNumber = "201515205156", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "2.10", PositionNumber = 22, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 2, BladePosNo = 10, MaxNrOfItems = 1 },

            new CabinetPosition() { ID = 54, CabinetNumber = "999999999991", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 55, CabinetNumber = "999999999991", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 56, CabinetNumber = "999999999991", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 57, CabinetNumber = "999999999991", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 58, CabinetNumber = "999999999991", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 59, CabinetNumber = "999999999991", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.6", PositionNumber = 6, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 6, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 60, CabinetNumber = "999999999991", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.7", PositionNumber = 7, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 7, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 61, CabinetNumber = "999999999991", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.8", PositionNumber = 8, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 8, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 62, CabinetNumber = "999999999991", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.9", PositionNumber = 9, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 9, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 63, CabinetNumber = "999999999991", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.10", PositionNumber = 10, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 10, MaxNrOfItems = 1 },

            new CabinetPosition() { ID = 64, CabinetNumber = "999999999992", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 65, CabinetNumber = "999999999992", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 66, CabinetNumber = "999999999992", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 67, CabinetNumber = "999999999992", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 68, CabinetNumber = "999999999992", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 69, CabinetNumber = "999999999992", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.6", PositionNumber = 6, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 6, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 70, CabinetNumber = "999999999992", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.7", PositionNumber = 7, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 7, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 71, CabinetNumber = "999999999992", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.8", PositionNumber = 8, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 8, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 72, CabinetNumber = "999999999992", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.9", PositionNumber = 9, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 9, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 73, CabinetNumber = "999999999992", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.10", PositionNumber = 10, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 10, MaxNrOfItems = 1 },

            new CabinetPosition() { ID = 74, CabinetNumber = "999999999993", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 75, CabinetNumber = "999999999993", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 76, CabinetNumber = "999999999993", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 77, CabinetNumber = "999999999993", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 78, CabinetNumber = "999999999993", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 79, CabinetNumber = "999999999993", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.6", PositionNumber = 6, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 6, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 80, CabinetNumber = "999999999993", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.7", PositionNumber = 7, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 7, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 81, CabinetNumber = "999999999993", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.8", PositionNumber = 8, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 8, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 82, CabinetNumber = "999999999993", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.9", PositionNumber = 9, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 9, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 83, CabinetNumber = "999999999993", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.10", PositionNumber = 10, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 10, MaxNrOfItems = 1 },

            new CabinetPosition() { ID = 84, CabinetNumber = "999999999994", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 85, CabinetNumber = "999999999994", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 86, CabinetNumber = "999999999994", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 87, CabinetNumber = "999999999994", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 88, CabinetNumber = "999999999994", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 89, CabinetNumber = "999999999994", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.6", PositionNumber = 6, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 6, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 90, CabinetNumber = "999999999994", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.7", PositionNumber = 7, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 7, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 91, CabinetNumber = "999999999994", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.8", PositionNumber = 8, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 8, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 92, CabinetNumber = "999999999994", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.9", PositionNumber = 9, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 9, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 93, CabinetNumber = "999999999994", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.10", PositionNumber = 10, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 10, MaxNrOfItems = 1 },

            new CabinetPosition() { ID = 94, CabinetNumber = "999999999995", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 95, CabinetNumber = "999999999995", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 96, CabinetNumber = "999999999995", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 97, CabinetNumber = "999999999995", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 98, CabinetNumber = "999999999995", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 99, CabinetNumber = "999999999995", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.6", PositionNumber = 6, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 6, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 100, CabinetNumber = "999999999995", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.7", PositionNumber = 7, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 7, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 101, CabinetNumber = "999999999995", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.8", PositionNumber = 8, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 8, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 102, CabinetNumber = "999999999995", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.9", PositionNumber = 9, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 9, MaxNrOfItems = 1 },
            new CabinetPosition() { ID = 103, CabinetNumber = "999999999995", CabinetCellTypeID = 4, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.10", PositionNumber = 10, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 10, MaxNrOfItems = 1 },


        };

        public List<CabinetLog> CabinetLogs => new List<CabinetLog>()
        {
            new CabinetLog() { ID = 1, CabinetNumber = "210309081254", CabinetName = "IBK Nieuw Vennep", Level = LogLevel.Info, LogResourcePath = "INFO: IBK 210309081254 gestart", LogDT = DateTime.UtcNow.AddHours(-1), Source = LogSource.CabinetUI, UpdateDT = DateTime.UtcNow.AddHours(-1) },
            new CabinetLog() { ID = 2, CabinetNumber = "210103102111", CabinetName = "IBK Amsterdam", Level = LogLevel.Info, LogResourcePath = "INFO: IBK 210103102111 gestart", LogDT = DateTime.UtcNow.AddHours(-1), Source = LogSource.CabinetUI, UpdateDT = DateTime.UtcNow.AddHours(-1) },
            new CabinetLog() { ID = 3, CabinetNumber = "200214160401", CabinetName = "IBK Den Haag", Level = LogLevel.Info, LogResourcePath = "INFO: IBK 200214160401 gestart", LogDT = DateTime.UtcNow.AddHours(-1), Source = LogSource.CabinetUI, UpdateDT = DateTime.UtcNow.AddHours(-1) },
            new CabinetLog() { ID = 4, CabinetNumber = "201515205156", CabinetName = "Combilocker Breda", Level = LogLevel.Info, LogResourcePath = "INFO: IBK 201515205156 gestart", LogDT = DateTime.UtcNow.AddHours(-1), Source = LogSource.CabinetUI, UpdateDT = DateTime.UtcNow.AddHours(-1) },
        };

        public List<CabinetAccessInterval> CabinetAccessIntervals => new List<CabinetAccessInterval>()
        {
            new CabinetAccessInterval() { ID = 1, CTAMRoleID = 11, StartWeekDayNr = 1, StartTime = TimeSpan.Parse("13:00"), EndWeekDayNr = 1, EndTime = TimeSpan.Parse("17:00")},
            new CabinetAccessInterval() { ID = 2, CTAMRoleID = 11, StartWeekDayNr = 2, StartTime = TimeSpan.Parse("13:00"), EndWeekDayNr = 2, EndTime = TimeSpan.Parse("17:00")},
            new CabinetAccessInterval() { ID = 3, CTAMRoleID = 11, StartWeekDayNr = 3, StartTime = TimeSpan.Parse("13:00"), EndWeekDayNr = 3, EndTime = TimeSpan.Parse("17:00")},
            new CabinetAccessInterval() { ID = 4, CTAMRoleID = 11, StartWeekDayNr = 4, StartTime = TimeSpan.Parse("13:00"), EndWeekDayNr = 4, EndTime = TimeSpan.Parse("17:00")},
            new CabinetAccessInterval() { ID = 5, CTAMRoleID = 11, StartWeekDayNr = 5, StartTime = TimeSpan.Parse("13:00"), EndWeekDayNr = 5, EndTime = TimeSpan.Parse("17:00")}
        };

        public List<ItemType> ItemTypes => new List<ItemType>()
        {
            new ItemType() { ID = 1, Description = "C2000 portofoon", IsStoredInLocker = true, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 5, Width = 4, Height = 20, TagType = TagType.NFC },
            new ItemType() { ID = 2, Description = "Mobiele telefoon", IsStoredInLocker = true, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 1, Width = 7, Height = 14, TagType = TagType.LF },
            new ItemType() { ID = 3, Description = "Bodycam", IsStoredInLocker = true, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 5, Width = 5, Height = 5, TagType = TagType.LF },
            new ItemType() { ID = 4, Description = "Laptop", IsStoredInLocker = true, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 4, Width = 28, Height = 20, TagType = TagType.LF },
            new ItemType() { ID = 5, Description = "Tablet", IsStoredInLocker = true, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 1, Width = 15, Height = 20, TagType = TagType.LF },
            new ItemType() { ID = 6, Description = "Sleutel", IsStoredInLocker = false, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 0, Width = 0, Height = 0, TagType = TagType.Barcode, RequiresMileageRegistration = true }

        };

        public List<CTAMRole_ItemType> RoleItemTypes => new List<CTAMRole_ItemType>()
        {
            // TLB Nieuw-Vennep, C2000 portofoon, Mobiel, Bodycam
            new CTAMRole_ItemType() { CTAMRoleID = 3, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1  },
            new CTAMRole_ItemType() { CTAMRoleID = 3, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1  },
            new CTAMRole_ItemType() { CTAMRoleID = 3, ItemTypeID = 3, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1  },
            // IBK Nieuw-Vennep, C2000 portofoon, Mobiel, Bodycam, Laptop, Tablet
            new CTAMRole_ItemType() { CTAMRoleID = 6, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1  },
            new CTAMRole_ItemType() { CTAMRoleID = 6, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            new CTAMRole_ItemType() { CTAMRoleID = 6, ItemTypeID = 3, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            new CTAMRole_ItemType() { CTAMRoleID = 6, ItemTypeID = 4, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            new CTAMRole_ItemType() { CTAMRoleID = 6, ItemTypeID = 5, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            // TLB Amsterdam, Mobiel
            new CTAMRole_ItemType() { CTAMRoleID = 4, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            // IBK Amsterdam, C2000 portofoon, Mobiel, Bodycam
            new CTAMRole_ItemType() { CTAMRoleID = 7, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            new CTAMRole_ItemType() { CTAMRoleID = 7, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            new CTAMRole_ItemType() { CTAMRoleID = 7, ItemTypeID = 3, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            // TLB Den Haag, C2000 portofoon, Mobiel
            new CTAMRole_ItemType() { CTAMRoleID = 5, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            new CTAMRole_ItemType() { CTAMRoleID = 5, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            // IBK Den Haag, C2000 portofoon, Mobiel, Bodycam, Laptop, Tablet
            new CTAMRole_ItemType() { CTAMRoleID = 8, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 8, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 8, ItemTypeID = 3, CreateDT = DateTime.UtcNow, MaxQtyToPick = 2 },
            new CTAMRole_ItemType() { CTAMRoleID = 8, ItemTypeID = 4, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 8, ItemTypeID = 5, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            // IBK KC Tilburg, Sleutel
            new CTAMRole_ItemType() { CTAMRoleID = 10, ItemTypeID = 6, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
            // Combilocker Breda, C2000 portofoon, Mobiel, Bodycam, Laptop, Tablet, Sleutel
            new CTAMRole_ItemType() { CTAMRoleID = 12, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 10 },
            new CTAMRole_ItemType() { CTAMRoleID = 12, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 10 },
            new CTAMRole_ItemType() { CTAMRoleID = 12, ItemTypeID = 3, CreateDT = DateTime.UtcNow, MaxQtyToPick = 10 },
            new CTAMRole_ItemType() { CTAMRoleID = 12, ItemTypeID = 4, CreateDT = DateTime.UtcNow, MaxQtyToPick = 10 },
            new CTAMRole_ItemType() { CTAMRoleID = 12, ItemTypeID = 5, CreateDT = DateTime.UtcNow, MaxQtyToPick = 10 },
            new CTAMRole_ItemType() { CTAMRoleID = 12, ItemTypeID = 6, CreateDT = DateTime.UtcNow, MaxQtyToPick = 10 },

            new CTAMRole_ItemType() { CTAMRoleID = 13, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 13, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 13, ItemTypeID = 3, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 13, ItemTypeID = 4, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 13, ItemTypeID = 5, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 13, ItemTypeID = 6, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },

            new CTAMRole_ItemType() { CTAMRoleID = 14, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 14, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 14, ItemTypeID = 3, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 14, ItemTypeID = 4, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 14, ItemTypeID = 5, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
            new CTAMRole_ItemType() { CTAMRoleID = 14, ItemTypeID = 6, CreateDT = DateTime.UtcNow, MaxQtyToPick = 3 },
        };

        public List<ErrorCode> ErrorCodes => new List<ErrorCode>()
        {
            new ErrorCode() { ID = 1, Code = "01", Description = "Antenne", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new ErrorCode() { ID = 2, Code = "02", Description = "Zijaansluiting", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new ErrorCode() { ID = 3, Code = "03", Description = "Scherm", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new ErrorCode() { ID = 4, Code = "04", Description = "Onderaansluiting", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new ErrorCode() { ID = 5, Code = "05", Description = "Toetsenbord", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new ErrorCode() { ID = 6, Code = "06", Description = "Volumeknop", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new ErrorCode() { ID = 7, Code = "07", Description = "Netwerkprobleem", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new ErrorCode() { ID = 8, Code = "08", Description = "Updateprobleem", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new ErrorCode() { ID = 9, Code = "09", Description = "PUK-code", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new ErrorCode() { ID = 10, Code = "10", Description = "Overig", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow}
        };

        public List<Item> Items => new List<Item>()
        {
            new Item() { ID = 1, ItemTypeID = 1, Description = "Portofoon 1", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "111111", Tagnumber = "111111", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI111111" },
            new Item() { ID = 2, ItemTypeID = 1, Description = "Portofoon 2", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "111112", Tagnumber = "111112", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI111112" },
            new Item() { ID = 3, ItemTypeID = 1, Description = "Portofoon 3", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "111113", Tagnumber = "111113", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI111113" },
            new Item() { ID = 4, ItemTypeID = 1, Description = "Portofoon 4", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "111114", Tagnumber = "111114", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI111114" },
            new Item() { ID = 5, ItemTypeID = 5, Description = "Tablet 1", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "222221", Tagnumber = "222221", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI222221" },
            new Item() { ID = 6, ItemTypeID = 5, Description = "Tablet 2", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "222222", Tagnumber = "222222", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI222222" },
            new Item() { ID = 7, ItemTypeID = 5, Description = "Tablet 3", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "222223", Tagnumber = "222223", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI222223" },
            new Item() { ID = 8, ItemTypeID = 5, Description = "Tablet 4", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "222224", Tagnumber = "222224", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI222224" },
            new Item() { ID = 9, ItemTypeID = 2, Description = "Telefoon 1", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "333331", Tagnumber = "333331", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI333331" },
            new Item() { ID = 10, ItemTypeID = 2, Description = "Telefoon 2", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "333332", Tagnumber = "333332", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI333332" },
            new Item() { ID = 11, ItemTypeID = 2, Description = "Telefoon 3", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "333333", Tagnumber = "333333", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI333333" },
            new Item() { ID = 12, ItemTypeID = 2, Description = "Telefoon 4", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "333334", Tagnumber = "333334", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI333334" },
            new Item() { ID = 13, ItemTypeID = 3, Description = "Bodycam 1", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "444441", Tagnumber = "444441", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI444441" },
            new Item() { ID = 14, ItemTypeID = 3, Description = "Bodycam 2", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "444442", Tagnumber = "444442", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI444442" },
            new Item() { ID = 15, ItemTypeID = 3, Description = "Bodycam 3", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "444443", Tagnumber = "444443", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI444443" },
            new Item() { ID = 16, ItemTypeID = 3, Description = "Bodycam 4", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "444444", Tagnumber = "444444", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI444444" },
            new Item() { ID = 17, ItemTypeID = 4, Description = "Laptop 1", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "555551", Tagnumber = "555551", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI555551" },
            new Item() { ID = 18, ItemTypeID = 4, Description = "Laptop 2", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "555552", Tagnumber = "555552", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI555552" },
            new Item() { ID = 19, ItemTypeID = 4, Description = "Laptop 3", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "555553", Tagnumber = "555553", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI555553" },
            new Item() { ID = 20, ItemTypeID = 4, Description = "Laptop 4", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "555554", Tagnumber = "555554", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI555554" },
            new Item() { ID = 21, ItemTypeID = 6, Description = "Auto AA-00-01", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "666661", Tagnumber = "666661", UpdateDT = DateTime.UtcNow, MaxLendingTimeInMins = 480, NrOfSubItems = 1, ExternalReferenceID = "CI666661" },
            new Item() { ID = 22, ItemTypeID = 6, Description = "Voordeur Gebouw A.12", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "666662", Tagnumber = "666662", UpdateDT = DateTime.UtcNow, MaxLendingTimeInMins = 480, NrOfSubItems = 2, ExternalReferenceID = "CI666662" },
            new Item() { ID = 23, ItemTypeID = 3, Description = "Bodycam 5", CreateDT = DateTime.UtcNow, Status = ItemStatus.INITIAL, Barcode = "444445", Tagnumber = "444445", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI444445" },
            new Item() { ID = 24, ItemTypeID = 3, Description = "Bodycam 6", CreateDT = DateTime.UtcNow, Status = ItemStatus.INITIAL, Barcode = "444446", Tagnumber = "444446", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI444446" },
            new Item() { ID = 25, ItemTypeID = 1, Description = "PPortofoon 5", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "111115", Tagnumber = "111115", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI111115" },
            new Item() { ID = 26, ItemTypeID = 1, Description = "PPortofoon 6", CreateDT = DateTime.UtcNow, Status = ItemStatus.DEFECT, ErrorCodeID = 9, Barcode = "111116", Tagnumber = "111116", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI111116" },
            new Item() { ID = 27, ItemTypeID = 1, Description = "PPortofoon 7", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "111117", Tagnumber = "111117", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI111117" },
            new Item() { ID = 28, ItemTypeID = 1, Description = "TmpPortofoon 10", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "1111110", Tagnumber = "1111110", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI1111110" },
            new Item() { ID = 29, ItemTypeID = 1, Description = "TmpPortofoon 11", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "1111111", Tagnumber = "1111111", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI1111111" },
            new Item() { ID = 30, ItemTypeID = 1, Description = "TmpPortofoon 12", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "1111112", Tagnumber = "1111112", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI1111112" },

            new Item() { ID = 31, ItemTypeID = 2, Description = "PTelefoon 5", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "333335", Tagnumber = "333335", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI333335" },
            new Item() { ID = 32, ItemTypeID = 2, Description = "PTelefoon 6", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "333336", Tagnumber = "333336", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI333336" },
            new Item() { ID = 33, ItemTypeID = 2, Description = "PTelefoon 7", CreateDT = DateTime.UtcNow, Status = ItemStatus.IN_REPAIR, ErrorCodeID = 2, Barcode = "333337", Tagnumber = "333337", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI333337" },
            new Item() { ID = 34, ItemTypeID = 2, Description = "TmpTelefoon 10", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "3333310", Tagnumber = "3333310", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI3333310" },
            new Item() { ID = 35, ItemTypeID = 2, Description = "TmpTelefoon 11", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "3333311", Tagnumber = "3333311", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI3333311" },
            new Item() { ID = 36, ItemTypeID = 2, Description = "TmpTelefoon 12", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "3333312", Tagnumber = "3333312", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI3333312" },
            new Item() { ID = 37, ItemTypeID = 2, Description = "TmpTelefoon 13", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "3333313", Tagnumber = "3333313", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI3333313" },
            new Item() { ID = 38, ItemTypeID = 2, Description = "TmpTelefoon 14", CreateDT = DateTime.UtcNow, Status = ItemStatus.INITIAL, Barcode = "3333314", Tagnumber = "3333314", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI3333314" },
            new Item() { ID = 39, ItemTypeID = 2, Description = "TmpTelefoon 15", CreateDT = DateTime.UtcNow, Status = ItemStatus.INITIAL, Barcode = "3333315", Tagnumber = "3333315", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI3333315" },
            new Item() { ID = 40, ItemTypeID = 2, Description = "TmpTelefoon 16", CreateDT = DateTime.UtcNow, Status = ItemStatus.INITIAL, Barcode = "3333316", Tagnumber = "3333316", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI3333316" },
            new Item() { ID = 41, ItemTypeID = 2, Description = "TmpTelefoon 17", CreateDT = DateTime.UtcNow, Status = ItemStatus.INITIAL, Barcode = "3333317", Tagnumber = "3333317", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI3333371" },
            new Item() { ID = 42, ItemTypeID = 2, Description = "TmpTelefoon 18", CreateDT = DateTime.UtcNow, Status = ItemStatus.INITIAL, Barcode = "3333318", Tagnumber = "3333318", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI3333318" },

            // C2000 Portofoons inside Combilocker Breda
            new Item() { ID = 43, ItemTypeID = 1, Description = "C2000 Portofoon 1", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "1", Tagnumber = "1", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 44, ItemTypeID = 1, Description = "C2000 Portofoon 2", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "2", Tagnumber = "2", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 45, ItemTypeID = 1, Description = "C2000 Portofoon 3", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "3", Tagnumber = "3", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 46, ItemTypeID = 1, Description = "C2000 Portofoon 4", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "4", Tagnumber = "4", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 47, ItemTypeID = 1, Description = "C2000 Portofoon 5", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "5", Tagnumber = "5", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 48, ItemTypeID = 1, Description = "C2000 Portofoon 6", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "6", Tagnumber = "6", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 49, ItemTypeID = 1, Description = "C2000 Portofoon 7", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "7", Tagnumber = "7", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 50, ItemTypeID = 1, Description = "C2000 Portofoon 8", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "8", Tagnumber = "8", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            // Sleutes inside Combilocker Breda
            new Item() { ID = 51, ItemTypeID = 6, Description = "Sleutel 1", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "11", Tagnumber = "11", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 52, ItemTypeID = 6, Description = "Sleutel 2", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "22", Tagnumber = "22", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 53, ItemTypeID = 6, Description = "Sleutel 3", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "33", Tagnumber = "33", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 54, ItemTypeID = 6, Description = "Sleutel 4", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "44", Tagnumber = "44", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 55, ItemTypeID = 6, Description = "Sleutel 5", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "55", Tagnumber = "55", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 56, ItemTypeID = 6, Description = "Sleutel 6", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "66", Tagnumber = "66", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 57, ItemTypeID = 6, Description = "Sleutel 7", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "77", Tagnumber = "77", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 58, ItemTypeID = 6, Description = "Sleutel 8", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "88", Tagnumber = "88", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 59, ItemTypeID = 6, Description = "Sleutel 9", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "99", Tagnumber = "99", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },
            new Item() { ID = 60, ItemTypeID = 6, Description = "Sleutel 10", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "100", Tagnumber = "100", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = null },

        };

        public List<ItemDetail> ItemDetails => new List<ItemDetail>()
        {
            new ItemDetail() { ID = 1, ItemID = 21, Description = "Auto AA-00-01", CreateDT = DateTime.UtcNow, FreeText1 = "Audi A8" },
            new ItemDetail() { ID = 2, ItemID = 22, Description = "Slot boven", CreateDT = DateTime.UtcNow, FreeText1 = "SN: AG.6453", FreeText2 = "MEGA" },
            new ItemDetail() { ID = 3, ItemID = 23, Description = "Slot onder", CreateDT = DateTime.UtcNow, FreeText1 = "SN: 76733-1", FreeText2 = "FAAS EDELSTAHL GMBH & CO. KG" }
        };

        public List<ItemSet> ItemSets => new List<ItemSet>()
        {
            new ItemSet() { SetCode = "Item_Set_1", ItemID = 20, Status = ItemSetStatus.OK, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
            new ItemSet() { SetCode = "Item_Set_1", ItemID = 16, Status = ItemSetStatus.OK, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow}
        };

        public List<ItemType_ErrorCode> ItemTypeErrorCodes => new List<ItemType_ErrorCode>()
        {
            new ItemType_ErrorCode() { ItemTypeID = 1, ErrorCodeID = 1, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 1, ErrorCodeID = 2, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 1, ErrorCodeID = 3, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 1, ErrorCodeID = 4, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 1, ErrorCodeID = 5, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 1, ErrorCodeID = 6, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 1, ErrorCodeID = 7, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 1, ErrorCodeID = 8, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 1, ErrorCodeID = 9, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 1, ErrorCodeID = 10, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 2, ErrorCodeID = 1, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 2, ErrorCodeID = 2, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 2, ErrorCodeID = 3, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 2, ErrorCodeID = 4, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 2, ErrorCodeID = 5, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 4, ErrorCodeID = 2, CreateDT = DateTime.UtcNow },
            new ItemType_ErrorCode() { ItemTypeID = 5, ErrorCodeID = 4, CreateDT = DateTime.UtcNow }
        };

        public List<AllowedCabinetPosition> AllowedCabinetPositions => new List<AllowedCabinetPosition>();

        public List<CabinetPositionContent> CabinetPositionContents => new List<CabinetPositionContent>()
        {

            new CabinetPositionContent() { CabinetPositionID = 3, ItemID = 13, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 4, ItemID = 15, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 5, ItemID = 26, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 7, ItemID = 4,  CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 8, ItemID = 35,  CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 9, ItemID = 36,  CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 10,ItemID = 37,  CreateDT = DateTime.UtcNow },
            // Item returned in Cabinet 210103102111
            new CabinetPositionContent() { CabinetPositionID = 12,ItemID = 10,  CreateDT = DateTime.UtcNow },
            // Combilocker Breda items
            new CabinetPositionContent() { CabinetPositionID = 44, ItemID = 43, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 45, ItemID = 44, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 46, ItemID = 45, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 47, ItemID = 46, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 48, ItemID = 47, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 49, ItemID = 48, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 50, ItemID = 49, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 51, ItemID = 50, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 32, ItemID = 51, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 33, ItemID = 52, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 34, ItemID = 53, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 35, ItemID = 54, CreateDT = DateTime.UtcNow },
            new CabinetPositionContent() { CabinetPositionID = 36, ItemID = 55, CreateDT = DateTime.UtcNow },
        };
        public List<CTAMUserPersonalItem> CTAMUserPersonalItems => new List<CTAMUserPersonalItem>()
        {
            new CTAMUserPersonalItem() { ID = 1, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, ItemID = 25, Status = UserPersonalItemStatus.OK },
            new CTAMUserPersonalItem() { ID = 2, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, ItemID = 31, Status = UserPersonalItemStatus.OK },
            new CTAMUserPersonalItem() { ID = 3, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, ItemID = 26, ReplacementItemID = 28, Status = UserPersonalItemStatus.Defect },
            new CTAMUserPersonalItem() { ID = 4, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, ItemID = 32, Status = UserPersonalItemStatus.OK },
            new CTAMUserPersonalItem() { ID = 5, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, ItemID = 27, Status = UserPersonalItemStatus.OK },
            new CTAMUserPersonalItem() { ID = 6, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, ItemID = 33, ReplacementItemID = 34, Status = UserPersonalItemStatus.Defect }
        };

        public List<CTAMUserInPossession> CTAMUserInPossessions => new List<CTAMUserInPossession>()
        {
            new CTAMUserInPossession() { ID = Guid.Parse("c084e3b9-42f9-4bc7-9250-01ad8fde7a09"), ItemID = 1, CabinetPositionIDOut = 1, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "gijs_123", CTAMUserNameOut = "Gijs", CTAMUserEmailOut = "gijs@nautaconnect.com", OutDT = DateTime.UtcNow.AddDays(-6), CreatedDT = DateTime.UtcNow.AddDays(-6), CabinetPositionIDIn=1, CabinetNumberIn = "210309081254" , CabinetNameIn = "IBK Nieuw Vennep", CTAMUserUIDIn = "gijs_123", CTAMUserNameIn = "Gijs", CTAMUserEmailIn = "gijs@nautaconnect.com", InDT = DateTime.UtcNow.AddDays(-5), ReturnBeforeDT=DateTime.UtcNow.AddDays(-4), Status = UserInPossessionStatus.Returned },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc02"), ItemID = 1, CabinetPositionIDOut = 1, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "gijs_123", CTAMUserNameOut = "Gijs", CTAMUserEmailOut = "gijs@nautaconnect.com", OutDT = DateTime.UtcNow, CreatedDT = DateTime.UtcNow, ReturnBeforeDT = DateTime.UtcNow.AddDays(4), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc03"), ItemID = 2, CabinetPositionIDOut = 2, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "kamran_123", CTAMUserNameOut = "Kamran", CTAMUserEmailOut = "kamran@nautaconnect.com", OutDT = DateTime.UtcNow, CreatedDT = DateTime.UtcNow, ReturnBeforeDT = DateTime.UtcNow.AddDays(4), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc04"), ItemID = 3, CabinetPositionIDOut = 3, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "faysal_123", CTAMUserNameOut = "Faysal", CTAMUserEmailOut = "faysal@nautaconnect.com", OutDT = DateTime.UtcNow, CreatedDT = DateTime.UtcNow, ReturnBeforeDT = DateTime.UtcNow.AddDays(4), Status = UserInPossessionStatus.Picked },

            // Agent005, personalitem pportofoon5 en ptelefoon5
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc05"), ItemID = 25, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CTAMUserNameOut = "Agent005", CTAMUserEmailOut = "agent005@politie.nl", OutDT = DateTime.UtcNow, CreatedDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc06"), ItemID = 31, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CTAMUserNameOut = "Agent005", CTAMUserEmailOut = "agent005@politie.nl", OutDT = DateTime.UtcNow, CreatedDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },

            // Agent006, personalitem ptelefoon6 en pportofoon6 => vervangen (defect) door tmpportofoon10
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc07"), ItemID = 32, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CTAMUserNameOut = "Agent006", CTAMUserEmailOut = "agent006@politie.nl", OutDT = DateTime.UtcNow, CreatedDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc08"), ItemID = 26, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CTAMUserNameOut = "Agent006", CTAMUserEmailOut = "agent006@politie.nl", OutDT = DateTime.UtcNow.AddDays(-1), CreatedDT = DateTime.UtcNow.AddDays(-1), CTAMUserUIDIn = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CTAMUserNameIn = "Agent006", CTAMUserEmailIn = "agent006@politie.nl", InDT = DateTime.UtcNow, CabinetPositionIDIn = 5, CabinetNumberIn = "210309081254", CabinetNameIn = "IBK Nieuw Vennep", Status = UserInPossessionStatus.Returned },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc09"), ItemID = 28, CabinetPositionIDOut = 3, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CTAMUserNameOut = "Agent006", CTAMUserEmailOut = "agent006@politie.nl", OutDT = DateTime.UtcNow, CreatedDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },

            // Agent007, personalitem pportofoon7 en ptelefoon7 (in_repair en bezit tlb) => vervangen door tmptelefoon7
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc10"), ItemID = 27, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CTAMUserNameOut = "Agent007", CTAMUserEmailOut = "agent007@politie.nl", OutDT = DateTime.UtcNow, CreatedDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc11"), ItemID = 33, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CTAMUserNameOut = "Agent007", CTAMUserEmailOut = "agent007@politie.nl", OutDT = DateTime.UtcNow.AddDays(-2), CreatedDT = DateTime.UtcNow.AddDays(-2), CTAMUserUIDIn = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CTAMUserNameIn = "Agent007", CTAMUserEmailIn = "agent007@politie.nl", InDT = DateTime.UtcNow, CabinetPositionIDIn = 4, CabinetNumberIn = "210309081254", CabinetNameIn = "IBK Nieuw Vennep", Status = UserInPossessionStatus.Returned },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc12"), ItemID = 34, CabinetPositionIDOut = 4, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CTAMUserNameOut = "Agent007", CTAMUserEmailOut = "agent007@politie.nl", OutDT = DateTime.UtcNow.AddDays(-1), CreatedDT = DateTime.UtcNow.AddDays(-1), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc13"), ItemID = 33, CabinetPositionIDOut = 4, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7008", CTAMUserNameOut = "Tlb001", CTAMUserEmailOut = "beheer001@politie.nl", OutDT = DateTime.UtcNow, CreatedDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },
            // Rupsje nooitgenoeg, Porto 1+2+3, Tablet 1+2+3, Telefoon 1+3+4, Bodycam 2+4, Laptop 1+2+3
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc15"), ItemID = 2, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc16"), ItemID = 3, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc17"), ItemID = 5, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc18"), ItemID = 6, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc19"), ItemID = 7, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc20"), ItemID = 9, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc21"), ItemID = 11, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc22"), ItemID = 12, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc23"), ItemID = 14, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc24"), ItemID = 16, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc25"), ItemID = 17, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc26"), ItemID = 18, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc27"), ItemID = 19, CabinetPositionIDOut = 4, CabinetNumberOut = "200214160401", CabinetNameOut = "IBK Den Haag", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7010", CTAMUserNameOut = "Rupsje Nooitgenoeg", CTAMUserEmailOut = "nooitgenoeg@bezos.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            // Gijs, sleutel 8
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc28"), ItemID = 56, CabinetPositionIDOut = 6, CabinetNumberOut = "201515205156", CabinetNameOut = "Combilocker Breda", CTAMUserUIDOut = "gijs_123", CTAMUserNameOut = "Gijs", CTAMUserEmailOut = "gijs@nautaconnect.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc29"), ItemID = 57, CabinetPositionIDOut = 7, CabinetNumberOut = "201515205156", CabinetNameOut = "Combilocker Breda", CTAMUserUIDOut = "gijs_123", CTAMUserNameOut = "Gijs", CTAMUserEmailOut = "gijs@nautaconnect.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc30"), ItemID = 58, CabinetPositionIDOut = 8, CabinetNumberOut = "201515205156", CabinetNameOut = "Combilocker Breda", CTAMUserUIDOut = "gijs_123", CTAMUserNameOut = "Gijs", CTAMUserEmailOut = "gijs@nautaconnect.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc31"), ItemID = 59, CabinetPositionIDOut = 9, CabinetNumberOut = "201515205156", CabinetNameOut = "Combilocker Breda", CTAMUserUIDOut = "gijs_123", CTAMUserNameOut = "Gijs", CTAMUserEmailOut = "gijs@nautaconnect.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
            new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc32"), ItemID = 60, CabinetPositionIDOut = 10, CabinetNumberOut = "201515205156", CabinetNameOut = "Combilocker Breda", CTAMUserUIDOut = "gijs_123", CTAMUserNameOut = "Gijs", CTAMUserEmailOut = "gijs@nautaconnect.com", OutDT = DateTime.UtcNow.AddDays(-5), CreatedDT = DateTime.UtcNow.AddDays(-5), Status = UserInPossessionStatus.Picked },
        };

        public List<CabinetStock> CabinetStocks => new List<CabinetStock>()
        {
            // IBK Nieuw Vennep
            new CabinetStock() { CabinetNumber = "210309081254", ItemTypeID = 1, MinimalStock = 1, ActualStock = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "210309081254", ItemTypeID = 2, MinimalStock = 0, ActualStock = 3, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "210309081254", ItemTypeID = 3, MinimalStock = 0, ActualStock = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "210309081254", ItemTypeID = 4, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            // IBK Amsterdam
            new CabinetStock() { CabinetNumber = "210103102111", ItemTypeID = 1, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "210103102111", ItemTypeID = 2, MinimalStock = 0, ActualStock = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "210103102111", ItemTypeID = 3, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            // IBK Den Haag
            new CabinetStock() { CabinetNumber = "200214160401", ItemTypeID = 1, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "200214160401", ItemTypeID = 2, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "200214160401", ItemTypeID = 3, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "200214160401", ItemTypeID = 4, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "200214160401", ItemTypeID = 5, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            // KeyConductor Tilburg
            new CabinetStock() { CabinetNumber = "190404194045", ItemTypeID = 6, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            // Combilocker Breda
            new CabinetStock() { CabinetNumber = "201515205156", ItemTypeID = 1, MinimalStock = 0, ActualStock = 8, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "201515205156", ItemTypeID = 2, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "201515205156", ItemTypeID = 3, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "201515205156", ItemTypeID = 4, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "201515205156", ItemTypeID = 5, MinimalStock = 0, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
            new CabinetStock() { CabinetNumber = "201515205156", ItemTypeID = 6, MinimalStock = 0, ActualStock = 5, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK }
        };

        public List<ItemToPick> ItemToPicks => new List<ItemToPick>()
        {
            new ItemToPick() { ID = 1, ItemID = 13, CabinetPositionID = 3, CTAMUserUID = "gijs_123", Status = ItemToPickStatus.ReadyToPick }
        };

        public List<MailQueue> MailQueue => new List<MailQueue>()
        {
            new MailQueue() { ID = 1, MailMarkupTemplateID = 1, MailTo = "software@nautaconnect.com", MailCC = null, Reference = "REF. 123", Prio = false, Subject = "Hallo", Body = "Hallo teamleden!", Status = MailQueueStatus.Created, CreateDT = DateTime.UtcNow}
        };

        public List<MailTemplate> MailTemplates => new List<MailTemplate>();
    }
}
