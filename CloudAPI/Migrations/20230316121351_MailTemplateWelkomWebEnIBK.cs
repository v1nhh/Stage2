using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class MailTemplateWelkomWebEnIBK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "Template",
                value: "<div>\r\n  <span style='font-size:11pt;'>\r\n  <p style='font-size:11pt;font-family:Calibri,sans-serif;margin:0;'>\r\n    {{body}}\r\n  </p>\r\n  </span>\r\n</div>\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Welkom bij de IBK-Beheerapplicatie en als IBK-gebruiker", "\r\nBeste collega,<br />\r\n<br />\r\n<b>Beheerapplicatie</b><br />\r\nEr is voor jou een account aangemaakt voor de applicatie waarmee de Intelligente BeheerKasten (IBK’s) beheerd kunnen worden. Door op de link te klikken kom je bij het inlogscherm van de IBK-Beheerapplicatie:<br />\r\n<a href='{{link}}' target='_blank' >IBK beheer applicatie</a> <br />\r\n<br />\r\nJouw inloggegevens:<br />\r\n<br />\r\nGebruikersnaam: Dit is je emailadres met NP+personeelsnummer (bijvoorbeeld NP123456@politie.nl). Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.<br />\r\nTijdelijk wachtwoord: {{password}}<br />\r\n<br />\r\nWijzig direct na het inloggen jouw wachtwoord! Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine letter en 1 cijfer.<br />\r\n<br />\r\n<b>Registreer toegangspas bij een IBK</b><br />\r\nOm jezelf als IBK-gebruiker te registeren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Doe dit zo snel mogelijk om collega’s met defecte portofoons te kunnen ondersteunen.<br />\r\n<ol>\r\n<li>Ga naar een IBK (klik <a href='https://blue.politie.local/ibk-locaties'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\r\n<li>Druk op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\r\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig personeelsnummer in.</li>\r\n<li>Voer in het veld ‘Vul pincode in’ de pincode ‘{{pinCode}}’ in.</li>\r\n<li>Scan jouw politie-toegangspas bij de kaartlezer onder het scherm (dit is dezelfde pas die toegang geeft tot de politiepanden/locaties).<br />\r\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu jouw collega’s met een defecte portofoon kunt ondersteunen bij een willekeurige IBK van jouw eenheid.</li>\r\n</ol>\r\n<br />\r\n\r\nZie voor meer uitleg, <a href='https://blue.politie.local/ibk-faq'>hulp en FAQ</a>.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n" });

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
                keyValue: 7,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nJouw wachtwoordwijziging voor de IBK-Beheerapplicatie is succesvol doorgevoerd! <br />\r\n<a href='{{link}}' target='_blank'>IBK beheer applicatie</a> <br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 9,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nJe ontvangt deze e-mail omdat je onlangs op de knop 'Wachtwoord vergeten' hebt gedrukt in de IBK-Beheerapplicatie.<br />\r\n<br />\r\nKlik op de onderstaande link om jouw wachtwoord te wijzigen. Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />\r\n<br />\r\n<a href='{{link}}' target='_blank'>Wachtwoord wijzigen</a><br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 11,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nBinnen de IBK-Beheerapplicatie zijn jouw gegevens aangepast, de wijziging is:<br />\r\n<br />\r\n<table>{{changes}}</table><br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 13,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nJouw IBK-account is verwijderd door de Functioneel Beheerder. Dit houdt in dat je vanaf nu niet meer van een IBK gebruik kunt maken.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 15,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nDe voorraad van item type '{{itemTypeDescription}}' in IBK '{{cabinetName}}' op locatie '{{cabinetLocationDescr}}' is minder ({{actualStock}}) dan de opgegeven minimale voorraad ({{minimalStock}}).<br />\r\n<br />\r\nWil je er zorg voor dragen dat de voorraad van '{{itemTypeDescription}}' op locatie '{{cabinetLocationDescr}}’ op zijn minst tot het minimale voorraadniveau ({{minimalStock}}) wordt aangevuld.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 17,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nDe voorraad van item type '{{itemTypeDescription}}' in IBK '{{cabinetName}}' op locatie '{{cabinetLocationDescr}}' is weer op of boven het opgegeven minimale voorraadniveau ({{minimalStock}}).<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 19,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nItem '{{itemDescription}}' van type '{{itemTypeDescription}}' is defect gemeld door gebruiker '{{userName}}' met errorcode: '{{errorCodeDescription}}'. <br />\r\n<br />\r\nU kunt het ophalen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 21,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nItem <i>{{putItemDescription}}</i> met CI-nummer <i>'{{putItemExternalReferenceID}}'</i> van het type <i>{{putItemTypeDescription}}</i> is op <i>{{actionDT}}</i> defect gemeld door gebruiker <i>{{userName}}</i> met errorcode: <i>{{errorCode}} {{errorCodeDescription}}</i>. \r\nTer reparatie kan dit item opgehaald worden bij IBK <i>{{cabinetName}}</i> op locatie <i>{{cabinetLocationDescr}} {{cabinetDescription}}</i>.<br />\r\n<br />\r\nGebruiker <i>{{userName}}</i> heeft nu tijdelijk vervangend item <i>{{takeItemDescription}}</i> met CI-nummer <i>'{{takeItemExternalReferenceID}}'</i> van het type <i>{{takeItemTypeDescription}}</i> in gebruik.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 27,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nTijdelijk Item <i>{{putItemDescription}}</i> met nummer <i>'{{putItemExternalReferenceID}}'</i> van het type <i>{{putItemTypeDescription}}</i> is op <i>{{actionDT}}</i> door gebruiker <i>{{userName}}</i> weer ingeleverd en moet daarom in CMDB ontkoppeld worden van de gebruiker en de status ‘In stock’ krijgen. Dit tijdelijke item heeft de defectmelding: '{{errorCodeDescription}}'.<br />\r\n<br />\r\nGebruiker <i>{{userName}}</i> is sinds <i>{{actionDT}}</i> in bezit van item <i>{{takeItemDescription}}</i> met nummer <i>'{{takeItemExternalReferenceID}}'</i> van het type <i>{{takeItemTypeDescription}}</i>; in CMDB moet daarom item <i>{{takeItemDescription}}</i> met nummer <i>'{{takeItemExternalReferenceID}}'</i> gekoppeld worden aan gebruiker <i>{{userName}}</i> en de status ‘in use’ krijgen.<br />\r\n<br />\r\nGraag na de CMDB-wijzigingen de status van het incident van item <i>{{takeItemDescription}}</i> met nummer <i>'{{takeItemExternalReferenceID}}'</i> op ‘Resolved’ zetten.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 29,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nVanaf nu kun je je defecte C2000 portofoon direct en 24x7 omruilen voor een werkend tijdelijk exemplaar bij een onbemande Intelligente BeheerKast (IBK). Registreer zo snel mogelijk hiervoor je pas. In deze mail kun je lezen hoe je dit kunt doen.<br />\r\n<br />\r\n<b>Op tien locaties</b><br />\r\nEerder moest je voor een portofoon-reparatie een afspraak maken bij een LO-Servicebalie. <br />\r\nVerdeeld over de eenheid Den Haag vind je nu tien onbemande IBK’s waar je direct en 24x7 terecht kunt om een defecte portofoon om te ruilen voor een werkend tijdelijk exemplaar. Als jouw portofoon gerepareerd is, krijg je hiervan automatisch een melding en kun je de tijdelijke portofoon bij dezelfde IBK weer omruilen voor jouw eigen portofoon.<br />\r\n<br />\r\n<b>Registreer je pas met onderstaande pincode</b><br />\r\nOm jezelf als IBK-gebruiker te registeren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Dit kun je vandaag al doen zodat je later, in geval van een portofoon-defect direct geholpen kunt worden.<br />\r\n<ol>\r\n<li>Ga naar een IBK (klik <a href='https://blue.politie.local/ibk-locaties'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\r\n<li>Druk op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\r\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig personeelsnummer in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\r\n<li>Voer in het veld ‘Vul pincode in’ de pincode ‘{{pinCode}}’ in.</li>\r\n<li>Scan jouw politie-toegangspas bij de kaartlezer onder het scherm (dit is dezelfde pas die toegang geeft tot de politiepanden/locaties).<br />\r\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\r\n</ol>\r\n<br />\r\n<b>Nu alleen voor portofoon 'basis'</b><br />\r\nOp dit moment kunnen alleen portofoons met de fleetmap 'Basis' gerepareerd worden met behulp van de IBK’s. In de nabije toekomst zullen er nog meer fleetmaps en items in geplaatst gaan worden.<br />\r\n<br />\r\nZie voor meer uitleg, <a href='https://blue.politie.local/ibk-faq'>hulp en FAQ</a>.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 31,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nEr is voor jou een nieuwe pincode aangemaakt waarmee je een toegangspas kunt koppelen aan jouw IBK-account. Jouw nieuwe pincode is: {{pinCode}}<br />\r\n<ol>\r\n<li>Ga naar een IBK (klik <a href='https://blue.politie.local/ibk-locaties'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\r\n<li>Druk op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\r\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig personeelsnummer in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\r\n<li>Voer in het veld ‘Vul pincode in’ de nieuwe pincode ‘{{pinCode}}’ in.</li>\r\n<li>Scan jouw politie-toegangspas bij de kaartlezer onder het scherm (dit is dezelfde pas die toegang geeft tot de politiepanden/locaties).<br />\r\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\r\n</ol>\r\n<br />\r\nZie voor meer uitleg, <a href='https://blue.politie.local/ibk-faq'>hulp en FAQ</a>.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                column: "Template",
                value: "<div>\n  <span style='font-size:11pt;'>\n  <p style='font-size:11pt;font-family:Calibri,sans-serif;margin:0;'>\n    {{body}}\n  </p>\n  </span>\n</div>\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Welkom bij CTAM - uw inloggegevens", "Beste {{name}},<br /><br />Welkom bij CTAM!<br /><br />Uw inloggegevens<br />Gebruikersnaam: {{email}}<br />Tijdelijke wachtwoord: {{password}}<br />Login code: {{loginCode}}<br />Pincode: {{pinCode}}" });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 3,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nEr is voor jou een account aangemaakt voor de applicatie waarmee de Intelligente BeheerKasten (IBK’s) beheerd kunnen worden. Door op de link te klikken kom je bij het inlogscherm van de IBK-Beheerapplicatie:<br />\n<a href='{{link}}' target='_blank' >IBK beheer applicatie</a> <br />\n<br />\nJouw inloggegevens:<br />\n<br />\nGebruikersnaam: Dit is je emailadres met NP+personeelsnummer (bijvoorbeeld NP123456@politie.nl). Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.<br />\nTijdelijk wachtwoord: {{password}}<br />\n<br />\nWijzig direct na het inloggen jouw wachtwoord! Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine letter en 1 cijfer.<br />\n<br />\nZie voor meer uitleg, <a href='https://blue.politie.local/ibk-faq'>hulp en FAQ</a>.<br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 5,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nJe hebt een wachtwoord-reset aangevraagd, jouw tijdelijke wachtwoord is {{password}}.<br />\n<br />\nVerander jouw tijdelijke wachtwoord gelijk na het inloggen. Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 7,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nJouw wachtwoordwijziging voor de IBK-Beheerapplicatie is succesvol doorgevoerd! <br />\n<a href='{{link}}' target='_blank'>IBK beheer applicatie</a> <br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 9,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nJe ontvangt deze e-mail omdat je onlangs op de knop 'Wachtwoord vergeten' hebt gedrukt in de IBK-Beheerapplicatie.<br />\n<br />\nKlik op de onderstaande link om jouw wachtwoord te wijzigen. Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />\n<br />\n<a href='{{link}}' target='_blank'>Wachtwoord wijzigen</a><br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 11,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nBinnen de IBK-Beheerapplicatie zijn jouw gegevens aangepast, de wijziging is:<br />\n<br />\n<table>{{changes}}</table><br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 13,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nJouw IBK-account is verwijderd door de Functioneel Beheerder. Dit houdt in dat je vanaf nu niet meer van een IBK gebruik kunt maken.<br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 15,
                column: "Template",
                value: "\nBeste Collega,<br />\n<br />\nDe voorraad van item type '{{itemTypeDescription}}' in IBK '{{cabinetName}}' op locatie '{{cabinetLocationDescr}}' is minder ({{actualStock}}) dan de opgegeven minimale voorraad ({{minimalStock}}).<br />\n<br />\nWil je er zorg voor dragen dat de voorraad van '{{itemTypeDescription}}' op locatie '{{cabinetLocationDescr}}’ op zijn minst tot het minimale voorraadniveau ({{minimalStock}}) wordt aangevuld.<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 17,
                column: "Template",
                value: "\nBeste Collega,<br />\n<br />\nDe voorraad van item type '{{itemTypeDescription}}' in IBK '{{cabinetName}}' op locatie '{{cabinetLocationDescr}}' is weer op of boven het opgegeven minimale voorraadniveau ({{minimalStock}}).<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 19,
                column: "Template",
                value: "\nBeste Collega,<br />\n<br />\nItem '{{itemDescription}}' van type '{{itemTypeDescription}}' is defect gemeld door gebruiker '{{userName}}' met errorcode: '{{errorCodeDescription}}'. <br />\n<br />\nU kunt het ophalen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'.<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 21,
                column: "Template",
                value: "\nBeste Collega,<br />\n<br />\nItem <i>{{putItemDescription}}</i> met CI-nummer <i>'{{putItemExternalReferenceID}}'</i> van het type <i>{{putItemTypeDescription}}</i> is op <i>{{actionDT}}</i> defect gemeld door gebruiker <i>{{userName}}</i> met errorcode: <i>{{errorCode}} {{errorCodeDescription}}</i>. \nTer reparatie kan dit item opgehaald worden bij IBK <i>{{cabinetName}}</i> op locatie <i>{{cabinetLocationDescr}} {{cabinetDescription}}</i>.<br />\n<br />\nGebruiker <i>{{userName}}</i> heeft nu tijdelijk vervangend item <i>{{takeItemDescription}}</i> met CI-nummer <i>'{{takeItemExternalReferenceID}}'</i> van het type <i>{{takeItemTypeDescription}}</i> in gebruik.<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 27,
                column: "Template",
                value: "\nBeste Collega,<br />\n<br />\nTijdelijk Item <i>{{putItemDescription}}</i> met nummer <i>'{{putItemExternalReferenceID}}'</i> van het type <i>{{putItemTypeDescription}}</i> is op <i>{{actionDT}}</i> door gebruiker <i>{{userName}}</i> weer ingeleverd en moet daarom in CMDB ontkoppeld worden van de gebruiker en de status ‘In stock’ krijgen. Dit tijdelijke item heeft de defectmelding: '{{errorCodeDescription}}'.<br />\n<br />\nGebruiker <i>{{userName}}</i> is sinds <i>{{actionDT}}</i> in bezit van item <i>{{takeItemDescription}}</i> met nummer <i>'{{takeItemExternalReferenceID}}'</i> van het type <i>{{takeItemTypeDescription}}</i>; in CMDB moet daarom item <i>{{takeItemDescription}}</i> met nummer <i>'{{takeItemExternalReferenceID}}'</i> gekoppeld worden aan gebruiker <i>{{userName}}</i> en de status ‘in use’ krijgen.<br />\n<br />\nGraag na de CMDB-wijzigingen de status van het incident van item <i>{{takeItemDescription}}</i> met nummer <i>'{{takeItemExternalReferenceID}}'</i> op ‘Resolved’ zetten.<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 29,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nVanaf nu kun je je defecte C2000 portofoon direct en 24x7 omruilen voor een werkend tijdelijk exemplaar bij een onbemande Intelligente BeheerKast (IBK). Registreer zo snel mogelijk hiervoor je pas. In deze mail kun je lezen hoe je dit kunt doen.<br />\n<br />\n<b>Op tien locaties</b><br />\nEerder moest je voor een portofoon-reparatie een afspraak maken bij een LO-Servicebalie. <br />\nVerdeeld over de eenheid Den Haag vind je nu tien onbemande IBK’s waar je direct en 24x7 terecht kunt om een defecte portofoon om te ruilen voor een werkend tijdelijk exemplaar. Als jouw portofoon gerepareerd is, krijg je hiervan automatisch een melding en kun je de tijdelijke portofoon bij dezelfde IBK weer omruilen voor jouw eigen portofoon.<br />\n<br />\n<b>Registreer je pas met onderstaande pincode</b><br />\nOm jezelf als IBK-gebruiker te registeren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Dit kun je vandaag al doen zodat je later, in geval van een portofoon-defect direct geholpen kunt worden.<br />\n<ol>\n<li>Ga naar een IBK (klik <a href='https://blue.politie.local/ibk-locaties'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\n<li>Druk op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig personeelsnummer in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\n<li>Voer in het veld ‘Vul pincode in’ de pincode ‘{{pinCode}}’ in.</li>\n<li>Scan jouw politie-toegangspas bij de kaartlezer onder het scherm (dit is dezelfde pas die toegang geeft tot de politiepanden/locaties).<br />\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\n</ol>\n<br />\n<b>Nu alleen voor portofoon 'basis'</b><br />\nOp dit moment kunnen alleen portofoons met de fleetmap 'Basis' gerepareerd worden met behulp van de IBK’s. In de nabije toekomst zullen er nog meer fleetmaps en items in geplaatst gaan worden.<br />\n<br />\nZie voor meer uitleg, <a href='https://blue.politie.local/ibk-faq'>hulp en FAQ</a>.<br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 31,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nEr is voor jou een nieuwe pincode aangemaakt waarmee je een toegangspas kunt koppelen aan jouw IBK-account. Jouw nieuwe pincode is: {{pinCode}}<br />\n<ol>\n<li>Ga naar een IBK (klik <a href='https://blue.politie.local/ibk-locaties'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\n<li>Druk op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig personeelsnummer in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\n<li>Voer in het veld ‘Vul pincode in’ de nieuwe pincode ‘{{pinCode}}’ in.</li>\n<li>Scan jouw politie-toegangspas bij de kaartlezer onder het scherm (dit is dezelfde pas die toegang geeft tot de politiepanden/locaties).<br />\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\n</ol>\n<br />\nZie voor meer uitleg, <a href='https://blue.politie.local/ibk-faq'>hulp en FAQ</a>.<br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");
        }
    }
}
