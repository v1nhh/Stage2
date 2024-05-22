using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class addIncorrectEmails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "ID", "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[,]
                {
                    { 49, "nl-NL", "incorrect_return_closed_uip", "Item incorrect teruggebracht afgesloten record", "\r\nBeste Collega,<br />\r\n<br />\r\nItem '{{itemDescription}}' van type '{{itemTypeDescription}}' is incorrect terug gemeld door gebruiker '{{userEmail}}' aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}' <br />\r\n<br />\r\nDeze actie is geregistreerd doormiddel van het afsluiten van een bezitsrecord en item '{{itemDescription}}' is weer in omloop. <br />\r\nHet is mogelijk dat gebruiker '{{userEmail}}' item '{{itemDescription}}' incorrect uitgeworpen heeft gekregen en hierna als incorrect terug heeft gemeld.<br />\r\nOm te voorkomen dat gebruiker '{{userEmail}}' een onterechte registratie heeft kunt u dit onderzoeken. <br />\r\n<br />\r\nBekijk in de gebruikers geschiedenis van gebruiker '{{userEmail}}' of de gebruiker items in bezit geregistreerd heeft.<br />\r\nAls gebruiker '{{userEmail}}' items in bezit geregistreerd heeft kunt u in de operationele en technische logs bekijken welke handelingen vooraf zijn gegaan aan de incorrect terug melding.<br />\r\nLijkt het erop dat gebruiker '{{userEmail}}' een onterechte registratie heeft, neem dan contact op met gebruiker '{{userEmail}}' om te vragen naar eventuele incorrecte uitnamen.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n", null },
                    { 50, "en-US", "incorrect_return_closed_uip", "Item incorrect returned closed record", "\r\nDear Colleague,<br />\r\n<br />\r\nItem '{{itemDescription}}' of type '{{itemTypeDescription}}' has been incorrectly reported returned by user '{{userEmail}}' to the IBK '{{cabinetName}}' at position '{{positionAlias}}'<br />\r\n<br />\r\nThis action has been registrated by closing a possession record and item '{{itemDescription}}' is back in circulation. <br />\r\nIt is possible that user '{{userEmail}}' has been incorrectly given item '{{itemDescription}}' and thereafter reported it as incorrectly returned.<br />\r\nTo prevent user '{{userEmail}}' from having an incorrect registration, you can investigate this. <br />\r\n<br />\r\nView the user history of user '{{userEmail}}' to see if the user has registered items in possession.<br />\r\nIf user '{{userEmail}}' has items registered in possession, you can check the operational and technical logs to see which actions preceded the incorrect return notification.<br />\r\nIf it appears that user '{{userEmail}}' has an incorrect registration, please contact user '{{userEmail}}' to inquire about any incorrect withdrawals.<br />\r\n<br />\r\n<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />\r\n", null },
                    { 51, "nl-NL", "incorrect_return_created_uip", "Item incorrect teruggebracht aangemaakt record", "\r\nBeste Collega,<br />\r\n<br />\r\nItem '{{itemDescription}}' van type '{{itemTypeDescription}}' is incorrect terug gemeld door gebruiker '{{userEmail}}' aan de IBK '{{cabinetName}}' op positie '{{positionAlias}}' <br />\r\n<br />\r\nDeze actie is geregistreerd doormiddel van het aanmaken van een bezitsrecord en item '{{itemDescription}}' is weer in omloop. <br />\r\nHet is mogelijk dat gebruiker '{{userEmail}}' item '{{itemDescription}}' incorrect uitgeworpen heeft gekregen en hierna als incorrect terug heeft gemeld.<br />\r\nOm te voorkomen dat gebruiker '{{userEmail}}' een onterechte registratie heeft kunt u dit onderzoeken. <br />\r\n<br />\r\nBekijk in de gebruikers geschiedenis van gebruiker '{{userEmail}}' of de gebruiker items in bezit geregistreerd heeft.<br />\r\nAls gebruiker '{{userEmail}}' items in bezit geregistreerd heeft kunt u in de operationele en technische logs bekijken welke handelingen vooraf zijn gegaan aan de incorrect terug melding.<br />\r\nLijkt het erop dat gebruiker '{{userEmail}}' een onterechte registratie heeft, neem dan contact op met gebruiker '{{userEmail}}' om te vragen naar eventuele incorrecte uitnamen.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n", null },
                    { 52, "en-US", "incorrect_return_created_uip", "Item incorrect returned created record", "\r\nDear Colleague,<br />\r\n<br />\r\nItem '{{itemDescription}}' of type '{{itemTypeDescription}}' has been incorrectly reported returned by user '{{userEmail}}' to the IBK '{{cabinetName}}' at position '{{positionAlias}}'<br />\r\n<br />\r\nThis action has been registrated by creating a possession record and item '{{itemDescription}}' is back in circulation. <br />\r\nIt is possible that user '{{userEmail}}' has been incorrectly given item '{{itemDescription}}' and thereafter reported it as incorrectly returned.<br />\r\nTo prevent user '{{userEmail}}' from having an incorrect registration, you can investigate this. <br />\r\n<br />\r\nView the user history of user '{{userEmail}}' to see if the user has registered items in possession.<br />\r\nIf user '{{userEmail}}' has items registered in possession, you can check the operational and technical logs to see which actions preceded the incorrect return notification.<br />\r\nIf it appears that user '{{userEmail}}' has an incorrect registration, please contact user '{{userEmail}}' to inquire about any incorrect withdrawals.<br />\r\n<br />\r\n<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />\r\n", null },
                    { 53, "ar-AE", "incorrect_return_closed_uip", "العنصر المرتجع بشكل غير صحيح يغلق السجل", "عزيزي الزميل،<br />\r\n<br />\r\nتم الإبلاغ عن العنصر '{{itemDescription}}' من النوع '{{itemTypeDescription}}' بشكل خاطئ كمرتجع من قبل المستخدم '{{userEmail}}' إلى IBK '{{cabinetName}}' في الموقع '{{positionAlias}}'<br />\r\n<br />\r\nتم تسجيل هذا الإجراء بإغلاق سجل الاستحواذ والعنصر '{{itemDescription}}' عاد إلى التداول. <br />\r\nمن الممكن أن يكون المستخدم '{{userEmail}}' قد تلقى العنصر '{{itemDescription}}' بشكل خاطئ ومن ثم أبلغ عنه كمرتجع خاطئ.<br />\r\nلمنع المستخدم '{{userEmail}}' من وجود تسجيل خاطئ، يمكنك التحقيق في ذلك. <br />\r\n<br />\r\nاطلع على سجل المستخدم للمستخدم '{{userEmail}}' لمعرفة ما إذا كان لدى المستخدم عناصر مسجلة تحت حيازته.<br />\r\nإذا كان لدى المستخدم '{{userEmail}}' عناصر مسجلة تحت حيازته، يمكنك التحقق من السجلات التشغيلية والفنية لمعرفة الإجراءات التي سبقت إشعار الإرجاع الخاطئ.<br />\r\nإذا بدا أن المستخدم '{{userEmail}}' لديه تسجيل خاطئ، يرجى التواصل مع المستخدم '{{userEmail}}' للاستفسار عن أي عمليات سحب خاطئة.<br />\r\n<br />\r\n<b>لا ترد على هذا البريد الإلكتروني، فقد تم إرساله من عنوان 'لا يمكن الرد عليه'.</b><br />\r\n", null },
                    { 54, "ar-AE", "incorrect_return_created_uip", "تم إنشاء سجل للعنصر المرتجع بشكل غير صحيح", "عزيزي الزميل،<br />\r\n<br />\r\nتم الإبلاغ بشكل غير صحيح عن عودة العنصر '{{itemDescription}}' من النوع '{{itemTypeDescription}}' من قبل المستخدم '{{userEmail}}' إلى الـ IBK '{{cabinetName}}' في الموقع '{{positionAlias}}'<br />\r\n<br />\r\nتم تسجيل هذا الإجراء من خلال إنشاء سجل ملكية والعنصر '{{itemDescription}}' قد عاد إلى التداول. <br />\r\nمن المحتمل أن يكون المستخدم '{{userEmail}}' قد حصل بطريق الخطأ على العنصر '{{itemDescription}}' ومن ثم أبلغ عنه كمرتجع بشكل خاطئ.<br />\r\nلمنع المستخدم '{{userEmail}}' من الحصول على تسجيل خاطئ، يمكنك التحقيق في هذا الأمر. <br />\r\n<br />\r\nراجع تاريخ المستخدم للمستخدم '{{userEmail}}' لمعرفة ما إذا كان المستخدم قد سجل عناصر تحت ملكيته.<br />\r\nإذا كان لدى المستخدم '{{userEmail}}' عناصر مسجلة تحت ملكيته، يمكنك فحص السجلات التشغيلية والفنية لمعرفة الأحداث التي سبقت إشعار الإرجاع الخاطئ.<br />\r\nإذا بدا أن المستخدم '{{userEmail}}' لديه تسجيل خاطئ، يرجى الاتصال بالمستخدم '{{userEmail}}' للاستفسار عن أي عمليات سحب خاطئة.<br />\r\n<br />\r\n<b>لا ترد على هذا البريد الإلكتروني، فقد تم إرساله من عنوان 'لا يمكن الرد عليه'.</b><br />\r\n", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 36);
        }
    }
}
