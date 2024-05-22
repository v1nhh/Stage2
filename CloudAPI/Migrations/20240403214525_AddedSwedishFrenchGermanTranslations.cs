using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class AddedSwedishFrenchGermanTranslations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\n<b>Beheerapplicatie</b><br />\nEr is voor jou een account aangemaakt voor de applicatie waarmee de Intelligente BeheerKasten (IBK’s) beheerd kunnen worden. Door op de link te klikken kom je bij het inlogscherm van de IBK-Beheerapplicatie:<br />\n<a href='{{link}}' target='_blank' >IBK beheer applicatie</a> <br />\n<br />\nJouw inloggegevens:<br />\n<br />\nGebruikersnaam: Dit is je emailadres met NP+personeelsnummer (bijvoorbeeld NP123456@politie.nl). Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.<br />\nTijdelijk wachtwoord: {{password}}<br />\n<br />\nWijzig direct na het inloggen jouw wachtwoord! Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine letter en 1 cijfer.<br />\n<br />\n<b>Registreer toegangspas bij een IBK</b><br />\nOm jezelf als IBK-gebruiker te registeren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Doe dit zo snel mogelijk om collega’s met defecte portofoons te kunnen ondersteunen.<br />\n<ol>\n<li>Ga naar een IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\n<li>Druk op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig personeelsnummer in.</li>\n<li>Voer in het veld ‘Vul pincode in’ de pincode ‘{{pinCode}}’ in.</li>\n<li>Scan jouw politie-toegangspas bij de kaartlezer onder het scherm (dit is dezelfde pas die toegang geeft tot de politiepanden/locaties).<br />\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu jouw collega’s met een defecte portofoon kunt ondersteunen bij een willekeurige IBK van jouw eenheid.</li>\n</ol>\n<br />\n\nZie voor meer uitleg, <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hulp en FAQ</a>.<br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 3,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nEr is voor jou een account aangemaakt voor de applicatie waarmee de Intelligente BeheerKasten (IBK’s) beheerd kunnen worden. Door op de link te klikken kom je bij het inlogscherm van de IBK-Beheerapplicatie:<br />\n<a href='{{link}}' target='_blank' >IBK beheer applicatie</a> <br />\n<br />\nJouw inloggegevens:<br />\n<br />\nGebruikersnaam: Dit is je emailadres met NP+personeelsnummer (bijvoorbeeld NP123456@politie.nl). Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.<br />\nTijdelijk wachtwoord: {{password}}<br />\n<br />\nWijzig direct na het inloggen jouw wachtwoord! Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine letter en 1 cijfer.<br />\n<br />\nZie voor meer uitleg, <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hulp en FAQ</a>.<br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 5,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nJe hebt een wachtwoord-reset aangevraagd, jouw tijdelijke wachtwoord is <b>{{password}}</b><br />\n<br />\nGa naar de IBK-Beheerapplicatie en verander direct jouw tijdelijke wachtwoord. Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />\n<br />\nLog binnen de IBK-Beheerapplicatie altijd in met jouw NP-emailadres (bijvoorbeeld NP250537@politie.nl)<br />\n<br />\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

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
                value: "\nBeste collega,<br />\n<br />\nAls jouw C2000-portofoon defect raakt, kun je die vanaf nu direct en 24x7 omruilen voor een werkend tijdelijk exemplaar bij een onbemande Intelligente BeheerKast (IBK). Registreer hiervoor zo snel mogelijk je pas. In deze mail kun je lezen hoe je dit kunt doen.<br />\n<br />\n<b>Nieuw: op tien locaties een IBK</b><br />\nEerder moest je voor een portofoon-reparatie een afspraak maken bij een LO-Servicebalie. <br />\nVerdeeld over jouw eenheid vind je nu tien onbemande IBK’s waar je direct en 24x7 terecht kunt om een defecte portofoon om te ruilen voor een werkend tijdelijk exemplaar. Als jouw portofoon gerepareerd is, krijg je hiervan automatisch een melding en kun je de tijdelijke portofoon bij dezelfde IBK weer omruilen voor jouw eigen portofoon.<br />\n<br />\n<b>Registreer je pas met onderstaande pincode</b><br />\nOm jezelf als IBK-gebruiker te registreren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Dit kun je vandaag al doen zodat je later, in geval van een portofoon-defect direct geholpen kunt worden.<br />\n<ol>\n<li>Ga naar een fysieke IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\n<li>Druk bij de IBK op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig <b>Personeelsnummer</b> in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\n<li>Voer in het veld ‘Vul pincode in’ de <b>pincode ‘{{pinCode}}’</b> in.</li>\n<li>Scan jouw politie-toegangspas bij de scanner onder het scherm (dit is dezelfde pas die jou toegang geeft tot de politiepanden).<br />\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\n</ol>\n<br />\n<b>Nu alleen voor portofoon met basisprogrammering</b><br />\nOp dit moment kunnen alleen portofoons met een basisprogrammering gerepareerd worden met behulp van de IBK’s. Voor portofoons met een andere programmering zoals bijvoorbeeld ‘Recherche’ of ‘Onderhandelaar’ kan het huidige reparatieproces met de LO-Servicebalie gebruikt worden. In de nabije toekomst zullen ook andere portofoons en items in de IBK’s geplaatst gaan worden, registreer daarom dus zo snel mogelijk jouw toegangspas.<br />\n<br />\nKlik <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hier</a> voor meer uitleg, hulp en FAQ. Heb je nog aanvullende vragen en/of opmerkingen, neem dan contact op met Functioneel Beheer IBK via\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 31,
                column: "Template",
                value: "\nBeste collega,<br />\n<br />\nEr is voor jou een nieuwe pincode aangemaakt waarmee je een toegangspas kunt koppelen aan jouw IBK-account. Jouw nieuwe pincode is: <b>‘{{pinCode}}’</b><br />\n<ol>\n<li>Ga naar een fysieke IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\n<li>Druk bij de IBK op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig <b>Personeelsnummer</b> in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\n<li>Voer in het veld ‘Vul pincode in’ de nieuwe <b>pincode ‘{{pinCode}}’</b> in.</li>\n<li>Scan jouw politie-toegangspas bij de scanner onder het scherm (dit is dezelfde pas die jou toegang geeft tot de politiepanden).<br />\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\n</ol>\n<br />\nKlik <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hier</a> voor meer uitleg, hulp en FAQ. Heb je nog aanvullende vragen en/of opmerkingen, neem dan contact op met Functioneel Beheer IBK via <a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\n<br />\nMet vriendelijke groet,<br />\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\n<br />\n\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 49,
                column: "Template",
                value: "\nBeste Collega,<br />\n<br />\nItem '{{itemDescription}}' van type '{{itemTypeDescription}}' is incorrect terug gemeld door gebruiker '{{userEmail}}' aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}' <br />\n<br />\nDeze actie is geregistreerd doormiddel van het afsluiten van een bezitsrecord en item '{{itemDescription}}' is weer in omloop. <br />\nHet is mogelijk dat gebruiker '{{userEmail}}' item '{{itemDescription}}' incorrect uitgeworpen heeft gekregen en hierna als incorrect terug heeft gemeld.<br />\nOm te voorkomen dat gebruiker '{{userEmail}}' een onterechte registratie heeft kunt u dit onderzoeken. <br />\n<br />\nBekijk in de gebruikers geschiedenis van gebruiker '{{userEmail}}' of de gebruiker items in bezit geregistreerd heeft.<br />\nAls gebruiker '{{userEmail}}' items in bezit geregistreerd heeft kunt u in de operationele en technische logs bekijken welke handelingen vooraf zijn gegaan aan de incorrect terug melding.<br />\nLijkt het erop dat gebruiker '{{userEmail}}' een onterechte registratie heeft, neem dan contact op met gebruiker '{{userEmail}}' om te vragen naar eventuele incorrecte uitnamen.<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 50,
                column: "Template",
                value: "\nDear Colleague,<br />\n<br />\nItem '{{itemDescription}}' of type '{{itemTypeDescription}}' has been incorrectly reported returned by user '{{userEmail}}' to the IBK '{{cabinetName}}' at position '{{positionAlias}}'<br />\n<br />\nThis action has been registrated by closing a possession record and item '{{itemDescription}}' is back in circulation. <br />\nIt is possible that user '{{userEmail}}' has been incorrectly given item '{{itemDescription}}' and thereafter reported it as incorrectly returned.<br />\nTo prevent user '{{userEmail}}' from having an incorrect registration, you can investigate this. <br />\n<br />\nView the user history of user '{{userEmail}}' to see if the user has registered items in possession.<br />\nIf user '{{userEmail}}' has items registered in possession, you can check the operational and technical logs to see which actions preceded the incorrect return notification.<br />\nIf it appears that user '{{userEmail}}' has an incorrect registration, please contact user '{{userEmail}}' to inquire about any incorrect withdrawals.<br />\n<br />\n<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 51,
                column: "Template",
                value: "\nBeste Collega,<br />\n<br />\nItem '{{itemDescription}}' van type '{{itemTypeDescription}}' is incorrect terug gemeld door gebruiker '{{userEmail}}' aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}' <br />\n<br />\nDeze actie is geregistreerd doormiddel van het aanmaken van een bezitsrecord en item '{{itemDescription}}' is weer in omloop. <br />\nHet is mogelijk dat gebruiker '{{userEmail}}' item '{{itemDescription}}' incorrect uitgeworpen heeft gekregen en hierna als incorrect terug heeft gemeld.<br />\nOm te voorkomen dat gebruiker '{{userEmail}}' een onterechte registratie heeft kunt u dit onderzoeken. <br />\n<br />\nBekijk in de gebruikers geschiedenis van gebruiker '{{userEmail}}' of de gebruiker items in bezit geregistreerd heeft.<br />\nAls gebruiker '{{userEmail}}' items in bezit geregistreerd heeft kunt u in de operationele en technische logs bekijken welke handelingen vooraf zijn gegaan aan de incorrect terug melding.<br />\nLijkt het erop dat gebruiker '{{userEmail}}' een onterechte registratie heeft, neem dan contact op met gebruiker '{{userEmail}}' om te vragen naar eventuele incorrecte uitnamen.<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 52,
                column: "Template",
                value: "\nDear Colleague,<br />\n<br />\nItem '{{itemDescription}}' of type '{{itemTypeDescription}}' has been incorrectly reported returned by user '{{userEmail}}' to the IBK '{{cabinetName}}' at position '{{positionAlias}}'<br />\n<br />\nThis action has been registrated by creating a possession record and item '{{itemDescription}}' is back in circulation. <br />\nIt is possible that user '{{userEmail}}' has been incorrectly given item '{{itemDescription}}' and thereafter reported it as incorrectly returned.<br />\nTo prevent user '{{userEmail}}' from having an incorrect registration, you can investigate this. <br />\n<br />\nView the user history of user '{{userEmail}}' to see if the user has registered items in possession.<br />\nIf user '{{userEmail}}' has items registered in possession, you can check the operational and technical logs to see which actions preceded the incorrect return notification.<br />\nIf it appears that user '{{userEmail}}' has an incorrect registration, please contact user '{{userEmail}}' to inquire about any incorrect withdrawals.<br />\n<br />\n<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 53,
                column: "Template",
                value: "\nعزيزي الزميل،<br />\n<br />\nتم الإبلاغ عن العنصر '{{itemDescription}}' من النوع '{{itemTypeDescription}}' بشكل خاطئ كمرتجع من قبل المستخدم '{{userEmail}}' إلى IBK '{{cabinetName}}' في الموقع '{{positionAlias}}'<br />\n<br />\nتم تسجيل هذا الإجراء بإغلاق سجل الاستحواذ والعنصر '{{itemDescription}}' عاد إلى التداول. <br />\nمن الممكن أن يكون المستخدم '{{userEmail}}' قد تلقى العنصر '{{itemDescription}}' بشكل خاطئ ومن ثم أبلغ عنه كمرتجع خاطئ.<br />\nلمنع المستخدم '{{userEmail}}' من وجود تسجيل خاطئ، يمكنك التحقيق في ذلك. <br />\n<br />\nاطلع على سجل المستخدم للمستخدم '{{userEmail}}' لمعرفة ما إذا كان لدى المستخدم عناصر مسجلة تحت حيازته.<br />\nإذا كان لدى المستخدم '{{userEmail}}' عناصر مسجلة تحت حيازته، يمكنك التحقق من السجلات التشغيلية والفنية لمعرفة الإجراءات التي سبقت إشعار الإرجاع الخاطئ.<br />\nإذا بدا أن المستخدم '{{userEmail}}' لديه تسجيل خاطئ، يرجى التواصل مع المستخدم '{{userEmail}}' للاستفسار عن أي عمليات سحب خاطئة.<br />\n<br />\n<b>لا ترد على هذا البريد الإلكتروني، فقد تم إرساله من عنوان 'لا يمكن الرد عليه'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 54,
                column: "Template",
                value: "\nعزيزي الزميل،<br />\n<br />\nتم الإبلاغ بشكل غير صحيح عن عودة العنصر '{{itemDescription}}' من النوع '{{itemTypeDescription}}' من قبل المستخدم '{{userEmail}}' إلى الـ IBK '{{cabinetName}}' في الموقع '{{positionAlias}}'<br />\n<br />\nتم تسجيل هذا الإجراء من خلال إنشاء سجل ملكية والعنصر '{{itemDescription}}' قد عاد إلى التداول. <br />\nمن المحتمل أن يكون المستخدم '{{userEmail}}' قد حصل بطريق الخطأ على العنصر '{{itemDescription}}' ومن ثم أبلغ عنه كمرتجع بشكل خاطئ.<br />\nلمنع المستخدم '{{userEmail}}' من الحصول على تسجيل خاطئ، يمكنك التحقيق في هذا الأمر. <br />\n<br />\nراجع تاريخ المستخدم للمستخدم '{{userEmail}}' لمعرفة ما إذا كان المستخدم قد سجل عناصر تحت ملكيته.<br />\nإذا كان لدى المستخدم '{{userEmail}}' عناصر مسجلة تحت ملكيته، يمكنك فحص السجلات التشغيلية والفنية لمعرفة الأحداث التي سبقت إشعار الإرجاع الخاطئ.<br />\nإذا بدا أن المستخدم '{{userEmail}}' لديه تسجيل خاطئ، يرجى الاتصال بالمستخدم '{{userEmail}}' للاستفسار عن أي عمليات سحب خاطئة.<br />\n<br />\n<b>لا ترد على هذا البريد الإلكتروني، فقد تم إرساله من عنوان 'لا يمكن الرد عليه'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 55,
                column: "Template",
                value: "\nBeste Collega,<br />\n<br />\nIn IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) is onbekende inhoud geplaatst.<br />\n<br />\nOm positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />\nDit doet u door middel van de volgende stappen:<br />\n1. Log in op de IBK<br />\n2. Druk op de tegel verwijderen<br />\n3. Druk op het uitroepteken rechtbovenin<br />\n4. Selecteer de positie die u wilt oplossen<br />\n5. Druk op vrijgeven keycop(s)<br />\n<br />\nGa de items langs en handel deze af.<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 56,
                column: "Template",
                value: "\nDear Colleague,<br />\n<br />\nIn cabinet '{{cabinetName}}' at position '{{positionAlias}}' ({{positionNumber}}), unknown content has been placed.<br />\n<br />\nTo resolve position '{{positionAlias}}' ({{positionNumber}}), you need to go through the correction flow.<br />\nYou do this by following these steps:<br />\n1. Log in to the cabinet<br />\n2. Press the remove tile<br />\n3. Press the exclamation mark at the top right<br />\n4. Select the position you want to resolve<br />\n5. Press release keycop(s)<br />\n<br />\nGo through the items and handle them.<br />\n<br />\n<b>Do not reply to this email, it was sent via a 'no-reply address'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 57,
                column: "Template",
                value: "\nBeste Collega,<br />\n<br />\nIBK '{{cabinetName}}' positie '{{positionAlias}}' ({{positionNumber}}) is defect.<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 58,
                column: "Template",
                value: "\nDear Colleague,<br />\n<br />\nCabinet '{{cabinetName}}' position '{{positionAlias}}' ({{positionNumber}}) is defect.<br />\n<br />\n<b>Do not reply to this email, it was sent via a 'no-reply address'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 59,
                column: "Template",
                value: "\nBeste Collega,<br />\n<br />\nIn IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) ontbreekt {{itemDescription}}.<br />\n<br />\nOm alleen positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />\nDit doet u door middel van de volgende stappen:<br />\n1. Log in op de IBK<br />\n2. Druk op de tegel verwijderen<br />\n3. Druk op het uitroepteken rechtbovenin<br />\n4. Selecteer de positie die u wilt oplossen<br />\n5. Druk op vrijgeven keycop(s)<br />\n<br />\nOm {{itemDescription}} op te lossen dient u contact op te nemen met de persoon waarop deze geregistreerd staat.<br />\nLET OP! Deze registratie is een mogelijkheid niet een zekerheid!<br />\n<br />\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 60,
                column: "Template",
                value: "\nDear Colleague,<br />\n<br />\nIn cabinet '{{cabinetName}}' at position '{{positionAlias}}' ({{positionNumber}}), {{itemDescription}} is missing.<br />\n<br />\nTo resolve only position '{{positionAlias}}' ({{positionNumber}}), you need to follow the correction flow.<br />\nYou do this by following these steps:<br />\n1. Log in to the cabinet<br />\n2. Press the remove tile<br />\n3. Press the exclamation mark at the top right<br />\n4. Select the position you want to resolve<br />\n5. Press release keycop(s)<br />\n<br />\nTo resolve {{itemDescription}}, you need to contact the person it is registered to.<br />\nNOTE! This registration is a possibility, not a certainty!<br />\n<br />\n<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 61,
                column: "Template",
                value: "\nعزيزي الزميل،<br />\n<br />\nفي الخزانة '{{cabinetName}}' عند الموقع '{{positionAlias}}' ({{positionNumber}})، تم وضع محتوى غير معروف.<br />\n<br />\nلحل موقع '{{positionAlias}}' ({{positionNumber}})، تحتاج إلى المرور بعملية التصحيح.<br />\nيمكنك القيام بذلك باتباع هذه الخطوات:<br />\n1. قم بتسجيل الدخول إلى الخزانة<br />\n2. اضغط على إزالة البلاط<br />\n3. اضغط على علامة التعجب في أعلى اليمين<br />\n4. اختر الموقع الذي تريد حله<br />\n5. اضغط على زر إطلاق المفاتيح<br />\n<br />\nاذهب خلال العناصر وتعامل معها.<br />\n<br />\n<b>لا ترد على هذا البريد الإلكتروني، تم إرساله عبر 'عنوان لا يقبل الرد'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 62,
                column: "Template",
                value: "\nعزيزي الزميل،<br />\n<br />\nموقع الخزانة '{{cabinetName}}' '{{positionAlias}}' ({{positionNumber}}) به عيب.<br />\n<br />\n<b>لا ترد على هذا البريد الإلكتروني، تم إرساله عبر 'عنوان لا يقبل الرد'.</b><br />\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 63,
                column: "Template",
                value: "\nعزيزي الزميل،<br />\n<br />\nفي الخزانة '{{cabinetName}}' عند الموقع '{{positionAlias}}' ({{positionNumber}})، {{itemDescription}} مفقود.<br />\n<br />\nلحل موقع '{{positionAlias}}' ({{positionNumber}}) فقط، تحتاج إلى اتباع عملية التصحيح.<br />\nيمكنك القيام بذلك باتباع هذه الخطوات:<br />\n1. قم بتسجيل الدخول إلى الخزانة<br />\n2. اضغط على إزالة البلاط<br />\n3. اضغط على علامة التعجب في أعلى اليمين<br />\n4. اختر الموقع الذي تريد حله<br />\n5. اضغط على زر إطلاق المفاتيح<br />\n<br />\nلحل {{itemDescription}}، تحتاج إلى الاتصال بالشخص المسجل لديه.<br />\nملاحظة! هذا التسجيل هو احتمال، وليس يقين!<br />\n<br />\n<b>لا ترد على هذا البريد الإلكتروني، تم إرساله من 'عنوان لا يقبل الرد'.</b><br />\n");

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "ID", "IsActive", "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[,]
                {
                    { 64, false, "de-DE", "welcome_web_and_cabinet_login", "Willkommen bei CTAM - Ihre Anmeldedaten", "Sehr geehrte(r) {{name}},<br /><br />Willkommen bei CTAM!<br /><br />Ihre Anmeldedaten<br />Benutzername: {{email}}<br />Temporäres Passwort: {{password}}<br />Anmeldecode: {{loginCode}}<br />Pin-Code: {{pinCode}}", null },
                    { 65, false, "fr-FR", "welcome_web_and_cabinet_login", "Bienvenue à CTAM - vos données de connexion", "Cher(e) {{name}},<br /><br />Bienvenue à CTAM!<br /><br />Vos données de connexion<br />Nom d'utilisateur: {{email}}<br />Mot de passe temporaire: {{password}}<br />Code de connexion: {{loginCode}}<br />Code PIN: {{pinCode}}", null },
                    { 66, false, "sv-SE", "welcome_web_and_cabinet_login", "Välkommen till CTAM - dina inloggningsuppgifter", "Kära {{name}},<br /><br />Välkommen till CTAM!<br /><br />Dina inloggningsuppgifter<br />Användarnamn: {{email}}<br />Tillfälligt lösenord: {{password}}<br />Inloggningskod: {{loginCode}}<br />PIN-kod: {{pinCode}}", null },
                    { 67, false, "de-DE", "welcome_web_login", "Willkommen bei CTAM - Ihre Anmeldedaten", "Sehr geehrte(r) {{name}},<br /><br />Willkommen bei CTAM!<br /><br />Ihre Anmeldedaten<br />Benutzername: {{email}}<br />Temporäres Passwort: {{password}}", null },
                    { 68, false, "de-DE", "temporary_password", "Passwort zurücksetzen", "Sehr geehrte(r) {{name}},<br /><br />Ihr temporäres Passwort lautet {{password}}<br />Ändern Sie es, sobald Sie sich anmelden!", null },
                    { 69, false, "de-DE", "password_changed", "Neues Passwort", "Sehr geehrte(r) {{name}},<br /><br />Ihr Passwort wurde erfolgreich geändert!", null },
                    { 70, false, "de-DE", "forgot_password", "Passwort vergessen", "Sehr geehrte(r) {{name}},<br /><br />Klicken Sie auf den unten stehenden Link, um Ihr Passwort zu ändern.<br /><br /><a href='{{link}}' target='_blank'>Passwort ändern</a><br /><br />Sie erhalten diese E-Mail, weil Sie kürzlich auf der Website von Nauta Connect auf 'Passwort vergessen' geklickt haben. Wenn Sie diese Änderung nicht veranlasst haben, kontaktieren Sie bitte sofort Ihren Administrator.", null },
                    { 71, false, "fr-FR", "welcome_web_login", "Bienvenue à CTAM - vos données de connexion", "Cher(e) {{name}},<br /><br />Bienvenue à CTAM!<br /><br />Vos données de connexion<br />Nom d'utilisateur: {{email}}<br />Mot de passe temporaire: {{password}}", null },
                    { 72, false, "fr-FR", "temporary_password", "Réinitialiser le mot de passe", "Cher(e) {{name}},<br /><br />Votre mot de passe temporaire est {{password}}<br />Changez-le dès que vous vous connectez!", null },
                    { 73, false, "fr-FR", "password_changed", "Nouveau mot de passe", "Cher(e) {{name}},<br /><br />Votre mot de passe a été changé avec succès!", null },
                    { 74, false, "fr-FR", "forgot_password", "Mot de passe oublié", "Cher(e) {{name}},<br /><br />Cliquez sur le lien ci-dessous pour changer votre mot de passe.<br /><br /><a href='{{link}}' target='_blank'>Changer le mot de passe</a><br /><br />Vous recevez ce courriel parce que vous avez récemment appuyé sur le bouton 'Mot de passe oublié' sur le site de Nauta Connect. Si vous n'avez pas initié ce changement, veuillez contacter immédiatement votre administrateur.", null },
                    { 75, false, "sv-SE", "welcome_web_login", "Välkommen till CTAM - dina inloggningsuppgifter", "Kära {{name}},<br /><br />Välkommen till CTAM!<br /><br />Dina inloggningsuppgifter<br />Användarnamn: {{email}}<br />Tillfälligt lösenord: {{password}}", null }
                });

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "ID", "IsActive", "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[,]
                {
                    { 76, false, "sv-SE", "temporary_password", "Återställ lösenord", "Kära {{name}},<br /><br />Ditt tillfälliga lösenord är {{password}}<br />Ändra det så snart du loggar in!", null },
                    { 77, false, "sv-SE", "password_changed", "Nytt lösenord", "Kära {{name}},<br /><br />Ditt lösenord har ändrats framgångsrikt!", null },
                    { 78, false, "sv-SE", "forgot_password", "Glömt lösenord", "Kära {{name}},<br /><br />Klicka på länken nedan för att ändra ditt lösenord.<br /><br /><a href='{{link}}' target='_blank'>Ändra lösenord</a><br /><br />Du får detta e-postmeddelande eftersom du nyligen tryckte på 'Glömt lösenord'-knappen på Nauta Connects webbplats. Om du inte initierade denna ändring, vänligen kontakta din administratör omedelbart.", null },
                    { 79, false, "de-DE", "user_modified", "Ihre Änderungen", "Sehr geehrte(r) {{name}},<br /><br />Überprüfen Sie Ihre Änderungen unten:<br /><br /><table>{{changes}}</table>", null },
                    { 80, false, "sv-SE", "user_modified", "Dina ändringar", "Kära {{name}},<br /><br />Granska dina ändringar nedan:<br /><br /><table>{{changes}}</table>", null },
                    { 81, false, "fr-FR", "user_modified", "Vos modifications", "Cher(e) {{name}},<br /><br />Examinez vos modifications ci-dessous:<br /><br /><table>{{changes}}</table>", null },
                    { 82, false, "de-DE", "user_deleted", "Ihr CTAM-Konto wurde gelöscht", "Sehr geehrte(r) {{name}},<br /><br />Ihr CTAM-Konto wurde vom Administrator gelöscht", null },
                    { 83, false, "de-DE", "stock_below_minimum", "Bestand im Schrank unter Minimum", "Sehr geehrter Schrankadministrator,<br /><br />Der Bestand von {{itemTypeDescription}} im Schrank '{{cabinetNumber}}, {{cabinetName}}' am Standort '{{cabinetLocationDescr}}' liegt unter ({{actualStock}}) dem Mindestbestand ({{minimalStock}}).", null },
                    { 84, false, "de-DE", "stock_at_minimum_level", "Bestand im Schrank zurück auf Mindestniveau", "Sehr geehrter Schrankadministrator,<br /><br />Der Bestand von {{itemTypeDescription}} im Schrank '{{cabinetNumber}}, {{cabinetName}}' am Standort '{{cabinetLocationDescr}}' hat das Mindestniveau ({{minimalStock}}) erreicht.", null },
                    { 85, false, "de-DE", "item_status_changed_to_defect", "Artikel als defekt gemeldet", "Sehr geehrter Schrankadministrator, <br /><br />Artikel '{{itemDescription}}' vom Typ '{{itemTypeDescription}}' wurde von Benutzer '{{userName}}' mit Fehlercode: '{{errorCodeDescription}}' als defekt gemeldet. <br /><br />Sie können es im Schrank '{{cabinetName}}' auf Position '{{positionAlias}}' abholen.", null },
                    { 86, false, "sv-SE", "user_deleted", "Ditt CTAM-konto har raderats", "Kära {{name}},<br /><br />Ditt CTAM-konto har raderats av administratören", null },
                    { 87, false, "sv-SE", "stock_below_minimum", "Skåpets lager under minimum", "Kära skåpadministratör,<br /><br />Lagret av {{itemTypeDescription}} i skåpet '{{cabinetNumber}}, {{cabinetName}}' på plats '{{cabinetLocationDescr}}' är under ({{actualStock}}) det minimala lagret ({{minimalStock}}).", null },
                    { 88, false, "sv-SE", "stock_at_minimum_level", "Skåpets lager återgick till minimal nivå", "Kära skåpadministratör,<br /><br />Lagret av {{itemTypeDescription}} i skåpet '{{cabinetNumber}}, {{cabinetName}}' på plats '{{cabinetLocationDescr}}' har nått den minimala lagernivån ({{minimalStock}}).", null },
                    { 89, false, "sv-SE", "item_status_changed_to_defect", "Artikel rapporterad defekt", "Kära skåpadministratör, <br /><br />Artikel '{{itemDescription}}' av typ '{{itemTypeDescription}}' rapporteras defekt av användare '{{userName}}' med felkod: '{{errorCodeDescription}}'. <br /><br />Du kan hämta den i skåpet '{{cabinetName}}' på position '{{positionAlias}}'.", null },
                    { 90, false, "fr-FR", "user_deleted", "Votre compte CTAM est supprimé", "Cher(e) {{name}},<br /><br />Votre compte CTAM a été supprimé par l'administrateur", null },
                    { 91, false, "fr-FR", "stock_below_minimum", "Stock de l'armoire en dessous du minimum", "Cher administrateur de l'armoire,<br /><br />Le stock de {{itemTypeDescription}} dans l'armoire '{{cabinetNumber}}, {{cabinetName}}' à l'emplacement '{{cabinetLocationDescr}}' est en dessous ({{actualStock}}) du stock minimal ({{minimalStock}}).", null },
                    { 92, false, "fr-FR", "stock_at_minimum_level", "Stock de l'armoire revenu au niveau minimal", "Cher administrateur de l'armoire,<br /><br />Le stock de {{itemTypeDescription}} dans l'armoire '{{cabinetNumber}}, {{cabinetName}}' à l'emplacement '{{cabinetLocationDescr}}' a atteint le niveau de stock minimal ({{minimalStock}}).", null },
                    { 93, false, "fr-FR", "item_status_changed_to_defect", "Article signalé défectueux", "Cher administrateur de l'armoire, <br /><br />L'article '{{itemDescription}}' de type '{{itemTypeDescription}}' est signalé défectueux par l'utilisateur '{{userName}}' avec le code d'erreur: '{{errorCodeDescription}}'. <br /><br />Vous pouvez le récupérer dans l'armoire '{{cabinetName}}' à la position '{{positionAlias}}'.", null },
                    { 94, false, "sv-SE", "personal_item_status_changed_to_swappedback", "Temporärt föremål {{putItemTypeDescription}} återlämnat {{cabinetLocationDescr}} {{errorCodeDescription}}", "Användare bytte tillbaka temporärt föremål. <br /><br />Temporärt föremål <span style='font-style:italic'>'{{putItemDescription}}'</span> med <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> av typ <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> är bytt tillbaka vid <span style='font-style:italic'>'{{actionDT}}'</span> av användare <span style='font-style:italic'>'{{userName}}'</span> och bör kopplas bort från användaren och få status 'I lager'. Detta temporära föremål har felkod: <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br /><br />Användare <span style='font-style:italic'>'{{userName}}'</span> är sedan <span style='font-style:italic'>'{{actionDT}}'</span> i besittning av föremål <span style='font-style:italic'>'{{takeItemDescription}}'</span> med <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> av typ <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>; i CMDB ska föremålet <span style='font-style:italic'>'{{takeItemDescription}}'</span> med <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> kopplas bort från användare <span style='font-style:italic'>'{{userName}}'</span> och få status 'i användning'. <br /><br />Vänligen efter CMDB-ändrat sätt statusen för händelsen av föremålet <span style='font-style:italic'>'{{takeItemDescription}}'</span> med <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> till 'Löst'.", null },
                    { 95, false, "fr-FR", "personal_item_status_changed_to_swappedback", "Article temporaire {{putItemTypeDescription}} retourné {{cabinetLocationDescr}} {{errorCodeDescription}}", "L'utilisateur a échangé l'article temporaire. <br /><br />L'article temporaire <span style='font-style:italic'>'{{putItemDescription}}'</span> avec <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> du type <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> est échangé à nouveau au <span style='font-style:italic'>'{{actionDT}}'</span> par l'utilisateur <span style='font-style:italic'>'{{userName}}'</span> et devrait être déconnecté de l'utilisateur et obtenir le statut ‘En stock’. Cet article temporaire a le code d'erreur : <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br /><br />L'utilisateur <span style='font-style:italic'>'{{userName}}'</span> est depuis le <span style='font-style:italic'>'{{actionDT}}'</span> en possession de l'article <span style='font-style:italic'>'{{takeItemDescription}}'</span> avec <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> du type <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>; dans le CMDB l'article <span style='font-style:italic'>'{{takeItemDescription}}'</span> avec <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> devrait être déconnecté de l'utilisateur <span style='font-style:italic'>'{{userName}}'</span> et obtenir le statut ‘en utilisation’. <br /><br />Veuillez après le changement du CMDB mettre le statut de l'incident de l'article <span style='font-style:italic'>'{{takeItemDescription}}'</span> avec <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> à ‘Résolu’.", null },
                    { 96, false, "de-DE", "personal_item_status_changed_to_swappedback", "Temporärer Artikel {{putItemTypeDescription}} zurückgegeben {{cabinetLocationDescr}} {{errorCodeDescription}}", "Benutzer hat temporären Artikel zurückgetauscht. <br /><br />Temporärer Artikel <span style='font-style:italic'>'{{putItemDescription}}'</span> mit <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> vom Typ <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> ist am <span style='font-style:italic'>'{{actionDT}}'</span> vom Benutzer <span style='font-style:italic'>'{{userName}}'</span> zurückgetauscht worden und sollte vom Benutzer getrennt und den Status ‘Auf Lager’ bekommen. Dieser temporäre Artikel hat den Fehlercode: <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br /><br />Benutzer <span style='font-style:italic'>'{{userName}}'</span> ist seit <span style='font-style:italic'>'{{actionDT}}'</span> im Besitz des Artikels <span style='font-style:italic'>'{{takeItemDescription}}'</span> mit <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> vom Typ <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>; im CMDB sollte der Artikel <span style='font-style:italic'>'{{takeItemDescription}}'</span> mit <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> vom Benutzer <span style='font-style:italic'>'{{userName}}'</span> getrennt und den Status ‘in Gebrauch’ bekommen. <br /><br />Bitte setzen Sie nach der CMDB-Änderung den Status des Vorfalls des Artikels <span style='font-style:italic'>'{{takeItemDescription}}'</span> mit <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> auf ‘Gelöst’.", null },
                    { 97, false, "sv-SE", "welcome_cabinet_login", "Din pinkod för skåpsinloggning", "Kära {{userName}},<br /><br />Din pinkod är {{pinCode}}.", null },
                    { 98, false, "fr-FR", "welcome_cabinet_login", "Votre code PIN pour la connexion au casier", "Cher {{userName}},<br /><br />Votre code PIN est {{pinCode}}.", null },
                    { 99, false, "de-DE", "welcome_cabinet_login", "Ihr PIN-Code für den Schranks Login", "Liebe(r) {{userName}},<br /><br />Ihr PIN-Code ist {{pinCode}}.", null },
                    { 100, false, "sv-SE", "pincode_changed", "Återställ pinkod", "Kära {{name}},<br /><br />Din nya pinkod är {{pinCode}}<br />", null },
                    { 101, false, "fr-FR", "pincode_changed", "Réinitialisation du code PIN", "Cher {{name}},<br /><br />Votre nouveau code PIN est {{pinCode}}<br />", null },
                    { 102, false, "de-DE", "pincode_changed", "PIN-Code zurücksetzen", "Liebe(r) {{name}},<br /><br />Ihr neuer PIN-Code ist {{pinCode}}<br />", null },
                    { 103, false, "de-DE", "incorrect_return_closed_uip", "Falsche Rückgabe, Datensatz geschlossen", "\nLiebe Kollegin, lieber Kollege,<br />\n<br />\nDer Artikel '{{itemDescription}}' vom Typ '{{itemTypeDescription}}' wurde fälschlicherweise von Benutzer '{{userEmail}}' im IBK '{{cabinetName}}' an Position '{{positionAlias}}' als zurückgegeben gemeldet.<br />\n<br />\nDiese Aktion wurde durch Schließen eines Besitzdatensatzes registriert, und der Artikel '{{itemDescription}}' ist wieder im Umlauf. <br />\nEs ist möglich, dass dem Benutzer '{{userEmail}}' fälschlicherweise der Artikel '{{itemDescription}}' ausgehändigt wurde und dieser daraufhin als falsch zurückgegeben gemeldet wurde.<br />\nUm zu verhindern, dass der Benutzer '{{userEmail}}' eine falsche Registrierung hat, können Sie dies untersuchen. <br />\n<br />\nSehen Sie sich die Benutzerhistorie von '{{userEmail}}' an, um festzustellen, ob der Benutzer Artikel in Besitz registriert hat.<br />\nWenn der Benutzer '{{userEmail}}' Artikel in Besitz registriert hat, können Sie die betrieblichen und technischen Protokolle überprüfen, um zu sehen, welche Aktionen der falschen Rückgabemeldung vorausgingen.<br />\nWenn es scheint, dass der Benutzer '{{userEmail}}' eine falsche Registrierung hat, kontaktieren Sie bitte den Benutzer '{{userEmail}}', um nach etwaigen falschen Entnahmen zu fragen.<br />\n<br />\n<b>Antworten Sie nicht auf diese E-Mail, sie wurde von einer 'no-reply Adresse' gesendet.</b><br />\n", null },
                    { 104, false, "sv-SE", "incorrect_return_closed_uip", "Felaktig återlämning, posten stängd", "\nKära kollega,<br />\n<br />\nArtikeln '{{itemDescription}}' av typ '{{itemTypeDescription}}' har felaktigt rapporterats återlämnad av användare '{{userEmail}}' till IBK '{{cabinetName}}' vid position '{{positionAlias}}'<br />\n<br />\nDenna åtgärd har registrerats genom att stänga en äganderättsrekord och artikeln '{{itemDescription}}' är åter i omlopp. <br />\nDet är möjligt att användaren '{{userEmail}}' felaktigt har fått artikeln '{{itemDescription}}' och därefter rapporterat den som felaktigt återlämnad.<br />\nFör att förhindra att användaren '{{userEmail}}' har en felaktig registrering, kan du undersöka detta. <br />\n<br />\nVisa användarhistoriken för '{{userEmail}}' för att se om användaren har registrerat artiklar i besittning.<br />\nOm användaren '{{userEmail}}' har registrerade artiklar i besittning, kan du kontrollera de operativa och tekniska loggarna för att se vilka åtgärder som föregick den felaktiga returanmälan.<br />\nOm det verkar som att användaren '{{userEmail}}' har en felaktig registrering, vänligen kontakta användaren '{{userEmail}}' för att fråga om eventuella felaktiga uttag.<br />\n<br />\n<b>Svara inte på detta e-postmeddelande, det har skickats från en 'no-reply adress'.</b><br />\n", null },
                    { 105, false, "fr-FR", "incorrect_return_closed_uip", "Retour incorrect, enregistrement fermé", "\nCher collègue,<br />\n<br />\nL'article '{{itemDescription}}' de type '{{itemTypeDescription}}' a été signalé incorrectement retourné par l'utilisateur '{{userEmail}}' au IBK '{{cabinetName}}' à la position '{{positionAlias}}'.<br />\n<br />\nCette action a été enregistrée en fermant un dossier de possession et l'article '{{itemDescription}}' est de nouveau en circulation. <br />\nIl est possible que l'utilisateur '{{userEmail}}' ait reçu par erreur l'article '{{itemDescription}}' et l'ait ensuite signalé comme retourné incorrectement.<br />\nPour éviter une inscription incorrecte de l'utilisateur '{{userEmail}}', vous pouvez enquêter sur cette situation. <br />\n<br />\nConsultez l'historique de l'utilisateur '{{userEmail}}' pour voir si l'utilisateur a enregistré des articles en possession.<br />\nSi l'utilisateur '{{userEmail}}' a des articles enregistrés en possession, vous pouvez vérifier les journaux opérationnels et techniques pour voir quelles actions ont précédé la notification de retour incorrect.<br />\nS'il apparaît que l'utilisateur '{{userEmail}}' a une inscription incorrecte, veuillez contacter l'utilisateur '{{userEmail}}' pour vous renseigner sur d'éventuels retraits incorrects.<br />\n<br />\n<b>Ne répondez pas à cet email, il a été envoyé depuis une adresse 'no-reply'.</b><br />\n", null },
                    { 106, false, "de-DE", "incorrect_return_created_uip", "Falsche Rückgabe hat Datensatz erstellt", "\nLiebe Kollegin, lieber Kollege,<br />\n<br />\nDer Artikel '{{itemDescription}}' vom Typ '{{itemTypeDescription}}' wurde fälschlicherweise von Benutzer '{{userEmail}}' als zurückgegeben im IBK '{{cabinetName}}' am Platz '{{positionAlias}}' gemeldet.<br />\n<br />\nDiese Aktion wurde durch Erstellen eines Besitzdatensatzes registriert, und der Artikel '{{itemDescription}}' ist wieder im Umlauf. <br />\nEs ist möglich, dass dem Benutzer '{{userEmail}}' fälschlicherweise der Artikel '{{itemDescription}}' gegeben und daraufhin als falsch zurückgegeben gemeldet wurde.<br />\nUm zu verhindern, dass der Benutzer '{{userEmail}}' eine falsche Registrierung hat, können Sie dies untersuchen. <br />\n<br />\nSehen Sie sich die Benutzerhistorie von '{{userEmail}}' an, um zu prüfen, ob der Benutzer Artikel in Besitz registriert hat.<br />\nWenn der Benutzer '{{userEmail}}' Artikel in Besitz registriert hat, können Sie die Betriebs- und Technikprotokolle überprüfen, um zu sehen, welche Aktionen der falschen Rückgabemeldung vorausgingen.<br />\nWenn es scheint, dass der Benutzer '{{userEmail}}' eine falsche Registrierung hat, kontaktieren Sie bitte den Benutzer '{{userEmail}}', um nach etwaigen falschen Abhebungen zu fragen.<br />\n<br />\n<b>Antworten Sie nicht auf diese E-Mail, sie wurde von einer 'no-reply Adresse' gesendet.</b><br />\n", null },
                    { 107, false, "sv-SE", "incorrect_return_created_uip", "Felaktig återlämning skapade post", "\nKära kollega,<br />\n<br />\nArtikeln '{{itemDescription}}' av typ '{{itemTypeDescription}}' har felaktigt rapporterats återlämnad av användare '{{userEmail}}' till IBK '{{cabinetName}}' vid position '{{positionAlias}}'<br />\n<br />\nDenna åtgärd har registrerats genom att skapa ett äganderättsrekord och artikeln '{{itemDescription}}' är åter i omlopp. <br />\nDet är möjligt att användaren '{{userEmail}}' felaktigt har fått artikeln '{{itemDescription}}' och därefter rapporterat den som felaktigt återlämnad.<br />\nFör att förhindra att användaren '{{userEmail}}' har en felaktig registrering, kan du undersöka detta. <br />\n<br />\nVisa användarhistoriken för '{{userEmail}}' för att se om användaren har registrerat artiklar i besittning.<br />\nOm användaren '{{userEmail}}' har registrerade artiklar i besittning, kan du kontrollera de operativa och tekniska loggarna för att se vilka åtgärder som föregick den felaktiga returanmälan.<br />\nOm det verkar som att användaren '{{userEmail}}' har en felaktig registrering, vänligen kontakta användaren '{{userEmail}}' för att fråga om eventuella felaktiga uttag.<br />\n<br />\n<b>Svara inte på detta e-postmeddelande, det har skickats från en 'no-reply adress'.</b><br />\n", null },
                    { 108, false, "fr-FR", "incorrect_return_created_uip", "Création d'un enregistrement pour retour incorrect", "\nCher Collègue,<br />\n<br />\nL'article '{{itemDescription}}' de type '{{itemTypeDescription}}' a été signalé incorrectement retourné par l'utilisateur '{{userEmail}}' au IBK '{{cabinetName}}' à la position '{{positionAlias}}'.<br />\n<br />\nCette action a été enregistrée en créant un dossier de possession et l'article '{{itemDescription}}' est de nouveau en circulation. <br />\nIl est possible que l'utilisateur '{{userEmail}}' ait reçu par erreur l'article '{{itemDescription}}' et l'ait ensuite signalé comme retourné incorrectement.<br />\nPour éviter une inscription incorrecte de l'utilisateur '{{userEmail}}', vous pouvez enquêter sur cette situation. <br />\n<br />\nConsultez l'historique de l'utilisateur '{{userEmail}}' pour voir si l'utilisateur a enregistré des articles en possession.<br />\nSi l'utilisateur '{{userEmail}}' a des articles enregistrés en possession, vous pouvez vérifier les journaux opérationnels et techniques pour voir quelles actions ont précédé la notification de retour incorrect.<br />\nS'il apparaît que l'utilisateur '{{userEmail}}' a une inscription incorrecte, veuillez contacter l'utilisateur '{{userEmail}}' pour vous renseigner sur d'éventuels retraits incorrects.<br />\n<br />\n<b>Ne répondez pas à cet email, il a été envoyé depuis une adresse 'no-reply'.</b><br />\n", null },
                    { 109, false, "de-DE", "unknown_content_position", "Unbekannter Inhalt", "\nLiebe Kollegin, lieber Kollege,<br />\n<br />\nIm Schrank '{{cabinetName}}' an der Position '{{positionAlias}}' ({{positionNumber}}) wurde unbekannter Inhalt abgelegt.<br />\n<br />\nUm die Position '{{positionAlias}}' ({{positionNumber}}) zu klären, müssen Sie den Korrekturablauf durchführen.<br />\nFolgen Sie dazu diesen Schritten:<br />\n1. Melden Sie sich am Schrank an<br />\n2. Drücken Sie auf das Entfernen-Feld<br />\n3. Drücken Sie das Ausrufezeichen oben rechts<br />\n4. Wählen Sie die Position aus, die Sie klären möchten<br />\n5. Drücken Sie auf Schlüssel freigeben<br />\n<br />\nGehen Sie die Gegenstände durch und bearbeiten Sie sie.<br />\n<br />\n<b>Antworten Sie nicht auf diese E-Mail, sie wurde über eine 'no-reply-Adresse' gesendet.</b><br />\n", null },
                    { 110, false, "sv-SE", "unknown_content_position", "Okänt innehåll", "\nKära kollega,<br />\n<br />\nI skåpet '{{cabinetName}}' vid position '{{positionAlias}}' ({{positionNumber}}), har okänt innehåll placerats.<br />\n<br />\nFör att lösa position '{{positionAlias}}' ({{positionNumber}}), behöver du genomgå korrektionsflödet.<br />\nDu gör detta genom att följa dessa steg:<br />\n1. Logga in på skåpet<br />\n2. Tryck på ta bort-plattan<br />\n3. Tryck på utropstecknet högst upp till höger<br />\n4. Välj den position du vill lösa<br />\n5. Tryck på frigör nyckelkopior<br />\n<br />\nGå igenom föremålen och hantera dem.<br />\n<br />\n<b>Svara inte på detta e-postmeddelande, det skickades från en 'no-reply-adress'.</b><br />\n", null },
                    { 111, false, "fr-FR", "unknown_content_position", "Contenu inconnu", "\nCher collègue,<br />\n<br />\nDans l'armoire '{{cabinetName}}' à la position '{{positionAlias}}' ({{positionNumber}}), un contenu inconnu a été placé.<br />\n<br />\nPour résoudre la position '{{positionAlias}}' ({{positionNumber}}), vous devez suivre le processus de correction.<br />\nVous le faites en suivant ces étapes :<br />\n1. Connectez-vous à l'armoire<br />\n2. Appuyez sur la tuile de suppression<br />\n3. Appuyez sur le point d'exclamation en haut à droite<br />\n4. Sélectionnez la position que vous souhaitez résoudre<br />\n5. Appuyez sur libérer les clés<br />\n<br />\nPassez en revue les articles et gérez-les.<br />\n<br />\n<b>Ne répondez pas à cet email, il a été envoyé depuis une adresse 'no-reply'.</b><br />\n", null },
                    { 112, false, "de-DE", "defect_position", "Defekte Position", "\nLiebe Kollegin, lieber Kollege,<br />\n<br />\nPosition '{{positionAlias}}' ({{positionNumber}}) im Schrank '{{cabinetName}}' ist defekt.<br />\n<br />\n<b>Antworten Sie nicht auf diese E-Mail, sie wurde über eine 'no-reply-Adresse' gesendet.</b><br />\n", null },
                    { 113, false, "sv-SE", "defect_position", "Defekt position", "\nKära kollega,<br />\n<br />\nPosition '{{positionAlias}}' ({{positionNumber}}) i skåpet '{{cabinetName}}' är defekt.<br />\n<br />\n<b>Svara inte på detta e-postmeddelande, det skickades från en 'no-reply-adress'.</b><br />\n", null },
                    { 114, false, "fr-FR", "defect_position", "Position défectueuse", "\nCher collègue,<br />\n<br />\nLa position '{{positionAlias}}' ({{positionNumber}}) dans l'armoire '{{cabinetName}}' est défectueuse.<br />\n<br />\n<b>Ne répondez pas à cet email, il a été envoyé depuis une adresse 'no-reply'.</b><br />\n", null },
                    { 115, false, "de-DE", "missing_content_position", "Fehlender Inhalt", "\nLiebe Kollegin, lieber Kollege,<br />\n<br />\nIm Schrank '{{cabinetName}}' an der Position '{{positionAlias}}' ({{positionNumber}}) fehlt {{itemDescription}}.<br />\n<br />\nUm nur die Position '{{positionAlias}}' ({{positionNumber}}) zu klären, müssen Sie den Korrekturablauf durchführen.<br />\nFolgen Sie dazu diesen Schritten:<br />\n1. Melden Sie sich am Schrank an<br />\n2. Drücken Sie auf das Entfernen-Feld<br />\n3. Drücken Sie das Ausrufezeichen oben rechts<br />\n4. Wählen Sie die Position aus, die Sie klären möchten<br />\n5. Drücken Sie auf Schlüssel freigeben<br />\n<br />\nUm {{itemDescription}} zu klären, müssen Sie sich mit der Person in Verbindung setzen, auf die es registriert ist.<br />\nHINWEIS! Diese Registrierung ist eine Möglichkeit, keine Gewissheit!<br />\n<br />\n<b>Antworten Sie nicht auf diese E-Mail, sie wurde über eine 'no-reply-Adresse' gesendet.</b><br />\n", null },
                    { 116, false, "sv-SE", "missing_content_position", "Saknat innehåll", "\nKära kollega,<br />\n<br />\nI skåpet '{{cabinetName}}' vid position '{{positionAlias}}' ({{positionNumber}}), saknas {{itemDescription}}.<br />\n<br />\nFör att endast lösa position '{{positionAlias}}' ({{positionNumber}}), behöver du följa korrektionsflödet.<br />\nDu gör detta genom att följa dessa steg:<br />\n1. Logga in på skåpet<br />\n2. Tryck på ta bort-plattan<br />\n3. Tryck på utropstecknet högst upp till höger<br />\n4. Välj den position du vill lösa<br />\n5. Tryck på frigör nyckelkopior<br />\n<br />\nFör att lösa {{itemDescription}}, behöver du kontakta den person det är registrerat på.<br />\nOBS! Denna registrering är en möjlighet, inte en säkerhet!<br />\n<br />\n<b>Svara inte på detta e-postmeddelande, det skickades från en 'no-reply-adress'.</b><br />\n", null },
                    { 117, false, "fr-FR", "missing_content_position", "Contenu manquant", "\nCher collègue,<br />\n<br />\nDans l'armoire '{{cabinetName}}' à la position '{{positionAlias}}' ({{positionNumber}}), {{itemDescription}} manque.<br />\n<br />\nPour résoudre uniquement la position '{{positionAlias}}' ({{positionNumber}}), vous devez suivre le processus de correction.<br />\nVous le faites en suivant ces étapes :<br />\n1. Connectez-vous à l'armoire<br />\n2. Appuyez sur la tuile de suppression<br />\n3. Appuyez sur le point d'exclamation en haut à droite<br />\n4. Sélectionnez la position que vous souhaitez résoudre<br />\n5. Appuyez sur libérer les clés<br />\n<br />\nPour résoudre {{itemDescription}}, vous devez contacter la personne à qui elle est enregistrée.<br />\nNOTE ! Cette inscription est une possibilité, pas une certitude !<br />\n<br />\n<b>Ne répondez pas à cet email, il a été envoyé depuis une adresse 'no-reply'.</b><br />\n", null }
                });

            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "ID", "IsActive", "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[,]
                {
                    { 118, false, "sv-SE", "personal_item_status_changed_to_repaired", "Ditt personliga föremål är reparerat", "\nKära {{userName}}, <br /><br />Personligt föremål '{{itemDescription}}' av typ '{{itemTypeDescription}}' är rapporterat reparerat. <br /><br />Du kan byta tillbaka det vid skåpet '{{cabinetName}}' på position '{{positionAlias}}'.", null },
                    { 119, false, "de-DE", "personal_item_status_changed_to_repaired", "Ihr persönlicher Gegenstand ist repariert", "\nLiebe(r) {{userName}}, <br /><br />Persönlicher Gegenstand '{{itemDescription}}' vom Typ '{{itemTypeDescription}}' wurde als repariert gemeldet. <br /><br />Sie können ihn am Schrank '{{cabinetName}}' auf Position '{{positionAlias}}' zurücktauschen.", null },
                    { 120, false, "fr-FR", "personal_item_status_changed_to_repaired", "Votre objet personnel est réparé", "\nCher(ère) {{userName}}, <br /><br />L'objet personnel '{{itemDescription}}' de type '{{itemTypeDescription}}' est signalé comme réparé. <br /><br />Vous pouvez l'échanger à nouveau au casier '{{cabinetName}}' à la position '{{positionAlias}}'.", null },
                    { 121, false, "sv-SE", "personal_item_status_changed_to_replaced", "Ditt personliga föremål är utbytt", "\nKära {{userName}}, <br /><br />Personligt föremål '{{itemDescription}}' av typ '{{itemTypeDescription}}' är rapporterat utbytt. <br /><br />Du kan hämta det vid skåpet '{{cabinetName}}' på position '{{positionAlias}}'.", null },
                    { 122, false, "de-DE", "personal_item_status_changed_to_replaced", "Ihr persönlicher Gegenstand wurde ersetzt", "\nLiebe(r) {{userName}}, <br /><br />Persönlicher Gegenstand '{{itemDescription}}' vom Typ '{{itemTypeDescription}}' wurde als ersetzt gemeldet. <br /><br />Sie können ihn am Schrank '{{cabinetName}}' auf Position '{{positionAlias}}' abholen.", null },
                    { 123, false, "fr-FR", "personal_item_status_changed_to_replaced", "Votre objet personnel a été remplacé", "\nCher(ère) {{userName}}, <br /><br />L'objet personnel '{{itemDescription}}' de type '{{itemTypeDescription}}' est signalé comme remplacé. <br /><br />Vous pouvez le récupérer au casier '{{cabinetName}}' à la position '{{positionAlias}}'.", null },
                    { 124, false, "sv-SE", "personal_item_status_changed_to_defect", "Defekt rapporterad {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}", "\nFöremål {{putItemDescription}} rapporterades defekt den {{actionDT}} av {{userName}} med fel: {{errorCodeDescription}}. Skåp {{cabinetName}} i låda {{positionAlias}} på plats {{cabinetLocationDescr}}. <br /><br />Användare {{userName}} har nu ersättningsföremål {{takeItemDescription}}.", null },
                    { 125, false, "de-DE", "personal_item_status_changed_to_defect", "Defekt gemeldet {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}", "\nArtikel {{putItemDescription}} wurde am {{actionDT}} von {{userName}} als defekt gemeldet mit Fehler: {{errorCodeDescription}}. Schrank {{cabinetName}} in Fach {{positionAlias}} am Ort {{cabinetLocationDescr}}. <br /><br />Benutzer {{userName}} hat nun Ersatzartikel {{takeItemDescription}}.", null },
                    { 126, false, "fr-FR", "personal_item_status_changed_to_defect", "Défaut signalé {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}", "\nArticle {{putItemDescription}} signalé défectueux le {{actionDT}} par {{userName}} avec erreur : {{errorCodeDescription}}. Armoire {{cabinetName}} dans casier {{positionAlias}} à l'emplacement {{cabinetLocationDescr}}. <br /><br />L'utilisateur {{userName}} possède maintenant l'article de remplacement {{takeItemDescription}}.", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 112);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 113);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 114);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 115);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 116);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 117);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 118);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 119);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 120);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 121);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 122);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 123);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 124);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 125);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 126);

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
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\n<b>Beheerapplicatie</b><br />\r\nEr is voor jou een account aangemaakt voor de applicatie waarmee de Intelligente BeheerKasten (IBK’s) beheerd kunnen worden. Door op de link te klikken kom je bij het inlogscherm van de IBK-Beheerapplicatie:<br />\r\n<a href='{{link}}' target='_blank' >IBK beheer applicatie</a> <br />\r\n<br />\r\nJouw inloggegevens:<br />\r\n<br />\r\nGebruikersnaam: Dit is je emailadres met NP+personeelsnummer (bijvoorbeeld NP123456@politie.nl). Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.<br />\r\nTijdelijk wachtwoord: {{password}}<br />\r\n<br />\r\nWijzig direct na het inloggen jouw wachtwoord! Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine letter en 1 cijfer.<br />\r\n<br />\r\n<b>Registreer toegangspas bij een IBK</b><br />\r\nOm jezelf als IBK-gebruiker te registeren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Doe dit zo snel mogelijk om collega’s met defecte portofoons te kunnen ondersteunen.<br />\r\n<ol>\r\n<li>Ga naar een IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\r\n<li>Druk op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\r\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig personeelsnummer in.</li>\r\n<li>Voer in het veld ‘Vul pincode in’ de pincode ‘{{pinCode}}’ in.</li>\r\n<li>Scan jouw politie-toegangspas bij de kaartlezer onder het scherm (dit is dezelfde pas die toegang geeft tot de politiepanden/locaties).<br />\r\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu jouw collega’s met een defecte portofoon kunt ondersteunen bij een willekeurige IBK van jouw eenheid.</li>\r\n</ol>\r\n<br />\r\n\r\nZie voor meer uitleg, <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hulp en FAQ</a>.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 3,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nEr is voor jou een account aangemaakt voor de applicatie waarmee de Intelligente BeheerKasten (IBK’s) beheerd kunnen worden. Door op de link te klikken kom je bij het inlogscherm van de IBK-Beheerapplicatie:<br />\r\n<a href='{{link}}' target='_blank' >IBK beheer applicatie</a> <br />\r\n<br />\r\nJouw inloggegevens:<br />\r\n<br />\r\nGebruikersnaam: Dit is je emailadres met NP+personeelsnummer (bijvoorbeeld NP123456@politie.nl). Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.<br />\r\nTijdelijk wachtwoord: {{password}}<br />\r\n<br />\r\nWijzig direct na het inloggen jouw wachtwoord! Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine letter en 1 cijfer.<br />\r\n<br />\r\nZie voor meer uitleg, <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hulp en FAQ</a>.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 5,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nJe hebt een wachtwoord-reset aangevraagd, jouw tijdelijke wachtwoord is <b>{{password}}</b><br />\r\n<br />\r\nGa naar de IBK-Beheerapplicatie en verander direct jouw tijdelijke wachtwoord. Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />\r\n<br />\r\nLog binnen de IBK-Beheerapplicatie altijd in met jouw NP-emailadres (bijvoorbeeld NP250537@politie.nl)<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

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
                value: "\r\nBeste collega,<br />\r\n<br />\r\nAls jouw C2000-portofoon defect raakt, kun je die vanaf nu direct en 24x7 omruilen voor een werkend tijdelijk exemplaar bij een onbemande Intelligente BeheerKast (IBK). Registreer hiervoor zo snel mogelijk je pas. In deze mail kun je lezen hoe je dit kunt doen.<br />\r\n<br />\r\n<b>Nieuw: op tien locaties een IBK</b><br />\r\nEerder moest je voor een portofoon-reparatie een afspraak maken bij een LO-Servicebalie. <br />\r\nVerdeeld over jouw eenheid vind je nu tien onbemande IBK’s waar je direct en 24x7 terecht kunt om een defecte portofoon om te ruilen voor een werkend tijdelijk exemplaar. Als jouw portofoon gerepareerd is, krijg je hiervan automatisch een melding en kun je de tijdelijke portofoon bij dezelfde IBK weer omruilen voor jouw eigen portofoon.<br />\r\n<br />\r\n<b>Registreer je pas met onderstaande pincode</b><br />\r\nOm jezelf als IBK-gebruiker te registreren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Dit kun je vandaag al doen zodat je later, in geval van een portofoon-defect direct geholpen kunt worden.<br />\r\n<ol>\r\n<li>Ga naar een fysieke IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\r\n<li>Druk bij de IBK op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\r\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig <b>Personeelsnummer</b> in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\r\n<li>Voer in het veld ‘Vul pincode in’ de <b>pincode ‘{{pinCode}}’</b> in.</li>\r\n<li>Scan jouw politie-toegangspas bij de scanner onder het scherm (dit is dezelfde pas die jou toegang geeft tot de politiepanden).<br />\r\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\r\n</ol>\r\n<br />\r\n<b>Nu alleen voor portofoon met basisprogrammering</b><br />\r\nOp dit moment kunnen alleen portofoons met een basisprogrammering gerepareerd worden met behulp van de IBK’s. Voor portofoons met een andere programmering zoals bijvoorbeeld ‘Recherche’ of ‘Onderhandelaar’ kan het huidige reparatieproces met de LO-Servicebalie gebruikt worden. In de nabije toekomst zullen ook andere portofoons en items in de IBK’s geplaatst gaan worden, registreer daarom dus zo snel mogelijk jouw toegangspas.<br />\r\n<br />\r\nKlik <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hier</a> voor meer uitleg, hulp en FAQ. Heb je nog aanvullende vragen en/of opmerkingen, neem dan contact op met Functioneel Beheer IBK via\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 31,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nEr is voor jou een nieuwe pincode aangemaakt waarmee je een toegangspas kunt koppelen aan jouw IBK-account. Jouw nieuwe pincode is: <b>‘{{pinCode}}’</b><br />\r\n<ol>\r\n<li>Ga naar een fysieke IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\r\n<li>Druk bij de IBK op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\r\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig <b>Personeelsnummer</b> in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\r\n<li>Voer in het veld ‘Vul pincode in’ de nieuwe <b>pincode ‘{{pinCode}}’</b> in.</li>\r\n<li>Scan jouw politie-toegangspas bij de scanner onder het scherm (dit is dezelfde pas die jou toegang geeft tot de politiepanden).<br />\r\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\r\n</ol>\r\n<br />\r\nKlik <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hier</a> voor meer uitleg, hulp en FAQ. Heb je nog aanvullende vragen en/of opmerkingen, neem dan contact op met Functioneel Beheer IBK via <a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 49,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nItem '{{itemDescription}}' van type '{{itemTypeDescription}}' is incorrect terug gemeld door gebruiker '{{userEmail}}' aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}' <br />\r\n<br />\r\nDeze actie is geregistreerd doormiddel van het afsluiten van een bezitsrecord en item '{{itemDescription}}' is weer in omloop. <br />\r\nHet is mogelijk dat gebruiker '{{userEmail}}' item '{{itemDescription}}' incorrect uitgeworpen heeft gekregen en hierna als incorrect terug heeft gemeld.<br />\r\nOm te voorkomen dat gebruiker '{{userEmail}}' een onterechte registratie heeft kunt u dit onderzoeken. <br />\r\n<br />\r\nBekijk in de gebruikers geschiedenis van gebruiker '{{userEmail}}' of de gebruiker items in bezit geregistreerd heeft.<br />\r\nAls gebruiker '{{userEmail}}' items in bezit geregistreerd heeft kunt u in de operationele en technische logs bekijken welke handelingen vooraf zijn gegaan aan de incorrect terug melding.<br />\r\nLijkt het erop dat gebruiker '{{userEmail}}' een onterechte registratie heeft, neem dan contact op met gebruiker '{{userEmail}}' om te vragen naar eventuele incorrecte uitnamen.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 50,
                column: "Template",
                value: "\r\nDear Colleague,<br />\r\n<br />\r\nItem '{{itemDescription}}' of type '{{itemTypeDescription}}' has been incorrectly reported returned by user '{{userEmail}}' to the IBK '{{cabinetName}}' at position '{{positionAlias}}'<br />\r\n<br />\r\nThis action has been registrated by closing a possession record and item '{{itemDescription}}' is back in circulation. <br />\r\nIt is possible that user '{{userEmail}}' has been incorrectly given item '{{itemDescription}}' and thereafter reported it as incorrectly returned.<br />\r\nTo prevent user '{{userEmail}}' from having an incorrect registration, you can investigate this. <br />\r\n<br />\r\nView the user history of user '{{userEmail}}' to see if the user has registered items in possession.<br />\r\nIf user '{{userEmail}}' has items registered in possession, you can check the operational and technical logs to see which actions preceded the incorrect return notification.<br />\r\nIf it appears that user '{{userEmail}}' has an incorrect registration, please contact user '{{userEmail}}' to inquire about any incorrect withdrawals.<br />\r\n<br />\r\n<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 51,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nItem '{{itemDescription}}' van type '{{itemTypeDescription}}' is incorrect terug gemeld door gebruiker '{{userEmail}}' aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}' <br />\r\n<br />\r\nDeze actie is geregistreerd doormiddel van het aanmaken van een bezitsrecord en item '{{itemDescription}}' is weer in omloop. <br />\r\nHet is mogelijk dat gebruiker '{{userEmail}}' item '{{itemDescription}}' incorrect uitgeworpen heeft gekregen en hierna als incorrect terug heeft gemeld.<br />\r\nOm te voorkomen dat gebruiker '{{userEmail}}' een onterechte registratie heeft kunt u dit onderzoeken. <br />\r\n<br />\r\nBekijk in de gebruikers geschiedenis van gebruiker '{{userEmail}}' of de gebruiker items in bezit geregistreerd heeft.<br />\r\nAls gebruiker '{{userEmail}}' items in bezit geregistreerd heeft kunt u in de operationele en technische logs bekijken welke handelingen vooraf zijn gegaan aan de incorrect terug melding.<br />\r\nLijkt het erop dat gebruiker '{{userEmail}}' een onterechte registratie heeft, neem dan contact op met gebruiker '{{userEmail}}' om te vragen naar eventuele incorrecte uitnamen.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 52,
                column: "Template",
                value: "\r\nDear Colleague,<br />\r\n<br />\r\nItem '{{itemDescription}}' of type '{{itemTypeDescription}}' has been incorrectly reported returned by user '{{userEmail}}' to the IBK '{{cabinetName}}' at position '{{positionAlias}}'<br />\r\n<br />\r\nThis action has been registrated by creating a possession record and item '{{itemDescription}}' is back in circulation. <br />\r\nIt is possible that user '{{userEmail}}' has been incorrectly given item '{{itemDescription}}' and thereafter reported it as incorrectly returned.<br />\r\nTo prevent user '{{userEmail}}' from having an incorrect registration, you can investigate this. <br />\r\n<br />\r\nView the user history of user '{{userEmail}}' to see if the user has registered items in possession.<br />\r\nIf user '{{userEmail}}' has items registered in possession, you can check the operational and technical logs to see which actions preceded the incorrect return notification.<br />\r\nIf it appears that user '{{userEmail}}' has an incorrect registration, please contact user '{{userEmail}}' to inquire about any incorrect withdrawals.<br />\r\n<br />\r\n<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 53,
                column: "Template",
                value: "\r\nعزيزي الزميل،<br />\r\n<br />\r\nتم الإبلاغ عن العنصر '{{itemDescription}}' من النوع '{{itemTypeDescription}}' بشكل خاطئ كمرتجع من قبل المستخدم '{{userEmail}}' إلى IBK '{{cabinetName}}' في الموقع '{{positionAlias}}'<br />\r\n<br />\r\nتم تسجيل هذا الإجراء بإغلاق سجل الاستحواذ والعنصر '{{itemDescription}}' عاد إلى التداول. <br />\r\nمن الممكن أن يكون المستخدم '{{userEmail}}' قد تلقى العنصر '{{itemDescription}}' بشكل خاطئ ومن ثم أبلغ عنه كمرتجع خاطئ.<br />\r\nلمنع المستخدم '{{userEmail}}' من وجود تسجيل خاطئ، يمكنك التحقيق في ذلك. <br />\r\n<br />\r\nاطلع على سجل المستخدم للمستخدم '{{userEmail}}' لمعرفة ما إذا كان لدى المستخدم عناصر مسجلة تحت حيازته.<br />\r\nإذا كان لدى المستخدم '{{userEmail}}' عناصر مسجلة تحت حيازته، يمكنك التحقق من السجلات التشغيلية والفنية لمعرفة الإجراءات التي سبقت إشعار الإرجاع الخاطئ.<br />\r\nإذا بدا أن المستخدم '{{userEmail}}' لديه تسجيل خاطئ، يرجى التواصل مع المستخدم '{{userEmail}}' للاستفسار عن أي عمليات سحب خاطئة.<br />\r\n<br />\r\n<b>لا ترد على هذا البريد الإلكتروني، فقد تم إرساله من عنوان 'لا يمكن الرد عليه'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 54,
                column: "Template",
                value: "\r\nعزيزي الزميل،<br />\r\n<br />\r\nتم الإبلاغ بشكل غير صحيح عن عودة العنصر '{{itemDescription}}' من النوع '{{itemTypeDescription}}' من قبل المستخدم '{{userEmail}}' إلى الـ IBK '{{cabinetName}}' في الموقع '{{positionAlias}}'<br />\r\n<br />\r\nتم تسجيل هذا الإجراء من خلال إنشاء سجل ملكية والعنصر '{{itemDescription}}' قد عاد إلى التداول. <br />\r\nمن المحتمل أن يكون المستخدم '{{userEmail}}' قد حصل بطريق الخطأ على العنصر '{{itemDescription}}' ومن ثم أبلغ عنه كمرتجع بشكل خاطئ.<br />\r\nلمنع المستخدم '{{userEmail}}' من الحصول على تسجيل خاطئ، يمكنك التحقيق في هذا الأمر. <br />\r\n<br />\r\nراجع تاريخ المستخدم للمستخدم '{{userEmail}}' لمعرفة ما إذا كان المستخدم قد سجل عناصر تحت ملكيته.<br />\r\nإذا كان لدى المستخدم '{{userEmail}}' عناصر مسجلة تحت ملكيته، يمكنك فحص السجلات التشغيلية والفنية لمعرفة الأحداث التي سبقت إشعار الإرجاع الخاطئ.<br />\r\nإذا بدا أن المستخدم '{{userEmail}}' لديه تسجيل خاطئ، يرجى الاتصال بالمستخدم '{{userEmail}}' للاستفسار عن أي عمليات سحب خاطئة.<br />\r\n<br />\r\n<b>لا ترد على هذا البريد الإلكتروني، فقد تم إرساله من عنوان 'لا يمكن الرد عليه'.</b><br />\r\n");

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
                keyValue: 56,
                column: "Template",
                value: "\r\nDear Colleague,<br />\r\n<br />\r\nIn cabinet '{{cabinetName}}' at position '{{positionAlias}}' ({{positionNumber}}), unknown content has been placed.<br />\r\n<br />\r\nTo resolve position '{{positionAlias}}' ({{positionNumber}}), you need to go through the correction flow.<br />\r\nYou do this by following these steps:<br />\r\n1. Log in to the cabinet<br />\r\n2. Press the remove tile<br />\r\n3. Press the exclamation mark at the top right<br />\r\n4. Select the position you want to resolve<br />\r\n5. Press release keycop(s)<br />\r\n<br />\r\nGo through the items and handle them.<br />\r\n<br />\r\n<b>Do not reply to this email, it was sent via a 'no-reply address'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 57,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nIBK '{{cabinetName}}' positie '{{positionAlias}}' ({{positionNumber}}) is defect.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 58,
                column: "Template",
                value: "\r\nDear Colleague,<br />\r\n<br />\r\nCabinet '{{cabinetName}}' position '{{positionAlias}}' ({{positionNumber}}) is defect.<br />\r\n<br />\r\n<b>Do not reply to this email, it was sent via a 'no-reply address'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 59,
                column: "Template",
                value: "\r\nBeste Collega,<br />\r\n<br />\r\nIn IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) ontbreekt {{itemDescription}}.<br />\r\n<br />\r\nOm alleen positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />\r\nDit doet u door middel van de volgende stappen:<br />\r\n1. Log in op de IBK<br />\r\n2. Druk op de tegel verwijderen<br />\r\n3. Druk op het uitroepteken rechtbovenin<br />\r\n4. Selecteer de positie die u wilt oplossen<br />\r\n5. Druk op vrijgeven keycop(s)<br />\r\n<br />\r\nOm {{itemDescription}} op te lossen dient u contact op te nemen met de persoon waarop deze geregistreerd staat.<br />\r\nLET OP! Deze registratie is een mogelijkheid niet een zekerheid!<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 60,
                column: "Template",
                value: "\r\nDear Colleague,<br />\r\n<br />\r\nIn cabinet '{{cabinetName}}' at position '{{positionAlias}}' ({{positionNumber}}), {{itemDescription}} is missing.<br />\r\n<br />\r\nTo resolve only position '{{positionAlias}}' ({{positionNumber}}), you need to follow the correction flow.<br />\r\nYou do this by following these steps:<br />\r\n1. Log in to the cabinet<br />\r\n2. Press the remove tile<br />\r\n3. Press the exclamation mark at the top right<br />\r\n4. Select the position you want to resolve<br />\r\n5. Press release keycop(s)<br />\r\n<br />\r\nTo resolve {{itemDescription}}, you need to contact the person it is registered to.<br />\r\nNOTE! This registration is a possibility, not a certainty!<br />\r\n<br />\r\n<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 61,
                column: "Template",
                value: "\r\nعزيزي الزميل،<br />\r\n<br />\r\nفي الخزانة '{{cabinetName}}' عند الموقع '{{positionAlias}}' ({{positionNumber}})، تم وضع محتوى غير معروف.<br />\r\n<br />\r\nلحل موقع '{{positionAlias}}' ({{positionNumber}})، تحتاج إلى المرور بعملية التصحيح.<br />\r\nيمكنك القيام بذلك باتباع هذه الخطوات:<br />\r\n1. قم بتسجيل الدخول إلى الخزانة<br />\r\n2. اضغط على إزالة البلاط<br />\r\n3. اضغط على علامة التعجب في أعلى اليمين<br />\r\n4. اختر الموقع الذي تريد حله<br />\r\n5. اضغط على زر إطلاق المفاتيح<br />\r\n<br />\r\nاذهب خلال العناصر وتعامل معها.<br />\r\n<br />\r\n<b>لا ترد على هذا البريد الإلكتروني، تم إرساله عبر 'عنوان لا يقبل الرد'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 62,
                column: "Template",
                value: "\r\nعزيزي الزميل،<br />\r\n<br />\r\nموقع الخزانة '{{cabinetName}}' '{{positionAlias}}' ({{positionNumber}}) به عيب.<br />\r\n<br />\r\n<b>لا ترد على هذا البريد الإلكتروني، تم إرساله عبر 'عنوان لا يقبل الرد'.</b><br />\r\n");

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 63,
                column: "Template",
                value: "\r\nعزيزي الزميل،<br />\r\n<br />\r\nفي الخزانة '{{cabinetName}}' عند الموقع '{{positionAlias}}' ({{positionNumber}})، {{itemDescription}} مفقود.<br />\r\n<br />\r\nلحل موقع '{{positionAlias}}' ({{positionNumber}}) فقط، تحتاج إلى اتباع عملية التصحيح.<br />\r\nيمكنك القيام بذلك باتباع هذه الخطوات:<br />\r\n1. قم بتسجيل الدخول إلى الخزانة<br />\r\n2. اضغط على إزالة البلاط<br />\r\n3. اضغط على علامة التعجب في أعلى اليمين<br />\r\n4. اختر الموقع الذي تريد حله<br />\r\n5. اضغط على زر إطلاق المفاتيح<br />\r\n<br />\r\nلحل {{itemDescription}}، تحتاج إلى الاتصال بالشخص المسجل لديه.<br />\r\nملاحظة! هذا التسجيل هو احتمال، وليس يقين!<br />\r\n<br />\r\n<b>لا ترد على هذا البريد الإلكتروني، تم إرساله من 'عنوان لا يقبل الرد'.</b><br />\r\n");
        }
    }
}
