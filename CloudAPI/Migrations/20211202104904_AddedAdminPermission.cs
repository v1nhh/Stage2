using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudAPI.Migrations
{
    public partial class AddedAdminPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2021, 12, 2, 10, 49, 4, 322, DateTimeKind.Utc).AddTicks(2712));

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(4838), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5046) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5538), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5544) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5555), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5556) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5557), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5557) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5558), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5559) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5560), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5561) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5563), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5563) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5564), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5565) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5566), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5567) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5568), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5568) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5569), new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5570) });

            migrationBuilder.InsertData(
                schema: "UserRole",
                table: "CTAMPermission",
                columns: new[] { "ID", "CTAMModule", "CreateDT", "Description", "UpdateDT" },
                values: new object[] { 13, 1, new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5571), "Admin", new DateTime(2021, 12, 2, 10, 49, 4, 154, DateTimeKind.Utc).AddTicks(5571) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4290), new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4591) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4860), new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4873) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4905), new DateTime(2021, 12, 2, 10, 49, 4, 155, DateTimeKind.Utc).AddTicks(4906) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13);

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2021, 11, 26, 11, 1, 18, 366, DateTimeKind.Utc).AddTicks(3138));

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(7611), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(7873) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8346), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8358) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8369), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8371) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8372), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8373) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8374), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8375) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8377), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8378) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8380), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8381) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8382), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8383) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8385), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8386) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8388), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8389) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8390), new DateTime(2021, 11, 26, 11, 1, 18, 190, DateTimeKind.Utc).AddTicks(8391) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(3988), new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(4830) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(5323), new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(5352) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(5371), new DateTime(2021, 11, 26, 11, 1, 18, 192, DateTimeKind.Utc).AddTicks(5373) });
        }
    }
}
