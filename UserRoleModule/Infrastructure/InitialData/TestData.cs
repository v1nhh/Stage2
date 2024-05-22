using System;
using CTAM.Core.Constants;
using CTAM.Core.Enums;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using UserRoleModule.ApplicationCore.Entities;

namespace UserRoleModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class TestData: IDataInserts
    {
        public string Environment => DeploymentEnvironment.Test;

        public void InsertData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CTAMUser>()
                .HasData(
                    new CTAMUser() { Name = "Gijs", PhoneNumber = "+31610000000", UID = "gijs_123", Password = "1234", LoginCode = "000001", PinCode = "1234", CardCode = "11111", CreateDT = DateTime.UtcNow, Email = "gijs@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Ed", PhoneNumber = "+31610000001", UID = "ed_123", Password = "1234", LoginCode = "000002", PinCode = "1234", CardCode = "22222", CreateDT = DateTime.UtcNow, Email = "ed@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Lieuwe", PhoneNumber = "+31610000006", UID = "lieuwe_123", Password = "1234", LoginCode = "000007", PinCode = "1234", CardCode = "77777", CreateDT = DateTime.UtcNow, Email = "lieuwe@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Faysal", PhoneNumber = "+31610000002", UID = "faysal_123", Password = "1234", LoginCode = "000003", PinCode = "1234", CardCode = "33333", CreateDT = DateTime.UtcNow, Email = "faysal@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Samuel", PhoneNumber = "+31610000003", UID = "samuel_123", Password = "1234", LoginCode = "000004", PinCode = "1234", CardCode = "44444", CreateDT = DateTime.UtcNow, Email = "samuel@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Kamran", PhoneNumber = "+31610000004", UID = "kamran_123", Password = "1234", LoginCode = "000005", PinCode = "1234", CardCode = "55555", CreateDT = DateTime.UtcNow, Email = "kamran@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Jay", PhoneNumber = "+31610000005", UID = "jay_123", Password = "1234", LoginCode = "000006", PinCode = "1234", CardCode = "666666", CreateDT = DateTime.UtcNow, Email = "jay@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Sam", PhoneNumber = "+31610000007", UID = "sam_123", Password = "1234", LoginCode = "000008", PinCode = "1234", CardCode = "88888", CreateDT = DateTime.UtcNow, Email = "sam@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Ferhat", PhoneNumber = "+31610000009", UID = "ferhat_123", Password = "1234", LoginCode = "000009", PinCode = "1234", CardCode = "99999", CreateDT = DateTime.UtcNow, Email = "ferhat@nautaconnect.com", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Agent005", PhoneNumber = "+31610001005", UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", Password = "1234", LoginCode = "001005", PinCode = "1234", CardCode = "00005", CreateDT = DateTime.UtcNow, Email = "agent005@politie.nl", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Agent006", PhoneNumber = "+31610001006", UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", Password = "1234", LoginCode = "001006", PinCode = "1234", CardCode = "00006", CreateDT = DateTime.UtcNow, Email = "agent006@politie.nl", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Agent007", PhoneNumber = "+31610001007", UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", Password = "1234", LoginCode = "001007", PinCode = "1234", CardCode = "00007", CreateDT = DateTime.UtcNow, Email = "james@politie.nl", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow },
                    new CTAMUser() { Name = "Tlb001", PhoneNumber = "+31610001008", UID = "c084e3b9-42f9-4bc7-9250-01ad8fde7008", Password = "1234", LoginCode = "001008", PinCode = "1234", CardCode = "00008", CreateDT = DateTime.UtcNow, Email = "beheer001@politie.nl", LanguageCode = "nl-NL", UpdateDT = DateTime.UtcNow }
                );

            modelBuilder.Entity<CTAMRole>()
                .HasData(
                    new CTAMRole() { ID = 1, Description = "Admin", CreateDT = DateTime.UtcNow, ValidFromDT = DateTime.UtcNow, ValidUntilDT = DateTime.MaxValue },
                    new CTAMRole() { ID = 2, Description = "Functioneel beheerder", CreateDT = DateTime.UtcNow, ValidFromDT = DateTime.UtcNow, ValidUntilDT = DateTime.MaxValue, },
                    new CTAMRole() { ID = 3, Description = "TLB Nieuw-Vennep", CreateDT = DateTime.UtcNow,  },
                    new CTAMRole() { ID = 4, Description = "TLB Amsterdam", CreateDT = DateTime.UtcNow, },
                    new CTAMRole() { ID = 5, Description = "TLB Den Haag", CreateDT = DateTime.UtcNow, },
                    new CTAMRole() { ID = 6, Description = "IBK Nieuw-Vennep", CreateDT = DateTime.UtcNow },
                    new CTAMRole() { ID = 7, Description = "IBK Amsterdam", CreateDT = DateTime.UtcNow },
                    new CTAMRole() { ID = 8, Description = "IBK Den Haag", CreateDT = DateTime.UtcNow },
                    new CTAMRole() { ID = 9, Description = "TLB KC Tilburg", CreateDT = DateTime.UtcNow },
                    new CTAMRole() { ID = 10, Description = "IBK KC Tilburg", CreateDT = DateTime.UtcNow },
                    new CTAMRole() { ID = 11, Description = "IBK Nieuw-Vennep middag ploeg", CreateDT = DateTime.UtcNow }
                );

            modelBuilder.Entity<CTAMUser_Role>()
                .HasData(
                    new CTAMUser_Role() { CTAMUserUID = "gijs_123", CTAMRoleID = 6 },
                    new CTAMUser_Role() { CTAMUserUID = "gijs_123", CTAMRoleID = 2 },
                    new CTAMUser_Role() { CTAMUserUID = "gijs_123", CTAMRoleID = 3 },
                    new CTAMUser_Role() { CTAMUserUID = "ed_123", CTAMRoleID = 1 },
                    new CTAMUser_Role() { CTAMUserUID = "ed_123", CTAMRoleID = 8 },
                    new CTAMUser_Role() { CTAMUserUID = "ed_123", CTAMRoleID = 2 },
                    new CTAMUser_Role() { CTAMUserUID = "ed_123", CTAMRoleID = 5 },
                    new CTAMUser_Role() { CTAMUserUID = "faysal_123", CTAMRoleID = 6 },
                    new CTAMUser_Role() { CTAMUserUID = "samuel_123", CTAMRoleID = 8 },
                    new CTAMUser_Role() { CTAMUserUID = "samuel_123", CTAMRoleID = 2 },
                    new CTAMUser_Role() { CTAMUserUID = "samuel_123", CTAMRoleID = 5 },
                    new CTAMUser_Role() { CTAMUserUID = "kamran_123", CTAMRoleID = 6 },
                    new CTAMUser_Role() { CTAMUserUID = "kamran_123", CTAMRoleID = 2 },
                    new CTAMUser_Role() { CTAMUserUID = "kamran_123", CTAMRoleID = 3 },
                    new CTAMUser_Role() { CTAMUserUID = "jay_123", CTAMRoleID = 6 },
                    new CTAMUser_Role() { CTAMUserUID = "sam_123", CTAMRoleID = 8 },
                    new CTAMUser_Role() { CTAMUserUID = "lieuwe_123", CTAMRoleID = 6 },
                    new CTAMUser_Role() { CTAMUserUID = "lieuwe_123", CTAMRoleID = 2 },
                    new CTAMUser_Role() { CTAMUserUID = "lieuwe_123", CTAMRoleID = 3 },
                    new CTAMUser_Role() { CTAMUserUID = "ferhat_123", CTAMRoleID = 11 },
                    new CTAMUser_Role() { CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7005", CTAMRoleID = 6 },
                    new CTAMUser_Role() { CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7006", CTAMRoleID = 6 },
                    new CTAMUser_Role() { CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7007", CTAMRoleID = 6 },
                    new CTAMUser_Role() { CTAMUserUID = "c084e3b9-42f9-4bc7-9250-01ad8fde7008", CTAMRoleID = 3 }
                );


            modelBuilder.Entity<CTAMSetting>()
                .HasData(
                    new CTAMSetting() { ID = 3, ParName = "email_default_language", ParValue = "nl-NL", CreateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Management, UpdateDT = DateTime.UtcNow },
                    new CTAMSetting() { ID = 4, ParName = "hwapi_timeout", ParValue = "30", CreateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Cabinet, UpdateDT = DateTime.UtcNow },
                    new CTAMSetting() { ID = 5, ParName = "should_send_cabinet_login", ParValue = "false", CreateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Management, UpdateDT = DateTime.UtcNow},
                    new CTAMSetting() { ID = 6, ParName = "show_language_selection_on_cabinet", ParValue = "true", CreateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Cabinet, UpdateDT = DateTime.UtcNow },
                    new CTAMSetting() { ID = 7, ParName = "bulk_mail_amount", ParValue = "5", CreateDT = DateTime.UtcNow, CTAMModule = CTAMModule.Management, UpdateDT = DateTime.UtcNow }
            );

            modelBuilder.Entity<CTAMPermission>()
                .HasData(
                    new CTAMPermission() { ID = 1, CTAMModule = CTAMModule.Cabinet, Description = "Swap", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 2, CTAMModule = CTAMModule.Cabinet, Description = "SwapBack", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 3, CTAMModule = CTAMModule.Cabinet, Description = "Return", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 4, CTAMModule = CTAMModule.Cabinet, Description = "Pickup", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 5, CTAMModule = CTAMModule.Cabinet, Description = "Borrow", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 6, CTAMModule = CTAMModule.Cabinet, Description = "Repair", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 7, CTAMModule = CTAMModule.Cabinet, Description = "Replace", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 8, CTAMModule = CTAMModule.Cabinet, Description = "Remove", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 9, CTAMModule = CTAMModule.Cabinet, Description = "Add", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 10, CTAMModule = CTAMModule.Management, Description = "Read", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 11, CTAMModule = CTAMModule.Management, Description = "Write", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, },
                    new CTAMPermission() { ID = 12, CTAMModule = CTAMModule.Management, Description = "Delete", CreateDT = DateTime.UtcNow, UpdateDT = DateTime.UtcNow, }
                );

            modelBuilder.Entity<CTAMRole_Permission>()
              .HasData(
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 2, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 10, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 11, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 1, CTAMPermissionID = 12, CreateDT = DateTime.UtcNow, },

                   new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 10, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 11, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 2, CTAMPermissionID = 12, CreateDT = DateTime.UtcNow, },

                   new CTAMRole_Permission() { CTAMRoleID = 3, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 3, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 3, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 3, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },

                   new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 2, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 4, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },

                   new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 2, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 5, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },

                   new CTAMRole_Permission() { CTAMRoleID = 6, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 6, CTAMPermissionID = 2, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 6, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 6, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 6, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },

                   new CTAMRole_Permission() { CTAMRoleID = 7, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 7, CTAMPermissionID = 2, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 7, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 7, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 7, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },

                   new CTAMRole_Permission() { CTAMRoleID = 8, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 8, CTAMPermissionID = 2, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 8, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 8, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 8, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },

                   new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 2, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 6, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 7, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 8, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 9, CTAMPermissionID = 9, CreateDT = DateTime.UtcNow, },

                   new CTAMRole_Permission() { CTAMRoleID = 10, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 10, CTAMPermissionID = 2, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 10, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 10, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 10, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, },

                   new CTAMRole_Permission() { CTAMRoleID = 11, CTAMPermissionID = 1, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 11, CTAMPermissionID = 2, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 11, CTAMPermissionID = 3, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 11, CTAMPermissionID = 4, CreateDT = DateTime.UtcNow, },
                   new CTAMRole_Permission() { CTAMRoleID = 11, CTAMPermissionID = 5, CreateDT = DateTime.UtcNow, }
               );
        }
    }
}
