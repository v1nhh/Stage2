using System;
using CommunicationModule.ApplicationCore.Entities;
using CommunicationModule.ApplicationCore.Enums;
using CTAM.Core.Constants;
using CTAM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunicationModule.Infrastructure.InitialData
{
    // protection level to use only within current module
    class TestData : IDataInserts
    {
        public string Environment => DeploymentEnvironment.Test;

        public void InsertData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MailQueue>()
               .HasData(
                    new MailQueue() { ID = 1, MailMarkupTemplateID = 1, MailTo = "software@nautaconnect.com", MailCC = null, Reference = "REF. 123", Prio = false, Subject = "Hallo", Body = "Hallo teamleden!", Status = MailQueueStatus.Created, CreateDT = DateTime.UtcNow}

                );

            modelBuilder.Entity<MailTemplate>()
               .HasData(
                    new MailTemplate() { ID = 1, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebAndCabinetLogin.GetName(), Subject = "Welkom bij CTAM - uw inloggegevens", Template = @"Beste {{name}},<br/><br/>Welkom bij CTAM!<br/><br/>Uw inloggegevens<br/>Gebruikersnaam: {{email}}<br/>Tijdelijke wachtwoord: {{password}}<br/>Login code: {{loginCode}}<br/>Pincode: {{pinCode}}" },
                    new MailTemplate() { ID = 2, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebAndCabinetLogin.GetName(), Subject = "Welcome to CTAM - your login data", Template = @"Dear {{name}},<br/><br/>Welcome to CTAM!<br/><br/>Your login details<br/>Username: {{email}}<br/>Temporary password: {{password}}<br/>Login code: {{loginCode}}<br/>Pin code: {{pinCode}}" },
                    new MailTemplate() { ID = 3, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebLogin.GetName(), Subject = "Welkom bij CTAM - uw inloggegevens", Template = @"Beste {{name}},<br/><br/>Welkom bij CTAM!<br/><br/>Uw inloggegevens<br/>Gebruikersnaam: {{email}}<br/>Tijdelijke wachtwoord: {{password}}" },
                    new MailTemplate() { ID = 4, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeWebLogin.GetName(), Subject = "Welcome to CTAM - your login data", Template = @"Dear {{name}},<br/><br/>Welcome to CTAM!<br/><br/>Your login details<br/>Username: {{email}}<br/>Temporary password: {{password}}" },
                    new MailTemplate() { ID = 5, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.TemporaryPassword.GetName(), Subject = "Reset wachtwoord", Template = @"Beste {{name}},<br/><br/>Uw tijdelijke wachtwoord is {{password}}<br/>Verander die gelijk na het inloggen!" },
                    new MailTemplate() { ID = 6, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.TemporaryPassword.GetName(), Subject = "Reset password", Template = @"Dear {{name}},<br/><br/>Your temporary password is {{password}}<br/>Change it as soon as you login!" },
                    new MailTemplate() { ID = 7, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.PasswordChanged.GetName(), Subject = "Nieuw wachtwoord", Template = @"Beste {{name}},<br/><br/>Uw wachtwoord wijziging is succesvol doorgevoerd!" },
                    new MailTemplate() { ID = 8, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.PasswordChanged.GetName(), Subject = "New password", Template = @"Dear {{name}},<br/><br/>Your password has been changed successfully!" },
                    new MailTemplate() { ID = 9, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.ForgotPassword.GetName(), Subject = "Wachtwoord vergeten", Template = @"Beste {{name}},<br/><br/>Klik op de onderstaande link om uw wachtwoord te wijzigen.<br/><br/><a href='{{link}}' target='_blank'>Wachtwoord wijzigen</a><br/><br/>U ontvangt deze e-mail omdat u onlangs op de knop 'Wachtwoord vergeten' hebt gedrukt op de website van Nauta Connect. Als u deze wijziging niet heeft doorgevoerd, neem dan onmiddellijk contact op met uw beheerder." },
                    new MailTemplate() { ID = 10, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.ForgotPassword.GetName(), Subject = "Forgot password", Template = @"Dear {{name}},<br/><br/>Click the link below to change your password.<br/><br/><a href='{{link}}' target='_blank'>Change password</a><br/><br/>You’re receiving this email because you recently pressed 'Forgot password' button on website of Nauta Connect. If you did not initiate this change, please contact your administrator immediately." },
                    new MailTemplate() { ID = 11, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.UserModified.GetName(), Subject = "Uw aanpassingen", Template = @"Beste {{name}},<br/><br/>Bekijk hieronder uw aanpassingen:<br/><br/><table>{{changes}}</table>" },
                    new MailTemplate() { ID = 12, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.UserModified.GetName(), Subject = "Your changes", Template = @"Dear {{name}},<br/><br/>Review your changes below:<br/><br/><table>{{changes}}</table>" },
                    new MailTemplate() { ID = 13, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.UserDeleted.GetName(), Subject = "Uw CTAM account is verwijderd", Template = @"Beste {{name}},<br/><br/>Uw CTAM account is verwijderd door de administrator." },
                    new MailTemplate() { ID = 14, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.UserDeleted.GetName(), Subject = "Your CTAM account is deleted", Template = @"Dear {{name}},<br/><br/>Your CTAM account is deleted by the administrator" },
                    new MailTemplate() { ID = 15, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.StockBelowMinimum.GetName(), Subject = "IBK voorraad onder minimum aantal", Template = @"Beste IBK beheerder,<br/><br/>De voorraad van item type '{{itemTypeDescription}}' in IBK '{{cabinetNumber}}, {{cabinetName}}' op locatie '{{cabinetLocationDescr}}' is minder ({{actualStock}}) dan de opgegeven minimale voorraad ({{minimalStock}})." },
                    new MailTemplate() { ID = 16, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.StockBelowMinimum.GetName(), Subject = "Cabinet stock below minimal", Template = @"Dear cabinet administrator,<br/><br/>The cabinet stock of {{itemTypeDescription}} in cabinet '{{cabinetNumber}}, {{cabinetName}}' at location '{{cabinetLocationDescr}}' is below ({{actualStock}}) the minimal stock ({{minimalStock}})." },
                    new MailTemplate() { ID = 17, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.StockAtMinimalStockLevel.GetName(), Subject = "IBK voorraad weer op minimum aantal", Template = @"Beste IBK beheerder,<br/><br/>De voorraad van item type '{{itemTypeDescription}}' in IBK '{{cabinetNumber}}, {{cabinetName}}' op locatie '{{cabinetLocationDescr}}' is weer op het opgegeven minimale voorraad ({{minimalStock}} niveau)." },
                    new MailTemplate() { ID = 18, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.StockAtMinimalStockLevel.GetName(), Subject = "Cabinet stock returned at minimal level", Template = @"Dear cabinet administrator,<br/><br/>The cabinet stock of {{itemTypeDescription}} in cabinet '{{cabinetNumber}}, {{cabinetName}}' at location '{{cabinetLocationDescr}}' is has reached the minimal stock ({{minimalStock}} level)." },
                    new MailTemplate() { ID = 19, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeCabinetLogin.GetName(), Subject = "Uw pincode voor IBK aanmelden", Template = @"Beste {{userName}},<br/><br/>Uw pincode is {{pinCode}}." },
                    new MailTemplate() { ID = 20, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.WelcomeCabinetLogin.GetName(), Subject = "Your pincode for cabinet login", Template = @"Dear {{userName}},<br/><br/>Your pincode is {{pinCode}}." },
                    new MailTemplate() { ID = 21, LanguageCode = Language.Dutch.GetLanguageCode(), Name = DefaultEmailTemplate.PincodeChanged.GetName(), Subject = "Reset pincode", Template = @"Beste {{name}},<br/><br/>Uw nieuwe pincode is {{pinCode}}<br/>" },
                    new MailTemplate() { ID = 22, LanguageCode = Language.EnglishUS.GetLanguageCode(), Name = DefaultEmailTemplate.PincodeChanged.GetName(), Subject = "Reset pincode", Template = @"Dear {{name}},<br/><br/>Your new pincode is {{pinCode}}<br/>" }
                );

            modelBuilder.Entity<MailMarkupTemplate>()
                .HasData(
                    new MailMarkupTemplate() { ID = 1, Name = "default_nc", CreateDT = DateTime.UtcNow, Template = @"<div>
  <span style='font-size:11pt;'>
  <p style='font-size:11pt;font-family:Calibri,sans-serif;margin:0;'>
    {{body}}
    <br/><br/><br/>


  </p>
    <img src='https://nautaconnect.com/wp-content/uploads/2023/02/nautaconnect.svg' border='0' style='cursor: pointer; max-width: 200px; height: auto;'><br>
  </span></p>
  <table border='0' cellspacing='0' cellpadding='0' style='width:59.76%;height:105px;'>
    <tbody>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>HQ Nederland</td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>Hanzeweg 10, 3771 NG Barneveld<br>
        The Netherlands<strong></strong></td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>+31 (0) 252 241 544</td>
      </tr>
      <tr>
        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>
          <a href='https://nautaconnect.com/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'>www.nautaconnect.com</a>
        </td>
      </tr>
    </tbody>
  </table>
    
  <br>
</div>" }
                );
        }
    }
}
