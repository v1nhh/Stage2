using CTAMSharedLibrary.Resources;
namespace ItemCabinetModule.ApplicationCore.Enums
{
    public enum UserInPossessionStatus
    {
        Picked = 0,
        Returned = 1,
        Added = 2,
        Removed = 3,
        Overdue = 4,
        Unjustified = 5,
        InCorrectReturned = 6,
        DefectReturned = 7,
    }
    public static class UserInPossessionStatusExtension
    {

        public static string GetTranslation(this UserInPossessionStatus status)
        {
            return CloudTranslations.ResourceManager.GetString($"userInPossession.status.{status.ToString().ToLower()}");
        }
    }
}
