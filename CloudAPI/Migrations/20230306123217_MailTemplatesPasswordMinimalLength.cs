using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class MailTemplatesPasswordMinimalLength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 3,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nEr is voor jou een account aangemaakt voor de applicatie waarmee de Intelligente BeheerKasten (IBK’s) beheerd kunnen worden. Door op de link te klikken kom je bij het inlogscherm van de IBK-Beheerapplicatie:<br />\r\n<a href='{{link}}' target='_blank' >IBK beheer applicatie</a> <br />\r\n<br />\r\nJouw inloggegevens:<br />\r\n<br />\r\nGebruikersnaam: Dit is je emailadres met NP+personeelsnummer (bijvoorbeeld NP123456@politie.nl). Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.<br />\r\nTijdelijk wachtwoord: {{password}}<br />\r\n<br />\r\nWijzig direct na het inloggen jouw wachtwoord! Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine letter en 1 cijfer.<br />\r\n<br />\r\nZie voor meer uitleg, <a href='https://blue.politie.local/ibk-faq'>hulp en FAQ</a>.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 5,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nJe hebt een wachtwoord-reset aangevraagd, jouw tijdelijke wachtwoord is {{password}}.<br />\r\n<br />\r\nVerander jouw tijdelijke wachtwoord gelijk na het inloggen. Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 9,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nJe ontvangt deze e-mail omdat je onlangs op de knop 'Wachtwoord vergeten' hebt gedrukt in de IBK-Beheerapplicatie.<br />\r\n<br />\r\nKlik op de onderstaande link om jouw wachtwoord te wijzigen. Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />\r\n<br />\r\n<a href='{{link}}' target='_blank'>Wachtwoord wijzigen</a><br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 3,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nEr is voor jou een account aangemaakt voor de applicatie waarmee de Intelligente BeheerKasten (IBK’s) beheerd kunnen worden. Door op de link te klikken kom je bij het inlogscherm van de IBK-Beheerapplicatie:<br />\r\n<a href='{{link}}' target='_blank' >IBK beheer applicatie</a> <br />\r\n<br />\r\nJouw inloggegevens:<br />\r\n<br />\r\nGebruikersnaam: Dit is je emailadres met NP+personeelsnummer (bijvoorbeeld NP123456@politie.nl). Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.<br />\r\nTijdelijk wachtwoord: {{password}}<br />\r\n<br />\r\nWijzig direct na het inloggen jouw wachtwoord! Een wachtwoord moet minstens uit 6 karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine letter en 1 cijfer.<br />\r\n<br />\r\nZie voor meer uitleg, <a href='https://blue.politie.local/ibk-faq'>hulp en FAQ</a>.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 5,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nJe hebt een wachtwoord-reset aangevraagd, jouw tijdelijke wachtwoord is {{password}}.<br />\r\n<br />\r\nVerander jouw tijdelijke wachtwoord gelijk na het inloggen. Een wachtwoord moet minstens uit 6 karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 9,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nJe ontvangt deze e-mail omdat je onlangs op de knop 'Wachtwoord vergeten' hebt gedrukt in de IBK-Beheerapplicatie.<br />\r\n<br />\r\nKlik op de onderstaande link om jouw wachtwoord te wijzigen. Een wachtwoord moet minstens uit 6 karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />\r\n<br />\r\n<a href='{{link}}' target='_blank'>Wachtwoord wijzigen</a><br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");
        }
    }
}
