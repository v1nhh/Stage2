using CTAMSharedLibrary.Resources;
using System;
namespace ItemModule.ApplicationCore.Enums
{
    public enum ItemStatus
    {
        INITIAL = 0,
        ACTIVE = 1,
        NOT_ACTIVE = 2,
        DEFECT = 3,
        BLOCKED_FOR_SERVICE = 4,
        BEYOND_REPAIR = 5,
        IN_REPAIR = 6
    }

    public static class ItemStatusExtension
    {

        public static string GetTranslation(this ItemStatus status)
        {
            switch(status)
            {
                case ItemStatus.INITIAL:
                    return CloudTranslations.ResourceManager.GetString($"item.status.initial");
                case ItemStatus.ACTIVE:
                    return CloudTranslations.ResourceManager.GetString($"item.status.active");
                case ItemStatus.NOT_ACTIVE:
                    return CloudTranslations.ResourceManager.GetString($"item.status.notActive");
                case ItemStatus.DEFECT:
                    return CloudTranslations.ResourceManager.GetString($"item.status.defect");
                case ItemStatus.BLOCKED_FOR_SERVICE:
                    return CloudTranslations.ResourceManager.GetString($"item.status.blockedForService");
                case ItemStatus.BEYOND_REPAIR:
                    return CloudTranslations.ResourceManager.GetString($"item.status.beyondRepair");
                case ItemStatus.IN_REPAIR:
                    return CloudTranslations.ResourceManager.GetString($"item.status.inRepair");
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
