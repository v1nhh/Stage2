using System;
using CTAM.Core.Constants;
using CTAM.Core.Interfaces;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using Microsoft.EntityFrameworkCore;

namespace ItemModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class TestData : IDataInserts
    {
        public string Environment => DeploymentEnvironment.Test;

        public void InsertData(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ItemType>()
               .HasData(
                    new ItemType() { ID = 1, Description = "C2000 portofoon", IsStoredInLocker = true, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 5, Width = 4, Height = 20, TagType = TagType.NFC },
                    new ItemType() { ID = 2, Description = "Mobiele telefoon", IsStoredInLocker = true, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 1, Width = 7, Height = 14, TagType = TagType.LF },
                    new ItemType() { ID = 3, Description = "Bodycam", IsStoredInLocker = true, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 5, Width = 5, Height = 5, TagType = TagType.LF },
                    new ItemType() { ID = 4, Description = "Laptop", IsStoredInLocker = true, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 4, Width = 28, Height = 20, TagType = TagType.LF },
                    new ItemType() { ID = 5, Description = "Tablet", IsStoredInLocker = true, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 1, Width = 15, Height = 20, TagType = TagType.LF },
                    new ItemType() { ID = 6, Description = "Sleutel", IsStoredInLocker = false, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Depth = 0, Width = 0, Height = 0, TagType = TagType.Barcode, RequiresMileageRegistration = true }
                );

            modelBuilder.Entity<CTAMRole_ItemType>()
               .HasData(
                    // IBK Nieuw-Vennep
                    new CTAMRole_ItemType() { CTAMRoleID = 6, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1  },
                    new CTAMRole_ItemType() { CTAMRoleID = 6, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    new CTAMRole_ItemType() { CTAMRoleID = 6, ItemTypeID = 3, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    new CTAMRole_ItemType() { CTAMRoleID = 6, ItemTypeID = 4, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    new CTAMRole_ItemType() { CTAMRoleID = 6, ItemTypeID = 5, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    // IBK Amsterdam
                    new CTAMRole_ItemType() { CTAMRoleID = 7, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    new CTAMRole_ItemType() { CTAMRoleID = 7, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    new CTAMRole_ItemType() { CTAMRoleID = 7, ItemTypeID = 3, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    // IBK Den Haag
                    new CTAMRole_ItemType() { CTAMRoleID = 8, ItemTypeID = 1, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    new CTAMRole_ItemType() { CTAMRoleID = 8, ItemTypeID = 2, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    new CTAMRole_ItemType() { CTAMRoleID = 8, ItemTypeID = 3, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    new CTAMRole_ItemType() { CTAMRoleID = 8, ItemTypeID = 4, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    new CTAMRole_ItemType() { CTAMRoleID = 8, ItemTypeID = 5, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 },
                    // IBK KC Tilburg
                    new CTAMRole_ItemType() { CTAMRoleID = 10, ItemTypeID = 6, CreateDT = DateTime.UtcNow, MaxQtyToPick = 1 }
                );

            modelBuilder.Entity<ErrorCode>()
               .HasData(
                    new ErrorCode() { ID = 1, Code = "S", Description = "Scherm", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new ErrorCode() { ID = 2, Code = "Z", Description = "Zijaansluiting", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new ErrorCode() { ID = 3, Code = "N", Description = "Netwerkprobleem", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new ErrorCode() { ID = 4, Code = "AA", Description = "Accessoire-Aansluiting", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new ErrorCode() { ID = 5, Code = "U", Description = "Updateprobleem", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new ErrorCode() { ID = 6, Code = "PC", Description = "puk-code", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new ErrorCode() { ID = 7, Code = "OA", Description = "Onderaansluiting", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new ErrorCode() { ID = 8, Code = "A", Description = "Antenne", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new ErrorCode() { ID = 9, Code = "V", Description = "Volumeknop", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new ErrorCode() { ID = 10, Code = "OV", Description = "Overig", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow }
                );

            modelBuilder.Entity<Item>()
               .HasData(
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
                    new Item() { ID = 36, ItemTypeID = 2, Description = "TmpTelefoon 12", CreateDT = DateTime.UtcNow, Status = ItemStatus.ACTIVE, Barcode = "3333312", Tagnumber = "3333312", UpdateDT = DateTime.UtcNow, NrOfSubItems = 0, MaxLendingTimeInMins = 0, ExternalReferenceID = "CI3333312" }
                );

            modelBuilder.Entity<ItemDetail>()
              .HasData(
                   new ItemDetail() { ID = 1, ItemID = 21, Description = "Auto AA-00-01", CreateDT = DateTime.UtcNow, FreeText1 = "Audi A8" },
                   new ItemDetail() { ID = 2, ItemID = 22, Description = "Slot boven", CreateDT = DateTime.UtcNow, FreeText1 = "SN: AG.6453", FreeText2 = "MEGA" },
                   new ItemDetail() { ID = 3, ItemID = 23, Description = "Slot onder", CreateDT = DateTime.UtcNow, FreeText1 = "SN: 76733-1", FreeText2 = "FAAS EDELSTAHL GMBH & CO. KG" }
                );

            modelBuilder.Entity<ItemSet>()
              .HasData(
                   new ItemSet() { SetCode = "Item_Set_1", ItemID = 20, Status = ItemSetStatus.OK, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                   new ItemSet() { SetCode = "Item_Set_1", ItemID = 16, Status = ItemSetStatus.OK, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow }
                );

            modelBuilder.Entity<ItemType_ErrorCode>()
                .HasData(
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
                );
        }
    }
}
