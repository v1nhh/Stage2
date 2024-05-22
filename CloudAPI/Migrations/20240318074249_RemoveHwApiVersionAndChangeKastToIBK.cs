using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class RemoveHwApiVersionAndChangeKastToIBK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HwApiVersion",
                schema: "Cabinet",
                table: "CabinetProperties");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 55,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nIn IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) is onbekende inhoud geplaatst.<br />\r\n<br />\r\nOm positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />\r\nDit doet u door middel van de volgende stappen:<br />\r\n1. Log in op de IBK<br />\r\n2. Druk op de tegel verwijderen<br />\r\n3. Druk op het uitroepteken rechtbovenin<br />\r\n4. Selecteer de positie die u wilt oplossen<br />\r\n5. Druk op vrijgeven keycop(s)<br />\r\n<br />\r\nGa de items langs en handel deze af.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 59,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nIn IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) ontbreekt {{itemDescription}}.<br />\r\n<br />\r\nOm alleen positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />\r\nDit doet u door middel van de volgende stappen:<br />\r\n1. Log in op de IBK<br />\r\n2. Druk op de tegel verwijderen<br />\r\n3. Druk op het uitroepteken rechtbovenin<br />\r\n4. Selecteer de positie die u wilt oplossen<br />\r\n5. Druk op vrijgeven keycop(s)<br />\r\n<br />\r\nOm {{itemDescription}} op te lossen dient u contact op te nemen met de persoon waarop deze geregistreerd staat.<br />\r\nLET OP! Deze registratie is een mogelijkheid niet een zekerheid!<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HwApiVersion",
                schema: "Cabinet",
                table: "CabinetProperties",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true);

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 55,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nIn IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) is onbekende inhoud geplaatst.<br />\r\n<br />\r\nOm positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />\r\nDit doet u door middel van de volgende stappen:<br />\r\n1. Log in op de kast<br />\r\n2. Druk op de tegel verwijderen<br />\r\n3. Druk op het uitroepteken rechtbovenin<br />\r\n4. Selecteer de positie die u wilt oplossen<br />\r\n5. Druk op vrijgeven keycop(s)<br />\r\n<br />\r\nGa de items langs en handel deze af.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 59,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nIn IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) ontbreekt {{itemDescription}}.<br />\r\n<br />\r\nOm alleen positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />\r\nDit doet u door middel van de volgende stappen:<br />\r\n1. Log in op de kast<br />\r\n2. Druk op de tegel verwijderen<br />\r\n3. Druk op het uitroepteken rechtbovenin<br />\r\n4. Selecteer de positie die u wilt oplossen<br />\r\n5. Druk op vrijgeven keycop(s)<br />\r\n<br />\r\nOm {{itemDescription}} op te lossen dient u contact op te nemen met de persoon waarop deze geregistreerd staat.<br />\r\nLET OP! Deze registratie is een mogelijkheid niet een zekerheid!<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");
        }
    }
}
