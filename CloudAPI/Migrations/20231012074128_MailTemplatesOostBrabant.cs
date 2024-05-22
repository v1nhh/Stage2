﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class MailTemplatesOostBrabant : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 29,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Registreer je pas bij een Intelligente BeheerKast (IBK)", "\r\nBeste collega,<br />\r\n<br />\r\nAls jouw C2000-portofoon defect raakt, kun je die vanaf nu direct en 24x7 omruilen voor een werkend tijdelijk exemplaar bij een onbemande Intelligente BeheerKast (IBK). Registreer hiervoor zo snel mogelijk je pas. In deze mail kun je lezen hoe je dit kunt doen.<br />\r\n<br />\r\n<b>Nieuw: op tien locaties een IBK</b><br />\r\nEerder moest je voor een portofoon-reparatie een afspraak maken bij een LO-Servicebalie. <br />\r\nVerdeeld over jouw eenheid vind je nu tien onbemande IBK’s waar je direct en 24x7 terecht kunt om een defecte portofoon om te ruilen voor een werkend tijdelijk exemplaar. Als jouw portofoon gerepareerd is, krijg je hiervan automatisch een melding en kun je de tijdelijke portofoon bij dezelfde IBK weer omruilen voor jouw eigen portofoon.<br />\r\n<br />\r\n<b>Registreer je pas met onderstaande pincode</b><br />\r\nOm jezelf als IBK-gebruiker te registreren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Dit kun je vandaag al doen zodat je later, in geval van een portofoon-defect direct geholpen kunt worden.<br />\r\n<ol>\r\n<li>Ga naar een fysieke IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\r\n<li>Druk bij de IBK op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\r\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig <b>Personeelsnummer</b> in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\r\n<li>Voer in het veld ‘Vul pincode in’ de <b>pincode ‘{{pinCode}}’</b> in.</li>\r\n<li>Scan jouw politie-toegangspas bij de scanner onder het scherm (dit is dezelfde pas die jou toegang geeft tot de politiepanden).<br />\r\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\r\n</ol>\r\n<br />\r\n<b>Nu alleen voor portofoon met basisprogrammering</b><br />\r\nOp dit moment kunnen alleen portofoons met een basisprogrammering gerepareerd worden met behulp van de IBK’s. Voor portofoons met een andere programmering zoals bijvoorbeeld ‘Recherche’ of ‘Onderhandelaar’ kan het huidige reparatieproces met de LO-Servicebalie gebruikt worden. In de nabije toekomst zullen ook andere portofoons en items in de IBK’s geplaatst gaan worden, registreer daarom dus zo snel mogelijk jouw toegangspas.<br />\r\n<br />\r\nKlik <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hier</a> voor meer uitleg, hulp en FAQ. Heb je nog aanvullende vragen en/of opmerkingen, neem dan contact op met Functioneel Beheer IBK via\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n" });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 31,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nEr is voor jou een nieuwe pincode aangemaakt waarmee je een toegangspas kunt koppelen aan jouw IBK-account. Jouw nieuwe pincode is: <b>‘{{pinCode}}’</b><br />\r\n<ol>\r\n<li>Ga naar een fysieke IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\r\n<li>Druk bij de IBK op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\r\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig <b>Personeelsnummer</b> in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\r\n<li>Voer in het veld ‘Vul pincode in’ de nieuwe <b>pincode ‘{{pinCode}}’</b> in.</li>\r\n<li>Scan jouw politie-toegangspas bij de scanner onder het scherm (dit is dezelfde pas die jou toegang geeft tot de politiepanden).<br />\r\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\r\n</ol>\r\n<br />\r\nKlik <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hier</a> voor meer uitleg, hulp en FAQ. Heb je nog aanvullende vragen en/of opmerkingen, neem dan contact op met Functioneel Beheer IBK via <a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: 29,
                columns: new[] { "Subject", "Template" },
                values: new object[] { "Registreer je pas bij een Intelligente BeheerKast", "\r\nBeste collega,<br />\r\n<br />\r\nVanaf nu kun je je defecte C2000 portofoon direct en 24x7 omruilen voor een werkend tijdelijk exemplaar bij een onbemande Intelligente BeheerKast (IBK). Registreer zo snel mogelijk hiervoor je pas. In deze mail kun je lezen hoe je dit kunt doen.<br />\r\n<br />\r\n<b>Op tien locaties</b><br />\r\nEerder moest je voor een portofoon-reparatie een afspraak maken bij een LO-Servicebalie. <br />\r\nVerdeeld over de eenheid Den Haag vind je nu tien onbemande IBK’s waar je direct en 24x7 terecht kunt om een defecte portofoon om te ruilen voor een werkend tijdelijk exemplaar. Als jouw portofoon gerepareerd is, krijg je hiervan automatisch een melding en kun je de tijdelijke portofoon bij dezelfde IBK weer omruilen voor jouw eigen portofoon.<br />\r\n<br />\r\n<b>Registreer je pas met onderstaande pincode</b><br />\r\nOm jezelf als IBK-gebruiker te registeren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Dit kun je vandaag al doen zodat je later, in geval van een portofoon-defect direct geholpen kunt worden.<br />\r\n<ol>\r\n<li>Ga naar een IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\r\n<li>Druk op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\r\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig <b>personeelsnummer</b> in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\r\n<li>Voer in het veld ‘Vul pincode in’ de pincode <b>‘{{pinCode}}’</b> in.</li>\r\n<li>Scan jouw politie-toegangspas bij de kaartlezer onder het scherm (dit is dezelfde pas die toegang geeft tot de politiepanden/locaties).<br />\r\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\r\n</ol>\r\n<br />\r\n<b>Nu alleen voor portofoon met basisprogrammering</b><br />\r\nOp dit moment kunnen alleen portofoons met een basisprogrammering gerepareerd worden met behulp van de IBK’s. Voor portofoons met een andere programmering zoals bijvoorbeeld ‘Recherche’ of ‘Onderhandelaar’ kan het huidige reparatieproces met de LO-Servicebalie gebruikt worden. In de nabije toekomst zullen ook andere portofoons en items in de IBK’s geplaatst gaan worden.<br />\r\n<br />\r\nZie voor meer uitleg, <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hulp en FAQ</a>.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n" });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 31,
                column: "Template",
                value: "\r\nBeste collega,<br />\r\n<br />\r\nEr is voor jou een nieuwe pincode aangemaakt waarmee je een toegangspas kunt koppelen aan jouw IBK-account. Jouw nieuwe pincode is: {{pinCode}}<br />\r\n<ol>\r\n<li>Ga naar een IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>\r\n<li>Druk op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>\r\n<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig personeelsnummer in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>\r\n<li>Voer in het veld ‘Vul pincode in’ de nieuwe pincode ‘{{pinCode}}’ in.</li>\r\n<li>Scan jouw politie-toegangspas bij de kaartlezer onder het scherm (dit is dezelfde pas die toegang geeft tot de politiepanden/locaties).<br />\r\nHet groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>\r\n</ol>\r\n<br />\r\nZie voor meer uitleg, <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hulp en FAQ</a>.<br />\r\n<br />\r\nHeb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />\r\n<a href=\"mailto:intelligente-beheerkasten.ict@politie.nl\">intelligente-beheerkasten.ict@politie.nl</a><br />\r\n<br />\r\nMet vriendelijke groet,<br />\r\nFunctioneel Beheer Intelligente BeheerKasten (IBK)<br />\r\n<br />\r\n\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n");
        }
    }
}