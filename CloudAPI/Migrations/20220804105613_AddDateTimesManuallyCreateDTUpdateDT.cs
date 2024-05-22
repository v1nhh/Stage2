using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class AddDateTimesManuallyCreateDTUpdateDT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                schema: "Cabinet",
                table: "CabinetColumn",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                schema: "Cabinet",
                table: "CabinetCellType",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                schema: "Cabinet",
                table: "CabinetCell",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                schema: "Cabinet",
                table: "CabinetColumn",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                schema: "Cabinet",
                table: "CabinetCellType",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDT",
                schema: "Cabinet",
                table: "CabinetCell",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "getutcdate()");

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8369), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8573) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8891), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8899) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8908), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8908) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8909), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8910) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8911), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8912) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8913), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8913) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8914), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8915) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8916), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8917) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8918), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8919) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8920), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8920) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8921), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8922) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8923), new DateTime(2022, 3, 10, 8, 54, 8, 920, DateTimeKind.Utc).AddTicks(8923) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 921, DateTimeKind.Utc).AddTicks(6430), new DateTime(2022, 3, 10, 8, 54, 8, 921, DateTimeKind.Utc).AddTicks(6693) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 921, DateTimeKind.Utc).AddTicks(6925), new DateTime(2022, 3, 10, 8, 54, 8, 921, DateTimeKind.Utc).AddTicks(6938) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 3, 10, 8, 54, 8, 921, DateTimeKind.Utc).AddTicks(6963), new DateTime(2022, 3, 10, 8, 54, 8, 921, DateTimeKind.Utc).AddTicks(6964) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 3, 10, 8, 54, 9, 28, DateTimeKind.Utc).AddTicks(3345));
        }
    }
}
