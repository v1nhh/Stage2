using System.Collections.Generic;
using CabinetModule.ApplicationCore.Entities;
using CommunicationModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Entities;

namespace CTAMSeeder
{
    /// <summary>
    /// Interface for minimal seed data. 
    /// This ensures every type of data creates a user that can be used to login to the application.
    /// </summary>//  
    public interface ISeedData
    {
        TypeOfData SeederDataType { get; }
        List<CTAMUser> Users { get; }
        List<CTAMUser_Role> UserRoles { get; }
        List<CTAMRole> Roles { get; }
        List<CTAMRole_Permission> RolePermissions { get; }
        List<CTAMSetting> CTAMSetting { get; }
        List<Cabinet> Cabinets { get; }
        List<CTAMRole_Cabinet> RoleCabinets { get; }
        List<CabinetAction> CabinetActions { get; }
        List<CabinetColumn> CabinetColumns { get; }
        List<CabinetCell> CabinetCells { get; }
        List<CabinetDoor> CabinetDoor { get; }
        List<CabinetPosition> CabinetPositions { get; }
        List<CabinetLog> CabinetLogs { get; }
        List<CabinetAccessInterval> CabinetAccessIntervals { get; }
        List<ItemType> ItemTypes { get; }
        List<CTAMRole_ItemType> RoleItemTypes { get; }
        List<ErrorCode> ErrorCodes { get; }
        List<Item> Items { get; }
        List<ItemDetail> ItemDetails { get; }
        List<ItemSet> ItemSets { get; }
        List<ItemType_ErrorCode> ItemTypeErrorCodes { get; }
        List<AllowedCabinetPosition> AllowedCabinetPositions { get; }
        List<CabinetPositionContent> CabinetPositionContents { get; }
        List<CTAMUserPersonalItem> CTAMUserPersonalItems { get; }
        List<CTAMUserInPossession> CTAMUserInPossessions { get; }
        List<CabinetStock> CabinetStocks { get; }
        List<ItemToPick> ItemToPicks { get; }
        List<MailQueue> MailQueue { get; }
        List<MailTemplate> MailTemplates { get; }
    }

}
