using System;
using CTAM.Core.Constants;
using CTAM.Core.Interfaces;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using Microsoft.EntityFrameworkCore;

namespace ItemCabinetModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class TestData : IDataInserts
    {
        public string Environment => DeploymentEnvironment.Test;

        public void InsertData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AllowedCabinetPosition>()
               .HasData(
                    // Item is allowed only in 1 cabinet (210309081254) but 2 positions
                    new AllowedCabinetPosition() { ItemID = 2, CabinetPositionID = 1, CreateDT = DateTime.UtcNow, IsBaseCabinetPosition = true },
                    new AllowedCabinetPosition() { ItemID = 2, CabinetPositionID = 2, CreateDT = DateTime.UtcNow, IsBaseCabinetPosition = false },

                    // Item is allowed in 2 cabinets (210309081254 and 210103102111) 1 position in each
                    new AllowedCabinetPosition() { ItemID = 3, CabinetPositionID = 13, CreateDT = DateTime.UtcNow, IsBaseCabinetPosition = false },
                    new AllowedCabinetPosition() { ItemID = 3, CabinetPositionID = 5, CreateDT = DateTime.UtcNow, IsBaseCabinetPosition = true },

                    // Item is allowed only in 1 cabinet (210103102111) and only 1 position
                    new AllowedCabinetPosition() { ItemID = 18, CabinetPositionID = 12, CreateDT = DateTime.UtcNow, IsBaseCabinetPosition = true },

                    // Item is allowed only in 1 cabinet (210309081254) and only 1 position
                    new AllowedCabinetPosition() { ItemID = 4, CabinetPositionID = 7, CreateDT = DateTime.UtcNow, IsBaseCabinetPosition = true }
                );

            modelBuilder.Entity<CabinetPositionContent>()
               .HasData(
                    // Item returned in Cabinet 210309081254
                    new CabinetPositionContent() { ItemID = 7, CabinetPositionID = 6, CreateDT = DateTime.UtcNow },
                    new CabinetPositionContent() { ItemID = 13, CabinetPositionID = 3, CreateDT = DateTime.UtcNow },
                    new CabinetPositionContent() { ItemID = 15, CabinetPositionID = 4, CreateDT = DateTime.UtcNow },
                    new CabinetPositionContent() { ItemID = 4, CabinetPositionID = 7, CreateDT = DateTime.UtcNow },
                    new CabinetPositionContent() { ItemID = 17, CabinetPositionID = 10, CreateDT = DateTime.UtcNow },
                    new CabinetPositionContent() { ItemID = 26, CabinetPositionID = 5, CreateDT = DateTime.UtcNow },
                    // Item returned in Cabinet 210103102111
                    new CabinetPositionContent() { ItemID = 10, CabinetPositionID = 12, CreateDT = DateTime.UtcNow }
                );

            modelBuilder.Entity<CTAMUserPersonalItem>()
              .HasData(
                   new CTAMUserPersonalItem() { ID = 1, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", ItemID = 25, Status = UserPersonalItemStatus.OK, UpdateDT = DateTime.UtcNow },
                   new CTAMUserPersonalItem() { ID = 2, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", ItemID = 31, Status = UserPersonalItemStatus.OK, UpdateDT = DateTime.UtcNow },
                   new CTAMUserPersonalItem() { ID = 3, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CabinetNumber = "210309081254", ItemID = 26, ReplacementItemID = 28, Status = UserPersonalItemStatus.Defect, UpdateDT = DateTime.UtcNow },
                   new CTAMUserPersonalItem() { ID = 4, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", ItemID = 32, Status = UserPersonalItemStatus.OK, UpdateDT = DateTime.UtcNow },
                   new CTAMUserPersonalItem() { ID = 5, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", ItemID = 27, Status = UserPersonalItemStatus.OK, UpdateDT = DateTime.UtcNow },
                   new CTAMUserPersonalItem() { ID = 6, CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", ItemID = 33, ReplacementItemID = 34, Status = UserPersonalItemStatus.Defect, UpdateDT = DateTime.UtcNow }
               );

            modelBuilder.Entity<CTAMUserInPossession>()
               .HasData(
                    new CTAMUserInPossession() { ID = Guid.Parse("c084e3b9-42f9-4bc7-9250-01ad8fde7a09"), ItemID = 1, CabinetPositionIDOut = 1, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "gijs_123", CTAMUserNameOut = "Gijs", CTAMUserEmailOut = "gijs@nautaconnect.com", OutDT = DateTime.UtcNow.AddDays(-6), CabinetPositionIDIn=1, CabinetNumberIn = "210309081254" , CabinetNameIn = "IBK Nieuw Vennep", CTAMUserUIDIn = "gijs_123", CTAMUserNameIn = "Gijs", CTAMUserEmailIn = "gijs@nautaconnect.com", InDT = DateTime.UtcNow.AddDays(-5), ReturnBeforeDT=DateTime.UtcNow.AddDays(-4), Status = UserInPossessionStatus.Returned },
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc02"), ItemID = 1, CabinetPositionIDOut = 1, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "gijs_123", CTAMUserNameOut = "Gijs", CTAMUserEmailOut = "gijs@nautaconnect.com", OutDT = DateTime.UtcNow, ReturnBeforeDT = DateTime.UtcNow.AddDays(4), Status = UserInPossessionStatus.Picked },

                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc03"), ItemID = 2, CabinetPositionIDOut = 2, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "kamran_123", CTAMUserNameOut = "Kamran", CTAMUserEmailOut = "kamran@nautaconnect.com", OutDT = DateTime.UtcNow, ReturnBeforeDT = DateTime.UtcNow.AddDays(4), Status = UserInPossessionStatus.Picked },
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc04"), ItemID = 3, CabinetPositionIDOut = 3, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "faysal_123", CTAMUserNameOut = "Faysal", CTAMUserEmailOut = "faysal@nautaconnect.com", OutDT = DateTime.UtcNow, ReturnBeforeDT = DateTime.UtcNow.AddDays(4), Status = UserInPossessionStatus.Picked },

                    // Agent005, personalitem pportofoon5 en ptelefoon5
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc30"), ItemID = 25, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CTAMUserNameOut = "Agent005", CTAMUserEmailOut = "agent005@politie.nl", OutDT = DateTime.UtcNow.AddDays(-12), CabinetPositionIDIn = 1, CabinetNumberIn = "210309081254", CabinetNameIn = "IBK Nieuw Vennep", CTAMUserUIDIn = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CTAMUserNameIn = "Agent005", CTAMUserEmailIn = "agent005@politie.nl", InDT = DateTime.UtcNow.AddDays(-7), Status = UserInPossessionStatus.Returned },
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc15"), ItemID = 25, CabinetPositionIDOut = 1, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CTAMUserNameOut = "Agent005", CTAMUserEmailOut = "agent005@politie.nl", OutDT = DateTime.UtcNow.AddDays(-6), CabinetPositionIDIn = 1, CabinetNumberIn = "210309081254", CabinetNameIn = "IBK Nieuw Vennep", CTAMUserUIDIn = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CTAMUserNameIn = "Agent005", CTAMUserEmailIn = "agent005@politie.nl", InDT = DateTime.UtcNow.AddDays(-1), Status = UserInPossessionStatus.Returned },
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc05"), ItemID = 25, CabinetPositionIDOut = 1, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CTAMUserNameOut = "Agent005", CTAMUserEmailOut = "agent005@politie.nl", OutDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc06"), ItemID = 31, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CTAMUserNameOut = "Agent005", CTAMUserEmailOut = "agent005@politie.nl", OutDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },

                    // Agent006, personalitem ptelefoon6 en pportofoon6 => vervangen (defect) door tmpportofoon10
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc07"), ItemID = 32, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CTAMUserNameOut = "Agent006", CTAMUserEmailOut = "agent006@politie.nl", OutDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc08"), ItemID = 26, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CTAMUserNameOut = "Agent006", CTAMUserEmailOut = "agent006@politie.nl", OutDT = DateTime.UtcNow.AddDays(-1), CTAMUserUIDIn = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CTAMUserNameIn = "Agent006", CTAMUserEmailIn = "agent006@politie.nl", InDT = DateTime.UtcNow, Status = UserInPossessionStatus.Returned },
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc09"), ItemID = 28, CabinetPositionIDOut = 3, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CTAMUserNameOut = "Agent006", CTAMUserEmailOut = "agent006@politie.nl", OutDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },

                    // Agent007, personalitem pportofoon7 en ptelefoon7 (blocked en bezit tlb) => vervangen dooro tmptelefoon7
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc10"), ItemID = 27, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CTAMUserNameOut = "Agent007", CTAMUserEmailOut = "agent007@politie.nl", OutDT = DateTime.UtcNow, Status = UserInPossessionStatus.Picked },
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc11"), ItemID = 33, CabinetNameOut = "Initiele import", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CTAMUserNameOut = "Agent007", CTAMUserEmailOut = "agent007@politie.nl", OutDT = DateTime.UtcNow.AddDays(-2), CTAMUserUIDIn = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CTAMUserNameIn = "Agent007", CTAMUserEmailIn = "agent007@politie.nl", InDT = DateTime.UtcNow, CabinetPositionIDIn = 4, CabinetNumberIn = "210309081254", CabinetNameIn = "IBK Nieuw Vennep", Status = UserInPossessionStatus.Returned },
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc12"), ItemID = 34, CabinetPositionIDOut = 4, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CTAMUserNameOut = "Agent007", CTAMUserEmailOut = "agent007@politie.nl", OutDT = DateTime.UtcNow.AddDays(-1), Status = UserInPossessionStatus.Picked },
                    new CTAMUserInPossession() { ID = Guid.Parse("96db88f3-0b33-455c-873f-d7762f82bc13"), ItemID = 33, CabinetPositionIDOut = 4, CabinetNumberOut = "210309081254", CabinetNameOut = "IBK Nieuw Vennep", CTAMUserUIDOut = "c084e3b9-42f9-4bc7-9250-01ad8fde7008", CTAMUserNameOut = "Tlb001", CTAMUserEmailOut = "beheer001@politie.nl", OutDT = DateTime.UtcNow, Status = UserInPossessionStatus.Removed }
                );

            modelBuilder.Entity<CabinetStock>()
                .HasData(
                    new CabinetStock() { CabinetNumber = "210309081254", ItemTypeID = 5, MinimalStock = 1, ActualStock = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
                    new CabinetStock() { CabinetNumber = "210309081254", ItemTypeID = 3, MinimalStock = 0, ActualStock = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
                    new CabinetStock() { CabinetNumber = "210309081254", ItemTypeID = 1, MinimalStock = 1, ActualStock = 0, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = CabinetStockStatus.WarningBelowMinimumSend },
                    new CabinetStock() { CabinetNumber = "210309081254", ItemTypeID = 4, MinimalStock = 0, ActualStock = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK },
                    new CabinetStock() { CabinetNumber = "210103102111", ItemTypeID = 2, MinimalStock = 1, ActualStock = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, Status = (int)CabinetStockStatus.OK }
                );

            modelBuilder.Entity<ItemToPick>()
               .HasData(
                    new ItemToPick() { ID = 1, ItemID = 13, CabinetPositionID = 3, CTAMUserUID = "gijs_123", Status = ItemToPickStatus.ReadyToPick }
                );
        }
    }
}
