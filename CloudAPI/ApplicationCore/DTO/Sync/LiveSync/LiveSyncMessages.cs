using CTAM.Core.Enums;
using System;
using System.Collections.Generic;

namespace CloudAPI.ApplicationCore.DTO.Sync.LiveSync
{

    public abstract class LiveSyncMessage
    {
        public ChangeAction ChangeAction { get; set; }
    }

    public interface IHasCabinetNumbers
    {
        List<string> CabinetNumbers { get; set; }
    }

    public class SingleLiveSyncMessage : IHasCabinetNumbers
    {
        public List<string> CabinetNumbers { get; set; }
    }

    public class CollectedLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public List<string> CabinetNumbers { get; set; }
        public List<CabinetUILiveSyncMessage> CabinetUIMessages { get; set; } = new List<CabinetUILiveSyncMessage>();
        public List<CabinetLiveSyncMessage> CabinetMessages { get; set; } = new List<CabinetLiveSyncMessage>();
        public List<CabinetDoorLiveSyncMessage> CabinetDoorMessages { get; set; } = new List<CabinetDoorLiveSyncMessage>();
        public List<CabinetStockLiveSyncMessage> CabinetStockMessages { get; set; } = new List<CabinetStockLiveSyncMessage>();
        public List<CabinetPositionLiveSyncMessage> CabinetPositionMessages { get; set; } = new List<CabinetPositionLiveSyncMessage>();
        public List<ErrorCodeLiveSyncMessage> ErrorCodeMessages { get; set; } = new List<ErrorCodeLiveSyncMessage>();
        public List<SettingLiveSyncMessage> SettingMessages { get; set; } = new List<SettingLiveSyncMessage>();
        public List<ItemLiveSyncMessage> ItemMessages { get; set; } = new List<ItemLiveSyncMessage>();
        public List<ItemTypeLiveSyncMessage> ItemTypeMessages { get; set; } = new List<ItemTypeLiveSyncMessage>();
        public List<UserLiveSyncMessage> UserMessages { get; set; } = new List<UserLiveSyncMessage>();
        public List<RoleLiveSyncMessage> RoleMessages { get; set; } = new List<RoleLiveSyncMessage>();
        public List<CabinetAccessIntervalLiveSyncMessage> CabinetAccessIntervalMessages { get; set; } = new List<CabinetAccessIntervalLiveSyncMessage>();
        public List<UserPersonalItemLiveSyncMessage> UserPersonalItemMessages { get; set; } = new List<UserPersonalItemLiveSyncMessage>();
        public List<UserInPossessionLiveSyncMessage> UserInPossessionMessages { get; set; } = new List<UserInPossessionLiveSyncMessage>();
        public List<UserRoleLiveSyncMessage> UserRoleMessages { get; set; } = new List<UserRoleLiveSyncMessage>();
        public List<RolePermissionLiveSyncMessage> RolePermissionMessages { get; set; } = new List<RolePermissionLiveSyncMessage>();
        public List<RoleCabinetLiveSyncMessage> RoleCabinetMessages { get; set; } = new List<RoleCabinetLiveSyncMessage>();
        public List<RoleItemTypeLiveSyncMessage> RoleItemTypeMessages { get; set; } = new List<RoleItemTypeLiveSyncMessage>();
        public List<ItemTypeErrorCodeLiveSyncMessage> ItemTypeErrorCodeMessages { get; set; } = new List<ItemTypeErrorCodeLiveSyncMessage>();
    }

    public class CabinetLiveSyncMessage : LiveSyncMessage
    {
        public string CabinetNumber { get; set; }
    }

    public class CabinetUILiveSyncMessage : LiveSyncMessage
    {
        public string CabinetNumber { get; set; }
    }

    public class CabinetDoorLiveSyncMessage : LiveSyncMessage
    {
        public int DoorID { get; set; }
        public string CabinetNumber { get; set; }
    }

    public class ItemLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public int ItemID { get; set; }
        public List<string> CabinetNumbers { get; set; }
    }

    public class ItemTypeLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public int ItemTypeID { get; set; }
        public List<string> CabinetNumbers { get; set; }
    }

    public class UserLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public string UserUID { get; set; }
        public List<string> CabinetNumbers { get; set; }
    }

    public class UserRoleLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public string UserUID { get; set; }
        public int RoleID { get; set; }
        public List<string> CabinetNumbers { get; set; }
    }

    public class RoleLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public int RoleID { get; set; }
        public List<string> CabinetNumbers { get; set; }
    }

    public class RolePermissionLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public int RoleID { get; set; }
        public int PermissionID { get; set; }
        public List<string> CabinetNumbers { get; set; }
    }

    public class RoleCabinetLiveSyncMessage : LiveSyncMessage
    {
        public int RoleID { get; set; }
        public string CabinetNumber { get; set; }
    }

    public class ItemTypeErrorCodeLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public List<string> CabinetNumbers { get; set; }
        public int ItemTypeID { get; set; }
        public int ErrorCodeID { get; set; }
    }

    public class RoleItemTypeLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public int RoleID { get; set; }
        public int ItemTypeID { get; set; }
        public int MaxQty { get; set; }
        public List<string> CabinetNumbers { get; set; }
    }

    public class CabinetAccessIntervalLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public int RoleID { get; set; }
        public int CabinetAccessIntervalID { get; set; }
        public List<string> CabinetNumbers { get; set; }
    }

    public class CabinetStockLiveSyncMessage : LiveSyncMessage
    {
        public int ItemTypeID { get; set; }
        public string CabinetNumber { get; set; }
    }

    public class CabinetPositionLiveSyncMessage : LiveSyncMessage
    {
        public int CabinetPositionID { get; set; }
        public string CabinetNumber { get; set; }
    }

    public class SettingLiveSyncMessage : LiveSyncMessage
    {
        public int SettingID { get; set; }
        public CTAMModule CTAMModule { get; set; }
    }


    public class ErrorCodeLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public List<string> CabinetNumbers { get; set; }
        public int ErrorCodeID { get; set; }
    }

    public class UserInPossessionLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public List<string> CabinetNumbers { get; set; }
        public Guid UserInPossessionID { get; set; }
    }

    public class UserPersonalItemLiveSyncMessage : LiveSyncMessage, IHasCabinetNumbers
    {
        public List<string> CabinetNumbers { get; set; }
        public int UserPersonalItemID { get; set; }
    }

}