using CTAMSharedLibrary.Resources;
namespace CabinetModule.ApplicationCore.Enums
{
    public enum CabinetActionStatus
    {
        None = 0,

        // TerugOmruilen
        SwapBack = 1,

        // TerugBrengen
        Return = 2,

        // Afhalen
        PickUp = 3,

        // Lenen
        Borrow = 4,

        // Repareren
        Repair = 5,

        // Vervangen
        Replace = 6,

        // Toevoegen
        Add = 7,

        // Verwijderen
        Remove = 8,

        // Omruilen
        Swap = 9,

        // Gepepareerd
        Repaired = 10
    }
    public static class CabinetActionStatusExtension
    {

        public static string GetTranslation(this CabinetActionStatus status)
        {
            return CloudTranslations.ResourceManager.GetString($"logs.cabinetActionStatus.{status.ToString().ToLower()}");
        }
    }
}
