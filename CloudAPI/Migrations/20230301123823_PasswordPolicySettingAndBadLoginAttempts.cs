using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class PasswordPolicySettingAndBadLoginAttempts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BadLoginAttempts",
                schema: "UserRole",
                table: "CTAMUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                schema: "UserRole",
                table: "CTAMSetting",
                columns: new[] { "ID", "CTAMModule", "CreateDT", "ParName", "ParValue", "UpdateDT" },
                values: new object[] { 11, 0, new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(465), "pasword_policy", "2", new DateTime(2023, 2, 21, 9, 16, 53, 157, DateTimeKind.Utc).AddTicks(465) });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 21,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "IBK: verstoring aangemeld voor {{putItemTypeDescription}} op locatie {{cabinetLocationDescr}} met de foutmelding {{errorCode}}", "\r\nBeste Collega,<br />\r\n<br />\r\nItem <i>{{putItemDescription}}</i> met CI-nummer <i>'{{putItemExternalReferenceID}}'</i> van het type <i>{{putItemTypeDescription}}</i> is op <i>{{actionDT}}</i> defect gemeld door gebruiker <i>{{userName}}</i> met errorcode: <i>{{errorCode}} {{errorCodeDescription}}</i>. \r\nTer reparatie kan dit item opgehaald worden bij IBK <i>{{cabinetName}}</i> op locatie <i>{{cabinetLocationDescr}} {{cabinetDescription}}</i>.<br />\r\n<br />\r\nGebruiker <i>{{userName}}</i> heeft nu tijdelijk vervangend item <i>{{takeItemDescription}}</i> met CI-nummer <i>'{{takeItemExternalReferenceID}}'</i> van het type <i>{{takeItemTypeDescription}}</i> in gebruik.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "UserRole",
                table: "CTAMSetting",
                keyColumn: "ID",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "BadLoginAttempts",
                schema: "UserRole",
                table: "CTAMUser");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 21,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "IBK: verstoring aangemeld op {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCode}}", "\r\nBeste Collega,<br />\r\n<br />\r\nItem <i>{{putItemDescription}}</i> met CI-nummer <i>'{{putItemExternalReferenceID}}'</i> van het type <i>{{putItemTypeDescription}}</i> is op <i>{{actionDT}}</i> defect gemeld door gebruiker <i>{{userName}}</i> met errorcode: <i>{{errorCode}} {{errorCodeDescription}}</i>. \r\nU kunt het ophalen aan de IBK <i>{{cabinetName}}</i> op locatie <i>{{cabinetLocationDescr}} {{cabinetDescription}}</i>.<br />\r\n<br />\r\nGebruiker <i>{{userName}}</i> heeft nu tijdelijk vervangend item <i>{{takeItemDescription}}</i> met CI-nummer <i>'{{takeItemExternalReferenceID}}'</i> van het type <i>{{takeItemTypeDescription}}</i> in gebruik.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n" });
        }
    }
}
