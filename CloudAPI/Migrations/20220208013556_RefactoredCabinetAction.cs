using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudAPI.Migrations
{
    public partial class RefactoredCabinetAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActionParameters",
                schema: "Cabinet",
                table: "CabinetAction");

            migrationBuilder.AddColumn<string>(
                name: "ErrorCodeDescription",
                schema: "Cabinet",
                table: "CabinetAction",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PositionAlias",
                schema: "Cabinet",
                table: "CabinetAction",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PutItemDescription",
                schema: "Cabinet",
                table: "CabinetAction",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TakeItemDescription",
                schema: "Cabinet",
                table: "CabinetAction",
                maxLength: 250,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorCodeDescription",
                schema: "Cabinet",
                table: "CabinetAction");

            migrationBuilder.DropColumn(
                name: "PositionAlias",
                schema: "Cabinet",
                table: "CabinetAction");

            migrationBuilder.DropColumn(
                name: "PutItemDescription",
                schema: "Cabinet",
                table: "CabinetAction");

            migrationBuilder.DropColumn(
                name: "TakeItemDescription",
                schema: "Cabinet",
                table: "CabinetAction");

            migrationBuilder.AddColumn<string>(
                name: "ActionParameters",
                schema: "Cabinet",
                table: "CabinetAction",
                type: "nvarchar(max)",
                nullable: true);

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
    }
}
