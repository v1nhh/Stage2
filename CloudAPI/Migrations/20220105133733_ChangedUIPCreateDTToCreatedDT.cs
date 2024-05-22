using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudAPI.Migrations
{
    public partial class ChangedUIPCreateDTToCreatedDT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDT",
                schema: "ItemCabinet",
                table: "CTAMUserInPossession",
                newName: "CreatedDT");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 1, 5, 13, 37, 32, 785, DateTimeKind.Utc).AddTicks(4011));

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(4412), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(4776) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5445), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5460) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5483), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5485) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5487), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5489) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5491), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5493) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5495), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5496) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5498), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5500) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5502), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5503) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5505), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5507) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5509), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5510) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5513), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5514) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5516), new DateTime(2022, 1, 5, 13, 37, 32, 526, DateTimeKind.Utc).AddTicks(5518) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(2305), new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(2969) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(3506), new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(3541) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(3563), new DateTime(2022, 1, 5, 13, 37, 32, 528, DateTimeKind.Utc).AddTicks(3564) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDT",
                schema: "ItemCabinet",
                table: "CTAMUserInPossession",
                newName: "CreateDT");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2021, 12, 15, 14, 25, 28, 677, DateTimeKind.Utc).AddTicks(3424));

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(5728), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6102) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6801), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6827) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6842), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6843) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6845), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6846) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6849), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6850) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6852), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6853) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6855), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6857) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6859), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6861) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6862), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6864) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6865), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6866) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6868), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6869) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6871), new DateTime(2021, 12, 15, 14, 25, 28, 432, DateTimeKind.Utc).AddTicks(6873) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 434, DateTimeKind.Utc).AddTicks(3245), new DateTime(2021, 12, 15, 14, 25, 28, 434, DateTimeKind.Utc).AddTicks(3881) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 434, DateTimeKind.Utc).AddTicks(4351), new DateTime(2021, 12, 15, 14, 25, 28, 434, DateTimeKind.Utc).AddTicks(4386) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 14, 25, 28, 434, DateTimeKind.Utc).AddTicks(4442), new DateTime(2021, 12, 15, 14, 25, 28, 434, DateTimeKind.Utc).AddTicks(4443) });
        }
    }
}
