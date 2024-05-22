using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class RemovePickUpFromPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4216), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4220) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4223), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4223) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4224), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4224) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4225), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4225) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4225), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4226) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4228), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4228) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4228), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4229) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4229), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4229) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4231), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4231) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4232), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4232) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4232), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4233) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4327), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4328) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4329), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4329) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4331), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4332), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4332) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4333), new DateTime(2023, 2, 7, 12, 55, 12, 869, DateTimeKind.Utc).AddTicks(4334) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 888, DateTimeKind.Utc).AddTicks(5899), new DateTime(2023, 2, 7, 12, 55, 12, 888, DateTimeKind.Utc).AddTicks(5902) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 888, DateTimeKind.Utc).AddTicks(5906), new DateTime(2023, 2, 7, 12, 55, 12, 888, DateTimeKind.Utc).AddTicks(5907) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 888, DateTimeKind.Utc).AddTicks(5908), new DateTime(2023, 2, 7, 12, 55, 12, 888, DateTimeKind.Utc).AddTicks(5908) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 888, DateTimeKind.Utc).AddTicks(5909), new DateTime(2023, 2, 7, 12, 55, 12, 888, DateTimeKind.Utc).AddTicks(5910) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 2, 7, 12, 55, 12, 888, DateTimeKind.Utc).AddTicks(5911), new DateTime(2023, 2, 7, 12, 55, 12, 888, DateTimeKind.Utc).AddTicks(5911) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2023, 2, 7, 12, 55, 12, 907, DateTimeKind.Utc).AddTicks(4525));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8476), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8478) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8482), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8482) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8484), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8484) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8484), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8485) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8485), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8486) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8487), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8488) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8488), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8488) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8489), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8489) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8491), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8491) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8492), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8492) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8493), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8493) });

            migrationBuilder.InsertData(
                schema: "UserRole",
                table: "CTAMPermission",
                columns: new[] { "ID", "CTAMModule", "CreateDT", "Description", "UpdateDT" },
                values: new object[] { 4, 1, new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8483), "Pickup", new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8483) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8603), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8605) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8606), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8606) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8608), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8608) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8609), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8609) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8615), new DateTime(2023, 1, 19, 13, 6, 21, 473, DateTimeKind.Utc).AddTicks(8615) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 498, DateTimeKind.Utc).AddTicks(3407), new DateTime(2023, 1, 19, 13, 6, 21, 498, DateTimeKind.Utc).AddTicks(3412) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 498, DateTimeKind.Utc).AddTicks(3417), new DateTime(2023, 1, 19, 13, 6, 21, 498, DateTimeKind.Utc).AddTicks(3418) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 498, DateTimeKind.Utc).AddTicks(3419), new DateTime(2023, 1, 19, 13, 6, 21, 498, DateTimeKind.Utc).AddTicks(3420) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 498, DateTimeKind.Utc).AddTicks(3421), new DateTime(2023, 1, 19, 13, 6, 21, 498, DateTimeKind.Utc).AddTicks(3422) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 19, 13, 6, 21, 498, DateTimeKind.Utc).AddTicks(3423), new DateTime(2023, 1, 19, 13, 6, 21, 498, DateTimeKind.Utc).AddTicks(3427) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2023, 1, 19, 13, 6, 21, 517, DateTimeKind.Utc).AddTicks(9767));
        }
    }
}
