using Bogus;
using CabinetModule.ApplicationCore.Entities;
using CabinetModule.ApplicationCore.Enums;
using CTAM.Core;
using CTAM.Core.Utilities;
using EFCore.BulkExtensions;
using ItemCabinetModule.ApplicationCore.Entities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.Entities;
using ItemModule.ApplicationCore.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserRoleModule.ApplicationCore.Entities;
using UserRoleModule.ApplicationCore.Enums;

namespace CTAMSeeder
{
    public class BogusDummyDataSeeder: IBulkDataSeeder
    {
        private readonly Faker _faker = new Faker();
        private readonly MainDbContext _dbContext;

        public BogusDummyDataSeeder(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task LinkUserPersonalItems()
        {
            Console.WriteLine("Called LinkUserPersonalItems");

            var itemsWithoutUIP = await (from item in _dbContext.Item()
                                           join uip in _dbContext.CTAMUserInPossession()
                                           on item.ID equals uip.ItemID into guip
                                           from subuip in guip.DefaultIfEmpty()
                                           where subuip == null && item.Status == ItemStatus.INITIAL
                                           select item).ToListAsync();

            var userUIDsWithoutUPI = await (from user in _dbContext.CTAMUser()
                                   join upi in _dbContext.CTAMUserPersonalItem()
                                   on user.UID equals upi.CTAMUserUID into gupi
                                   from subupi in gupi.DefaultIfEmpty()
                                   where subupi == null
                                   select user).Select(u => u.UID).ToListAsync();

            var newUPIs = new List<CTAMUserPersonalItem>() { };
            var newUIPs = new List<CTAMUserInPossession>() { };

            int maxSize = userUIDsWithoutUPI.Count > itemsWithoutUIP.Count
                ? itemsWithoutUIP.Count
                : userUIDsWithoutUPI.Count;

            for (int i = 0; i < maxSize - 100; i++)
            {
                itemsWithoutUIP[i].Status = ItemStatus.ACTIVE;

                newUPIs.Add(new CTAMUserPersonalItem()
                {
                    CTAMUserUID = userUIDsWithoutUPI[i],
                    ItemID = itemsWithoutUIP[i].ID,
                    Status = UserPersonalItemStatus.OK
                });

                newUIPs.Add(new CTAMUserInPossession()
                {
                    ID = Guid.NewGuid(),
                    ItemID = itemsWithoutUIP[i].ID,
                    Status = UserInPossessionStatus.Picked,
                    CTAMUserUIDOut = userUIDsWithoutUPI[i],
                    CTAMUserNameOut = "RandomTestName",
                    CTAMUserEmailOut = "RandomTestEmail",
                    OutDT = DateTime.UtcNow,
                    CreatedDT = DateTime.UtcNow,
                    CabinetNameOut = "Personal Item Testing Endpoint"
                });
            }

            await _dbContext.BulkUpdateAsync(itemsWithoutUIP);
            await _dbContext.BulkInsertAsync(newUPIs);
            await _dbContext.BulkInsertAsync(newUIPs);
            await _dbContext.SaveChangesAsync();
        }

        // UserRoleModule
        public async Task<List<CTAMUser>> AddUsers(int amount)
        {
            Console.WriteLine("Called AddUsers");
            var currentUsers = await _dbContext.CTAMUser().ToListAsync();
            //// get current unique values to prevent duplicates
            var currentLoginCodes = currentUsers.Select(u => u.LoginCode).ToHashSet();
            var currentCardCodes = currentUsers.Select(u => u.CardCode).ToHashSet();

            var codes = new Queue<string>();
            Random rnd = new Random();
            while (codes.Count < amount)
            {
                var r = rnd.Next().ToString("0000000000");//Math.Floor(Math.Random * 100000) + 1;
                if (!codes.Contains(r) && !currentLoginCodes.Contains(r) && !currentCardCodes.Contains(r))
                { //make sure number is 5 digit
                    codes.Enqueue(r);
                }
            }
            Console.WriteLine($"Generated codes amount for users: {codes.Count}");
            
            var bulkUsers = new Faker<CTAMUser>("nl")
                .RuleFor(u => u.UID, f => f.Random.Uuid().ToString())
                .RuleFor(u => u.Name, f => f.Person.FirstName)
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(firstName: u.Name, uniqueSuffix: f.UniqueIndex.ToString(), provider: "nautaconnect.com"))
                .RuleFor(u => u.LoginCode, (f, u) => codes.Dequeue())
                .RuleFor(u => u.CardCode, (f, u) => u.LoginCode)
                .RuleFor(u => u.LanguageCode, f => "nl-NL")
                .RuleFor(u => u.Password, f => "Test@1")
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber("+316########"))
                .Generate(amount);
            Console.WriteLine($"Generated users amount: {bulkUsers.Count}");
            //await _dbContext.CTAMUser().AddRangeAsync(bulkUsers);
            await _dbContext.BulkInsertAsync(bulkUsers);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Users inserted");
            return bulkUsers;
        }

        public async Task<List<CTAMRole>> AddRoles(int amount)
        {
            Console.WriteLine("Called AddRoles");
            var roles = new Faker<CTAMRole>("nl")
                //.RuleFor(r => r.ID, f => f.UniqueIndex)
                .RuleFor(r => r.Description, f => f.Name.JobTitle() + "_" + f.UniqueIndex.ToString())
                .Generate(amount);

            Console.WriteLine($"Generated roles amount: {roles.Count}");
            //await _dbContext.AddRangeAsync(roles);
            await _dbContext.BulkInsertAsync(roles, new BulkConfig { SetOutputIdentity = true });
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Roles inserted");
            return roles;
        }

        public async Task<List<CTAMRole_Permission>> AddRolePermissions(List<CTAMRole> roles, List<CTAMPermission> permissions)
        {
            Console.WriteLine("Called AddRolePermissions");
            var rolePermissions = _faker.MakeRelation(permissions, roles,
                (p, r) => new CTAMRole_Permission
                {
                    CTAMRoleID = r.ID,
                    CTAMPermissionID = p.ID
                }
            ).ToList();
            Console.WriteLine($"Generated amount of rolePermissions: {rolePermissions.Count}");
            //await _dbContext.AddRangeAsync(rolePermissions);
            await _dbContext.BulkInsertAsync(rolePermissions);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("RolePermissions inserted");

            return rolePermissions;
        }

        public async Task<List<CTAMUser_Role>> AddUserRoles(List<CTAMUser> users, List<CTAMRole> roles, int maxRolesPerUser)
        {
            Console.WriteLine("Called AddUserRoles");
            var userRoles = _faker.MakeRelation(users, roles,
                (u, r) => new CTAMUser_Role
                {
                    CTAMUserUID = u.UID,
                    CTAMRoleID = r.ID,
                },
                maxRolesPerUser
            ).ToList();

            Console.WriteLine($"Generated userRoles amount: {userRoles.Count}");
            //await _dbContext.AddRangeAsync(userRoles);
            await _dbContext.BulkInsertAsync(userRoles);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("UserRoles inserted");
            return userRoles;
        }


        // CabinetModule
        public async Task<List<Cabinet>> AddCabinets(int amount)
        {
            Console.WriteLine("Called AddCabinets");
            var cabinets = new Faker<Cabinet>("nl")
                .RuleFor(c => c.LocationDescr, f => f.Address.City())
                .RuleFor(c => c.CabinetNumber, f => f.UniqueIndex.ToString("000000000000"))
                .RuleFor(c => c.Name, (f, c) => "IBK " + c.LocationDescr + "_" + f.UniqueIndex.ToString())
                .RuleFor(c => c.CabinetType, f => f.PickRandom<CabinetType>())
                .RuleFor(c => c.Description, (f, c) => Enum.GetName(typeof(CabinetType), c.CabinetType) + c.LocationDescr)
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(firstName: c.LocationDescr, uniqueSuffix: f.UniqueIndex.ToString(), provider: "nautaconnect.com"))
                .RuleFor(c => c.LoginMethod, f => LoginMethod.CardCode)
                .RuleFor(c => c.Status, f => f.PickRandomWithout<CabinetStatus>(CabinetStatus.OfflineInUse, CabinetStatus.Syncing))
                .RuleFor(c => c.IsActive, f => f.Random.Bool())
                .RuleFor(c => c.CabinetLanguage, f => f.PickRandomParam<string>(new string[] {"nl-NL", "en-US"}))
                .Generate(600);

            Console.WriteLine($"Generated amount of cabinets: {cabinets.Count}");
            //await _dbContext.AddRangeAsync(cabinets);
            await _dbContext.BulkInsertAsync(cabinets);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Cabinets inserted");
            return cabinets;
        }

        public async Task<List<CTAMRole_Cabinet>> AddRoleCabinets(List<CTAMRole> roles, List<Cabinet> cabinets, int maxCabinetsPerRole)
        {
            Console.WriteLine("Called AddRoleCabinets");
            var roleCabinets = _faker.MakeRelation(roles, cabinets,
                (r, c) => new CTAMRole_Cabinet
                {
                    CabinetNumber = c.CabinetNumber,
                    CTAMRoleID = r.ID,
                },
                maxCabinetsPerRole
            ).ToList();

            Console.WriteLine($"Generated amount of roleCabinets: {roleCabinets.Count}");
            //await _dbContext.AddRangeAsync(roleCabinets);
            await _dbContext.BulkInsertAsync(roleCabinets);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("RoleCabinets inserted");
            return roleCabinets;
        }


        // ItemModule
        public async Task<List<ItemType>> AddItemTypes(int amount)
        {
            Console.WriteLine("Called AddItemTypes");
            var itemTypes = new Faker<ItemType>("nl")
                //.RuleFor(it => it.ID, f => f.UniqueIndex + 7)
                .RuleFor(it => it.Description, f => f.Commerce.Product() + "_" + f.Address.City() + "_" + f.UniqueIndex.ToString())
                .RuleFor(it => it.Width, f => f.Random.Int(1, 50))
                .RuleFor(it => it.Height, f => f.Random.Int(1, 50))
                .RuleFor(it => it.Depth, f => f.Random.Int(1, 50))
                .RuleFor(it => it.TagType, f => f.PickRandom<TagType>())
                .RuleFor(it => it.RequiresMileageRegistration, f => f.Random.Bool())
                .RuleFor(it => it.IsStoredInLocker, f => f.Random.Bool())
                .Generate(amount);

            Console.WriteLine($"Generated amount of itemTypes: {itemTypes.Count}");
            //await _dbContext.AddRangeAsync(itemTypes);
            await _dbContext.BulkInsertAsync(itemTypes, new BulkConfig { SetOutputIdentity = true });
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("ItemTypes inserted");
            return itemTypes;
        }

        public async Task<List<CTAMRole_ItemType>> AddRoleItemTypes(List<CTAMRole> roles, List<ItemType> itemTypes, int maxItemTypesPerRole)
        {
            Console.WriteLine("Called AddRoleItemTypes");
            var roleItemTypes = _faker.MakeRelation(roles, itemTypes,
                (r, it) => new CTAMRole_ItemType
                {
                    ItemTypeID = it.ID,
                    CTAMRoleID = r.ID,
                },
                maxItemTypesPerRole
            ).ToList();

            Console.WriteLine($"Generated amount of roleItemTypes: {roleItemTypes.Count}");
            //await _dbContext.AddRangeAsync(roleItemTypes);
            await _dbContext.BulkInsertAsync(roleItemTypes);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("RoleItemTypes inserted");
            return roleItemTypes;
        }

        public async Task<List<Item>> AddItems(int amount, List<ItemType> itemTypes)
        {
            Console.WriteLine("Called AddItems");
            var items = new Faker<Item>("nl")
                //.RuleFor(it => it.ID, f => f.UniqueIndex + 25)
                // Add ItemType to match item description with ItemType foreign key
                .RuleFor(it => it.ItemType, f => f.PickRandom(itemTypes))
                .RuleFor(it => it.ItemTypeID, (f, it) => it.ItemType.ID)
                .RuleFor(it => it.Description, (f, it) => it.ItemType.Description + "_" + f.UniqueIndex.ToString())
                .RuleFor(it => it.Barcode, f => f.Commerce.Ean8())
                .RuleFor(it => it.Tagnumber, f => f.Commerce.Ean8())
                .RuleFor(it => it.Status, f => f.PickRandom<ItemStatus>())
                .RuleFor(it => it.MaxLendingTimeInMins, f => f.Random.Int(0, 99999))
                .Generate(amount);
            // Remove ItemType to prevent miration errors
            items.ForEach(it => it.ItemType = null);

            Console.WriteLine($"Generated amount of items: {items.Count}");
            //await _dbContext.AddRangeAsync(items);
            await _dbContext.BulkInsertAsync(items, new BulkConfig { SetOutputIdentity = true });
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("Items inserted");
            return items;
        }

        public async Task<List<CabinetLog>> AddCabinetLogs(int amount, List<Cabinet> cabinets)
        {
            Console.WriteLine("Called AddCabinetLogs");
            var logs = new Faker<CabinetLog>("nl")
                .RuleFor(c => c.LogDT, f => f.Date.Between(DateTime.UtcNow.AddDays(-200), DateTime.UtcNow))
                .RuleFor(c => c.UpdateDT, (f, c) => c.LogDT)
                .RuleFor(c => c.Level, f => f.Random.Enum<LogLevel>())
                .RuleFor(c => c.CabinetNumber, f => f.PickRandom(cabinets).CabinetNumber)
                .RuleFor(c => c.CabinetName, (f, c) => cabinets.First(cab => cab.CabinetNumber == c.CabinetNumber).Name)
                .RuleFor(c => c.Source, f => LogSource.LocalAPI)
                .RuleFor(r => r.LogResourcePath, f => f.Random.Words(5))
                .Generate(amount);

            Console.WriteLine($"Generated cabinetlogs amount: {logs.Count}");
            //await _dbContext.AddRangeAsync(roles);
            await _dbContext.BulkInsertAsync(logs, new BulkConfig { SetOutputIdentity = true });
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("CabinetLogs inserted");
            return logs;
        }
        public async Task<List<CabinetStock>> AddCabinetStocks(List<CTAMRole_Cabinet> rolecabinets, List<CTAMRole_ItemType> roleitemtypes)
        {
            Console.WriteLine("Called AddCabinetStocks");
            var cabinetstocks = rolecabinets.Join(roleitemtypes, (rc) => rc.CTAMRoleID, (ri) => ri.CTAMRoleID,
                                                  (rc, ri) => new CabinetStock()
                                                  {
                                                      CabinetNumber = rc.CabinetNumber,
                                                      ItemTypeID = ri.ItemTypeID,
                                                      CreateDT = DateTime.UtcNow,
                                                      MinimalStock = 0,
                                                      ActualStock = 0,
                                                      Status = CabinetStockStatus.OK
                                                  })
                .GroupBy(cs => new { cs.CabinetNumber, cs.ItemTypeID })
                .Select(csgrp => csgrp.First())
                .ToList();

            Console.WriteLine($"Generated cabinetstocks amount: {cabinetstocks.Count}");
            await _dbContext.BulkInsertAsync(cabinetstocks, new BulkConfig { SetOutputIdentity = true });
            await _dbContext.SaveChangesAsync();
            Console.WriteLine("CabinetStocks inserted");
            return cabinetstocks;
        }
    }
}
