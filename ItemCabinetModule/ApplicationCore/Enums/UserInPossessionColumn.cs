namespace ItemCabinetModule.ApplicationCore.Enums
{
    public enum UserInPossessionColumn
    {
        // Keep this in sync with CloudApi method ToColumnString of UserInPossessionDTO and
        // enum UserInPossessionColumn and UserInPossessionColumnsExtension GetTranslation

        Status = 0,
        OutCTAMUserName = 1,
        OutCTAMUserEmail = 2,
        OutDT = 3,
        CabinetPositionOutCabinetName = 4,
        InCTAMUserName = 5,
        InCTAMUserEmail = 6,
        InDT = 7,
        CabinetPositionInCabinetName = 8,
        Item_Description = 9,
        ItemType_Description = 10,
        None = 9999,
    }
}
