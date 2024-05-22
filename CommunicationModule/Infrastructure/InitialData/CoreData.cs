using CommunicationModule.ApplicationCore.Entities;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core.Constants;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace CommunicationModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class CoreData : IDataInserts
    {
        public string Environment => DeploymentEnvironment.Core;

        public void InsertData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailTemplate>()
               .HasData(
                    new MailTemplate()
                    {
                        ID = 1,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.WelcomeWebAndCabinetLogin.GetName(),
                        Subject = "Welkom bij de IBK-Beheerapplicatie en als IBK-gebruiker",
                        // "IBK Fase 1a Mail welkomst mail beheer applicatie en IBK registratie v0.1.docx" 
                        Template = @"
Beste collega,<br />
<br />
<b>Beheerapplicatie</b><br />
Er is voor jou een account aangemaakt voor de applicatie waarmee de Intelligente BeheerKasten (IBK’s) beheerd kunnen worden. Door op de link te klikken kom je bij het inlogscherm van de IBK-Beheerapplicatie:<br />
<a href='{{link}}' target='_blank' >IBK beheer applicatie</a> <br />
<br />
Jouw inloggegevens:<br />
<br />
Gebruikersnaam: Dit is je emailadres met NP+personeelsnummer (bijvoorbeeld NP123456@politie.nl). Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.<br />
Tijdelijk wachtwoord: {{password}}<br />
<br />
Wijzig direct na het inloggen jouw wachtwoord! Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine letter en 1 cijfer.<br />
<br />
<b>Registreer toegangspas bij een IBK</b><br />
Om jezelf als IBK-gebruiker te registeren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Doe dit zo snel mogelijk om collega’s met defecte portofoons te kunnen ondersteunen.<br />
<ol>
<li>Ga naar een IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>
<li>Druk op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>
<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig personeelsnummer in.</li>
<li>Voer in het veld ‘Vul pincode in’ de pincode ‘{{pinCode}}’ in.</li>
<li>Scan jouw politie-toegangspas bij de kaartlezer onder het scherm (dit is dezelfde pas die toegang geeft tot de politiepanden/locaties).<br />
Het groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu jouw collega’s met een defecte portofoon kunt ondersteunen bij een willekeurige IBK van jouw eenheid.</li>
</ol>
<br />

Zie voor meer uitleg, <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hulp en FAQ</a>.<br />
<br />
Heb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />
<a href=""mailto:intelligente-beheerkasten.ict@politie.nl"">intelligente-beheerkasten.ict@politie.nl</a><br />
<br />
Met vriendelijke groet,<br />
Functioneel Beheer Intelligente BeheerKasten (IBK)<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 2, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebAndCabinetLogin.GetName(), Subject = "Welcome to CTAM - your login data", Template = @"Dear {{name}},<br /><br />Welcome to CTAM!<br /><br />Your login details<br />Username: {{email}}<br />Temporary password: {{password}}<br />Login code: {{loginCode}}<br />Pin code: {{pinCode}}" },
                    new MailTemplate()
                    {
                        ID = 3,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.WelcomeWebLogin.GetName(),
                        Subject = "Welkom bij de IBK-Beheerapplicatie",
                        // "IBK Fase 1a Mail welkoms mail beheer applicatie v0.3.docx" 
                        Template = @"
Beste collega,<br />
<br />
Er is voor jou een account aangemaakt voor de applicatie waarmee de Intelligente BeheerKasten (IBK’s) beheerd kunnen worden. Door op de link te klikken kom je bij het inlogscherm van de IBK-Beheerapplicatie:<br />
<a href='{{link}}' target='_blank' >IBK beheer applicatie</a> <br />
<br />
Jouw inloggegevens:<br />
<br />
Gebruikersnaam: Dit is je emailadres met NP+personeelsnummer (bijvoorbeeld NP123456@politie.nl). Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.<br />
Tijdelijk wachtwoord: {{password}}<br />
<br />
Wijzig direct na het inloggen jouw wachtwoord! Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine letter en 1 cijfer.<br />
<br />
Zie voor meer uitleg, <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hulp en FAQ</a>.<br />
<br />
Heb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />
<a href=""mailto:intelligente-beheerkasten.ict@politie.nl"">intelligente-beheerkasten.ict@politie.nl</a><br />
<br />
Met vriendelijke groet,<br />
Functioneel Beheer Intelligente BeheerKasten (IBK)<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 4, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebLogin.GetName(), Subject = "Welcome to CTAM - your login data", Template = @"Dear {{name}},<br /><br />Welcome to CTAM!<br /><br />Your login details<br />Username: {{email}}<br />Temporary password: {{password}}" },
                    new MailTemplate()
                    {
                        ID = 5,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.TemporaryPassword.GetName(),
                        Subject = "Reset wachtwoord voor IBK-Beheerapplicatie",
                        // "IBK Fase 1a Mail Wachtwoord Reset v1.1 Nog niet in Prod(1).docx"
                        Template = @"
Beste collega,<br />
<br />
Je hebt een wachtwoord-reset aangevraagd, jouw tijdelijke wachtwoord is <b>{{password}}</b><br />
<br />
Ga naar de IBK-Beheerapplicatie en verander direct jouw tijdelijke wachtwoord. Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />
<br />
Log binnen de IBK-Beheerapplicatie altijd in met jouw NP-emailadres (bijvoorbeeld NP250537@politie.nl)<br />
<br />
Heb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />
<a href=""mailto:intelligente-beheerkasten.ict@politie.nl"">intelligente-beheerkasten.ict@politie.nl</a><br />
<br />
Met vriendelijke groet,<br />
Functioneel Beheer Intelligente BeheerKasten (IBK)<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 6, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.TemporaryPassword.GetName(), Subject = "Reset password", Template = @"Dear {{name}},<br /><br />Your temporary password is {{password}}<br />Change it as soon as you login!" },
                    new MailTemplate()
                    {
                        ID = 7,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.PasswordChanged.GetName(),
                        Subject = "Nieuw wachtwoord voor IBK-Beheerapplicatie",
                        // "IBK Fase 1a Mail Wachtwoord Nieuw v0.3.docx" 
                        Template = @"
Beste collega,<br />
<br />
Jouw wachtwoordwijziging voor de IBK-Beheerapplicatie is succesvol doorgevoerd! <br />
<a href='{{link}}' target='_blank'>IBK beheer applicatie</a> <br />
<br />
Heb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />
<a href=""mailto:intelligente-beheerkasten.ict@politie.nl"">intelligente-beheerkasten.ict@politie.nl</a><br />
<br />
Met vriendelijke groet,<br />
Functioneel Beheer Intelligente BeheerKasten (IBK)<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 8, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.PasswordChanged.GetName(), Subject = "New password", Template = @"Dear {{name}},<br /><br />Your password has been changed successfully!" },
                    new MailTemplate()
                    {
                        ID = 9,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.ForgotPassword.GetName(),
                        Subject = "Wachtwoord van de IBK-Beheerapplicatie vergeten",
                        // "IBK Fase 1a Mail Wachtwoord vergeten v0.3.docx"
                        Template = @"
Beste collega,<br />
<br />
Je ontvangt deze e-mail omdat je onlangs op de knop 'Wachtwoord vergeten' hebt gedrukt in de IBK-Beheerapplicatie.<br />
<br />
Klik op de onderstaande link om jouw wachtwoord te wijzigen. Een wachtwoord moet minstens uit {{minimalLength}} karakters bestaan waarvan minimaal 1 hoofdletter, 1 kleine-letter en 1 cijfer.<br />
<br />
<a href='{{link}}' target='_blank'>Wachtwoord wijzigen</a><br />
<br />
Heb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />
<a href=""mailto:intelligente-beheerkasten.ict@politie.nl"">intelligente-beheerkasten.ict@politie.nl</a><br />
<br />
Met vriendelijke groet,<br />
Functioneel Beheer Intelligente BeheerKasten (IBK)<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 10, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.ForgotPassword.GetName(), Subject = "Forgot password", Template = @"Dear {{name}},<br /><br />Click the link below to change your password.<br /><br /><a href='{{link}}' target='_blank'>Change password</a><br /><br />You’re receiving this email because you recently pressed 'Forgot password' button on website of Nauta Connect. If you did not initiate this change, please contact your administrator immediately." },
                    new MailTemplate()
                    {
                        ID = 11,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.UserModified.GetName(),
                        Subject = "Wijziging gegevens binnen de IBK-Beheerapplicatie",
                        // "IBK Fase 1a Mail Profielwijziging v0.3.docx"
                        Template = @"
Beste collega,<br />
<br />
Binnen de IBK-Beheerapplicatie zijn jouw gegevens aangepast, de wijziging is:<br />
<br />
<table>{{changes}}</table><br />
<br />
Heb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />
<a href=""mailto:intelligente-beheerkasten.ict@politie.nl"">intelligente-beheerkasten.ict@politie.nl</a><br />
<br />
Met vriendelijke groet,<br />
Functioneel Beheer Intelligente BeheerKasten (IBK)<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 12, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.UserModified.GetName(), Subject = "Your changes", Template = @"Dear {{name}},<br /><br />Review your changes below:<br /><br /><table>{{changes}}</table>" },
                    new MailTemplate()
                    {
                        ID = 13,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.UserDeleted.GetName(),
                        Subject = "IBK-account is uit de IBK-Beheerapplicatie verwijderd",
                        // "IBK Fase 1a Mail Account verwijderd v0.3.docx" 
                        Template = @"
Beste collega,<br />
<br />
Jouw IBK-account is verwijderd door de Functioneel Beheerder. Dit houdt in dat je vanaf nu niet meer van een IBK gebruik kunt maken.<br />
<br />
Heb je hier vragen over, neem dan contact op met Functioneel Beheer IBK.<br />
<a href=""mailto:intelligente-beheerkasten.ict@politie.nl"">intelligente-beheerkasten.ict@politie.nl</a><br />
<br />
Met vriendelijke groet,<br />
Functioneel Beheer Intelligente BeheerKasten (IBK)<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 14, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.UserDeleted.GetName(), Subject = "Your CTAM account is deleted", Template = @"Dear {{name}},<br /><br />Your CTAM account is deleted by the administrator" },
                    new MailTemplate()
                    {
                        ID = 15,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.StockBelowMinimum.GetName(),
                        Subject = "IBK voorraad '{{itemTypeDescription}}' op '{{cabinetLocationDescr}}' is onder minimum voorraadniveau",
                        // "IBK Fase 1a Mail Aantal items beneden het minimum v0.1 .docx" 
                        Template = @"
Beste Collega,<br />
<br />
De voorraad van item type '{{itemTypeDescription}}' in IBK '{{cabinetName}}' op locatie '{{cabinetLocationDescr}}' is minder ({{actualStock}}) dan de opgegeven minimale voorraad ({{minimalStock}}).<br />
<br />
Wil je er zorg voor dragen dat de voorraad van '{{itemTypeDescription}}' op locatie '{{cabinetLocationDescr}}’ op zijn minst tot het minimale voorraadniveau ({{minimalStock}}) wordt aangevuld.<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 16, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.StockBelowMinimum.GetName(), Subject = "Cabinet stock below minimal", Template = @"Dear cabinet administrator,<br /><br />The cabinet stock of {{itemTypeDescription}} in cabinet '{{cabinetNumber}}, {{cabinetName}}' at location '{{cabinetLocationDescr}}' is below ({{actualStock}}) the minimal stock ({{minimalStock}})." },
                    new MailTemplate()
                    {
                        ID = 17,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.StockAtMinimalStockLevel.GetName(),
                        Subject = "IBK voorraad '{{itemTypeDescription}}' op '{{cabinetLocationDescr}}' is weer op niveau",
                        // "IBK Fase 1a Mail Aantal items is op het minimum niveau v0.1 .docx" 
                        Template = @"
Beste Collega,<br />
<br />
De voorraad van item type '{{itemTypeDescription}}' in IBK '{{cabinetName}}' op locatie '{{cabinetLocationDescr}}' is weer op of boven het opgegeven minimale voorraadniveau ({{minimalStock}}).<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 18, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.StockAtMinimalStockLevel.GetName(), Subject = "Cabinet stock returned at minimal level", Template = @"Dear cabinet administrator,<br /><br />The cabinet stock of {{itemTypeDescription}} in cabinet '{{cabinetNumber}}, {{cabinetName}}' at location '{{cabinetLocationDescr}}' is has reached the minimal stock ({{minimalStock}} level)." },
                    new MailTemplate()
                    {
                        ID = 19,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.ItemStatusChangedToDefect.GetName(),
                        Subject = "Item defect gemeld",
                        Template = @"
Beste Collega,<br />
<br />
Item '{{itemDescription}}' van type '{{itemTypeDescription}}' is defect gemeld door gebruiker '{{userName}}' met errorcode: '{{errorCodeDescription}}'. <br />
<br />
U kunt het ophalen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'.<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 20, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.ItemStatusChangedToDefect.GetName(), Subject = "Item reported defect", Template = @"Dear cabinet administrator, <br /><br />Item '{{itemDescription}}' of type '{{itemTypeDescription}}' is reported defect by user '{{userName}}' with errorcode:  '{{errorCodeDescription}}'. <br /><br />You can pick it up at cabinet '{{cabinetName}}' on position '{{positionAlias}}'." },
                    new MailTemplate()
                    {
                        ID = 21,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.PersonalItemStatusChangedToDefect.GetName(),
                        Subject = "IBK: verstoring aangemeld voor {{putItemTypeDescription}} op locatie {{cabinetLocationDescr}} met de foutmelding {{errorCode}}",
                        // "IBK Fase 1a Mail Een Item is defect v0.7.docx" 
                        Template = @"
Beste Collega,<br />
<br />
Item <i>{{putItemDescription}}</i> met CI-nummer <i>'{{putItemExternalReferenceID}}'</i> van het type <i>{{putItemTypeDescription}}</i> is op <i>{{actionDT}}</i> defect gemeld door gebruiker <i>{{userName}}</i> met errorcode: <i>{{errorCode}} {{errorCodeDescription}}</i>. 
Ter reparatie kan dit item opgehaald worden bij IBK <i>{{cabinetName}}</i> op locatie <i>{{cabinetLocationDescr}} {{cabinetDescription}}</i>.<br />
<br />
Gebruiker <i>{{userName}}</i> heeft nu tijdelijk vervangend item <i>{{takeItemDescription}}</i> met CI-nummer <i>'{{takeItemExternalReferenceID}}'</i> van het type <i>{{takeItemTypeDescription}}</i> in gebruik.<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
            new MailTemplate() { ID = 22, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.PersonalItemStatusChangedToDefect.GetName(), Subject = "Defect reported {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}", Template = @"Item {{putItemDescription}} reported defect on {{actionDT}} by {{userName}} with error: {{errorCodeDescription}}. Cabinet {{cabinetName}} in locker {{positionAlias}} on location {{cabinetLocationDescr}}. <br /><br />User {{userName}} now has replacement item {{takeItemDescription}}." },
            new MailTemplate() { ID = 23, IsActive = false, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.PersonalItemStatusChangedToRepaired.GetName(), Subject = "Uw persoonlijke item is gerepareerd", Template = @"Beste {{userName}}, <br /><br />Persoonlijke item '{{itemDescription}}' van type '{{itemTypeDescription}}' is gerepareerd gemeld. <br /><br />U kunt het terug omruilen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'." },
            new MailTemplate() { ID = 24, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.PersonalItemStatusChangedToRepaired.GetName(), Subject = "Your personal item is repaired", Template = @"Dear {{userName}}, <br /><br />Personal item '{{itemDescription}}' of type '{{itemTypeDescription}}' is reported repaired. <br /><br />You can swap it back at cabinet '{{cabinetName}}' on position '{{positionAlias}}'." },
                    new MailTemplate() { ID = 25, IsActive = false, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.PersonalItemStatusChangedToReplaced.GetName(), Subject = "Uw persoonlijke item is vervangen", Template = @"Beste {{userName}}, <br /><br />Persoonlijke item '{{itemDescription}}' van type '{{itemTypeDescription}}' is vervangen gemeld. <br /><br />U kunt het ophalen aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}'." },
                    new MailTemplate() { ID = 26, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.PersonalItemStatusChangedToReplaced.GetName(), Subject = "Your personal item is replaced", Template = @"Dear {{userName}}, <br /><br />Personal item '{{itemDescription}}' of type '{{itemTypeDescription}}' is reported replaced. <br /><br />You can pick it up at cabinet '{{cabinetName}}' on position '{{positionAlias}}'." },
                    new MailTemplate()
                    {
                        ID = 27,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.PersonalItemStatusChangedToSwappedBack.GetName(),
                        Subject = "Agent heeft tijdelijk item bij een IBK weer ingeruild voor zijn eigen item",
                        // "IBK Fase 1a Mail Agent heeft Item weer in bezit v0.1.rtf" 
                        Template = @"
Beste Collega,<br />
<br />
Tijdelijk Item <i>{{putItemDescription}}</i> met nummer <i>'{{putItemExternalReferenceID}}'</i> van het type <i>{{putItemTypeDescription}}</i> is op <i>{{actionDT}}</i> door gebruiker <i>{{userName}}</i> weer ingeleverd en moet daarom in CMDB ontkoppeld worden van de gebruiker en de status ‘In stock’ krijgen. Dit tijdelijke item heeft de defectmelding: '{{errorCodeDescription}}'.<br />
<br />
Gebruiker <i>{{userName}}</i> is sinds <i>{{actionDT}}</i> in bezit van item <i>{{takeItemDescription}}</i> met nummer <i>'{{takeItemExternalReferenceID}}'</i> van het type <i>{{takeItemTypeDescription}}</i>; in CMDB moet daarom item <i>{{takeItemDescription}}</i> met nummer <i>'{{takeItemExternalReferenceID}}'</i> gekoppeld worden aan gebruiker <i>{{userName}}</i> en de status ‘in use’ krijgen.<br />
<br />
Graag na de CMDB-wijzigingen de status van het incident van item <i>{{takeItemDescription}}</i> met nummer <i>'{{takeItemExternalReferenceID}}'</i> op ‘Resolved’ zetten.<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 28, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.PersonalItemStatusChangedToSwappedBack.GetName(), Subject = "Temporal item {{putItemTypeDescription}} returned {{cabinetLocationDescr}} {{errorCodeDescription}}", Template = @"User swapped back temporary item. <br /><br />Temporal item <span style='font-style:italic'>'{{putItemDescription}}'</span> with <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> of type <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> is swapped back at <span style='font-style:italic'>'{{actionDT}}'</span> by user <span style='font-style:italic'>'{{userName}}'</span> and should be disconnected from the user and get status ‘In stock’. This temporal item has errorcode: <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br /><br />User <span style='font-style:italic'>'{{userName}}'</span> is since <span style='font-style:italic'>'{{actionDT}}'</span> in possession of item <span style='font-style:italic'>'{{takeItemDescription}}'</span> with <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> of type <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>; in CMDB the item <span style='font-style:italic'>'{{takeItemDescription}}'</span> with <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> should be disconnected from user <span style='font-style:italic'>'{{userName}}'</span> and get status ‘in use’. <br /><br />Please after CMDB-changed put the status of the incident of item <span style='font-style:italic'>'{{takeItemDescription}}'</span> with <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> to ‘Resolved’." },
                    new MailTemplate()
                    {
                        ID = 29,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.WelcomeCabinetLogin.GetName(),
                        Subject = "Registreer je pas bij een Intelligente BeheerKast (IBK)",
                        // "IBK Fase 1a Mail Pas Registeren v2.0 Nog niet in Prod(1).docx"
                        Template = @"
Beste collega,<br />
<br />
Als jouw C2000-portofoon defect raakt, kun je die vanaf nu direct en 24x7 omruilen voor een werkend tijdelijk exemplaar bij een onbemande Intelligente BeheerKast (IBK). Registreer hiervoor zo snel mogelijk je pas. In deze mail kun je lezen hoe je dit kunt doen.<br />
<br />
<b>Nieuw: op tien locaties een IBK</b><br />
Eerder moest je voor een portofoon-reparatie een afspraak maken bij een LO-Servicebalie. <br />
Verdeeld over jouw eenheid vind je nu tien onbemande IBK’s waar je direct en 24x7 terecht kunt om een defecte portofoon om te ruilen voor een werkend tijdelijk exemplaar. Als jouw portofoon gerepareerd is, krijg je hiervan automatisch een melding en kun je de tijdelijke portofoon bij dezelfde IBK weer omruilen voor jouw eigen portofoon.<br />
<br />
<b>Registreer je pas met onderstaande pincode</b><br />
Om jezelf als IBK-gebruiker te registreren, moet jouw pas eenmalig gekoppeld worden aan het IBK-systeem. Dit kun je vandaag al doen zodat je later, in geval van een portofoon-defect direct geholpen kunt worden.<br />
<ol>
<li>Ga naar een fysieke IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>
<li>Druk bij de IBK op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>
<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig <b>Personeelsnummer</b> in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>
<li>Voer in het veld ‘Vul pincode in’ de <b>pincode ‘{{pinCode}}’</b> in.</li>
<li>Scan jouw politie-toegangspas bij de scanner onder het scherm (dit is dezelfde pas die jou toegang geeft tot de politiepanden).<br />
Het groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>
</ol>
<br />
<b>Nu alleen voor portofoon met basisprogrammering</b><br />
Op dit moment kunnen alleen portofoons met een basisprogrammering gerepareerd worden met behulp van de IBK’s. Voor portofoons met een andere programmering zoals bijvoorbeeld ‘Recherche’ of ‘Onderhandelaar’ kan het huidige reparatieproces met de LO-Servicebalie gebruikt worden. In de nabije toekomst zullen ook andere portofoons en items in de IBK’s geplaatst gaan worden, registreer daarom dus zo snel mogelijk jouw toegangspas.<br />
<br />
Klik <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hier</a> voor meer uitleg, hulp en FAQ. Heb je nog aanvullende vragen en/of opmerkingen, neem dan contact op met Functioneel Beheer IBK via
<a href=""mailto:intelligente-beheerkasten.ict@politie.nl"">intelligente-beheerkasten.ict@politie.nl</a><br />
<br />
Met vriendelijke groet,<br />
Functioneel Beheer Intelligente BeheerKasten (IBK)<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 30, IsActive = false, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeCabinetLogin.GetName(), Subject = "Your pincode for cabinet login", Template = @"Dear {{userName}},<br /><br />Your pincode is {{pinCode}}." },
                    new MailTemplate()
                    {
                        ID = 31,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.PincodeChanged.GetName(),
                        Subject = "Nieuwe pincode voor Intelligente BeheerKasten (IBK)",
                        // "IBK Fase 1a Mail Pincode Reset v2.0 Nog niet in Prod(1).docx" 
                        Template = @"
Beste collega,<br />
<br />
Er is voor jou een nieuwe pincode aangemaakt waarmee je een toegangspas kunt koppelen aan jouw IBK-account. Jouw nieuwe pincode is: <b>‘{{pinCode}}’</b><br />
<ol>
<li>Ga naar een fysieke IBK (klik <a href='https://intranet.politie.local/algemenedocumenten/1206/ibk-locaties.html'>hier</a> voor een lijst met IBK-locaties binnen jouw Eenheid).</li>
<li>Druk bij de IBK op de knop ‘Nieuwe pas koppelen’ op het IBK-scherm.</li>
<li>Voer in het veld ‘Vul usercode in’ jouw 6-cijferig <b>Personeelsnummer</b> in. Jouw personeelsnummer kun je vinden door jezelf op te zoeken in <a href='https://blue.politie.local/blueconnect/overview'>BlueConnect</a>.</li>
<li>Voer in het veld ‘Vul pincode in’ de nieuwe <b>pincode ‘{{pinCode}}’</b> in.</li>
<li>Scan jouw politie-toegangspas bij de scanner onder het scherm (dit is dezelfde pas die jou toegang geeft tot de politiepanden).<br />
Het groene scherm met ‘pas koppelen geslaagd’ geeft vervolgens aan dat alles goed gegaan is en dat je vanaf nu met een defecte portofoon altijd snel geholpen kunt worden bij een willekeurige IBK van jouw eenheid.</li>
</ol>
<br />
Klik <a href='https://intranet.politie.local/categorie/ondersteuning/onderwerpen/i/intelligente-beheerkasten-ibk/overzicht/vraag-en-antwoord'>hier</a> voor meer uitleg, hulp en FAQ. Heb je nog aanvullende vragen en/of opmerkingen, neem dan contact op met Functioneel Beheer IBK via <a href=""mailto:intelligente-beheerkasten.ict@politie.nl"">intelligente-beheerkasten.ict@politie.nl</a><br />
<br />
Met vriendelijke groet,<br />
Functioneel Beheer Intelligente BeheerKasten (IBK)<br />
<br />

<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate() { ID = 32, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.PincodeChanged.GetName(), Subject = "Reset pincode", Template = @"Dear {{name}},<br /><br />Your new pincode is {{pinCode}}<br />" },
                    new MailTemplate() { ID = 33, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebAndCabinetLogin.GetName(), Subject = "مرحبًا بك في CTAM - بيانات تسجيل الدخول الخاصة بك", Template = "عزيزي {{name}}،<br /><br />مرحبًا بك في CTAM!<br /><br />تفاصيل تسجيل الدخول الخاصة بك<br />اسم المستخدم: {{email}}<br />كلمة المرور المؤقتة: {{password}}<br />رمز الدخول: {{loginCode}}<br />الرقم السري: {{pinCode}}" },
                    new MailTemplate() { ID = 34, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebLogin.GetName(), Subject = "مرحبًا بك في CTAM - بيانات تسجيل الدخول الخاصة بك", Template = "عزيزي {{name}}،<br /><br />مرحبًا بك في CTAM!<br /><br />تفاصيل تسجيل الدخول الخاصة بك<br />اسم المستخدم: {{email}}<br />كلمة المرور المؤقتة: {{password}}" },
                    new MailTemplate() { ID = 35, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.TemporaryPassword.GetName(), Subject = "إعادة تعيين كلمة المرور", Template = "عزيزي {{name}}،<br /><br />كلمة المرور المؤقتة الخاصة بك هي {{password}}<br />قم بتغييرها فور تسجيل الدخول!" },
                    new MailTemplate() { ID = 36, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.PasswordChanged.GetName(), Subject = "كلمة مرور جديدة", Template = "عزيزي {{name}}،<br /><br />تم تغيير كلمة المرور الخاصة بك بنجاح!" },
                    new MailTemplate() { ID = 37, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.ForgotPassword.GetName(), Subject = "نسيت كلمة المرور", Template = "عزيزي {{name}}،<br /><br />اضغط على الرابط أدناه لتغيير كلمة المرور الخاصة بك.<br /><br /><a href='{{link}}' target='_blank'>تغيير كلمة المرور</a><br /><br />أنت تتلقى هذا البريد الإلكتروني لأنك ضغطت مؤخرًا على زر 'نسيت كلمة المرور' على موقع Nauta Connect. إذا لم تكن قد بدأت هذا التغيير، يرجى الاتصال بالمسؤول الخاص بك فورًا." },
                    new MailTemplate() { ID = 38, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.UserModified.GetName(), Subject = "تغييراتك", Template = "عزيزي {{name}}،<br /><br />راجع التغييرات الخاصة بك أدناه:<br /><br /><table>{{changes}}</table>" },
                    new MailTemplate() { ID = 39, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.UserDeleted.GetName(), Subject = "تم حذف حسابك في CTAM", Template = "عزيزي {{name}}،<br /><br />تم حذف حسابك في CTAM من قبل المسؤول" },
                    new MailTemplate() { ID = 40, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.StockBelowMinimum.GetName(), Subject = "مخزون الخزانة أقل من الحد الأدنى", Template = "عزيز مدير الخزانة،<br /><br />مخزون {{itemTypeDescription}} في الخزانة '{{cabinetNumber}}, {{cabinetName}}' في الموقع '{{cabinetLocationDescr}}' أقل ({{actualStock}}) من الحد الأدنى للمخزون ({{minimalStock}})." },
                    new MailTemplate() { ID = 41, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.StockAtMinimalStockLevel.GetName(), Subject = "مخزون الخزانة عاد إلى المستوى الأدنى", Template = "عزيز مدير الخزانة،<br /><br />مخزون {{itemTypeDescription}} في الخزانة '{{cabinetNumber}}, {{cabinetName}}' في الموقع '{{cabinetLocationDescr}}' قد بلغ مستوى الحد الأدنى للمخزون ({{minimalStock}})." },
                    new MailTemplate() { ID = 42, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.ItemStatusChangedToDefect.GetName(), Subject = "تم الإبلاغ عن عيب في العنصر", Template = "عزيز مدير الخزانة، <br /><br />تم الإبلاغ عن عطل في العنصر '{{itemDescription}}' من نوع '{{itemTypeDescription}}' من قبل المستخدم '{{userName}}' مع رمز الخطأ: '{{errorCodeDescription}}'. <br /><br />يمكنك استلامه من الخزانة '{{cabinetName}}' في الموضع '{{positionAlias}}'." },
                    new MailTemplate() { ID = 43, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.PersonalItemStatusChangedToDefect.GetName(), Subject = "تم الإبلاغ عن عيب {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}", Template = "تم الإبلاغ عن عيب في العنصر {{putItemDescription}} بتاريخ {{actionDT}} من قبل {{userName}} مع خطأ: {{errorCodeDescription}}. الخزانة {{cabinetName}} في خزانة {{positionAlias}} بالموقع {{cabinetLocationDescr}}. <br /><br />المستخدم {{userName}} لديه الآن عنصر بديل {{takeItemDescription}}." },
                    new MailTemplate() { ID = 44, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.PersonalItemStatusChangedToRepaired.GetName(), Subject = "تم إصلاح العنصر الشخصي الخاص بك", Template = "عزيز {{userName}}, <br /><br />تم الإبلاغ عن إصلاح العنصر الشخصي '{{itemDescription}}' من نوع '{{itemTypeDescription}}'. <br /><br />يمكنك استبداله مرة أخرى في الخزانة '{{cabinetName}}' على الموضع '{{positionAlias}}'." },
                    new MailTemplate() { ID = 45, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.PersonalItemStatusChangedToReplaced.GetName(), Subject = "تم استبدال العنصر الشخصي الخاص بك", Template = "عزيز {{userName}}, <br /><br />تم الإبلاغ عن استبدال العنصر الشخصي '{{itemDescription}}' من نوع '{{itemTypeDescription}}'. <br /><br />يمكنك استلامه في الخزانة '{{cabinetName}}' على الموضع '{{positionAlias}}'." },
                    new MailTemplate() { ID = 46, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.PersonalItemStatusChangedToSwappedBack.GetName(), Subject = "تم إرجاع العنصر المؤقت {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}", Template = "قام المستخدم بإعادة تبديل العنصر المؤقت. <br /><br />تم تبديل العنصر المؤقت <span style='font-style:italic'>'{{putItemDescription}}'</span> بمرجع خارجي <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> من نوع <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> مرة أخرى في <span style='font-style:italic'>'{{actionDT}}'</span> بواسطة المستخدم <span style='font-style:italic'>'{{userName}}'</span> ويجب فصله عن المستخدم وتحديث حالته إلى ‘متوفر’. هذا العنصر المؤقت له رمز خطأ: <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br /><br />المستخدم <span style='font-style:italic'>'{{userName}}'</span> يمتلك منذ <span style='font-style:italic'>'{{actionDT}}'</span> العنصر <span style='font-style:italic'>'{{takeItemDescription}}'</span> بمرجع خارجي <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> من نوع <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>؛ في CMDB يجب فصل العنصر <span style='font-style:italic'>'{{takeItemDescription}}'</span> بمرجع خارجي <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> عن المستخدم <span style='font-style:italic'>'{{userName}}'</span> وتحديث حالته إلى ‘مستخدم’. <br /><br />يرجى بعد تغيير CMDB تحديث حالة الحادثة للعنصر <span style='font-style:italic'>'{{takeItemDescription}}'</span> بمرجع خارجي <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> إلى ‘تم الحل’." },
                    new MailTemplate() { ID = 47, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeCabinetLogin.GetName(), Subject = "رقمك السري لتسجيل الدخول في الخزانة", Template = "عزيز {{userName}},<br /><br />رقمك السري هو {{pinCode}}." },
                    new MailTemplate() { ID = 48, LanguageCode = Language.Arabic.GetLanguageCode(), Name = DefaultEmailTemplate.PincodeChanged.GetName(), Subject = "إعادة تعيين الرقم السري", Template = "عزيز {{name}},<br /><br />رقمك السري الجديد هو {{pinCode}}<br />" },
                    new MailTemplate()
                    {
                        ID = 49,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.IncorrectReturnClosedUIP.GetName(),
                        Subject = "Item incorrect teruggebracht afgesloten record",
                        Template = @"
Beste Collega,<br />
<br />
Item '{{itemDescription}}' van type '{{itemTypeDescription}}' is incorrect terug gemeld door gebruiker '{{userEmail}}' aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}' <br />
<br />
Deze actie is geregistreerd doormiddel van het afsluiten van een bezitsrecord en item '{{itemDescription}}' is weer in omloop. <br />
Het is mogelijk dat gebruiker '{{userEmail}}' item '{{itemDescription}}' incorrect uitgeworpen heeft gekregen en hierna als incorrect terug heeft gemeld.<br />
Om te voorkomen dat gebruiker '{{userEmail}}' een onterechte registratie heeft kunt u dit onderzoeken. <br />
<br />
Bekijk in de gebruikers geschiedenis van gebruiker '{{userEmail}}' of de gebruiker items in bezit geregistreerd heeft.<br />
Als gebruiker '{{userEmail}}' items in bezit geregistreerd heeft kunt u in de operationele en technische logs bekijken welke handelingen vooraf zijn gegaan aan de incorrect terug melding.<br />
Lijkt het erop dat gebruiker '{{userEmail}}' een onterechte registratie heeft, neem dan contact op met gebruiker '{{userEmail}}' om te vragen naar eventuele incorrecte uitnamen.<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 50,
                        IsActive = false,
                        LanguageCode = Language.EnglishUS.GetLanguageCode(),
                        Name = DefaultEmailTemplate.IncorrectReturnClosedUIP.GetName(),
                        Subject = "Item incorrect returned closed record",
                        Template = @"
Dear Colleague,<br />
<br />
Item '{{itemDescription}}' of type '{{itemTypeDescription}}' has been incorrectly reported returned by user '{{userEmail}}' to the IBK '{{cabinetName}}' at position '{{positionAlias}}'<br />
<br />
This action has been registrated by closing a possession record and item '{{itemDescription}}' is back in circulation. <br />
It is possible that user '{{userEmail}}' has been incorrectly given item '{{itemDescription}}' and thereafter reported it as incorrectly returned.<br />
To prevent user '{{userEmail}}' from having an incorrect registration, you can investigate this. <br />
<br />
View the user history of user '{{userEmail}}' to see if the user has registered items in possession.<br />
If user '{{userEmail}}' has items registered in possession, you can check the operational and technical logs to see which actions preceded the incorrect return notification.<br />
If it appears that user '{{userEmail}}' has an incorrect registration, please contact user '{{userEmail}}' to inquire about any incorrect withdrawals.<br />
<br />
<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 51,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.IncorrectReturnCreatedUIP.GetName(),
                        Subject = "Item incorrect teruggebracht aangemaakt record",
                        Template = @"
Beste Collega,<br />
<br />
Item '{{itemDescription}}' van type '{{itemTypeDescription}}' is incorrect terug gemeld door gebruiker '{{userEmail}}' aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}' <br />
<br />
Deze actie is geregistreerd doormiddel van het aanmaken van een bezitsrecord en item '{{itemDescription}}' is weer in omloop. <br />
Het is mogelijk dat gebruiker '{{userEmail}}' item '{{itemDescription}}' incorrect uitgeworpen heeft gekregen en hierna als incorrect terug heeft gemeld.<br />
Om te voorkomen dat gebruiker '{{userEmail}}' een onterechte registratie heeft kunt u dit onderzoeken. <br />
<br />
Bekijk in de gebruikers geschiedenis van gebruiker '{{userEmail}}' of de gebruiker items in bezit geregistreerd heeft.<br />
Als gebruiker '{{userEmail}}' items in bezit geregistreerd heeft kunt u in de operationele en technische logs bekijken welke handelingen vooraf zijn gegaan aan de incorrect terug melding.<br />
Lijkt het erop dat gebruiker '{{userEmail}}' een onterechte registratie heeft, neem dan contact op met gebruiker '{{userEmail}}' om te vragen naar eventuele incorrecte uitnamen.<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 52,
                        IsActive = false,
                        LanguageCode = Language.EnglishUS.GetLanguageCode(),
                        Name = DefaultEmailTemplate.IncorrectReturnCreatedUIP.GetName(),
                        Subject = "Item incorrect returned created record",
                        Template = @"
Dear Colleague,<br />
<br />
Item '{{itemDescription}}' of type '{{itemTypeDescription}}' has been incorrectly reported returned by user '{{userEmail}}' to the IBK '{{cabinetName}}' at position '{{positionAlias}}'<br />
<br />
This action has been registrated by creating a possession record and item '{{itemDescription}}' is back in circulation. <br />
It is possible that user '{{userEmail}}' has been incorrectly given item '{{itemDescription}}' and thereafter reported it as incorrectly returned.<br />
To prevent user '{{userEmail}}' from having an incorrect registration, you can investigate this. <br />
<br />
View the user history of user '{{userEmail}}' to see if the user has registered items in possession.<br />
If user '{{userEmail}}' has items registered in possession, you can check the operational and technical logs to see which actions preceded the incorrect return notification.<br />
If it appears that user '{{userEmail}}' has an incorrect registration, please contact user '{{userEmail}}' to inquire about any incorrect withdrawals.<br />
<br />
<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 53,
                        IsActive = false,
                        LanguageCode = Language.Arabic.GetLanguageCode(),
                        Name = DefaultEmailTemplate.IncorrectReturnClosedUIP.GetName(),
                        Subject = "العنصر المرتجع بشكل غير صحيح يغلق السجل",
                        Template = @"
عزيزي الزميل،<br />
<br />
تم الإبلاغ عن العنصر '{{itemDescription}}' من النوع '{{itemTypeDescription}}' بشكل خاطئ كمرتجع من قبل المستخدم '{{userEmail}}' إلى IBK '{{cabinetName}}' في الموقع '{{positionAlias}}'<br />
<br />
تم تسجيل هذا الإجراء بإغلاق سجل الاستحواذ والعنصر '{{itemDescription}}' عاد إلى التداول. <br />
من الممكن أن يكون المستخدم '{{userEmail}}' قد تلقى العنصر '{{itemDescription}}' بشكل خاطئ ومن ثم أبلغ عنه كمرتجع خاطئ.<br />
لمنع المستخدم '{{userEmail}}' من وجود تسجيل خاطئ، يمكنك التحقيق في ذلك. <br />
<br />
اطلع على سجل المستخدم للمستخدم '{{userEmail}}' لمعرفة ما إذا كان لدى المستخدم عناصر مسجلة تحت حيازته.<br />
إذا كان لدى المستخدم '{{userEmail}}' عناصر مسجلة تحت حيازته، يمكنك التحقق من السجلات التشغيلية والفنية لمعرفة الإجراءات التي سبقت إشعار الإرجاع الخاطئ.<br />
إذا بدا أن المستخدم '{{userEmail}}' لديه تسجيل خاطئ، يرجى التواصل مع المستخدم '{{userEmail}}' للاستفسار عن أي عمليات سحب خاطئة.<br />
<br />
<b>لا ترد على هذا البريد الإلكتروني، فقد تم إرساله من عنوان 'لا يمكن الرد عليه'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 54,
                        IsActive = false,
                        LanguageCode = Language.Arabic.GetLanguageCode(),
                        Name = DefaultEmailTemplate.IncorrectReturnCreatedUIP.GetName(),
                        Subject = "تم إنشاء سجل للعنصر المرتجع بشكل غير صحيح",
                        Template = @"
عزيزي الزميل،<br />
<br />
تم الإبلاغ بشكل غير صحيح عن عودة العنصر '{{itemDescription}}' من النوع '{{itemTypeDescription}}' من قبل المستخدم '{{userEmail}}' إلى الـ IBK '{{cabinetName}}' في الموقع '{{positionAlias}}'<br />
<br />
تم تسجيل هذا الإجراء من خلال إنشاء سجل ملكية والعنصر '{{itemDescription}}' قد عاد إلى التداول. <br />
من المحتمل أن يكون المستخدم '{{userEmail}}' قد حصل بطريق الخطأ على العنصر '{{itemDescription}}' ومن ثم أبلغ عنه كمرتجع بشكل خاطئ.<br />
لمنع المستخدم '{{userEmail}}' من الحصول على تسجيل خاطئ، يمكنك التحقيق في هذا الأمر. <br />
<br />
راجع تاريخ المستخدم للمستخدم '{{userEmail}}' لمعرفة ما إذا كان المستخدم قد سجل عناصر تحت ملكيته.<br />
إذا كان لدى المستخدم '{{userEmail}}' عناصر مسجلة تحت ملكيته، يمكنك فحص السجلات التشغيلية والفنية لمعرفة الأحداث التي سبقت إشعار الإرجاع الخاطئ.<br />
إذا بدا أن المستخدم '{{userEmail}}' لديه تسجيل خاطئ، يرجى الاتصال بالمستخدم '{{userEmail}}' للاستفسار عن أي عمليات سحب خاطئة.<br />
<br />
<b>لا ترد على هذا البريد الإلكتروني، فقد تم إرساله من عنوان 'لا يمكن الرد عليه'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 55,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.UnknownContentPosition.GetName(),
                        Subject = "Onbekende inhoud",
                        Template = @"
Beste Collega,<br />
<br />
In IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) is onbekende inhoud geplaatst.<br />
<br />
Om positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />
Dit doet u door middel van de volgende stappen:<br />
1. Log in op de IBK<br />
2. Druk op de tegel verwijderen<br />
3. Druk op het uitroepteken rechtbovenin<br />
4. Selecteer de positie die u wilt oplossen<br />
5. Druk op vrijgeven keycop(s)<br />
<br />
Ga de items langs en handel deze af.<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 56,
                        IsActive = false,
                        LanguageCode = Language.EnglishUS.GetLanguageCode(),
                        Name = DefaultEmailTemplate.UnknownContentPosition.GetName(),
                        Subject = "Unknown content",
                        Template = @"
Dear Colleague,<br />
<br />
In cabinet '{{cabinetName}}' at position '{{positionAlias}}' ({{positionNumber}}), unknown content has been placed.<br />
<br />
To resolve position '{{positionAlias}}' ({{positionNumber}}), you need to go through the correction flow.<br />
You do this by following these steps:<br />
1. Log in to the cabinet<br />
2. Press the remove tile<br />
3. Press the exclamation mark at the top right<br />
4. Select the position you want to resolve<br />
5. Press release keycop(s)<br />
<br />
Go through the items and handle them.<br />
<br />
<b>Do not reply to this email, it was sent via a 'no-reply address'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 57,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.DefectPosition.GetName(),
                        Subject = "Defecte positie",
                        Template = @"
Beste Collega,<br />
<br />
IBK '{{cabinetName}}' positie '{{positionAlias}}' ({{positionNumber}}) is defect.<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 58,
                        IsActive = false,
                        LanguageCode = Language.EnglishUS.GetLanguageCode(),
                        Name = DefaultEmailTemplate.DefectPosition.GetName(),
                        Subject = "Defect position",
                        Template = @"
Dear Colleague,<br />
<br />
Cabinet '{{cabinetName}}' position '{{positionAlias}}' ({{positionNumber}}) is defect.<br />
<br />
<b>Do not reply to this email, it was sent via a 'no-reply address'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 59,
                        IsActive = false,
                        LanguageCode = Language.Dutch.GetLanguageCode(),
                        Name = DefaultEmailTemplate.MissingContentPosition.GetName(),
                        Subject = "Ontbrekende inhoud",
                        Template = @"
Beste Collega,<br />
<br />
In IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) ontbreekt {{itemDescription}}.<br />
<br />
Om alleen positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />
Dit doet u door middel van de volgende stappen:<br />
1. Log in op de IBK<br />
2. Druk op de tegel verwijderen<br />
3. Druk op het uitroepteken rechtbovenin<br />
4. Selecteer de positie die u wilt oplossen<br />
5. Druk op vrijgeven keycop(s)<br />
<br />
Om {{itemDescription}} op te lossen dient u contact op te nemen met de persoon waarop deze geregistreerd staat.<br />
LET OP! Deze registratie is een mogelijkheid niet een zekerheid!<br />
<br />
<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 60,
                        IsActive = false,
                        LanguageCode = Language.EnglishUS.GetLanguageCode(),
                        Name = DefaultEmailTemplate.MissingContentPosition.GetName(),
                        Subject = "Missing content",
                        Template = @"
Dear Colleague,<br />
<br />
In cabinet '{{cabinetName}}' at position '{{positionAlias}}' ({{positionNumber}}), {{itemDescription}} is missing.<br />
<br />
To resolve only position '{{positionAlias}}' ({{positionNumber}}), you need to follow the correction flow.<br />
You do this by following these steps:<br />
1. Log in to the cabinet<br />
2. Press the remove tile<br />
3. Press the exclamation mark at the top right<br />
4. Select the position you want to resolve<br />
5. Press release keycop(s)<br />
<br />
To resolve {{itemDescription}}, you need to contact the person it is registered to.<br />
NOTE! This registration is a possibility, not a certainty!<br />
<br />
<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 61,
                        IsActive = false,
                        LanguageCode = Language.Arabic.GetLanguageCode(),
                        Name = DefaultEmailTemplate.UnknownContentPosition.GetName(),
                        Subject = "محتوى غير معروف",
                        Template = @"
عزيزي الزميل،<br />
<br />
في الخزانة '{{cabinetName}}' عند الموقع '{{positionAlias}}' ({{positionNumber}})، تم وضع محتوى غير معروف.<br />
<br />
لحل موقع '{{positionAlias}}' ({{positionNumber}})، تحتاج إلى المرور بعملية التصحيح.<br />
يمكنك القيام بذلك باتباع هذه الخطوات:<br />
1. قم بتسجيل الدخول إلى الخزانة<br />
2. اضغط على إزالة البلاط<br />
3. اضغط على علامة التعجب في أعلى اليمين<br />
4. اختر الموقع الذي تريد حله<br />
5. اضغط على زر إطلاق المفاتيح<br />
<br />
اذهب خلال العناصر وتعامل معها.<br />
<br />
<b>لا ترد على هذا البريد الإلكتروني، تم إرساله عبر 'عنوان لا يقبل الرد'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 62,
                        IsActive = false,
                        LanguageCode = Language.Arabic.GetLanguageCode(),
                        Name = DefaultEmailTemplate.DefectPosition.GetName(),
                        Subject = "موقع معطل",
                        Template = @"
عزيزي الزميل،<br />
<br />
موقع الخزانة '{{cabinetName}}' '{{positionAlias}}' ({{positionNumber}}) به عيب.<br />
<br />
<b>لا ترد على هذا البريد الإلكتروني، تم إرساله عبر 'عنوان لا يقبل الرد'.</b><br />
"
                    },
                    new MailTemplate()
                    {
                        ID = 63,
                        IsActive = false,
                        LanguageCode = Language.Arabic.GetLanguageCode(),
                        Name = DefaultEmailTemplate.MissingContentPosition.GetName(),
                        Subject = "محتوى مفقود",
                        Template = @"
عزيزي الزميل،<br />
<br />
في الخزانة '{{cabinetName}}' عند الموقع '{{positionAlias}}' ({{positionNumber}})، {{itemDescription}} مفقود.<br />
<br />
لحل موقع '{{positionAlias}}' ({{positionNumber}}) فقط، تحتاج إلى اتباع عملية التصحيح.<br />
يمكنك القيام بذلك باتباع هذه الخطوات:<br />
1. قم بتسجيل الدخول إلى الخزانة<br />
2. اضغط على إزالة البلاط<br />
3. اضغط على علامة التعجب في أعلى اليمين<br />
4. اختر الموقع الذي تريد حله<br />
5. اضغط على زر إطلاق المفاتيح<br />
<br />
لحل {{itemDescription}}، تحتاج إلى الاتصال بالشخص المسجل لديه.<br />
ملاحظة! هذا التسجيل هو احتمال، وليس يقين!<br />
<br />
<b>لا ترد على هذا البريد الإلكتروني، تم إرساله من 'عنوان لا يقبل الرد'.</b><br />
"
                    },
 new MailTemplate() { ID = 64, IsActive = false, LanguageCode = Language.German.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebAndCabinetLogin.GetName(), Subject = "Willkommen bei CTAM - Ihre Anmeldedaten", Template = @"Sehr geehrte(r) {{name}},<br /><br />Willkommen bei CTAM!<br /><br />Ihre Anmeldedaten<br />Benutzername: {{email}}<br />Temporäres Passwort: {{password}}<br />Anmeldecode: {{loginCode}}<br />Pin-Code: {{pinCode}}" },

new MailTemplate() { ID = 65, IsActive = false, LanguageCode = Language.French.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebAndCabinetLogin.GetName(), Subject = "Bienvenue à CTAM - vos données de connexion", Template = @"Cher(e) {{name}},<br /><br />Bienvenue à CTAM!<br /><br />Vos données de connexion<br />Nom d'utilisateur: {{email}}<br />Mot de passe temporaire: {{password}}<br />Code de connexion: {{loginCode}}<br />Code PIN: {{pinCode}}" },

new MailTemplate() { ID = 66, IsActive = false, LanguageCode = Language.Swedish.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebAndCabinetLogin.GetName(), Subject = "Välkommen till CTAM - dina inloggningsuppgifter", Template = @"Kära {{name}},<br /><br />Välkommen till CTAM!<br /><br />Dina inloggningsuppgifter<br />Användarnamn: {{email}}<br />Tillfälligt lösenord: {{password}}<br />Inloggningskod: {{loginCode}}<br />PIN-kod: {{pinCode}}" },
// German templates
new MailTemplate() { ID = 67, IsActive = false, LanguageCode = Language.German.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebLogin.GetName(), Subject = "Willkommen bei CTAM - Ihre Anmeldedaten", Template = @"Sehr geehrte(r) {{name}},<br /><br />Willkommen bei CTAM!<br /><br />Ihre Anmeldedaten<br />Benutzername: {{email}}<br />Temporäres Passwort: {{password}}" },
new MailTemplate() { ID = 68, IsActive = false, LanguageCode = Language.German.GetLanguageCode(), Name = DefaultEmailTemplate.TemporaryPassword.GetName(), Subject = "Passwort zurücksetzen", Template = @"Sehr geehrte(r) {{name}},<br /><br />Ihr temporäres Passwort lautet {{password}}<br />Ändern Sie es, sobald Sie sich anmelden!" },
new MailTemplate() { ID = 69, IsActive = false, LanguageCode = Language.German.GetLanguageCode(), Name = DefaultEmailTemplate.PasswordChanged.GetName(), Subject = "Neues Passwort", Template = @"Sehr geehrte(r) {{name}},<br /><br />Ihr Passwort wurde erfolgreich geändert!" },
new MailTemplate() { ID = 70, IsActive = false, LanguageCode = Language.German.GetLanguageCode(), Name = DefaultEmailTemplate.ForgotPassword.GetName(), Subject = "Passwort vergessen", Template = @"Sehr geehrte(r) {{name}},<br /><br />Klicken Sie auf den unten stehenden Link, um Ihr Passwort zu ändern.<br /><br /><a href='{{link}}' target='_blank'>Passwort ändern</a><br /><br />Sie erhalten diese E-Mail, weil Sie kürzlich auf der Website von Nauta Connect auf 'Passwort vergessen' geklickt haben. Wenn Sie diese Änderung nicht veranlasst haben, kontaktieren Sie bitte sofort Ihren Administrator." },

// French templates
new MailTemplate() { ID = 71, IsActive = false, LanguageCode = Language.French.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebLogin.GetName(), Subject = "Bienvenue à CTAM - vos données de connexion", Template = @"Cher(e) {{name}},<br /><br />Bienvenue à CTAM!<br /><br />Vos données de connexion<br />Nom d'utilisateur: {{email}}<br />Mot de passe temporaire: {{password}}" },
new MailTemplate() { ID = 72, IsActive = false, LanguageCode = Language.French.GetLanguageCode(), Name = DefaultEmailTemplate.TemporaryPassword.GetName(), Subject = "Réinitialiser le mot de passe", Template = @"Cher(e) {{name}},<br /><br />Votre mot de passe temporaire est {{password}}<br />Changez-le dès que vous vous connectez!" },
new MailTemplate() { ID = 73, IsActive = false, LanguageCode = Language.French.GetLanguageCode(), Name = DefaultEmailTemplate.PasswordChanged.GetName(), Subject = "Nouveau mot de passe", Template = @"Cher(e) {{name}},<br /><br />Votre mot de passe a été changé avec succès!" },
new MailTemplate() { ID = 74, IsActive = false, LanguageCode = Language.French.GetLanguageCode(), Name = DefaultEmailTemplate.ForgotPassword.GetName(), Subject = "Mot de passe oublié", Template = @"Cher(e) {{name}},<br /><br />Cliquez sur le lien ci-dessous pour changer votre mot de passe.<br /><br /><a href='{{link}}' target='_blank'>Changer le mot de passe</a><br /><br />Vous recevez ce courriel parce que vous avez récemment appuyé sur le bouton 'Mot de passe oublié' sur le site de Nauta Connect. Si vous n'avez pas initié ce changement, veuillez contacter immédiatement votre administrateur." },

// Swedish templates
new MailTemplate() { ID = 75, IsActive = false, LanguageCode = Language.Swedish.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebLogin.GetName(), Subject = "Välkommen till CTAM - dina inloggningsuppgifter", Template = @"Kära {{name}},<br /><br />Välkommen till CTAM!<br /><br />Dina inloggningsuppgifter<br />Användarnamn: {{email}}<br />Tillfälligt lösenord: {{password}}" },
new MailTemplate() { ID = 76, IsActive = false, LanguageCode = Language.Swedish.GetLanguageCode(), Name = DefaultEmailTemplate.TemporaryPassword.GetName(), Subject = "Återställ lösenord", Template = @"Kära {{name}},<br /><br />Ditt tillfälliga lösenord är {{password}}<br />Ändra det så snart du loggar in!" },
new MailTemplate() { ID = 77, IsActive = false, LanguageCode = Language.Swedish.GetLanguageCode(), Name = DefaultEmailTemplate.PasswordChanged.GetName(), Subject = "Nytt lösenord", Template = @"Kära {{name}},<br /><br />Ditt lösenord har ändrats framgångsrikt!" },
new MailTemplate() { ID = 78, IsActive = false, LanguageCode = Language.Swedish.GetLanguageCode(), Name = DefaultEmailTemplate.ForgotPassword.GetName(), Subject = "Glömt lösenord", Template = @"Kära {{name}},<br /><br />Klicka på länken nedan för att ändra ditt lösenord.<br /><br /><a href='{{link}}' target='_blank'>Ändra lösenord</a><br /><br />Du får detta e-postmeddelande eftersom du nyligen tryckte på 'Glömt lösenord'-knappen på Nauta Connects webbplats. Om du inte initierade denna ändring, vänligen kontakta din administratör omedelbart." },
// German template
new MailTemplate() { ID = 79, IsActive = false, LanguageCode = Language.German.GetLanguageCode(), Name = DefaultEmailTemplate.UserModified.GetName(), Subject = "Ihre Änderungen", Template = @"Sehr geehrte(r) {{name}},<br /><br />Überprüfen Sie Ihre Änderungen unten:<br /><br /><table>{{changes}}</table>" },

// Swedish template
new MailTemplate() { ID = 80, IsActive = false, LanguageCode = Language.Swedish.GetLanguageCode(), Name = DefaultEmailTemplate.UserModified.GetName(), Subject = "Dina ändringar", Template = @"Kära {{name}},<br /><br />Granska dina ändringar nedan:<br /><br /><table>{{changes}}</table>" },

// French template
new MailTemplate() { ID = 81, IsActive = false, LanguageCode = Language.French.GetLanguageCode(), Name = DefaultEmailTemplate.UserModified.GetName(), Subject = "Vos modifications", Template = @"Cher(e) {{name}},<br /><br />Examinez vos modifications ci-dessous:<br /><br /><table>{{changes}}</table>" },
// German templates
new MailTemplate() { ID = 82, IsActive = false, LanguageCode = Language.German.GetLanguageCode(), Name = DefaultEmailTemplate.UserDeleted.GetName(), Subject = "Ihr CTAM-Konto wurde gelöscht", Template = @"Sehr geehrte(r) {{name}},<br /><br />Ihr CTAM-Konto wurde vom Administrator gelöscht" },
new MailTemplate() { ID = 83, IsActive = false, LanguageCode = Language.German.GetLanguageCode(), Name = DefaultEmailTemplate.StockBelowMinimum.GetName(), Subject = "Bestand im Schrank unter Minimum", Template = @"Sehr geehrter Schrankadministrator,<br /><br />Der Bestand von {{itemTypeDescription}} im Schrank '{{cabinetNumber}}, {{cabinetName}}' am Standort '{{cabinetLocationDescr}}' liegt unter ({{actualStock}}) dem Mindestbestand ({{minimalStock}})." },
new MailTemplate() { ID = 84, IsActive = false, LanguageCode = Language.German.GetLanguageCode(), Name = DefaultEmailTemplate.StockAtMinimalStockLevel.GetName(), Subject = "Bestand im Schrank zurück auf Mindestniveau", Template = @"Sehr geehrter Schrankadministrator,<br /><br />Der Bestand von {{itemTypeDescription}} im Schrank '{{cabinetNumber}}, {{cabinetName}}' am Standort '{{cabinetLocationDescr}}' hat das Mindestniveau ({{minimalStock}}) erreicht." },
new MailTemplate() { ID = 85, IsActive = false, LanguageCode = Language.German.GetLanguageCode(), Name = DefaultEmailTemplate.ItemStatusChangedToDefect.GetName(), Subject = "Artikel als defekt gemeldet", Template = @"Sehr geehrter Schrankadministrator, <br /><br />Artikel '{{itemDescription}}' vom Typ '{{itemTypeDescription}}' wurde von Benutzer '{{userName}}' mit Fehlercode: '{{errorCodeDescription}}' als defekt gemeldet. <br /><br />Sie können es im Schrank '{{cabinetName}}' auf Position '{{positionAlias}}' abholen." },

// Swedish templates
new MailTemplate() { ID = 86, IsActive = false, LanguageCode = Language.Swedish.GetLanguageCode(), Name = DefaultEmailTemplate.UserDeleted.GetName(), Subject = "Ditt CTAM-konto har raderats", Template = @"Kära {{name}},<br /><br />Ditt CTAM-konto har raderats av administratören" },
new MailTemplate() { ID = 87, IsActive = false, LanguageCode = Language.Swedish.GetLanguageCode(), Name = DefaultEmailTemplate.StockBelowMinimum.GetName(), Subject = "Skåpets lager under minimum", Template = @"Kära skåpadministratör,<br /><br />Lagret av {{itemTypeDescription}} i skåpet '{{cabinetNumber}}, {{cabinetName}}' på plats '{{cabinetLocationDescr}}' är under ({{actualStock}}) det minimala lagret ({{minimalStock}})." },
new MailTemplate() { ID = 88, IsActive = false, LanguageCode = Language.Swedish.GetLanguageCode(), Name = DefaultEmailTemplate.StockAtMinimalStockLevel.GetName(), Subject = "Skåpets lager återgick till minimal nivå", Template = @"Kära skåpadministratör,<br /><br />Lagret av {{itemTypeDescription}} i skåpet '{{cabinetNumber}}, {{cabinetName}}' på plats '{{cabinetLocationDescr}}' har nått den minimala lagernivån ({{minimalStock}})." },
new MailTemplate() { ID = 89, IsActive = false, LanguageCode = Language.Swedish.GetLanguageCode(), Name = DefaultEmailTemplate.ItemStatusChangedToDefect.GetName(), Subject = "Artikel rapporterad defekt", Template = @"Kära skåpadministratör, <br /><br />Artikel '{{itemDescription}}' av typ '{{itemTypeDescription}}' rapporteras defekt av användare '{{userName}}' med felkod: '{{errorCodeDescription}}'. <br /><br />Du kan hämta den i skåpet '{{cabinetName}}' på position '{{positionAlias}}'." },

// French templates
new MailTemplate() { ID = 90, IsActive = false, LanguageCode = Language.French.GetLanguageCode(), Name = DefaultEmailTemplate.UserDeleted.GetName(), Subject = "Votre compte CTAM est supprimé", Template = @"Cher(e) {{name}},<br /><br />Votre compte CTAM a été supprimé par l'administrateur" },
new MailTemplate() { ID = 91, IsActive = false, LanguageCode = Language.French.GetLanguageCode(), Name = DefaultEmailTemplate.StockBelowMinimum.GetName(), Subject = "Stock de l'armoire en dessous du minimum", Template = @"Cher administrateur de l'armoire,<br /><br />Le stock de {{itemTypeDescription}} dans l'armoire '{{cabinetNumber}}, {{cabinetName}}' à l'emplacement '{{cabinetLocationDescr}}' est en dessous ({{actualStock}}) du stock minimal ({{minimalStock}})." },
new MailTemplate() { ID = 92, IsActive = false, LanguageCode = Language.French.GetLanguageCode(), Name = DefaultEmailTemplate.StockAtMinimalStockLevel.GetName(), Subject = "Stock de l'armoire revenu au niveau minimal", Template = @"Cher administrateur de l'armoire,<br /><br />Le stock de {{itemTypeDescription}} dans l'armoire '{{cabinetNumber}}, {{cabinetName}}' à l'emplacement '{{cabinetLocationDescr}}' a atteint le niveau de stock minimal ({{minimalStock}})." },
new MailTemplate() { ID = 93, IsActive = false, LanguageCode = Language.French.GetLanguageCode(), Name = DefaultEmailTemplate.ItemStatusChangedToDefect.GetName(), Subject = "Article signalé défectueux", Template = @"Cher administrateur de l'armoire, <br /><br />L'article '{{itemDescription}}' de type '{{itemTypeDescription}}' est signalé défectueux par l'utilisateur '{{userName}}' avec le code d'erreur: '{{errorCodeDescription}}'. <br /><br />Vous pouvez le récupérer dans l'armoire '{{cabinetName}}' à la position '{{positionAlias}}'." },

new MailTemplate()
{
    ID = 94,
    IsActive = false,
    LanguageCode = Language.Swedish.GetLanguageCode(),
    Name = DefaultEmailTemplate.PersonalItemStatusChangedToSwappedBack.GetName(),
    Subject = "Temporärt föremål {{putItemTypeDescription}} återlämnat {{cabinetLocationDescr}} {{errorCodeDescription}}",
    Template = @"Användare bytte tillbaka temporärt föremål. <br /><br />Temporärt föremål <span style='font-style:italic'>'{{putItemDescription}}'</span> med <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> av typ <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> är bytt tillbaka vid <span style='font-style:italic'>'{{actionDT}}'</span> av användare <span style='font-style:italic'>'{{userName}}'</span> och bör kopplas bort från användaren och få status 'I lager'. Detta temporära föremål har felkod: <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br /><br />Användare <span style='font-style:italic'>'{{userName}}'</span> är sedan <span style='font-style:italic'>'{{actionDT}}'</span> i besittning av föremål <span style='font-style:italic'>'{{takeItemDescription}}'</span> med <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> av typ <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>; i CMDB ska föremålet <span style='font-style:italic'>'{{takeItemDescription}}'</span> med <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> kopplas bort från användare <span style='font-style:italic'>'{{userName}}'</span> och få status 'i användning'. <br /><br />Vänligen efter CMDB-ändrat sätt statusen för händelsen av föremålet <span style='font-style:italic'>'{{takeItemDescription}}'</span> med <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> till 'Löst'."
},
    new MailTemplate()
    {
        ID = 95,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToSwappedBack.GetName(),
        Subject = "Article temporaire {{putItemTypeDescription}} retourné {{cabinetLocationDescr}} {{errorCodeDescription}}",
        Template = @"L'utilisateur a échangé l'article temporaire. <br /><br />L'article temporaire <span style='font-style:italic'>'{{putItemDescription}}'</span> avec <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> du type <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> est échangé à nouveau au <span style='font-style:italic'>'{{actionDT}}'</span> par l'utilisateur <span style='font-style:italic'>'{{userName}}'</span> et devrait être déconnecté de l'utilisateur et obtenir le statut ‘En stock’. Cet article temporaire a le code d'erreur : <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br /><br />L'utilisateur <span style='font-style:italic'>'{{userName}}'</span> est depuis le <span style='font-style:italic'>'{{actionDT}}'</span> en possession de l'article <span style='font-style:italic'>'{{takeItemDescription}}'</span> avec <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> du type <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>; dans le CMDB l'article <span style='font-style:italic'>'{{takeItemDescription}}'</span> avec <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> devrait être déconnecté de l'utilisateur <span style='font-style:italic'>'{{userName}}'</span> et obtenir le statut ‘en utilisation’. <br /><br />Veuillez après le changement du CMDB mettre le statut de l'incident de l'article <span style='font-style:italic'>'{{takeItemDescription}}'</span> avec <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> à ‘Résolu’."
    },
    new MailTemplate()
    {
        ID = 96,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToSwappedBack.GetName(),
        Subject = "Temporärer Artikel {{putItemTypeDescription}} zurückgegeben {{cabinetLocationDescr}} {{errorCodeDescription}}",
        Template = @"Benutzer hat temporären Artikel zurückgetauscht. <br /><br />Temporärer Artikel <span style='font-style:italic'>'{{putItemDescription}}'</span> mit <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> vom Typ <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> ist am <span style='font-style:italic'>'{{actionDT}}'</span> vom Benutzer <span style='font-style:italic'>'{{userName}}'</span> zurückgetauscht worden und sollte vom Benutzer getrennt und den Status ‘Auf Lager’ bekommen. Dieser temporäre Artikel hat den Fehlercode: <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br /><br />Benutzer <span style='font-style:italic'>'{{userName}}'</span> ist seit <span style='font-style:italic'>'{{actionDT}}'</span> im Besitz des Artikels <span style='font-style:italic'>'{{takeItemDescription}}'</span> mit <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> vom Typ <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>; im CMDB sollte der Artikel <span style='font-style:italic'>'{{takeItemDescription}}'</span> mit <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> vom Benutzer <span style='font-style:italic'>'{{userName}}'</span> getrennt und den Status ‘in Gebrauch’ bekommen. <br /><br />Bitte setzen Sie nach der CMDB-Änderung den Status des Vorfalls des Artikels <span style='font-style:italic'>'{{takeItemDescription}}'</span> mit <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> auf ‘Gelöst’."
    },

    new MailTemplate()
    {
        ID = 97,
        IsActive = false,
        LanguageCode = Language.Swedish.GetLanguageCode(),
        Name = DefaultEmailTemplate.WelcomeCabinetLogin.GetName(),
        Subject = "Din pinkod för skåpsinloggning",
        Template = @"Kära {{userName}},<br /><br />Din pinkod är {{pinCode}}."
    },
    new MailTemplate()
    {
        ID = 98,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.WelcomeCabinetLogin.GetName(),
        Subject = "Votre code PIN pour la connexion au casier",
        Template = @"Cher {{userName}},<br /><br />Votre code PIN est {{pinCode}}."
    },
    new MailTemplate()
    {
        ID = 99,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.WelcomeCabinetLogin.GetName(),
        Subject = "Ihr PIN-Code für den Schranks Login",
        Template = @"Liebe(r) {{userName}},<br /><br />Ihr PIN-Code ist {{pinCode}}."
    },
    new MailTemplate()
    {
        ID = 100,
        IsActive = false,
        LanguageCode = Language.Swedish.GetLanguageCode(),
        Name = DefaultEmailTemplate.PincodeChanged.GetName(),
        Subject = "Återställ pinkod",
        Template = @"Kära {{name}},<br /><br />Din nya pinkod är {{pinCode}}<br />"
    },
    new MailTemplate()
    {
        ID = 101,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.PincodeChanged.GetName(),
        Subject = "Réinitialisation du code PIN",
        Template = @"Cher {{name}},<br /><br />Votre nouveau code PIN est {{pinCode}}<br />"
    },
    new MailTemplate()
    {
        ID = 102,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.PincodeChanged.GetName(),
        Subject = "PIN-Code zurücksetzen",
        Template = @"Liebe(r) {{name}},<br /><br />Ihr neuer PIN-Code ist {{pinCode}}<br />"
    },
    new MailTemplate()
    {
        ID = 103,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.IncorrectReturnClosedUIP.GetName(),
        Subject = "Falsche Rückgabe, Datensatz geschlossen",
        Template = @"
Liebe Kollegin, lieber Kollege,<br />
<br />
Der Artikel '{{itemDescription}}' vom Typ '{{itemTypeDescription}}' wurde fälschlicherweise von Benutzer '{{userEmail}}' im IBK '{{cabinetName}}' an Position '{{positionAlias}}' als zurückgegeben gemeldet.<br />
<br />
Diese Aktion wurde durch Schließen eines Besitzdatensatzes registriert, und der Artikel '{{itemDescription}}' ist wieder im Umlauf. <br />
Es ist möglich, dass dem Benutzer '{{userEmail}}' fälschlicherweise der Artikel '{{itemDescription}}' ausgehändigt wurde und dieser daraufhin als falsch zurückgegeben gemeldet wurde.<br />
Um zu verhindern, dass der Benutzer '{{userEmail}}' eine falsche Registrierung hat, können Sie dies untersuchen. <br />
<br />
Sehen Sie sich die Benutzerhistorie von '{{userEmail}}' an, um festzustellen, ob der Benutzer Artikel in Besitz registriert hat.<br />
Wenn der Benutzer '{{userEmail}}' Artikel in Besitz registriert hat, können Sie die betrieblichen und technischen Protokolle überprüfen, um zu sehen, welche Aktionen der falschen Rückgabemeldung vorausgingen.<br />
Wenn es scheint, dass der Benutzer '{{userEmail}}' eine falsche Registrierung hat, kontaktieren Sie bitte den Benutzer '{{userEmail}}', um nach etwaigen falschen Entnahmen zu fragen.<br />
<br />
<b>Antworten Sie nicht auf diese E-Mail, sie wurde von einer 'no-reply Adresse' gesendet.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 104,
        IsActive = false,
        LanguageCode = Language.Swedish.GetLanguageCode(),
        Name = DefaultEmailTemplate.IncorrectReturnClosedUIP.GetName(),
        Subject = "Felaktig återlämning, posten stängd",
        Template = @"
Kära kollega,<br />
<br />
Artikeln '{{itemDescription}}' av typ '{{itemTypeDescription}}' har felaktigt rapporterats återlämnad av användare '{{userEmail}}' till IBK '{{cabinetName}}' vid position '{{positionAlias}}'<br />
<br />
Denna åtgärd har registrerats genom att stänga en äganderättsrekord och artikeln '{{itemDescription}}' är åter i omlopp. <br />
Det är möjligt att användaren '{{userEmail}}' felaktigt har fått artikeln '{{itemDescription}}' och därefter rapporterat den som felaktigt återlämnad.<br />
För att förhindra att användaren '{{userEmail}}' har en felaktig registrering, kan du undersöka detta. <br />
<br />
Visa användarhistoriken för '{{userEmail}}' för att se om användaren har registrerat artiklar i besittning.<br />
Om användaren '{{userEmail}}' har registrerade artiklar i besittning, kan du kontrollera de operativa och tekniska loggarna för att se vilka åtgärder som föregick den felaktiga returanmälan.<br />
Om det verkar som att användaren '{{userEmail}}' har en felaktig registrering, vänligen kontakta användaren '{{userEmail}}' för att fråga om eventuella felaktiga uttag.<br />
<br />
<b>Svara inte på detta e-postmeddelande, det har skickats från en 'no-reply adress'.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 105,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.IncorrectReturnClosedUIP.GetName(),
        Subject = "Retour incorrect, enregistrement fermé",
        Template = @"
Cher collègue,<br />
<br />
L'article '{{itemDescription}}' de type '{{itemTypeDescription}}' a été signalé incorrectement retourné par l'utilisateur '{{userEmail}}' au IBK '{{cabinetName}}' à la position '{{positionAlias}}'.<br />
<br />
Cette action a été enregistrée en fermant un dossier de possession et l'article '{{itemDescription}}' est de nouveau en circulation. <br />
Il est possible que l'utilisateur '{{userEmail}}' ait reçu par erreur l'article '{{itemDescription}}' et l'ait ensuite signalé comme retourné incorrectement.<br />
Pour éviter une inscription incorrecte de l'utilisateur '{{userEmail}}', vous pouvez enquêter sur cette situation. <br />
<br />
Consultez l'historique de l'utilisateur '{{userEmail}}' pour voir si l'utilisateur a enregistré des articles en possession.<br />
Si l'utilisateur '{{userEmail}}' a des articles enregistrés en possession, vous pouvez vérifier les journaux opérationnels et techniques pour voir quelles actions ont précédé la notification de retour incorrect.<br />
S'il apparaît que l'utilisateur '{{userEmail}}' a une inscription incorrecte, veuillez contacter l'utilisateur '{{userEmail}}' pour vous renseigner sur d'éventuels retraits incorrects.<br />
<br />
<b>Ne répondez pas à cet email, il a été envoyé depuis une adresse 'no-reply'.</b><br />
"
    },

    new MailTemplate()
    {
        ID = 106,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.IncorrectReturnCreatedUIP.GetName(),
        Subject = "Falsche Rückgabe hat Datensatz erstellt",
        Template = @"
Liebe Kollegin, lieber Kollege,<br />
<br />
Der Artikel '{{itemDescription}}' vom Typ '{{itemTypeDescription}}' wurde fälschlicherweise von Benutzer '{{userEmail}}' als zurückgegeben im IBK '{{cabinetName}}' am Platz '{{positionAlias}}' gemeldet.<br />
<br />
Diese Aktion wurde durch Erstellen eines Besitzdatensatzes registriert, und der Artikel '{{itemDescription}}' ist wieder im Umlauf. <br />
Es ist möglich, dass dem Benutzer '{{userEmail}}' fälschlicherweise der Artikel '{{itemDescription}}' gegeben und daraufhin als falsch zurückgegeben gemeldet wurde.<br />
Um zu verhindern, dass der Benutzer '{{userEmail}}' eine falsche Registrierung hat, können Sie dies untersuchen. <br />
<br />
Sehen Sie sich die Benutzerhistorie von '{{userEmail}}' an, um zu prüfen, ob der Benutzer Artikel in Besitz registriert hat.<br />
Wenn der Benutzer '{{userEmail}}' Artikel in Besitz registriert hat, können Sie die Betriebs- und Technikprotokolle überprüfen, um zu sehen, welche Aktionen der falschen Rückgabemeldung vorausgingen.<br />
Wenn es scheint, dass der Benutzer '{{userEmail}}' eine falsche Registrierung hat, kontaktieren Sie bitte den Benutzer '{{userEmail}}', um nach etwaigen falschen Abhebungen zu fragen.<br />
<br />
<b>Antworten Sie nicht auf diese E-Mail, sie wurde von einer 'no-reply Adresse' gesendet.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 107,
        IsActive = false,
        LanguageCode = Language.Swedish.GetLanguageCode(),
        Name = DefaultEmailTemplate.IncorrectReturnCreatedUIP.GetName(),
        Subject = "Felaktig återlämning skapade post",
        Template = @"
Kära kollega,<br />
<br />
Artikeln '{{itemDescription}}' av typ '{{itemTypeDescription}}' har felaktigt rapporterats återlämnad av användare '{{userEmail}}' till IBK '{{cabinetName}}' vid position '{{positionAlias}}'<br />
<br />
Denna åtgärd har registrerats genom att skapa ett äganderättsrekord och artikeln '{{itemDescription}}' är åter i omlopp. <br />
Det är möjligt att användaren '{{userEmail}}' felaktigt har fått artikeln '{{itemDescription}}' och därefter rapporterat den som felaktigt återlämnad.<br />
För att förhindra att användaren '{{userEmail}}' har en felaktig registrering, kan du undersöka detta. <br />
<br />
Visa användarhistoriken för '{{userEmail}}' för att se om användaren har registrerat artiklar i besittning.<br />
Om användaren '{{userEmail}}' har registrerade artiklar i besittning, kan du kontrollera de operativa och tekniska loggarna för att se vilka åtgärder som föregick den felaktiga returanmälan.<br />
Om det verkar som att användaren '{{userEmail}}' har en felaktig registrering, vänligen kontakta användaren '{{userEmail}}' för att fråga om eventuella felaktiga uttag.<br />
<br />
<b>Svara inte på detta e-postmeddelande, det har skickats från en 'no-reply adress'.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 108,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.IncorrectReturnCreatedUIP.GetName(),
        Subject = "Création d'un enregistrement pour retour incorrect",
        Template = @"
Cher Collègue,<br />
<br />
L'article '{{itemDescription}}' de type '{{itemTypeDescription}}' a été signalé incorrectement retourné par l'utilisateur '{{userEmail}}' au IBK '{{cabinetName}}' à la position '{{positionAlias}}'.<br />
<br />
Cette action a été enregistrée en créant un dossier de possession et l'article '{{itemDescription}}' est de nouveau en circulation. <br />
Il est possible que l'utilisateur '{{userEmail}}' ait reçu par erreur l'article '{{itemDescription}}' et l'ait ensuite signalé comme retourné incorrectement.<br />
Pour éviter une inscription incorrecte de l'utilisateur '{{userEmail}}', vous pouvez enquêter sur cette situation. <br />
<br />
Consultez l'historique de l'utilisateur '{{userEmail}}' pour voir si l'utilisateur a enregistré des articles en possession.<br />
Si l'utilisateur '{{userEmail}}' a des articles enregistrés en possession, vous pouvez vérifier les journaux opérationnels et techniques pour voir quelles actions ont précédé la notification de retour incorrect.<br />
S'il apparaît que l'utilisateur '{{userEmail}}' a une inscription incorrecte, veuillez contacter l'utilisateur '{{userEmail}}' pour vous renseigner sur d'éventuels retraits incorrects.<br />
<br />
<b>Ne répondez pas à cet email, il a été envoyé depuis une adresse 'no-reply'.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 109,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.UnknownContentPosition.GetName(),
        Subject = "Unbekannter Inhalt",
        Template = @"
Liebe Kollegin, lieber Kollege,<br />
<br />
Im Schrank '{{cabinetName}}' an der Position '{{positionAlias}}' ({{positionNumber}}) wurde unbekannter Inhalt abgelegt.<br />
<br />
Um die Position '{{positionAlias}}' ({{positionNumber}}) zu klären, müssen Sie den Korrekturablauf durchführen.<br />
Folgen Sie dazu diesen Schritten:<br />
1. Melden Sie sich am Schrank an<br />
2. Drücken Sie auf das Entfernen-Feld<br />
3. Drücken Sie das Ausrufezeichen oben rechts<br />
4. Wählen Sie die Position aus, die Sie klären möchten<br />
5. Drücken Sie auf Schlüssel freigeben<br />
<br />
Gehen Sie die Gegenstände durch und bearbeiten Sie sie.<br />
<br />
<b>Antworten Sie nicht auf diese E-Mail, sie wurde über eine 'no-reply-Adresse' gesendet.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 110,
        IsActive = false,
        LanguageCode = Language.Swedish.GetLanguageCode(),
        Name = DefaultEmailTemplate.UnknownContentPosition.GetName(),
        Subject = "Okänt innehåll",
        Template = @"
Kära kollega,<br />
<br />
I skåpet '{{cabinetName}}' vid position '{{positionAlias}}' ({{positionNumber}}), har okänt innehåll placerats.<br />
<br />
För att lösa position '{{positionAlias}}' ({{positionNumber}}), behöver du genomgå korrektionsflödet.<br />
Du gör detta genom att följa dessa steg:<br />
1. Logga in på skåpet<br />
2. Tryck på ta bort-plattan<br />
3. Tryck på utropstecknet högst upp till höger<br />
4. Välj den position du vill lösa<br />
5. Tryck på frigör nyckelkopior<br />
<br />
Gå igenom föremålen och hantera dem.<br />
<br />
<b>Svara inte på detta e-postmeddelande, det skickades från en 'no-reply-adress'.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 111,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.UnknownContentPosition.GetName(),
        Subject = "Contenu inconnu",
        Template = @"
Cher collègue,<br />
<br />
Dans l'armoire '{{cabinetName}}' à la position '{{positionAlias}}' ({{positionNumber}}), un contenu inconnu a été placé.<br />
<br />
Pour résoudre la position '{{positionAlias}}' ({{positionNumber}}), vous devez suivre le processus de correction.<br />
Vous le faites en suivant ces étapes :<br />
1. Connectez-vous à l'armoire<br />
2. Appuyez sur la tuile de suppression<br />
3. Appuyez sur le point d'exclamation en haut à droite<br />
4. Sélectionnez la position que vous souhaitez résoudre<br />
5. Appuyez sur libérer les clés<br />
<br />
Passez en revue les articles et gérez-les.<br />
<br />
<b>Ne répondez pas à cet email, il a été envoyé depuis une adresse 'no-reply'.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 112,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.DefectPosition.GetName(),
        Subject = "Defekte Position",
        Template = @"
Liebe Kollegin, lieber Kollege,<br />
<br />
Position '{{positionAlias}}' ({{positionNumber}}) im Schrank '{{cabinetName}}' ist defekt.<br />
<br />
<b>Antworten Sie nicht auf diese E-Mail, sie wurde über eine 'no-reply-Adresse' gesendet.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 113,
        IsActive = false,
        LanguageCode = Language.Swedish.GetLanguageCode(),
        Name = DefaultEmailTemplate.DefectPosition.GetName(),
        Subject = "Defekt position",
        Template = @"
Kära kollega,<br />
<br />
Position '{{positionAlias}}' ({{positionNumber}}) i skåpet '{{cabinetName}}' är defekt.<br />
<br />
<b>Svara inte på detta e-postmeddelande, det skickades från en 'no-reply-adress'.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 114,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.DefectPosition.GetName(),
        Subject = "Position défectueuse",
        Template = @"
Cher collègue,<br />
<br />
La position '{{positionAlias}}' ({{positionNumber}}) dans l'armoire '{{cabinetName}}' est défectueuse.<br />
<br />
<b>Ne répondez pas à cet email, il a été envoyé depuis une adresse 'no-reply'.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 115,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.MissingContentPosition.GetName(),
        Subject = "Fehlender Inhalt",
        Template = @"
Liebe Kollegin, lieber Kollege,<br />
<br />
Im Schrank '{{cabinetName}}' an der Position '{{positionAlias}}' ({{positionNumber}}) fehlt {{itemDescription}}.<br />
<br />
Um nur die Position '{{positionAlias}}' ({{positionNumber}}) zu klären, müssen Sie den Korrekturablauf durchführen.<br />
Folgen Sie dazu diesen Schritten:<br />
1. Melden Sie sich am Schrank an<br />
2. Drücken Sie auf das Entfernen-Feld<br />
3. Drücken Sie das Ausrufezeichen oben rechts<br />
4. Wählen Sie die Position aus, die Sie klären möchten<br />
5. Drücken Sie auf Schlüssel freigeben<br />
<br />
Um {{itemDescription}} zu klären, müssen Sie sich mit der Person in Verbindung setzen, auf die es registriert ist.<br />
HINWEIS! Diese Registrierung ist eine Möglichkeit, keine Gewissheit!<br />
<br />
<b>Antworten Sie nicht auf diese E-Mail, sie wurde über eine 'no-reply-Adresse' gesendet.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 116,
        IsActive = false,
        LanguageCode = Language.Swedish.GetLanguageCode(),
        Name = DefaultEmailTemplate.MissingContentPosition.GetName(),
        Subject = "Saknat innehåll",
        Template = @"
Kära kollega,<br />
<br />
I skåpet '{{cabinetName}}' vid position '{{positionAlias}}' ({{positionNumber}}), saknas {{itemDescription}}.<br />
<br />
För att endast lösa position '{{positionAlias}}' ({{positionNumber}}), behöver du följa korrektionsflödet.<br />
Du gör detta genom att följa dessa steg:<br />
1. Logga in på skåpet<br />
2. Tryck på ta bort-plattan<br />
3. Tryck på utropstecknet högst upp till höger<br />
4. Välj den position du vill lösa<br />
5. Tryck på frigör nyckelkopior<br />
<br />
För att lösa {{itemDescription}}, behöver du kontakta den person det är registrerat på.<br />
OBS! Denna registrering är en möjlighet, inte en säkerhet!<br />
<br />
<b>Svara inte på detta e-postmeddelande, det skickades från en 'no-reply-adress'.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 117,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.MissingContentPosition.GetName(),
        Subject = "Contenu manquant",
        Template = @"
Cher collègue,<br />
<br />
Dans l'armoire '{{cabinetName}}' à la position '{{positionAlias}}' ({{positionNumber}}), {{itemDescription}} manque.<br />
<br />
Pour résoudre uniquement la position '{{positionAlias}}' ({{positionNumber}}), vous devez suivre le processus de correction.<br />
Vous le faites en suivant ces étapes :<br />
1. Connectez-vous à l'armoire<br />
2. Appuyez sur la tuile de suppression<br />
3. Appuyez sur le point d'exclamation en haut à droite<br />
4. Sélectionnez la position que vous souhaitez résoudre<br />
5. Appuyez sur libérer les clés<br />
<br />
Pour résoudre {{itemDescription}}, vous devez contacter la personne à qui elle est enregistrée.<br />
NOTE ! Cette inscription est une possibilité, pas une certitude !<br />
<br />
<b>Ne répondez pas à cet email, il a été envoyé depuis une adresse 'no-reply'.</b><br />
"
    },
    new MailTemplate()
    {
        ID = 118,
        IsActive = false,
        LanguageCode = Language.Swedish.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToRepaired.GetName(),
        Subject = "Ditt personliga föremål är reparerat",
        Template = @"
Kära {{userName}}, <br /><br />Personligt föremål '{{itemDescription}}' av typ '{{itemTypeDescription}}' är rapporterat reparerat. <br /><br />Du kan byta tillbaka det vid skåpet '{{cabinetName}}' på position '{{positionAlias}}'."
    },
    new MailTemplate()
    {
        ID = 119,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToRepaired.GetName(),
        Subject = "Ihr persönlicher Gegenstand ist repariert",
        Template = @"
Liebe(r) {{userName}}, <br /><br />Persönlicher Gegenstand '{{itemDescription}}' vom Typ '{{itemTypeDescription}}' wurde als repariert gemeldet. <br /><br />Sie können ihn am Schrank '{{cabinetName}}' auf Position '{{positionAlias}}' zurücktauschen."
    },
    new MailTemplate()
    {
        ID = 120,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToRepaired.GetName(),
        Subject = "Votre objet personnel est réparé",
        Template = @"
Cher(ère) {{userName}}, <br /><br />L'objet personnel '{{itemDescription}}' de type '{{itemTypeDescription}}' est signalé comme réparé. <br /><br />Vous pouvez l'échanger à nouveau au casier '{{cabinetName}}' à la position '{{positionAlias}}'."
    },
    new MailTemplate()
    {
        ID = 121,
        IsActive = false,
        LanguageCode = Language.Swedish.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToReplaced.GetName(),
        Subject = "Ditt personliga föremål är utbytt",
        Template = @"
Kära {{userName}}, <br /><br />Personligt föremål '{{itemDescription}}' av typ '{{itemTypeDescription}}' är rapporterat utbytt. <br /><br />Du kan hämta det vid skåpet '{{cabinetName}}' på position '{{positionAlias}}'."
    },
    new MailTemplate()
    {
        ID = 122,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToReplaced.GetName(),
        Subject = "Ihr persönlicher Gegenstand wurde ersetzt",
        Template = @"
Liebe(r) {{userName}}, <br /><br />Persönlicher Gegenstand '{{itemDescription}}' vom Typ '{{itemTypeDescription}}' wurde als ersetzt gemeldet. <br /><br />Sie können ihn am Schrank '{{cabinetName}}' auf Position '{{positionAlias}}' abholen."
    },
    new MailTemplate()
    {
        ID = 123,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToReplaced.GetName(),
        Subject = "Votre objet personnel a été remplacé",
        Template = @"
Cher(ère) {{userName}}, <br /><br />L'objet personnel '{{itemDescription}}' de type '{{itemTypeDescription}}' est signalé comme remplacé. <br /><br />Vous pouvez le récupérer au casier '{{cabinetName}}' à la position '{{positionAlias}}'."
    },
    new MailTemplate()
    {
        ID = 124,
        IsActive = false,
        LanguageCode = Language.Swedish.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToDefect.GetName(),
        Subject = "Defekt rapporterad {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}",
        Template = @"
Föremål {{putItemDescription}} rapporterades defekt den {{actionDT}} av {{userName}} med fel: {{errorCodeDescription}}. Skåp {{cabinetName}} i låda {{positionAlias}} på plats {{cabinetLocationDescr}}. <br /><br />Användare {{userName}} har nu ersättningsföremål {{takeItemDescription}}."
    },
    new MailTemplate()
    {
        ID = 125,
        IsActive = false,
        LanguageCode = Language.German.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToDefect.GetName(),
        Subject = "Defekt gemeldet {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}",
        Template = @"
Artikel {{putItemDescription}} wurde am {{actionDT}} von {{userName}} als defekt gemeldet mit Fehler: {{errorCodeDescription}}. Schrank {{cabinetName}} in Fach {{positionAlias}} am Ort {{cabinetLocationDescr}}. <br /><br />Benutzer {{userName}} hat nun Ersatzartikel {{takeItemDescription}}."
    },
    new MailTemplate()
    {
        ID = 126,
        IsActive = false,
        LanguageCode = Language.French.GetLanguageCode(),
        Name = DefaultEmailTemplate.PersonalItemStatusChangedToDefect.GetName(),
        Subject = "Défaut signalé {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}",
        Template = @"
Article {{putItemDescription}} signalé défectueux le {{actionDT}} par {{userName}} avec erreur : {{errorCodeDescription}}. Armoire {{cabinetName}} dans casier {{positionAlias}} à l'emplacement {{cabinetLocationDescr}}. <br /><br />L'utilisateur {{userName}} possède maintenant l'article de remplacement {{takeItemDescription}}."
    }


                );
            modelBuilder.Entity<MailMarkupTemplate>()
                .HasData(
                    new MailMarkupTemplate() { ID = 1, Name = "default_nc", CreateDT = new DateTime(2023, 2, 21, 9, 16, 53, 200, DateTimeKind.Utc).AddTicks(5639), Template = @"<div>
  <span style='font-size:11pt;'>
  <p style='font-size:11pt;font-family:Calibri,sans-serif;margin:0;'>
    {{body}}
  </p>
  </span>
</div>
" }
                );
        }
    }
}
