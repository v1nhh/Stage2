using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudAPI.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cabinet");

            migrationBuilder.EnsureSchema(
                name: "Communication");

            migrationBuilder.EnsureSchema(
                name: "ItemCabinet");

            migrationBuilder.EnsureSchema(
                name: "Item");

            migrationBuilder.EnsureSchema(
                name: "Mileage");

            migrationBuilder.EnsureSchema(
                name: "Reservation");

            migrationBuilder.EnsureSchema(
                name: "UserRole");

            migrationBuilder.CreateTable(
                name: "Cabinet",
                schema: "Cabinet",
                columns: table => new
                {
                    CabinetNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    CabinetType = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    LoginMethod = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    LocationDescr = table.Column<string>(maxLength: 250, nullable: true),
                    CabinetConfiguration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    CabinetUIUpdateDT = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    LastSyncTimeStamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabinet", x => x.CabinetNumber);
                });

            migrationBuilder.CreateTable(
                name: "CabinetAction",
                schema: "Cabinet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinetNumber = table.Column<string>(maxLength: 20, nullable: false),
                    CabinetName = table.Column<string>(maxLength: 250, nullable: true),
                    ActionDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Action = table.Column<int>(nullable: false),
                    ActionParameters = table.Column<string>(nullable: true),
                    CTAMUserUID = table.Column<string>(maxLength: 50, nullable: true),
                    CTAMUserName = table.Column<string>(maxLength: 250, nullable: true),
                    CTAMUserEmail = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetAction", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CabinetCellType",
                schema: "Cabinet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecCode = table.Column<string>(maxLength: 30, nullable: true),
                    ShortDescr = table.Column<string>(maxLength: 50, nullable: true),
                    LongDescr = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecType = table.Column<int>(nullable: false),
                    Depth = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    Material = table.Column<string>(maxLength: 50, nullable: true),
                    Color = table.Column<string>(maxLength: 50, nullable: true),
                    LockType = table.Column<string>(maxLength: 50, nullable: true),
                    Reference = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetCellType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CabinetDoor",
                schema: "Cabinet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Alias = table.Column<string>(maxLength: 20, nullable: true),
                    GPIOPortDoorState = table.Column<int>(nullable: false),
                    ClosedLevel = table.Column<bool>(nullable: false),
                    GPIOPortDoorControl = table.Column<int>(nullable: false),
                    UnlockLevel = table.Column<bool>(nullable: false),
                    UnlockDuration = table.Column<int>(nullable: false),
                    MaxOpenDuration = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetDoor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CabinetLog",
                schema: "Cabinet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    CabinetNumber = table.Column<string>(maxLength: 20, nullable: false),
                    CabinetName = table.Column<string>(maxLength: 250, nullable: true),
                    Source = table.Column<int>(nullable: false),
                    Log = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CabinetUI",
                schema: "Cabinet",
                columns: table => new
                {
                    CabinetNumber = table.Column<string>(maxLength: 20, nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    LogoWhite = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoBlack = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Font = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetUI", x => x.CabinetNumber);
                });

            migrationBuilder.CreateTable(
                name: "InterfaceQueue",
                schema: "Communication",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Target = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    MessageHeader = table.Column<string>(maxLength: 250, nullable: false),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterfaceQueue", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MailMarkupTemplate",
                schema: "Communication",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Template = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailMarkupTemplate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MailTemplate",
                schema: "Communication",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    Subject = table.Column<string>(maxLength: 250, nullable: true),
                    Template = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageCode = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailTemplate", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ErrorCode",
                schema: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 10, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorCode", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ItemType",
                schema: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    TagType = table.Column<int>(nullable: false),
                    Depth = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    MaxLendingTimeInMins = table.Column<int>(nullable: false),
                    IsStoredInLocker = table.Column<bool>(nullable: false),
                    RequiresMileageRegistration = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemType", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReservationRecurrencySchedule",
                schema: "Reservation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecurrenceFrequency = table.Column<int>(nullable: false),
                    Interval = table.Column<int>(nullable: true),
                    DayNumber = table.Column<int>(nullable: true),
                    Sunday = table.Column<bool>(nullable: true),
                    Monday = table.Column<bool>(nullable: true),
                    Tuesday = table.Column<bool>(nullable: true),
                    Wednesday = table.Column<bool>(nullable: true),
                    Thursday = table.Column<bool>(nullable: true),
                    Friday = table.Column<bool>(nullable: true),
                    Saturday = table.Column<bool>(nullable: true),
                    WeekOfMonth = table.Column<int>(nullable: true),
                    MonthOfYear = table.Column<int>(nullable: true),
                    EndDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationRecurrencySchedule", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CTAMPermission",
                schema: "UserRole",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    CTAMModule = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAMPermission", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CTAMRole",
                schema: "UserRole",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    ValidFromDT = table.Column<DateTime>(nullable: true),
                    ValidUntilDT = table.Column<DateTime>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAMRole", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CTAMSetting",
                schema: "UserRole",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CTAMModule = table.Column<int>(nullable: false),
                    ParName = table.Column<string>(maxLength: 250, nullable: false),
                    ParValue = table.Column<string>(maxLength: 1000, nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAMSetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CTAMUser",
                schema: "UserRole",
                columns: table => new
                {
                    UID = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 250, nullable: false),
                    LoginCode = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    PinCode = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Password = table.Column<string>(maxLength: 250, nullable: true),
                    IsPasswordTemporary = table.Column<bool>(nullable: false),
                    CardCode = table.Column<string>(maxLength: 50, nullable: true),
                    LanguageCode = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAMUser", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "ManagementLog",
                schema: "UserRole",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Source = table.Column<int>(nullable: false),
                    Log = table.Column<string>(nullable: false),
                    CTAMUserUID = table.Column<string>(maxLength: 50, nullable: true),
                    CTAMUserName = table.Column<string>(maxLength: 250, nullable: true),
                    CTAMUserEmail = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManagementLog", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CabinetColumn",
                schema: "Cabinet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinetNumber = table.Column<string>(maxLength: 20, nullable: true),
                    ColumnNumber = table.Column<int>(nullable: false),
                    TemplateName = table.Column<string>(maxLength: 50, nullable: true),
                    Depth = table.Column<double>(nullable: false),
                    Width = table.Column<double>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    IsTemplate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetColumn", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CabinetColumn_Cabinet_CabinetNumber",
                        column: x => x.CabinetNumber,
                        principalSchema: "Cabinet",
                        principalTable: "Cabinet",
                        principalColumn: "CabinetNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CabinetProperties",
                schema: "Cabinet",
                columns: table => new
                {
                    CabinetNumber = table.Column<string>(nullable: false),
                    LocalApiVersion = table.Column<string>(maxLength: 25, nullable: true),
                    LocalUiVersion = table.Column<string>(maxLength: 25, nullable: true),
                    HwApiVersion = table.Column<string>(maxLength: 25, nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetProperties", x => x.CabinetNumber);
                    table.ForeignKey(
                        name: "FK_CabinetProperties_Cabinet_CabinetNumber",
                        column: x => x.CabinetNumber,
                        principalSchema: "Cabinet",
                        principalTable: "Cabinet",
                        principalColumn: "CabinetNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabinetPosition",
                schema: "Cabinet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinetNumber = table.Column<string>(maxLength: 20, nullable: false),
                    PositionNumber = table.Column<int>(maxLength: 10, nullable: false),
                    PositionAlias = table.Column<string>(maxLength: 20, nullable: true),
                    PositionType = table.Column<int>(nullable: false),
                    CabinetCellTypeID = table.Column<int>(nullable: false),
                    BladeNo = table.Column<int>(nullable: false),
                    BladePosNo = table.Column<int>(nullable: false),
                    CabinetDoorID = table.Column<int>(nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    MaxNrOfItems = table.Column<int>(nullable: false),
                    IsAllocated = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetPosition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CabinetPosition_CabinetCellType_CabinetCellTypeID",
                        column: x => x.CabinetCellTypeID,
                        principalSchema: "Cabinet",
                        principalTable: "CabinetCellType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinetPosition_CabinetDoor_CabinetDoorID",
                        column: x => x.CabinetDoorID,
                        principalSchema: "Cabinet",
                        principalTable: "CabinetDoor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CabinetPosition_Cabinet_CabinetNumber",
                        column: x => x.CabinetNumber,
                        principalSchema: "Cabinet",
                        principalTable: "Cabinet",
                        principalColumn: "CabinetNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MailQueue",
                schema: "Communication",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MailMarkupTemplateID = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    MailTo = table.Column<string>(maxLength: 250, nullable: false),
                    MailCC = table.Column<string>(maxLength: 250, nullable: true),
                    Prio = table.Column<bool>(nullable: false),
                    Subject = table.Column<string>(maxLength: 250, nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    FailedAttempts = table.Column<int>(nullable: false),
                    LastFailedErrorMessage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MailQueue", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MailQueue_MailMarkupTemplate_MailMarkupTemplateID",
                        column: x => x.MailMarkupTemplateID,
                        principalSchema: "Communication",
                        principalTable: "MailMarkupTemplate",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                schema: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 250, nullable: false),
                    ItemTypeID = table.Column<int>(nullable: false),
                    ReferenceCode = table.Column<string>(maxLength: 10, nullable: true),
                    Barcode = table.Column<string>(maxLength: 20, nullable: true),
                    Tagnumber = table.Column<string>(maxLength: 40, nullable: true),
                    ErrorCodeID = table.Column<int>(nullable: true),
                    MaxLendingTimeInMins = table.Column<int>(nullable: false),
                    NrOfSubItems = table.Column<int>(nullable: false),
                    AllowReservations = table.Column<bool>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    LastSyncTimeStamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Item_ErrorCode_ErrorCodeID",
                        column: x => x.ErrorCodeID,
                        principalSchema: "Item",
                        principalTable: "ErrorCode",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Item_ItemType_ItemTypeID",
                        column: x => x.ItemTypeID,
                        principalSchema: "Item",
                        principalTable: "ItemType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemType_ErrorCode",
                schema: "Item",
                columns: table => new
                {
                    ItemTypeID = table.Column<int>(nullable: false),
                    ErrorCodeID = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemType_ErrorCode", x => new { x.ItemTypeID, x.ErrorCodeID });
                    table.ForeignKey(
                        name: "FK_ItemType_ErrorCode_ErrorCode_ErrorCodeID",
                        column: x => x.ErrorCodeID,
                        principalSchema: "Item",
                        principalTable: "ErrorCode",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemType_ErrorCode_ItemType_ItemTypeID",
                        column: x => x.ItemTypeID,
                        principalSchema: "Item",
                        principalTable: "ItemType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabinetStock",
                schema: "ItemCabinet",
                columns: table => new
                {
                    CabinetNumber = table.Column<string>(maxLength: 20, nullable: false),
                    ItemTypeID = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    MinimalStock = table.Column<int>(nullable: false),
                    ActualStock = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    LastSyncTimeStamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetStock", x => new { x.CabinetNumber, x.ItemTypeID });
                    table.ForeignKey(
                        name: "FK_CabinetStock_Cabinet_CabinetNumber",
                        column: x => x.CabinetNumber,
                        principalSchema: "Cabinet",
                        principalTable: "Cabinet",
                        principalColumn: "CabinetNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinetStock_ItemType_ItemTypeID",
                        column: x => x.ItemTypeID,
                        principalSchema: "Item",
                        principalTable: "ItemType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                schema: "Reservation",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CTAMUserUID = table.Column<string>(maxLength: 50, nullable: true),
                    CTAMUserName = table.Column<string>(maxLength: 250, nullable: true),
                    CTAMUserEmail = table.Column<string>(maxLength: 250, nullable: true),
                    QRCode = table.Column<string>(maxLength: 255, nullable: true),
                    ReservationType = table.Column<int>(nullable: false),
                    StartDT = table.Column<DateTime>(nullable: false),
                    EndDT = table.Column<DateTime>(nullable: true),
                    NoteForUser = table.Column<string>(maxLength: 500, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TakeDT = table.Column<DateTime>(nullable: true),
                    PutDT = table.Column<DateTime>(nullable: true),
                    IsAdhoc = table.Column<bool>(nullable: false),
                    ReservationRecurrencyScheduleID = table.Column<int>(nullable: true),
                    ExternalReservationNumber = table.Column<string>(maxLength: 50, nullable: true),
                    ExternalReservationSourceType = table.Column<string>(maxLength: 50, nullable: true),
                    ExternalReservationCallBackInfo = table.Column<string>(maxLength: 255, nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    CancelledDT = table.Column<DateTime>(nullable: true),
                    CancelledByCTAMUserUID = table.Column<string>(maxLength: 50, nullable: true),
                    CancelledByCTAMUserName = table.Column<string>(maxLength: 250, nullable: true),
                    CancelledByCTAMUserEmail = table.Column<string>(maxLength: 250, nullable: true),
                    LastSyncTimeStamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reservation_ReservationRecurrencySchedule_ReservationRecurrencyScheduleID",
                        column: x => x.ReservationRecurrencyScheduleID,
                        principalSchema: "Reservation",
                        principalTable: "ReservationRecurrencySchedule",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CabinetAccessIntervals",
                schema: "Cabinet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CTAMRoleID = table.Column<int>(nullable: false),
                    StartWeekDayNr = table.Column<int>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndWeekDayNr = table.Column<int>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetAccessIntervals", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CabinetAccessIntervals_CTAMRole_CTAMRoleID",
                        column: x => x.CTAMRoleID,
                        principalSchema: "UserRole",
                        principalTable: "CTAMRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTAMRole_Cabinet",
                schema: "Cabinet",
                columns: table => new
                {
                    CabinetNumber = table.Column<string>(nullable: false),
                    CTAMRoleID = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAMRole_Cabinet", x => new { x.CTAMRoleID, x.CabinetNumber });
                    table.ForeignKey(
                        name: "FK_CTAMRole_Cabinet_CTAMRole_CTAMRoleID",
                        column: x => x.CTAMRoleID,
                        principalSchema: "UserRole",
                        principalTable: "CTAMRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTAMRole_Cabinet_Cabinet_CabinetNumber",
                        column: x => x.CabinetNumber,
                        principalSchema: "Cabinet",
                        principalTable: "Cabinet",
                        principalColumn: "CabinetNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTAMRole_ItemType",
                schema: "Item",
                columns: table => new
                {
                    CTAMRoleID = table.Column<int>(nullable: false),
                    ItemTypeID = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    MaxQtyToPick = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAMRole_ItemType", x => new { x.CTAMRoleID, x.ItemTypeID });
                    table.ForeignKey(
                        name: "FK_CTAMRole_ItemType_CTAMRole_CTAMRoleID",
                        column: x => x.CTAMRoleID,
                        principalSchema: "UserRole",
                        principalTable: "CTAMRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTAMRole_ItemType_ItemType_ItemTypeID",
                        column: x => x.ItemTypeID,
                        principalSchema: "Item",
                        principalTable: "ItemType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTAMRole_Permission",
                schema: "UserRole",
                columns: table => new
                {
                    CTAMRoleID = table.Column<int>(nullable: false),
                    CTAMPermissionID = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAMRole_Permission", x => new { x.CTAMRoleID, x.CTAMPermissionID });
                    table.ForeignKey(
                        name: "FK_CTAMRole_Permission_CTAMPermission_CTAMPermissionID",
                        column: x => x.CTAMPermissionID,
                        principalSchema: "UserRole",
                        principalTable: "CTAMPermission",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTAMRole_Permission_CTAMRole_CTAMRoleID",
                        column: x => x.CTAMRoleID,
                        principalSchema: "UserRole",
                        principalTable: "CTAMRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTAMUser_Role",
                schema: "UserRole",
                columns: table => new
                {
                    CTAMUserUID = table.Column<string>(nullable: false),
                    CTAMRoleID = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAMUser_Role", x => new { x.CTAMUserUID, x.CTAMRoleID });
                    table.ForeignKey(
                        name: "FK_CTAMUser_Role_CTAMRole_CTAMRoleID",
                        column: x => x.CTAMRoleID,
                        principalSchema: "UserRole",
                        principalTable: "CTAMRole",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTAMUser_Role_CTAMUser_CTAMUserUID",
                        column: x => x.CTAMUserUID,
                        principalSchema: "UserRole",
                        principalTable: "CTAMUser",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabinetCell",
                schema: "Cabinet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabinetColumnID = table.Column<int>(nullable: false),
                    CabinetCellTypeID = table.Column<int>(nullable: false),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetCell", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CabinetCell_CabinetCellType_CabinetCellTypeID",
                        column: x => x.CabinetCellTypeID,
                        principalSchema: "Cabinet",
                        principalTable: "CabinetCellType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinetCell_CabinetColumn_CabinetColumnID",
                        column: x => x.CabinetColumnID,
                        principalSchema: "Cabinet",
                        principalTable: "CabinetColumn",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemDetail",
                schema: "Item",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    FreeText1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeText2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeText3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeText4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FreeText5 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDetail", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemDetail_Item_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSet",
                schema: "Item",
                columns: table => new
                {
                    SetCode = table.Column<string>(maxLength: 20, nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSet", x => new { x.SetCode, x.ItemID });
                    table.ForeignKey(
                        name: "FK_ItemSet_Item_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllowedCabinetPosition",
                schema: "ItemCabinet",
                columns: table => new
                {
                    ItemID = table.Column<int>(nullable: false),
                    CabinetPositionID = table.Column<int>(nullable: false),
                    IsBaseCabinetPosition = table.Column<bool>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowedCabinetPosition", x => new { x.CabinetPositionID, x.ItemID });
                    table.ForeignKey(
                        name: "FK_AllowedCabinetPosition_CabinetPosition_CabinetPositionID",
                        column: x => x.CabinetPositionID,
                        principalSchema: "Cabinet",
                        principalTable: "CabinetPosition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllowedCabinetPosition_Item_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CabinetPositionContent",
                schema: "ItemCabinet",
                columns: table => new
                {
                    CabinetPositionID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false),
                    LastSyncTimeStamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CabinetPositionContent", x => new { x.CabinetPositionID, x.ItemID });
                    table.ForeignKey(
                        name: "FK_CabinetPositionContent_CabinetPosition_CabinetPositionID",
                        column: x => x.CabinetPositionID,
                        principalSchema: "Cabinet",
                        principalTable: "CabinetPosition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CabinetPositionContent_Item_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTAMUserInPossession",
                schema: "ItemCabinet",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    CTAMUserUIDOut = table.Column<string>(maxLength: 50, nullable: true),
                    CTAMUserNameOut = table.Column<string>(maxLength: 250, nullable: true),
                    CTAMUserEmailOut = table.Column<string>(maxLength: 250, nullable: true),
                    OutDT = table.Column<DateTime>(nullable: true, defaultValueSql: "getutcdate()"),
                    CabinetPositionIDOut = table.Column<int>(nullable: true),
                    CabinetNumberOut = table.Column<string>(maxLength: 20, nullable: true),
                    CabinetNameOut = table.Column<string>(maxLength: 250, nullable: true),
                    CTAMUserUIDIn = table.Column<string>(maxLength: 50, nullable: true),
                    CTAMUserNameIn = table.Column<string>(maxLength: 250, nullable: true),
                    CTAMUserEmailIn = table.Column<string>(maxLength: 250, nullable: true),
                    InDT = table.Column<DateTime>(nullable: true),
                    CabinetPositionIDIn = table.Column<int>(nullable: true),
                    CabinetNumberIn = table.Column<string>(maxLength: 20, nullable: true),
                    CabinetNameIn = table.Column<string>(maxLength: 250, nullable: true),
                    ReturnBeforeDT = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    LastSyncTimeStamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAMUserInPossession", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CTAMUserInPossession_Item_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CTAMUserPersonalItem",
                schema: "ItemCabinet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CTAMUserUID = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(nullable: true),
                    ItemID = table.Column<int>(nullable: false),
                    ReplacementItemID = table.Column<int>(nullable: true),
                    CabinetNumber = table.Column<string>(maxLength: 20, nullable: true),
                    CabinetName = table.Column<string>(maxLength: 250, nullable: true),
                    Status = table.Column<int>(nullable: false),
                    LastSyncTimeStamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CTAMUserPersonalItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CTAMUserPersonalItem_CTAMUser_CTAMUserUID",
                        column: x => x.CTAMUserUID,
                        principalSchema: "UserRole",
                        principalTable: "CTAMUser",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTAMUserPersonalItem_Item_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTAMUserPersonalItem_Item_ReplacementItemID",
                        column: x => x.ReplacementItemID,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemToPick",
                schema: "ItemCabinet",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CTAMUserUID = table.Column<string>(maxLength: 50, nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    CabinetPositionID = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    LastSyncTimeStamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemToPick", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ItemToPick_CTAMUser_CTAMUserUID",
                        column: x => x.CTAMUserUID,
                        principalSchema: "UserRole",
                        principalTable: "CTAMUser",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemToPick_CabinetPosition_CabinetPositionID",
                        column: x => x.CabinetPositionID,
                        principalSchema: "Cabinet",
                        principalTable: "CabinetPosition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemToPick_Item_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mileage",
                schema: "Mileage",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemID = table.Column<int>(nullable: false),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    LicensePlate = table.Column<string>(maxLength: 20, nullable: true),
                    CurrentMileage = table.Column<int>(nullable: false),
                    MaxDeltaMileage = table.Column<int>(nullable: false),
                    ServiceMileage = table.Column<int>(nullable: false),
                    UoM = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mileage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Mileage_Item_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationCabinetPosition",
                schema: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(nullable: false),
                    CabinetPositionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationCabinetPosition", x => new { x.ReservationID, x.CabinetPositionID });
                    table.ForeignKey(
                        name: "FK_ReservationCabinetPosition_CabinetPosition_CabinetPositionID",
                        column: x => x.CabinetPositionID,
                        principalSchema: "Cabinet",
                        principalTable: "CabinetPosition",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationCabinetPosition_Reservation_ReservationID",
                        column: x => x.ReservationID,
                        principalSchema: "Reservation",
                        principalTable: "Reservation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationItem",
                schema: "Reservation",
                columns: table => new
                {
                    ReservationID = table.Column<int>(nullable: false),
                    ItemID = table.Column<int>(nullable: false),
                    CabinetNumber = table.Column<string>(maxLength: 20, nullable: false),
                    CabinetName = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationItem", x => new { x.ReservationID, x.ItemID, x.CabinetNumber });
                    table.ForeignKey(
                        name: "FK_ReservationItem_Item_ItemID",
                        column: x => x.ItemID,
                        principalSchema: "Item",
                        principalTable: "Item",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationItem_Reservation_ReservationID",
                        column: x => x.ReservationID,
                        principalSchema: "Reservation",
                        principalTable: "Reservation",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MileageRegistration",
                schema: "Mileage",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MileageID = table.Column<int>(nullable: false),
                    CTAMUserUID = table.Column<string>(maxLength: 50, nullable: true),
                    CTAMUserName = table.Column<string>(maxLength: 250, nullable: true),
                    CTAMUserEmail = table.Column<string>(maxLength: 250, nullable: true),
                    CreateDT = table.Column<DateTime>(nullable: false, defaultValueSql: "getutcdate()"),
                    UserMileage = table.Column<int>(nullable: false),
                    ValidatedMileage = table.Column<int>(nullable: false),
                    ValidatedByCTAMUserUID = table.Column<string>(maxLength: 50, nullable: true),
                    ValidatedByCTAMUserName = table.Column<string>(maxLength: 250, nullable: true),
                    ValidatedByCTAMUserEmail = table.Column<string>(maxLength: 250, nullable: true),
                    ValidatedOnDT = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    LastSyncTimeStamp = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MileageRegistration", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MileageRegistration_Mileage_MileageID",
                        column: x => x.MileageID,
                        principalSchema: "Mileage",
                        principalTable: "Mileage",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Cabinet",
                table: "CabinetCellType",
                columns: new[] { "ID", "Color", "CreateDT", "Depth", "Height", "LockType", "LongDescr", "Material", "Picture", "Reference", "ShortDescr", "SpecCode", "SpecType", "UpdateDT", "Width" },
                values: new object[,]
                {
                    { 1, "None", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.0, 30.0, "Type of lock", "", "Metal", null, null, "Locker 30x30x30", "L30", 0, null, 30.0 },
                    { 2, "None", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.0, 0.0, "Type of lock", "", "Insert", null, null, "KeyCop Insert", "I", 1, null, 0.0 },
                    { 3, "Red", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.0, 15.0, "Type of lock", "", "Metal", null, null, "Locker 30x15x30", "L15", 0, null, 30.0 },
                    { 4, "Red", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45.0, 15.0, "Type of lock", "", "Metal", null, null, "Locker 45x45x15", "L45", 0, null, 45.0 },
                    { 5, "Red", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 45.0, 15.0, "Type of lock", "", "Metal", null, null, "Locker 45x20x15", "L20", 0, null, 20.0 }
                });

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                columns: new[] { "ID", "CreateDT", "Name", "Template", "UpdateDT" },
                values: new object[] { 1, new DateTime(2021, 11, 17, 9, 7, 45, 614, DateTimeKind.Utc).AddTicks(5744), "default_ct", @"<div>
  <span style='font-size:11pt;'>
  <p style='font-size:11pt;font-family:Calibri,sans-serif;margin:0;'>
    {{body}}
    <br/><br/><br/>


  </p>
    <img src='https://www.capturetech.com/wp-content/uploads/2020/03/Logo-Capturetech-nopayoff.png' border='0' style='cursor: pointer; max-width: 200px; height: auto;'><br>
  </span></p>
  <table border='0' cellspacing='0' cellpadding='0' style='width:59.76%;height:105px;'>
    <tbody>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>HQ Nederland</td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>Lireweg 42, 2153 PH Nieuw-Vennep<br>
        The Netherlands<strong></strong></td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>+31 (0) 252 241 544</td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>
          <a href='http://www.capturetech.com/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'>www.capturetech.com</a>
        </td>
      </tr>
    </tbody>
  </table>
  <span><a href='https://twitter.com/capturetechnl/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/twitter.png' style='cursor: pointer; width: 24px; height: 24px;'></a> 
  <a href='https://www.linkedin.com/company/capturetech/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/linkedin.png' style='cursor: pointer; width: 24px; height: 24px;'></a> 
  <a href='https://www.youtube.com/channel/UCA_hKzBYnr3UHDC0Cp0u2kQ' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/youtube.png' style='cursor: pointer; width: 24px; height: 24px;'></a></span>  
  <br>
</div>", null });

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "ID", "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[,]
                {
                    { 18, "en-US", "stock_at_minimum_level", "Cabinet stock returned at minimal level", "Dear cabinet administrator,<br/><br/>The cabinet stock of {{itemTypeDescription}} in cabinet '{{cabinetNumber}}, {{cabinetName}}' at location '{{cabinetLocationDescr}}' is has reached the minimal stock ({{minimalStock}} level).", null },
                    { 17, "nl-NL", "stock_at_minimum_level", "IBK voorraad weer op minimum aantal", "Beste IBK beheerder,<br/><br/>De voorraad van item type '{{itemTypeDescription}}' in IBK '{{cabinetNumber}}, {{cabinetName}}' op locatie '{{cabinetLocationDescr}}' is weer op het opgegeven minimale voorraad ({{minimalStock}} niveau).", null },
                    { 16, "en-US", "stock_below_minimum", "Cabinet stock below minimal", "Dear cabinet administrator,<br/><br/>The cabinet stock of {{itemTypeDescription}} in cabinet '{{cabinetNumber}}, {{cabinetName}}' at location '{{cabinetLocationDescr}}' is below ({{actualStock}}) the minimal stock ({{minimalStock}}).", null },
                    { 15, "nl-NL", "stock_below_minimum", "IBK voorraad onder minimum aantal", "Beste IBK beheerder,<br/><br/>De voorraad van item type '{{itemTypeDescription}}' in IBK '{{cabinetNumber}}, {{cabinetName}}' op locatie '{{cabinetLocationDescr}}' is minder ({{actualStock}}) dan de opgegeven minimale voorraad ({{minimalStock}}).", null },
                    { 14, "en-US", "user_deleted", "Your CTAM account is deleted", "Dear {{name}},<br/><br/>Your CTAM account is deleted by the administrator", null },
                    { 12, "en-US", "user_modified", "Your changes", "Dear {{name}},<br/><br/>Review your changes below:<br/><br/><table>{{changes}}</table>", null },
                    { 11, "nl-NL", "user_modified", "Uw aanpassingen", "Beste {{name}},<br/><br/>Bekijk hieronder uw aanpassingen:<br/><br/><table>{{changes}}</table>", null },
                    { 10, "en-US", "forgot_password", "Forgot password", "Dear {{name}},<br/><br/>Click the link below to change your password.<br/><br/><a href='{{link}}' target='_blank'>Change password</a><br/><br/>You’re receiving this email because you recently pressed 'Forgot password' button on website of CaptureTech. If you did not initiate this change, please contact your administrator immediately.", null },
                    { 13, "nl-NL", "user_deleted", "Uw CTAM account is verwijderd", "Beste {{name}},<br/><br/>Uw CTAM account is verwijderd door de administrator.", null },
                    { 8, "en-US", "password_changed", "New password", "Dear {{name}},<br/><br/>Your password has been changed successfully!", null },
                    { 7, "nl-NL", "password_changed", "Nieuw wachtwoord", "Beste {{name}},<br/><br/>Uw wachtwoord wijziging is succesvol doorgevoerd!", null },
                    { 6, "en-US", "temporary_password", "Reset password", "Dear {{name}},<br/><br/>Your temporary password is {{password}}<br/>Change it as soon as you login!", null },
                    { 5, "nl-NL", "temporary_password", "Reset wachtwoord", "Beste {{name}},<br/><br/>Uw tijdelijke wachtwoord is {{password}}<br/>Verander die gelijk na het inloggen!", null },
                    { 4, "en-US", "welcome_web_login", "Welcome to CTAM - your login data", "Dear {{name}},<br/><br/>Welcome to CTAM!<br/><br/>Your login details<br/>Username: {{email}}<br/>Temporary password: {{password}}", null },
                    { 3, "nl-NL", "welcome_web_login", "Welkom bij CTAM - uw inloggegevens", "Beste {{name}},<br/><br/>Welkom bij CTAM!<br/><br/>Uw inloggegevens<br/>Gebruikersnaam: {{email}}<br/>Tijdelijke wachtwoord: {{password}}", null },
                    { 2, "en-US", "welcome_web_and_cabinet_login", "Welcome to CTAM - your login data", "Dear {{name}},<br/><br/>Welcome to CTAM!<br/><br/>Your login details<br/>Username: {{email}}<br/>Temporary password: {{password}}<br/>Login code: {{loginCode}}<br/>Pin code: {{pinCode}}", null },
                    { 1, "nl-NL", "welcome_web_and_cabinet_login", "Welkom bij CTAM - uw inloggegevens", "Beste {{name}},<br/><br/>Welkom bij CTAM!<br/><br/>Uw inloggegevens<br/>Gebruikersnaam: {{email}}<br/>Tijdelijke wachtwoord: {{password}}<br/>Login code: {{loginCode}}<br/>Pincode: {{pinCode}}", null },
                    { 9, "nl-NL", "forgot_password", "Wachtwoord vergeten", "Beste {{name}},<br/><br/>Klik op de onderstaande link om uw wachtwoord te wijzigen.<br/><br/><a href='{{link}}' target='_blank'>Wachtwoord wijzigen</a><br/><br/>U ontvangt deze e-mail omdat u onlangs op de knop 'Wachtwoord vergeten' hebt gedrukt op de website van CaptureTech. Als u deze wijziging niet heeft doorgevoerd, neem dan onmiddellijk contact op met uw beheerder.", null }
                });

            migrationBuilder.InsertData(
                schema: "UserRole",
                table: "CTAMPermission",
                columns: new[] { "ID", "CTAMModule", "CreateDT", "Description", "UpdateDT" },
                values: new object[,]
                {
                    { 12, 0, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9204), "Delete", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9205) },
                    { 11, 0, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9202), "Write", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9203) },
                    { 10, 0, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9201), "Read", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9201) },
                    { 9, 1, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9199), "Add", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9200) },
                    { 8, 1, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9197), "Remove", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9198) },
                    { 4, 1, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9183), "Pickup", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9184) },
                    { 6, 1, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9187), "Repair", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9188) },
                    { 5, 1, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9185), "Borrow", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9186) },
                    { 3, 1, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9159), "Return", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9166) },
                    { 1, 1, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(7966), "Swap", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(8353) },
                    { 7, 1, new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9189), "Replace", new DateTime(2021, 11, 17, 9, 7, 45, 505, DateTimeKind.Utc).AddTicks(9190) }
                });

            migrationBuilder.InsertData(
                schema: "UserRole",
                table: "CTAMSetting",
                columns: new[] { "ID", "CTAMModule", "CreateDT", "ParName", "ParValue", "UpdateDT" },
                values: new object[,]
                {
                    { 4, 1, new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(1236), "hwapi_timeout", "30", new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(1253) },
                    { 3, 0, new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(633), "email_default_language", "nl-NL", new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(929) },
                    { 5, 0, new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(1308), "should_send_cabinet_login", "false", new DateTime(2021, 11, 17, 9, 7, 45, 507, DateTimeKind.Utc).AddTicks(1309) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CabinetAccessIntervals_CTAMRoleID",
                schema: "Cabinet",
                table: "CabinetAccessIntervals",
                column: "CTAMRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetCell_CabinetCellTypeID",
                schema: "Cabinet",
                table: "CabinetCell",
                column: "CabinetCellTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetCell_CabinetColumnID",
                schema: "Cabinet",
                table: "CabinetCell",
                column: "CabinetColumnID");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetCellType_ShortDescr",
                schema: "Cabinet",
                table: "CabinetCellType",
                column: "ShortDescr",
                unique: true,
                filter: "[ShortDescr] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetCellType_SpecCode",
                schema: "Cabinet",
                table: "CabinetCellType",
                column: "SpecCode",
                unique: true,
                filter: "[SpecCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetColumn_CabinetNumber",
                schema: "Cabinet",
                table: "CabinetColumn",
                column: "CabinetNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetPosition_CabinetCellTypeID",
                schema: "Cabinet",
                table: "CabinetPosition",
                column: "CabinetCellTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetPosition_CabinetDoorID",
                schema: "Cabinet",
                table: "CabinetPosition",
                column: "CabinetDoorID");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetPosition_CabinetNumber_PositionAlias",
                schema: "Cabinet",
                table: "CabinetPosition",
                columns: new[] { "CabinetNumber", "PositionAlias" },
                unique: true,
                filter: "[PositionAlias] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetPosition_CabinetNumber_PositionNumber",
                schema: "Cabinet",
                table: "CabinetPosition",
                columns: new[] { "CabinetNumber", "PositionNumber" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CTAMRole_Cabinet_CabinetNumber_CTAMRoleID",
                schema: "Cabinet",
                table: "CTAMRole_Cabinet",
                columns: new[] { "CabinetNumber", "CTAMRoleID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MailMarkupTemplate_Name",
                schema: "Communication",
                table: "MailMarkupTemplate",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MailQueue_MailMarkupTemplateID",
                schema: "Communication",
                table: "MailQueue",
                column: "MailMarkupTemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_MailTemplate_LanguageCode_Name",
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "LanguageCode", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CTAMRole_ItemType_ItemTypeID",
                schema: "Item",
                table: "CTAMRole_ItemType",
                column: "ItemTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CTAMRole_ItemType_CTAMRoleID_ItemTypeID",
                schema: "Item",
                table: "CTAMRole_ItemType",
                columns: new[] { "CTAMRoleID", "ItemTypeID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCode_Code",
                schema: "Item",
                table: "ErrorCode",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCode_Description",
                schema: "Item",
                table: "ErrorCode",
                column: "Description",
                unique: true,
                filter: "[Description] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Description",
                schema: "Item",
                table: "Item",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ErrorCodeID",
                schema: "Item",
                table: "Item",
                column: "ErrorCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemTypeID",
                schema: "Item",
                table: "Item",
                column: "ItemTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDetail_ItemID",
                schema: "Item",
                table: "ItemDetail",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDetail_ID_ItemID",
                schema: "Item",
                table: "ItemDetail",
                columns: new[] { "ID", "ItemID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemSet_ItemID",
                schema: "Item",
                table: "ItemSet",
                column: "ItemID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemSet_SetCode_ItemID",
                schema: "Item",
                table: "ItemSet",
                columns: new[] { "SetCode", "ItemID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemType_Description",
                schema: "Item",
                table: "ItemType",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemType_ErrorCode_ErrorCodeID",
                schema: "Item",
                table: "ItemType_ErrorCode",
                column: "ErrorCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_AllowedCabinetPosition_ItemID",
                schema: "ItemCabinet",
                table: "AllowedCabinetPosition",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetPositionContent_ItemID",
                schema: "ItemCabinet",
                table: "CabinetPositionContent",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_CabinetStock_ItemTypeID",
                schema: "ItemCabinet",
                table: "CabinetStock",
                column: "ItemTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CTAMUserInPossession_ItemID",
                schema: "ItemCabinet",
                table: "CTAMUserInPossession",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_CTAMUserPersonalItem_CTAMUserUID",
                schema: "ItemCabinet",
                table: "CTAMUserPersonalItem",
                column: "CTAMUserUID");

            migrationBuilder.CreateIndex(
                name: "IX_CTAMUserPersonalItem_ItemID",
                schema: "ItemCabinet",
                table: "CTAMUserPersonalItem",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_CTAMUserPersonalItem_ReplacementItemID",
                schema: "ItemCabinet",
                table: "CTAMUserPersonalItem",
                column: "ReplacementItemID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemToPick_CTAMUserUID",
                schema: "ItemCabinet",
                table: "ItemToPick",
                column: "CTAMUserUID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemToPick_CabinetPositionID",
                schema: "ItemCabinet",
                table: "ItemToPick",
                column: "CabinetPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemToPick_ItemID",
                schema: "ItemCabinet",
                table: "ItemToPick",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Mileage_ItemID",
                schema: "Mileage",
                table: "Mileage",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_MileageRegistration_CTAMUserName",
                schema: "Mileage",
                table: "MileageRegistration",
                column: "CTAMUserName");

            migrationBuilder.CreateIndex(
                name: "IX_MileageRegistration_CTAMUserUID",
                schema: "Mileage",
                table: "MileageRegistration",
                column: "CTAMUserUID");

            migrationBuilder.CreateIndex(
                name: "IX_MileageRegistration_MileageID",
                schema: "Mileage",
                table: "MileageRegistration",
                column: "MileageID");

            migrationBuilder.CreateIndex(
                name: "IX_MileageRegistration_ValidatedByCTAMUserName",
                schema: "Mileage",
                table: "MileageRegistration",
                column: "ValidatedByCTAMUserName");

            migrationBuilder.CreateIndex(
                name: "IX_MileageRegistration_ValidatedByCTAMUserUID",
                schema: "Mileage",
                table: "MileageRegistration",
                column: "ValidatedByCTAMUserUID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CTAMUserName",
                schema: "Reservation",
                table: "Reservation",
                column: "CTAMUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CTAMUserUID",
                schema: "Reservation",
                table: "Reservation",
                column: "CTAMUserUID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CancelledByCTAMUserName",
                schema: "Reservation",
                table: "Reservation",
                column: "CancelledByCTAMUserName");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CancelledByCTAMUserUID",
                schema: "Reservation",
                table: "Reservation",
                column: "CancelledByCTAMUserUID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ID",
                schema: "Reservation",
                table: "Reservation",
                column: "ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ReservationRecurrencyScheduleID",
                schema: "Reservation",
                table: "Reservation",
                column: "ReservationRecurrencyScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationCabinetPosition_CabinetPositionID",
                schema: "Reservation",
                table: "ReservationCabinetPosition",
                column: "CabinetPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_CabinetName",
                schema: "Reservation",
                table: "ReservationItem",
                column: "CabinetName");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_CabinetNumber",
                schema: "Reservation",
                table: "ReservationItem",
                column: "CabinetNumber");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationItem_ItemID",
                schema: "Reservation",
                table: "ReservationItem",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_CTAMPermission_Description_CTAMModule",
                schema: "UserRole",
                table: "CTAMPermission",
                columns: new[] { "Description", "CTAMModule" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CTAMRole_Description",
                schema: "UserRole",
                table: "CTAMRole",
                column: "Description",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CTAMRole_Permission_CTAMPermissionID",
                schema: "UserRole",
                table: "CTAMRole_Permission",
                column: "CTAMPermissionID");

            migrationBuilder.CreateIndex(
                name: "IX_CTAMRole_Permission_CTAMRoleID_CTAMPermissionID",
                schema: "UserRole",
                table: "CTAMRole_Permission",
                columns: new[] { "CTAMRoleID", "CTAMPermissionID" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CTAMSetting_ParName_CTAMModule",
                schema: "UserRole",
                table: "CTAMSetting",
                columns: new[] { "ParName", "CTAMModule" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CTAMUser_CardCode",
                schema: "UserRole",
                table: "CTAMUser",
                column: "CardCode",
                unique: true,
                filter: "[CardCode] IS NOT NULL AND [CardCode] <> ''");

            migrationBuilder.CreateIndex(
                name: "IX_CTAMUser_Email",
                schema: "UserRole",
                table: "CTAMUser",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CTAMUser_LoginCode",
                schema: "UserRole",
                table: "CTAMUser",
                column: "LoginCode",
                unique: true,
                filter: "[LoginCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CTAMUser_PinCode",
                schema: "UserRole",
                table: "CTAMUser",
                column: "PinCode");

            migrationBuilder.CreateIndex(
                name: "IX_CTAMUser_Role_CTAMRoleID_CTAMUserUID",
                schema: "UserRole",
                table: "CTAMUser_Role",
                columns: new[] { "CTAMRoleID", "CTAMUserUID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CabinetAccessIntervals",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "CabinetAction",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "CabinetCell",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "CabinetLog",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "CabinetProperties",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "CabinetUI",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "CTAMRole_Cabinet",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "InterfaceQueue",
                schema: "Communication");

            migrationBuilder.DropTable(
                name: "MailQueue",
                schema: "Communication");

            migrationBuilder.DropTable(
                name: "MailTemplate",
                schema: "Communication");

            migrationBuilder.DropTable(
                name: "CTAMRole_ItemType",
                schema: "Item");

            migrationBuilder.DropTable(
                name: "ItemDetail",
                schema: "Item");

            migrationBuilder.DropTable(
                name: "ItemSet",
                schema: "Item");

            migrationBuilder.DropTable(
                name: "ItemType_ErrorCode",
                schema: "Item");

            migrationBuilder.DropTable(
                name: "AllowedCabinetPosition",
                schema: "ItemCabinet");

            migrationBuilder.DropTable(
                name: "CabinetPositionContent",
                schema: "ItemCabinet");

            migrationBuilder.DropTable(
                name: "CabinetStock",
                schema: "ItemCabinet");

            migrationBuilder.DropTable(
                name: "CTAMUserInPossession",
                schema: "ItemCabinet");

            migrationBuilder.DropTable(
                name: "CTAMUserPersonalItem",
                schema: "ItemCabinet");

            migrationBuilder.DropTable(
                name: "ItemToPick",
                schema: "ItemCabinet");

            migrationBuilder.DropTable(
                name: "MileageRegistration",
                schema: "Mileage");

            migrationBuilder.DropTable(
                name: "ReservationCabinetPosition",
                schema: "Reservation");

            migrationBuilder.DropTable(
                name: "ReservationItem",
                schema: "Reservation");

            migrationBuilder.DropTable(
                name: "CTAMRole_Permission",
                schema: "UserRole");

            migrationBuilder.DropTable(
                name: "CTAMSetting",
                schema: "UserRole");

            migrationBuilder.DropTable(
                name: "CTAMUser_Role",
                schema: "UserRole");

            migrationBuilder.DropTable(
                name: "ManagementLog",
                schema: "UserRole");

            migrationBuilder.DropTable(
                name: "CabinetColumn",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "MailMarkupTemplate",
                schema: "Communication");

            migrationBuilder.DropTable(
                name: "Mileage",
                schema: "Mileage");

            migrationBuilder.DropTable(
                name: "CabinetPosition",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "Reservation",
                schema: "Reservation");

            migrationBuilder.DropTable(
                name: "CTAMPermission",
                schema: "UserRole");

            migrationBuilder.DropTable(
                name: "CTAMRole",
                schema: "UserRole");

            migrationBuilder.DropTable(
                name: "CTAMUser",
                schema: "UserRole");

            migrationBuilder.DropTable(
                name: "Item",
                schema: "Item");

            migrationBuilder.DropTable(
                name: "CabinetCellType",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "CabinetDoor",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "Cabinet",
                schema: "Cabinet");

            migrationBuilder.DropTable(
                name: "ReservationRecurrencySchedule",
                schema: "Reservation");

            migrationBuilder.DropTable(
                name: "ErrorCode",
                schema: "Item");

            migrationBuilder.DropTable(
                name: "ItemType",
                schema: "Item");
        }
    }
}
