using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core.Constants;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace CabinetModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class CoreData : IDataInserts
    {
        public string Environment => DeploymentEnvironment.Core;

        public void InsertData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CabinetCellType>()
                .HasData(
                    new CabinetCellType() { ID = 1, Color = "None", Depth = 0, Height = 0, Width = 0, LockType = "Type of lock", LongDescr = "", Material = "Insert", ShortDescr = "KeyCop INS", SpecCode = "KEY", SpecType = SpecType.Blade, CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1187), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1187) },
                    new CabinetCellType() { ID = 2, Color = "Grey", Depth = 48, Height = 15, Width = 18, LockType = "Type of lock", LongDescr = "", Material = "Metal", ShortDescr = "NP Klein", SpecCode = "NP18", SpecType = SpecType.Locker, CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191) },
                    new CabinetCellType() { ID = 3, Color = "Grey", Depth = 48, Height = 15, Width = 43, LockType = "Type of lock", LongDescr = "", Material = "Metal", ShortDescr = "NP Groot", SpecCode = "NP43", SpecType = SpecType.Locker, CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191) },
                    new CabinetCellType() { ID = 4, Color = "Grey", Depth = 100, Height = 100, Width = 100, LockType = "Type of lock", LongDescr = "", Material = "Metal", ShortDescr = "L100x100x100", SpecCode = "L100", SpecType = SpecType.Locker, CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191) },
                    new CabinetCellType() { ID = 5, Color = "Grey", Depth = 50, Height = 50, Width = 50, LockType = "Type of lock", LongDescr = "", Material = "Metal", ShortDescr = "L50x50x50", SpecCode = "L50", SpecType = SpecType.Locker, CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 179, DateTimeKind.Utc).AddTicks(1191) }
                );
        }
    }
}