using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudAPI.Migrations
{
    public partial class FixedUIPCreatedDTTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDt",
                schema: "ItemCabinet",
                table: "CTAMUserInPossession",
                newName: "CreatedDT");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 1, 19, 16, 4, 34, 840, DateTimeKind.Utc).AddTicks(2257));

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(3912), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4169) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4633), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4641) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4652), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4653) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4655), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4656) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4657), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4658) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4660), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4660) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4663), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4663) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4665), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4666) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4667), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4668) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4670), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4671) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4672), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4673) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4674), new DateTime(2022, 1, 19, 16, 4, 34, 676, DateTimeKind.Utc).AddTicks(4675) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 677, DateTimeKind.Utc).AddTicks(5426), new DateTime(2022, 1, 19, 16, 4, 34, 677, DateTimeKind.Utc).AddTicks(5980) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 677, DateTimeKind.Utc).AddTicks(6435), new DateTime(2022, 1, 19, 16, 4, 34, 677, DateTimeKind.Utc).AddTicks(6461) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 19, 16, 4, 34, 677, DateTimeKind.Utc).AddTicks(6555), new DateTime(2022, 1, 19, 16, 4, 34, 677, DateTimeKind.Utc).AddTicks(6557) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDT",
                schema: "ItemCabinet",
                table: "CTAMUserInPossession",
                newName: "CreatedDt");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 1, 10, 13, 59, 11, 992, DateTimeKind.Utc).AddTicks(328));

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(2726), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3108) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3749), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3770) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3785), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3786) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3787), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3788) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3789), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3790) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3792), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3792) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3794), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3795) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3796), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3799) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3800), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3801) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3802), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3803) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3804), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3805) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3806), new DateTime(2022, 1, 10, 13, 59, 11, 837, DateTimeKind.Utc).AddTicks(3807) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5249), new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5620) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5900), new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5917) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5949), new DateTime(2022, 1, 10, 13, 59, 11, 838, DateTimeKind.Utc).AddTicks(5950) });
        }
    }
}
