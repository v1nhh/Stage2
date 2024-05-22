using System;
using System.Collections.Generic;
using CabinetModule.ApplicationCore.Entities;
using CommunicationModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Entities;

namespace CTAMSeeder
{

    public class ProductionData : ISeedData
    {

        public TypeOfData SeederDataType => TypeOfData.ProductionData;

        public List<CTAMUser> Users => new List<CTAMUser>()
        {
            new CTAMUser() { Name = "FB Gebruiker", PhoneNumber = "+31612345678", UID = "999999", Password = "VeranderWachtwoord@1", LoginCode = "999999", PinCode = "123456", CardCode = "", CreateDT = DateTime.UtcNow, Email = "fb@mail.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "TLB Gebruiker", PhoneNumber = "+31612345678", UID = "999998", Password = "VeranderWachtwoord@1", LoginCode = "999998", PinCode = "123457", CardCode = "", CreateDT = DateTime.UtcNow, Email = "tlb@mail.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
            new CTAMUser() { Name = "IBK Gebruiker", PhoneNumber = "+31612345678", UID = "999997", Password = "VeranderWachtwoord@1", LoginCode = "999997", PinCode = "123458", CardCode = "", CreateDT = DateTime.UtcNow, Email = "ibk@mail.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow }

        };

        public List<CTAMUser_Role> UserRoles => new List<CTAMUser_Role>()
        {
            new CTAMUser_Role() { CTAMUserUID = "999999", CTAMRoleID = 1 },
            new CTAMUser_Role() { CTAMUserUID = "999998", CTAMRoleID = 2 },
            new CTAMUser_Role() { CTAMUserUID = "999997", CTAMRoleID = 3 }
        };

        public List<CTAMRole> Roles => new List<CTAMRole>()
        {
            new CTAMRole() { ID = 1, Description = "Functioneel Beheerder", CreateDT = DateTime.UtcNow, ValidFromDT = DateTime.UtcNow, ValidUntilDT = DateTime.MaxValue },
            new CTAMRole() { ID = 2, Description = "TLB Basis Rol", CreateDT = DateTime.UtcNow },
            new CTAMRole() { ID = 3, Description = "IBK Basis Rol", CreateDT = DateTime.UtcNow }
        };

        public List<CTAMRole_Permission> RolePermissions => new List<CTAMRole_Permission>()
        {
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 10, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 11, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 12, CreateDT = DateTime.UtcNow, },

            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 10, CreateDT = DateTime.UtcNow, },
            new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 13, CreateDT = DateTime.UtcNow, },

            new CTAMRole_Permission() { CTAMRoleID = 3, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
        };

        public List<CTAMSetting> CTAMSetting => new List<CTAMSetting>();

        public List<Cabinet> Cabinets => new List<Cabinet>();

        public List<CTAMRole_Cabinet> RoleCabinets => new List<CTAMRole_Cabinet>();

        public List<CabinetAction> CabinetActions => new List<CabinetAction>();

        public List<CabinetColumn> CabinetColumns => new List<CabinetColumn>();

        public List<CabinetCell> CabinetCells => new List<CabinetCell>();

        public List<CabinetDoor> CabinetDoor => new List<CabinetDoor>();

        public List<CabinetPosition> CabinetPositions => new List<CabinetPosition>();

        public List<CabinetLog> CabinetLogs => new List<CabinetLog>();

        public List<CabinetAccessInterval> CabinetAccessIntervals => new List<CabinetAccessInterval>();

        public List<ItemType> ItemTypes => new List<ItemType>();

        public List<CTAMRole_ItemType> RoleItemTypes => new List<CTAMRole_ItemType>();

        public List<ErrorCode> ErrorCodes => new List<ErrorCode>();

        public List<Item> Items => new List<Item>();

        public List<ItemDetail> ItemDetails => new List<ItemDetail>();

        public List<ItemSet> ItemSets => new List<ItemSet>();

        public List<ItemType_ErrorCode> ItemTypeErrorCodes => new List<ItemType_ErrorCode>();

        public List<AllowedCabinetPosition> AllowedCabinetPositions => new List<AllowedCabinetPosition>();

        public List<CabinetPositionContent> CabinetPositionContents => new List<CabinetPositionContent>();

        public List<CTAMUserPersonalItem> CTAMUserPersonalItems => new List<CTAMUserPersonalItem>();

        public List<CTAMUserInPossession> CTAMUserInPossessions => new List<CTAMUserInPossession>();

        public List<CabinetStock> CabinetStocks => new List<CabinetStock>();

        public List<ItemToPick> ItemToPicks => new List<ItemToPick>();

        public List<MailQueue> MailQueue => new List<MailQueue>();
        
        public List<MailTemplate> MailTemplates => new List<MailTemplate>();
    }
}
