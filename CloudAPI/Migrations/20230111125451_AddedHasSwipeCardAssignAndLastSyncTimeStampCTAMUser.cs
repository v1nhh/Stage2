using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class AddedHasSwipeCardAssignAndLastSyncTimeStampCTAMUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastSyncTimeStamp",
                schema: "UserRole",
                table: "CTAMUser",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasSwipeCardAssign",
                schema: "Cabinet",
                table: "Cabinet",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2512), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2516) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2519), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2519) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2522), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2522) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2523), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2524) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2524), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2525) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2525), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2526) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2527), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2527) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2528), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2528) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2530), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2530) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2532), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2532) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2533), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2534) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2534), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2535) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2656), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2657) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2658), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2658) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2660), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2660) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2661), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2661) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2665), new DateTime(2023, 1, 11, 12, 54, 50, 383, DateTimeKind.Utc).AddTicks(2665) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 402, DateTimeKind.Utc).AddTicks(4729), new DateTime(2023, 1, 11, 12, 54, 50, 402, DateTimeKind.Utc).AddTicks(4732) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 402, DateTimeKind.Utc).AddTicks(4736), new DateTime(2023, 1, 11, 12, 54, 50, 402, DateTimeKind.Utc).AddTicks(4736) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 402, DateTimeKind.Utc).AddTicks(4737), new DateTime(2023, 1, 11, 12, 54, 50, 402, DateTimeKind.Utc).AddTicks(4738) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 402, DateTimeKind.Utc).AddTicks(4739), new DateTime(2023, 1, 11, 12, 54, 50, 402, DateTimeKind.Utc).AddTicks(4739) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2023, 1, 11, 12, 54, 50, 402, DateTimeKind.Utc).AddTicks(4745), new DateTime(2023, 1, 11, 12, 54, 50, 402, DateTimeKind.Utc).AddTicks(4745) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2023, 1, 11, 12, 54, 50, 420, DateTimeKind.Utc).AddTicks(1139));

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 21,
                column: "Template",
                value: "Item <i>'{{putItemDescription}}'</i> met <i>'{{putItemExternalReferenceID}}'</i> van het type <i>'{{putItemTypeDescription}}'</i> is op <i>'{{actionDT}}'</i> defect gemeld door gebruiker <i>'{{userName}}'</i> met errorcode: <i>'{{errorCode}}' '{{errorCodeDescription}}'</i>. U kunt het ophalen aan de IBK <i>'{{cabinetName}}'</i> op locatie <i>'{{cabinetLocationDescr}}' '{{cabinetDescription}}'</i>.<br/><br/> Gebruiker <i>'{{userName}}'</i> heeft nu tijdelijk vervangend item <i>'{{takeItemDescription}}'</i> met <i>'{{takeItemExternalReferenceID}}'</i> van het type <i>'{{takeItemTypeDescription}}'</i> in gebruik.<br/><br/><b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres.</b>");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSyncTimeStamp",
                schema: "UserRole",
                table: "CTAMUser");

            migrationBuilder.DropColumn(
                name: "HasSwipeCardAssign",
                schema: "Cabinet",
                table: "Cabinet");

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4068), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4072) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4074), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4075) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4075), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4076) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4076), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4077) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4078), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4078) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4079), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4079) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 8,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4081), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4082) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4082), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4083) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4083), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4084) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 11,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4087), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4087) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 12,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4088), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4089) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMPermission",
                keyColumn: "ID",
                keyValue: 13,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4089), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4090) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4222), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4223) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4224), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4224) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4226), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4226) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 6,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4227), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4227) });

            migrationBuilder.UpdateData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 7,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4228), new DateTime(2022, 12, 21, 13, 19, 56, 919, DateTimeKind.Utc).AddTicks(4229) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 941, DateTimeKind.Utc).AddTicks(5925), new DateTime(2022, 12, 21, 13, 19, 56, 941, DateTimeKind.Utc).AddTicks(5928) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 2,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 941, DateTimeKind.Utc).AddTicks(5932), new DateTime(2022, 12, 21, 13, 19, 56, 941, DateTimeKind.Utc).AddTicks(5932) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 3,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 941, DateTimeKind.Utc).AddTicks(5934), new DateTime(2022, 12, 21, 13, 19, 56, 941, DateTimeKind.Utc).AddTicks(5934) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 4,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 941, DateTimeKind.Utc).AddTicks(5935), new DateTime(2022, 12, 21, 13, 19, 56, 941, DateTimeKind.Utc).AddTicks(5936) });

            migrationBuilder.UpdateData(
                schema: "Cabinet",
                table: "CabinetCellType",
                keyColumn: "ID",
                keyValue: 5,
                columns: new[] { "CreateDT", "UpdateDT" },
                values: new object[] { new DateTime(2022, 12, 21, 13, 19, 56, 941, DateTimeKind.Utc).AddTicks(5937), new DateTime(2022, 12, 21, 13, 19, 56, 941, DateTimeKind.Utc).AddTicks(5937) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "CreateDT",
                value: new DateTime(2022, 12, 21, 13, 19, 56, 969, DateTimeKind.Utc).AddTicks(708));

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 21,
                column: "Template",
                value: "Item <i>'{{putItemDescription}}'</i> met <i>'{{putItemExternalReferenceID}}'</i> van het type <i>'{{putItemTypeDescription}}'</i> is op <i>'{{actionDT}}'</i> defect gemeld door gebruiker <i>'{{userName}}'</i> met errorcode: <i>'{{errorCode}}' '{{errorCodeDescription}}'</i>. U kunt het ophalen aan de IBK <i>'{{cabinetName}}'</i> op locatie <i>'{{cabinetLocationDescr}}' '{{cabinetDescription}}'</i>.<br/><br/> Gebruiker <i>'{{userName}}'</i> heeft nu tijdelijk vervangend item <i>'{{takeItemDescription}}'</i> met <i>'{{takeItemExternalReferenceID}}'</i> van het type <i>'{{takeItemTypeDescription}}'</i> in gebruik.<br/><br/><b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b>");
        }
    }
}
