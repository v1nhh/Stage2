using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class MaxCharactersReferenceCodeTo40 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReferenceCode",
                schema: "Item",
                table: "Item",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8967), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8976) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8991), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8991) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8992), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8992) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8993), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8993) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8997), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8997) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8997), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(8998) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9003), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9003) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9003), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9004) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9006), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9006) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9012), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9012) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9013), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9013) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9014), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9014) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9308), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9308) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9310), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9310) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9315), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9315) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9315), new DateTime(2022, 11, 9, 9, 33, 43, 156, DateTimeKind.Utc).AddTicks(9316) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8932), new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8934) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8939), new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8939) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8940), new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8941) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8942), new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8942) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8943), new DateTime(2022, 11, 9, 9, 33, 43, 183, DateTimeKind.Utc).AddTicks(8943) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 11, 9, 9, 33, 43, 202, DateTimeKind.Utc).AddTicks(2209));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ReferenceCode",
                schema: "Item",
                table: "Item",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8400), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8404) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8407), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8407) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8408), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8408) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8408), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8409) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8409), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8410), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8412), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8412) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8413), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8413) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8413), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8414) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8415), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8415) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8416), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8416) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8417), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8417) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8574), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8575) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8576), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8576) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8577), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8578) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8578), new DateTime(2022, 10, 13, 7, 52, 48, 567, DateTimeKind.Utc).AddTicks(8579) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 585, DateTimeKind.Utc).AddTicks(886), new DateTime(2022, 10, 13, 7, 52, 48, 585, DateTimeKind.Utc).AddTicks(892) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 585, DateTimeKind.Utc).AddTicks(895), new DateTime(2022, 10, 13, 7, 52, 48, 585, DateTimeKind.Utc).AddTicks(895) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 585, DateTimeKind.Utc).AddTicks(896), new DateTime(2022, 10, 13, 7, 52, 48, 585, DateTimeKind.Utc).AddTicks(896) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 585, DateTimeKind.Utc).AddTicks(901), new DateTime(2022, 10, 13, 7, 52, 48, 585, DateTimeKind.Utc).AddTicks(901) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 10, 13, 7, 52, 48, 585, DateTimeKind.Utc).AddTicks(902), new DateTime(2022, 10, 13, 7, 52, 48, 585, DateTimeKind.Utc).AddTicks(903) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 10, 13, 7, 52, 48, 599, DateTimeKind.Utc).AddTicks(9383));
        }
    }
}
