using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudAPI.Migrations
{
    public partial class AddCabinetLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CabinetLanguage",
                schema: "Cabinet",
                table: "Cabinet",
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 3, 10, 8, 54, 9, 28, DateTimeKind.Utc).AddTicks(3345));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CabinetLanguage",
                schema: "Cabinet",
                table: "Cabinet");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 2, 8, 1, 35, 56, 209, DateTimeKind.Utc).AddTicks(7841));

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(5691), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(5988) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6492), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6503) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6514), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6516) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6518), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6519) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6521), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6522) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6523), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6525) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6526), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6528) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6529), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6530) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6532), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6533) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6535), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6536) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6538), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6539) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6540), new DateTime(2022, 2, 8, 1, 35, 56, 1, DateTimeKind.Utc).AddTicks(6542) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 2, DateTimeKind.Utc).AddTicks(8568), new DateTime(2022, 2, 8, 1, 35, 56, 2, DateTimeKind.Utc).AddTicks(9043) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 2, DateTimeKind.Utc).AddTicks(9405), new DateTime(2022, 2, 8, 1, 35, 56, 2, DateTimeKind.Utc).AddTicks(9427) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 2, 8, 1, 35, 56, 2, DateTimeKind.Utc).AddTicks(9445), new DateTime(2022, 2, 8, 1, 35, 56, 2, DateTimeKind.Utc).AddTicks(9446) });
        }
    }
}
