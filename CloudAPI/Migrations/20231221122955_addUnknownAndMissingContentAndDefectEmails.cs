using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class addUnknownAndMissingContentAndDefectEmails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "ID", "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[,]
                {
                    { 55, "nl-NL", "unknown_content_position", "Onbekende inhoud", "\r\nBeste Collega,<br />\r\n<br />\r\nIn IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) is onbekende inhoud geplaatst.<br />\r\n<br />\r\nOm positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />\r\nDit doet u door middel van de volgende stappen:<br />\r\n1. Log in op de kast<br />\r\n2. Druk op de tegel verwijderen<br />\r\n3. Druk op het uitroepteken rechtbovenin<br />\r\n4. Selecteer de positie die u wilt oplossen<br />\r\n5. Druk op vrijgeven keycop(s)<br />\r\n<br />\r\nGa de items langs en handel deze af.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n", null },
                    { 56, "en-US", "unknown_content_position", "Unknown content", "\r\nDear Colleague,<br />\r\n<br />\r\nIn cabinet '{{cabinetName}}' at position '{{positionAlias}}' ({{positionNumber}}), unknown content has been placed.<br />\r\n<br />\r\nTo resolve position '{{positionAlias}}' ({{positionNumber}}), you need to go through the correction flow.<br />\r\nYou do this by following these steps:<br />\r\n1. Log in to the cabinet<br />\r\n2. Press the remove tile<br />\r\n3. Press the exclamation mark at the top right<br />\r\n4. Select the position you want to resolve<br />\r\n5. Press release keycop(s)<br />\r\n<br />\r\nGo through the items and handle them.<br />\r\n<br />\r\n<b>Do not reply to this email, it was sent via a 'no-reply address'.</b><br />\r\n", null },
                    { 57, "nl-NL", "defect_position", "Defecte positie", "\r\nBeste Collega,<br />\r\n<br />\r\nIBK '{{cabinetName}}' positie '{{positionAlias}}' ({{positionNumber}}) is defect.<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n", null },
                    { 58, "en-US", "defect_position", "Defect position", "\r\nDear Colleague,<br />\r\n<br />\r\nCabinet '{{cabinetName}}' position '{{positionAlias}}' ({{positionNumber}}) is defect.<br />\r\n<br />\r\n<b>Do not reply to this email, it was sent via a 'no-reply address'.</b><br />\r\n", null },
                    { 59, "nl-NL", "missing_content_position", "Ontbrekende inhoud", "\r\nBeste Collega,<br />\r\n<br />\r\nIn IBK '{{cabinetName}}' op positie '{{positionAlias}}' ({{positionNumber}}) ontbreekt {{itemDescription}}.<br />\r\n<br />\r\nOm alleen positie '{{positionAlias}}' ({{positionNumber}}) op te lossen dient u de correctie flow te doorlopen.<br />\r\nDit doet u door middel van de volgende stappen:<br />\r\n1. Log in op de kast<br />\r\n2. Druk op de tegel verwijderen<br />\r\n3. Druk op het uitroepteken rechtbovenin<br />\r\n4. Selecteer de positie die u wilt oplossen<br />\r\n5. Druk op vrijgeven keycop(s)<br />\r\n<br />\r\nOm {{itemDescription}} op te lossen dient u contact op te nemen met de persoon waarop deze geregistreerd staat.<br />\r\nLET OP! Deze registratie is een mogelijkheid niet een zekerheid!<br />\r\n<br />\r\n<b>Beantwoord deze e-mail niet, deze is verzonden via een 'no-reply-adres'.</b><br />\r\n", null },
                    { 60, "en-US", "missing_content_position", "Missing content", "\r\nDear Colleague,<br />\r\n<br />\r\nIn cabinet '{{cabinetName}}' at position '{{positionAlias}}' ({{positionNumber}}), {{itemDescription}} is missing.<br />\r\n<br />\r\nTo resolve only position '{{positionAlias}}' ({{positionNumber}}), you need to follow the correction flow.<br />\r\nYou do this by following these steps:<br />\r\n1. Log in to the cabinet<br />\r\n2. Press the remove tile<br />\r\n3. Press the exclamation mark at the top right<br />\r\n4. Select the position you want to resolve<br />\r\n5. Press release keycop(s)<br />\r\n<br />\r\nTo resolve {{itemDescription}}, you need to contact the person it is registered to.<br />\r\nNOTE! This registration is a possibility, not a certainty!<br />\r\n<br />\r\n<b>Do not reply to this email, it has been sent from a 'no-reply address'.</b><br />\r\n", null },
                    { 61, "ar-AE", "unknown_content_position", "محتوى غير معروف", "عزيزي الزميل،<br />\r\n<br />\r\nفي الخزانة '{{cabinetName}}' عند الموقع '{{positionAlias}}' ({{positionNumber}})، تم وضع محتوى غير معروف.<br />\r\n<br />\r\nلحل موقع '{{positionAlias}}' ({{positionNumber}})، تحتاج إلى المرور بعملية التصحيح.<br />\r\nيمكنك القيام بذلك باتباع هذه الخطوات:<br />\r\n1. قم بتسجيل الدخول إلى الخزانة<br />\r\n2. اضغط على إزالة البلاط<br />\r\n3. اضغط على علامة التعجب في أعلى اليمين<br />\r\n4. اختر الموقع الذي تريد حله<br />\r\n5. اضغط على زر إطلاق المفاتيح<br />\r\n<br />\r\nاذهب خلال العناصر وتعامل معها.<br />\r\n<br />\r\n<b>لا ترد على هذا البريد الإلكتروني، تم إرساله عبر 'عنوان لا يقبل الرد'.</b><br />", null },
                    { 62, "ar-AE", "defect_position", "موقع معطل", "عزيزي الزميل،<br />\r\n<br />\r\nموقع الخزانة '{{cabinetName}}' '{{positionAlias}}' ({{positionNumber}}) به عيب.<br />\r\n<br />\r\n<b>لا ترد على هذا البريد الإلكتروني، تم إرساله عبر 'عنوان لا يقبل الرد'.</b><br />", null },
                    { 63, "ar-AE", "missing_content_position", "محتوى مفقود", "عزيزي الزميل،<br />\r\n<br />\r\nفي الخزانة '{{cabinetName}}' عند الموقع '{{positionAlias}}' ({{positionNumber}})، {{itemDescription}} مفقود.<br />\r\n<br />\r\nلحل موقع '{{positionAlias}}' ({{positionNumber}}) فقط، تحتاج إلى اتباع عملية التصحيح.<br />\r\nيمكنك القيام بذلك باتباع هذه الخطوات:<br />\r\n1. قم بتسجيل الدخول إلى الخزانة<br />\r\n2. اضغط على إزالة البلاط<br />\r\n3. اضغط على علامة التعجب في أعلى اليمين<br />\r\n4. اختر الموقع الذي تريد حله<br />\r\n5. اضغط على زر إطلاق المفاتيح<br />\r\n<br />\r\nلحل {{itemDescription}}، تحتاج إلى الاتصال بالشخص المسجل لديه.<br />\r\nملاحظة! هذا التسجيل هو احتمال، وليس يقين!<br />\r\n<br />\r\n<b>لا ترد على هذا البريد الإلكتروني، تم إرساله من 'عنوان لا يقبل الرد'.</b><br />", null }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 42);
        }
    }
}
