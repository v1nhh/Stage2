using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class AddedIDsToCabinetAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PutItemID",
                schema: "Cabinet",
                table: "CabinetAction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TakeItemID",
                schema: "Cabinet",
                table: "CabinetAction",
                type: "int",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PutItemID",
                schema: "Cabinet",
                table: "CabinetAction");

            migrationBuilder.DropColumn(
                name: "TakeItemID",
                schema: "Cabinet",
                table: "CabinetAction");

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4712), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4719) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4723), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4723) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4724), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4724) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4725), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4725) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4726), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4726) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4727), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4727) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4728), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4729) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4729), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4730) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4730), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4731) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4732), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4732) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4733), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4733) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4734), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4734) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4866), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4867) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4868), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4868) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4869), new DateTime(2022, 8, 4, 10, 56, 12, 965, DateTimeKind.Utc).AddTicks(4869) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 984, DateTimeKind.Utc).AddTicks(5365), new DateTime(2022, 8, 4, 10, 56, 12, 984, DateTimeKind.Utc).AddTicks(5367) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 984, DateTimeKind.Utc).AddTicks(5371), new DateTime(2022, 8, 4, 10, 56, 12, 984, DateTimeKind.Utc).AddTicks(5372) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 984, DateTimeKind.Utc).AddTicks(5373), new DateTime(2022, 8, 4, 10, 56, 12, 984, DateTimeKind.Utc).AddTicks(5373) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 984, DateTimeKind.Utc).AddTicks(5374), new DateTime(2022, 8, 4, 10, 56, 12, 984, DateTimeKind.Utc).AddTicks(5375) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 8, 4, 10, 56, 12, 984, DateTimeKind.Utc).AddTicks(5376), new DateTime(2022, 8, 4, 10, 56, 12, 984, DateTimeKind.Utc).AddTicks(5376) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 8, 4, 10, 56, 13, 16, DateTimeKind.Utc).AddTicks(3065));
        }
    }
}
