using CTAM.Core.Constants;
using CTAM.Core.Enums;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using UserRoleModule.ApplicationCore.Constants;
using UserRoleModule.ApplicationCore.Entities;

namespace UserRoleModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class CoreData : IDataInserts
    {
        public string Environment => DeploymentEnvironment.Core;

        public void InsertData(ModelBuilder modelBuilder)
        {
            
            
            modelBuilder.Entity<CTAMPermission>()
                 .HasData(
                      new CTAMPermission() { ID = 1, CTAMModule = CTAMModule.Cabinet, Description = "Swap", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(232), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(237), },
                      new CTAMPermission() { ID = 3, CTAMModule = CTAMModule.Cabinet, Description = "Return", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(241), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(242), },
                      //new CTAMPermission() { ID = 4, CTAMModule = CTAMModule.Cabinet, Description = "Pickup", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                      new CTAMPermission() { ID = 5, CTAMModule = CTAMModule.Cabinet, Description = "Borrow", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(242), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(243), },
                      new CTAMPermission() { ID = 6, CTAMModule = CTAMModule.Cabinet, Description = "Repair", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(244), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(244), },
                      new CTAMPermission() { ID = 7, CTAMModule = CTAMModule.Cabinet, Description = "Replace", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(245), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(245), },
                      new CTAMPermission() { ID = 8, CTAMModule = CTAMModule.Cabinet, Description = "Remove", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(247), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(247), },
                      new CTAMPermission() { ID = 9, CTAMModule = CTAMModule.Cabinet, Description = "Add", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(248), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(248), },
                      new CTAMPermission() { ID = 10, CTAMModule = CTAMModule.Management, Description = "Read", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(249), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(249), },
                      new CTAMPermission() { ID = 11, CTAMModule = CTAMModule.Management, Description = "Write", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(251), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(252) , },
                      new CTAMPermission() { ID = 12, CTAMModule = CTAMModule.Management, Description = "Delete", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(252), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(253)    , },
                      new CTAMPermission() { ID = 13, CTAMModule = CTAMModule.Cabinet, Description = "Admin", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(256), UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(256)    , }
                  );

            

            modelBuilder.Entity<CTAMSetting>()
                .HasData(
                    new CTAMSetting() { ID = 3, ParName = CTAMSettingKeys.EmailDefaultLanguage, ParValue = "nl-NL", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(455), CTAMModule = CTAMModule.Management, UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(456) },
                    new CTAMSetting() { ID = 4, ParName = CTAMSettingKeys.HWApiTimeout, ParValue = "30", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(456), CTAMModule = CTAMModule.Cabinet, UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(457) },
                    new CTAMSetting() { ID = 5, ParName = CTAMSettingKeys.ShouldSendCabinetLogin, ParValue = "false", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(458), CTAMModule = CTAMModule.Management, UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(459) },
                    new CTAMSetting() { ID = 6, ParName = CTAMSettingKeys.ShowLanguageSelectionOnCabinet, ParValue = "false", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(459), CTAMModule = CTAMModule.Cabinet, UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(459) },
                    new CTAMSetting() { ID = 7, ParName = CTAMSettingKeys.BulkMailAmount, ParValue = "5", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(461), CTAMModule = CTAMModule.Management, UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(461) },
                    new CTAMSetting() { ID = 8, ParName = CTAMSettingKeys.CleanOperationalLogsInterval, ParValue = "{\"LogType\":0,\"Amount\":1,\"IntervalType\":3}", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(462), CTAMModule = CTAMModule.Management, UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(462) },
                    new CTAMSetting() { ID = 9, ParName = CTAMSettingKeys.CleanTechnicalLogsInterval, ParValue = "{\"LogType\":1,\"Amount\":1,\"IntervalType\":3}", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(463), CTAMModule = CTAMModule.Management, UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(463) },
                    new CTAMSetting() { ID = 10, ParName = CTAMSettingKeys.CleanManagementLogsInterval, ParValue = "{\"LogType\":2,\"Amount\":1,\"IntervalType\":3}", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(463), CTAMModule = CTAMModule.Management, UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(464) },
                    new CTAMSetting() { ID = 11, ParName = CTAMSettingKeys.PasswordPolicy, ParValue = ((int)PasswordPolicy.High).ToString(), CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(465), CTAMModule = CTAMModule.Management, UpdateDT = new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(465) }
                );
        }
    }
}
