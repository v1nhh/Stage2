using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class ReplaceCTByNC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 9,
                columns: new[] { "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[]
                    { "nl-NL", "forgot_password", "Wachtwoord vergeten", "Beste {{name}},<br/><br/>Klik op de onderstaande link om uw wachtwoord te wijzigen.<br/><br/><a href='{{link}}' target='_blank'>Wachtwoord wijzigen</a><br/><br/>U ontvangt deze e-mail omdat u onlangs op de knop 'Wachtwoord vergeten' hebt gedrukt op de website van Nauta Connect. Als u deze wijziging niet heeft doorgevoerd, neem dan onmiddellijk contact op met uw beheerder.", null }
                );

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[]
                    { "en-US", "forgot_password", "Forgot password", "Dear {{name}},<br /><br />Click the link below to change your password.<br /><br /><a href='{{link}}' target='_blank'>Change password</a><br /><br />You’re receiving this email because you recently pressed 'Forgot password' button on website of Nauta Connect. If you did not initiate this change, please contact your administrator immediately.", null }
                );

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "Name", "Template", "UpdateDT"},
                values: new object[] { new DateTime(2021, 11, 26, 11, 1, 18, 366, DateTimeKind.Utc).AddTicks(3138), "default_nc", "<div>\r\n  <span style='font-size:11pt;'>\r\n  <p style='font-size:11pt;font-family:Calibri,sans-serif;margin:0;'>\r\n    {{body}}\r\n    <br/><br/><br/>\r\n\r\n\r\n  </p>\r\n    <img src='https://nautaconnect.com/wp-content/uploads/2023/02/nautaconnect.svg' border='0' style='cursor: pointer; max-width: 200px; height: auto;'><br>\r\n  </span></p>\r\n  <table border='0' cellspacing='0' cellpadding='0' style='width:59.76%;height:105px;'>\r\n    <tbody>\r\n      <tr>\r\n        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>HQ Nederland</td>\r\n      </tr>\r\n      <tr>\r\n        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>Hanzeweg 10, 3771 NG Barneveld<br>\r\n        The Netherlands<strong></strong></td>\r\n      </tr>\r\n      <tr>\r\n        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>+31 (0) 252 241 544</td>\r\n      </tr>\r\n      <tr>\r\n        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>\r\n          <a href='https://nautaconnect.com/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'>www.nautaconnect.com</a>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table>\r\n  <br>\r\n</div>", null});

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 37,
                columns: new[] { "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[] { "ar-AE", "forgot_password", "نسيت كلمة المرور", "عزيزي {{name}}،<br /><br />اضغط على الرابط أدناه لتغيير كلمة المرور الخاصة بك.<br /><br /><a href='{{link}}' target='_blank'>تغيير كلمة المرور</a><br /><br />أنت تتلقى هذا البريد الإلكتروني لأنك ضغطت مؤخرًا على زر 'نسيت كلمة المرور' على موقع Nauta Connect. إذا لم تكن قد بدأت هذا التغيير، يرجى الاتصال بالمسؤول الخاص بك فورًا.", null });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 37,
                columns: new[] { "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[] { "ar-AE", "forgot_password", "نسيت كلمة المرور", "عزيزي {{name}}،<br /><br />اضغط على الرابط أدناه لتغيير كلمة المرور الخاصة بك.<br /><br /><a href='{{link}}' target='_blank'>تغيير كلمة المرور</a><br /><br />أنت تتلقى هذا البريد الإلكتروني لأنك ضغطت مؤخرًا على زر 'نسيت كلمة المرور' على موقع CaptureTech. إذا لم تكن قد بدأت هذا التغيير، يرجى الاتصال بالمسؤول الخاص بك فورًا.", null });

            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailMarkupTemplate",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "CreateDT", "Name", "Template", "UpdateDT"},
                values: new object[] { new DateTime(2021, 11, 17, 9, 7, 45, 614, DateTimeKind.Utc).AddTicks(5744), "default_ct", "<div>\r\n  <span style='font-size:11pt;'>\r\n  <p style='font-size:11pt;font-family:Calibri,sans-serif;margin:0;'>\r\n    {{body}}\r\n    <br/><br/><br/>\r\n\r\n\r\n  </p>\r\n    <img src='https://www.capturetech.com/wp-content/uploads/2020/03/Logo-Capturetech-nopayoff.png' border='0' style='cursor: pointer; max-width: 200px; height: auto;'><br>\r\n  </span></p>\r\n  <table border='0' cellspacing='0' cellpadding='0' style='width:59.76%;height:105px;'>\r\n    <tbody>\r\n      <tr>\r\n        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>HQ Nederland</td>\r\n      </tr>\r\n      <tr>\r\n        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>Lireweg 42, 2153 PH Nieuw-Vennep<br>\r\n        The Netherlands<strong></strong></td>\r\n      </tr>\r\n      <tr>\r\n        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>+31 (0) 252 241 544</td>\r\n      </tr>\r\n      <tr>\r\n        <td valign='top' style='font-size:11pt;font-family:Calibri;width:100%;'>\r\n          <a href='http://www.capturetech.com/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'>www.capturetech.com</a>\r\n        </td>\r\n      </tr>\r\n    </tbody>\r\n  </table>\r\n  <span><a href='https://twitter.com/capturetechnl/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/twitter.png' style='cursor: pointer; width: 24px; height: 24px;'></a> \r\n  <a href='https://www.linkedin.com/company/capturetech/' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/linkedin.png' style='cursor: pointer; width: 24px; height: 24px;'></a> \r\n  <a href='https://www.youtube.com/channel/UCA_hKzBYnr3UHDC0Cp0u2kQ' target='_blank' rel='noopener noreferrer' data-auth='NotApplicable'><img src='https://ctrdeuwctamdevsalocalui.blob.core.windows.net/mail-markup/youtube.png' style='cursor: pointer; width: 24px; height: 24px;'></a></span>  \r\n  <br>\r\n</div>", null});


            migrationBuilder.UpdateData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 10,
                columns: new[] { "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[]
                    { "en-US", "forgot_password", "Forgot password", "Dear {{name}},<br/><br/>Click the link below to change your password.<br/><br/><a href='{{link}}' target='_blank'>Change password</a><br/><br/>You’re receiving this email because you recently pressed 'Forgot password' button on website of CaptureTech. If you did not initiate this change, please contact your administrator immediately.", null }
                );

            migrationBuilder.UpdateData(
               schema: "Communication",
               table: "MailTemplate",
               keyColumn: "ID",
               keyValue: 9,
               columns: new[] { "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
               values: new object[]
                   { "nl-NL", "forgot_password", "Wachtwoord vergeten", "Beste {{name}},<br/><br/>Klik op de onderstaande link om uw wachtwoord te wijzigen.<br/><br/><a href='{{link}}' target='_blank'>Wachtwoord wijzigen</a><br/><br/>U ontvangt deze e-mail omdat u onlangs op de knop 'Wachtwoord vergeten' hebt gedrukt op de website van CaptureTech. Als u deze wijziging niet heeft doorgevoerd, neem dan onmiddellijk contact op met uw beheerder.", null }
               );

        }
    }
}
