using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core.Constants;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using UserRoleModule.ApplicationCore.Enums;

namespace CabinetModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class TestData : IDataInserts
    {
        public string Environment => DeploymentEnvironment.Test;

        public void InsertData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cabinet>()
                .HasData(
                    new Cabinet() { Name = "IBK Nieuw Vennep", CabinetNumber = "210309081254", CabinetType= CabinetType.Locker, Description = "Lockerkast Nieuw-Vennep", Email = "supportnieuwvennep@nautaconnect.com", LocationDescr = "Nieuw-Vennep", CabinetErrorMessage = "Neem contact op met administrator.", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Online, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
                    new Cabinet() { Name = "IBK Amsterdam", CabinetNumber = "210103102111", CabinetType=CabinetType.Locker, Description = "Lockerkast Amsterdam", Email = "supportamsterdam@nautaconnect.com", LocationDescr = "Amsterdam", CabinetErrorMessage = "Neem contact op met administrator.", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Online, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
                    new Cabinet() { Name = "IBK Den Haag", CabinetNumber = "200214160401", CabinetType = CabinetType.Locker, Description = "Lockerkast Den Haag", Email = "supportdenhaag@nautaconnect.com", LocationDescr = "Den Haag", CabinetErrorMessage = "Neem contact op met administrator.", CabinetLanguage = "nl-NL", IsActive = true, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Online, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode },
                    new Cabinet() { Name = "KeyConductor Tilburg", CabinetNumber = "190404194045", CabinetType = CabinetType.KeyConductor, Description = "KeyConductor Tilburg", Email = "supporttilburg@nautaconnect.com", LocationDescr = "Tilburg", CabinetErrorMessage = "Neem contact op met administrator.", CabinetLanguage = "nl-NL", IsActive = false, CreateDT = DateTime.UtcNow, Status = CabinetStatus.Offline, UpdateDT = DateTime.UtcNow, LoginMethod = LoginMethod.CardCode }
                );

            modelBuilder.Entity<CTAMRole_Cabinet>()
                .HasData(
                    new CTAMRole_Cabinet() { CabinetNumber = "210309081254", CTAMRoleID = 3, CreateDT = DateTime.UtcNow},
                    new CTAMRole_Cabinet() { CabinetNumber = "210309081254", CTAMRoleID = 6, CreateDT = DateTime.UtcNow },
                    new CTAMRole_Cabinet() { CabinetNumber = "210309081254", CTAMRoleID = 11, CreateDT = DateTime.UtcNow },
                    new CTAMRole_Cabinet() { CabinetNumber = "210103102111", CTAMRoleID = 4, CreateDT = DateTime.UtcNow },
                    new CTAMRole_Cabinet() { CabinetNumber = "210103102111", CTAMRoleID = 7, CreateDT = DateTime.UtcNow },
                    new CTAMRole_Cabinet() { CabinetNumber = "200214160401", CTAMRoleID = 5, CreateDT = DateTime.UtcNow },
                    new CTAMRole_Cabinet() { CabinetNumber = "200214160401", CTAMRoleID = 8, CreateDT = DateTime.UtcNow },
                    new CTAMRole_Cabinet() { CabinetNumber = "190404194045", CTAMRoleID = 9, CreateDT = DateTime.UtcNow },
                    new CTAMRole_Cabinet() { CabinetNumber = "190404194045", CTAMRoleID = 10, CreateDT = DateTime.UtcNow }
                );

            modelBuilder.Entity<CabinetAction>()
                .HasData(
                    new CabinetAction() { ID = Guid.NewGuid(), CabinetNumber = "210309081254", CabinetName = "IBK Nieuw Vennep", PositionAlias = "1.1", Action = CabinetActionStatus.PickUp, TakeItemDescription = "PickedUp Item", ActionDT = DateTime.UtcNow.AddDays(-1), UpdateDT = DateTime.UtcNow.AddDays(-1), CTAMUserUID = "gijs_123", CTAMUserName = "Gijs", CTAMUserEmail = "gijs@nautaconnect.com", LogResourcePath = "cabinetActionLog.pickup"}
                );

            modelBuilder.Entity<CabinetCellType>()
                .HasData(
                    new CabinetCellType() { ID = 1, Color = "Grijs", Depth = 30, Height = 30, Width = 30, LockType = "Default", LongDescr = "", Material = "Metaal", ShortDescr = "Locker 30x30x30", SpecCode = "L30", SpecType = SpecType.Locker, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new CabinetCellType() { ID = 2, Color = "Geen", Depth = 0, Height = 0, Width = 0, LockType = "Default", LongDescr = "", Material = "Insert", ShortDescr = "KeyCop Insert", SpecCode = "I", SpecType = SpecType.Blade, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow },
                    new CabinetCellType() { ID = 3, Color = "Grijs", Depth = 30, Height = 15, Width = 30, LockType = "Default", LongDescr = "", Material = "Metaal", ShortDescr = "Locker 30x15x30", SpecCode = "L15", SpecType = SpecType.Locker, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow }

                );

            modelBuilder.Entity<CabinetColumn>()
                .HasData(
                    new CabinetColumn() { ID = 1, CabinetNumber = "210309081254", Depth = 30, Height = 30, Width = 30, IsTemplate = false, ColumnNumber = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
                    new CabinetColumn() { ID = 2, CabinetNumber = "210309081254", Depth = 15, Height = 15, Width = 15, IsTemplate = false, ColumnNumber = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow}
                );

            modelBuilder.Entity<CabinetCell>()
                .HasData(
                    new CabinetCell() { ID = 1, CabinetCellTypeID = 1, CabinetColumnID = 1, X = 1, Y = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow},
                    new CabinetCell() { ID = 2, CabinetCellTypeID = 1, CabinetColumnID = 1, X = 1, Y = 2, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow }
                );

            modelBuilder.Entity<CabinetDoor>()
               .HasData(
                    new CabinetDoor() { ID = 1, Alias = "Door_1", CabinetNumber = "190404194045", Status = CabinetDoorStatus.OK, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow }
                );

            modelBuilder.Entity<CabinetPosition>()
                .HasData(
                    new CabinetPosition() { ID = 1, CabinetNumber = "210309081254", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 2, CabinetNumber = "210309081254", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 3, CabinetNumber = "210309081254", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 4, CabinetNumber = "210309081254", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 5, CabinetNumber = "210309081254", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 6, CabinetNumber = "210309081254", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.6", PositionNumber = 6, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 6, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 7, CabinetNumber = "210309081254", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.7", PositionNumber = 7, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 7, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 8, CabinetNumber = "210309081254", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.8", PositionNumber = 8, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 8, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 9, CabinetNumber = "210309081254", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.9", PositionNumber = 9, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 9, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 10, CabinetNumber = "210309081254", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.10", PositionNumber = 10, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 10, MaxNrOfItems = 1 },

                    new CabinetPosition() { ID = 11, CabinetNumber = "210103102111", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 12, CabinetNumber = "210103102111", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 13, CabinetNumber = "210103102111", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 14, CabinetNumber = "210103102111", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
                   
                    new CabinetPosition() { ID = 15, CabinetNumber = "200214160401", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 16, CabinetNumber = "200214160401", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 17, CabinetNumber = "200214160401", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 18, CabinetNumber = "200214160401", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 19, CabinetNumber = "200214160401", CabinetCellTypeID = 1, CabinetDoorID = null, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Locker, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },

                    new CabinetPosition() { ID = 20, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.1", PositionNumber = 1, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 1, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 21, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.2", PositionNumber = 2, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 2, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 22, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.3", PositionNumber = 3, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 3, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 23, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.4", PositionNumber = 4, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 4, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 24, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.5", PositionNumber = 5, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 5, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 25, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.6", PositionNumber = 6, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 6, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 26, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.7", PositionNumber = 7, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 7, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 27, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.8", PositionNumber = 8, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 8, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 28, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.9", PositionNumber = 9, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 9, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 29, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.10", PositionNumber = 10, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 10, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 30, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.11", PositionNumber = 11, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 11, MaxNrOfItems = 1 },
                    new CabinetPosition() { ID = 31, CabinetNumber = "190404194045", CabinetCellTypeID = 2, CabinetDoorID = 1, CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, IsAllocated = false, PositionAlias = "1.12", PositionNumber = 12, PositionType = PositionType.Keycop, Status = (int)CabinetPositionStatus.OK, BladeNo = 1, BladePosNo = 12, MaxNrOfItems = 1 }


                );

            modelBuilder.Entity<CabinetLog>()
                .HasData(
                    new CabinetLog() { ID = 1, CabinetNumber = "210309081254", CabinetName = "IBK Nieuw Vennep", Level = LogLevel.Info, LogResourcePath = "INFO: IBK 210309081254 gestart", LogDT = DateTime.UtcNow.AddHours(-1), Source = LogSource.CabinetUI, UpdateDT = DateTime.UtcNow.AddHours(-1) },
                    new CabinetLog() { ID = 2, CabinetNumber = "210103102111", CabinetName = "IBK Amsterdam", Level = LogLevel.Info, LogResourcePath = "INFO: IBK 210103102111 gestart", LogDT = DateTime.UtcNow.AddHours(-1), Source = LogSource.CabinetUI, UpdateDT = DateTime.UtcNow.AddHours(-1) },
                    new CabinetLog() { ID = 3, CabinetNumber = "200214160401", CabinetName = "IBK Den Haag", Level = LogLevel.Info, LogResourcePath = "INFO: IBK 200214160401 gestart", LogDT = DateTime.UtcNow.AddHours(-1), Source = LogSource.CabinetUI, UpdateDT = DateTime.UtcNow.AddHours(-1) }
                );

            modelBuilder.Entity<CabinetAccessInterval>()
                .HasData(
                    new CabinetAccessInterval() { ID = 1, CTAMRoleID = 11, StartWeekDayNr = 0, StartTime = TimeSpan.Parse("12:00"), EndWeekDayNr = 6, EndTime = TimeSpan.Parse("17:00")}
                );
        }
    }
}
