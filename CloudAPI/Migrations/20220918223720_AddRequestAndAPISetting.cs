using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class AddRequestAndAPISetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterfaceQueue",
                schema: "Communication");

            migrationBuilder.DropPrimaryKey(name: "PK_CabinetAction", table: "CabinetAction", schema: "Cabinet");
            migrationBuilder.DropColumn(name: "ID", table: "CabinetAction", schema: "Cabinet");
            migrationBuilder.AddColumn<Guid>(name: "ID", schema: "Cabinet", table: "CabinetAction", type: "uniqueidentifier", nullable: false, defaultValueSql: "newId()");

            migrationBuilder.CreateTable(
                name: "APISetting",
                schema: "Communication",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TriggerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ResponseT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    API_URL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    API_KEY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    API_HEADERS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CrudOperation = table.Column<int>(type: "int", nullable: false),
                    Active = table.Column<int>(type: "int", nullable: false),
                    IntegrationSystem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APISetting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Request",
                schema: "Communication",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalRequestID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageHeader = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EntityID = table.Column<int>(type: "int", nullable: false),
                    APISettingID = table.Column<int>(type: "int", nullable: false),
                    ReferredRequestID = table.Column<int>(type: "int", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    CreateDT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateDT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Request", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Request_APISetting_APISettingID",
                        column: x => x.APISettingID,
                        principalSchema: "Communication",
                        principalTable: "APISetting",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Request_Request_ReferredRequestID",
                        column: x => x.ReferredRequestID,
                        principalSchema: "Communication",
                        principalTable: "Request",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "APISetting",
                columns: new[] { "ID", "API_HEADERS", "API_KEY", "API_URL", "Active", "CrudOperation", "IntegrationSystem", "RequestT", "ResponseT", "TriggerName" },
                values: new object[,]
                {
                    { 1, "{\"HeaderName1\":\"HeaderValue1\",\"HeaderName2\":\"HeaderValue2\"}", "key1", "http://localhost:5001/api/Request/swap", 1, 1, "CTAM Universeel", "CloudAPI.ApplicationCore.DTO.Requests.SwapNationalePolitieRequest", "CloudAPI.ApplicationCore.DTO.Responses.SwapNationalePolitieResponse", "SEND_DEFECT_ON_SWAP" },
                    { 2, "{}", "key2", "http://localhost:5001/api/Request/swap", 1, 2, "CTAM Universeel", "CloudAPI.ApplicationCore.DTO.Requests.SwapNationalePolitieRequest", "CloudAPI.ApplicationCore.DTO.Responses.SwapNationalePolitieResponse", "SEND_DEFECT_ON_SWAP" },
                    { 3, "{}", "key3", "http://localhost:5001/api/Request/repaired", 1, 1, "CTAM Universeel", "CloudAPI.ApplicationCore.DTO.Requests.RepairedNationalePolitieRequest", "CloudAPI.ApplicationCore.DTO.Responses.RepairedNationalePolitieResponse", "SEND_REPAIRED" },
                    { 4, "{}", "key4", "http://localhost:5001/api/Request/repaired", 1, 6, "CTAM Universeel", "CloudAPI.ApplicationCore.DTO.Requests.RepairedNationalePolitieRequest", "CloudAPI.ApplicationCore.DTO.Responses.RepairedNationalePolitieResponse", "SEND_REPAIRED" },
                    { 5, "{}", "key5", "http://localhost:5001/api/Request/requestTaakNummer", 1, 1, "CTAM Universeel", "CloudAPI.ApplicationCore.DTO.Requests.GetRTNNationalePolitieRequest", "CloudAPI.ApplicationCore.DTO.Responses.GetRTNNationalePolitieResponse", "GET-RTN" }
                });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1460), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1464) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1468), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1469) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1469), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1470) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1471), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1471) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1472), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1472) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1473), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1473) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1474), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1475) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1475), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1476) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1476), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1477) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1478), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1478) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1479), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1479) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1480), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1481) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1613), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1614) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1615), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1616) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1617), new DateTime(2022, 9, 18, 22, 37, 19, 671, DateTimeKind.Utc).AddTicks(1618) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 692, DateTimeKind.Utc).AddTicks(7288), new DateTime(2022, 9, 18, 22, 37, 19, 692, DateTimeKind.Utc).AddTicks(7290) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 692, DateTimeKind.Utc).AddTicks(7297), new DateTime(2022, 9, 18, 22, 37, 19, 692, DateTimeKind.Utc).AddTicks(7297) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 692, DateTimeKind.Utc).AddTicks(7298), new DateTime(2022, 9, 18, 22, 37, 19, 692, DateTimeKind.Utc).AddTicks(7299) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 692, DateTimeKind.Utc).AddTicks(7300), new DateTime(2022, 9, 18, 22, 37, 19, 692, DateTimeKind.Utc).AddTicks(7300) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 9, 18, 22, 37, 19, 692, DateTimeKind.Utc).AddTicks(7301), new DateTime(2022, 9, 18, 22, 37, 19, 692, DateTimeKind.Utc).AddTicks(7302) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 9, 18, 22, 37, 19, 712, DateTimeKind.Utc).AddTicks(2672));

            migrationBuilder.CreateIndex(
                name: "IX_Request_APISettingID",
                schema: "Communication",
                table: "Request",
                column: "APISettingID");

            migrationBuilder.CreateIndex(
                name: "IX_Request_ReferredRequestID",
                schema: "Communication",
                table: "Request",
                column: "ReferredRequestID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Request",
                schema: "Communication");

            migrationBuilder.DropTable(
                name: "APISetting",
                schema: "Communication");

            migrationBuilder.DropPrimaryKey(name: "PK_CabinetAction", table: "CabinetAction", schema: "Cabinet");
            migrationBuilder.DropColumn(name: "ID", table: "CabinetAction", schema: "Cabinet");
            migrationBuilder.AddColumn<int>(name: "ID", schema: "Cabinet", table: "CabinetAction", type: "uniqueidentifier", nullable: false);

            migrationBuilder.CreateTable(
                name: "InterfaceQueue",
                schema: "Communication",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDT = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    MessageBody = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MessageHeader = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Target = table.Column<int>(type: "int", nullable: false),
                    UpdateDT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterfaceQueue", x => x.ID);
                });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9527), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9530) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9533), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9534) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9534), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9535) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9535), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9535) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9536), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9536) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9537), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9537) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9538), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9538) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9539), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9539) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9540), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9540) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9541), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9541) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9542), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9542) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9543), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9543) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9628), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9629) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9630), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9630) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9631), new DateTime(2022, 8, 23, 10, 16, 40, 167, DateTimeKind.Utc).AddTicks(9632) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 183, DateTimeKind.Utc).AddTicks(1044), new DateTime(2022, 8, 23, 10, 16, 40, 183, DateTimeKind.Utc).AddTicks(1047) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 183, DateTimeKind.Utc).AddTicks(1050), new DateTime(2022, 8, 23, 10, 16, 40, 183, DateTimeKind.Utc).AddTicks(1051) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 183, DateTimeKind.Utc).AddTicks(1052), new DateTime(2022, 8, 23, 10, 16, 40, 183, DateTimeKind.Utc).AddTicks(1052) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 183, DateTimeKind.Utc).AddTicks(1053), new DateTime(2022, 8, 23, 10, 16, 40, 183, DateTimeKind.Utc).AddTicks(1053) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 23, 10, 16, 40, 183, DateTimeKind.Utc).AddTicks(1054), new DateTime(2022, 8, 23, 10, 16, 40, 183, DateTimeKind.Utc).AddTicks(1054) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 8, 23, 10, 16, 40, 207, DateTimeKind.Utc).AddTicks(1437));
        }
    }
}
