using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudAPI.Migrations
{
    public partial class AddedCabinetErrorMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CabinetErrorMessage",
                schema: "Cabinet",
                table: "Cabinet",
                maxLength: 250,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CabinetErrorMessage",
                schema: "Cabinet",
                table: "Cabinet");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2021, 12, 15, 13, 22, 41, 823, DateTimeKind.Utc).AddTicks(2163));

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4221), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4441) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4817), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4826) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4834), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4835) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4836), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4837) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4838), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4838) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4839), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4841) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4843), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4844) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4845), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4845) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4846), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4847) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4848), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4849) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4850), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4851) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4852), new DateTime(2021, 12, 15, 13, 22, 41, 719, DateTimeKind.Utc).AddTicks(4853) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(5819), new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(6133) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(6429), new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(6441) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(6490), new DateTime(2021, 12, 15, 13, 22, 41, 720, DateTimeKind.Utc).AddTicks(6491) });
        }
    }
}
