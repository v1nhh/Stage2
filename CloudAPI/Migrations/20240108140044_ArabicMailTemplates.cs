using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CloudAPI.Migrations
{
    public partial class ArabicMailTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Communication",
                table: "MailTemplate",
                columns: new[] { "ID", "LanguageCode", "Name", "Subject", "Template", "UpdateDT" },
                values: new object[,]
                {
                    { 33, "ar-AE", "welcome_web_and_cabinet_login", "مرحبًا بك في CTAM - بيانات تسجيل الدخول الخاصة بك", "عزيزي {{name}}،<br /><br />مرحبًا بك في CTAM!<br /><br />تفاصيل تسجيل الدخول الخاصة بك<br />اسم المستخدم: {{email}}<br />كلمة المرور المؤقتة: {{password}}<br />رمز الدخول: {{loginCode}}<br />الرقم السري: {{pinCode}}", null },
                    { 34, "ar-AE", "welcome_web_login", "مرحبًا بك في CTAM - بيانات تسجيل الدخول الخاصة بك", "عزيزي {{name}}،<br /><br />مرحبًا بك في CTAM!<br /><br />تفاصيل تسجيل الدخول الخاصة بك<br />اسم المستخدم: {{email}}<br />كلمة المرور المؤقتة: {{password}}", null },
                    { 35, "ar-AE", "temporary_password", "إعادة تعيين كلمة المرور", "عزيزي {{name}}،<br /><br />كلمة المرور المؤقتة الخاصة بك هي {{password}}<br />قم بتغييرها فور تسجيل الدخول!", null },
                    { 36, "ar-AE", "password_changed", "كلمة مرور جديدة", "عزيزي {{name}}،<br /><br />تم تغيير كلمة المرور الخاصة بك بنجاح!", null },
                    { 37, "ar-AE", "forgot_password", "نسيت كلمة المرور", "عزيزي {{name}}،<br /><br />اضغط على الرابط أدناه لتغيير كلمة المرور الخاصة بك.<br /><br /><a href='{{link}}' target='_blank'>تغيير كلمة المرور</a><br /><br />أنت تتلقى هذا البريد الإلكتروني لأنك ضغطت مؤخرًا على زر 'نسيت كلمة المرور' على موقع CaptureTech. إذا لم تكن قد بدأت هذا التغيير، يرجى الاتصال بالمسؤول الخاص بك فورًا.", null },
                    { 38, "ar-AE", "user_modified", "تغييراتك", "عزيزي {{name}}،<br /><br />راجع التغييرات الخاصة بك أدناه:<br /><br /><table>{{changes}}</table>", null },
                    { 39, "ar-AE", "user_deleted", "تم حذف حسابك في CTAM", "عزيزي {{name}}،<br /><br />تم حذف حسابك في CTAM من قبل المسؤول", null },
                    { 40, "ar-AE", "stock_below_minimum", "مخزون الخزانة أقل من الحد الأدنى", "عزيز مدير الخزانة،<br /><br />مخزون {{itemTypeDescription}} في الخزانة '{{cabinetNumber}}, {{cabinetName}}' في الموقع '{{cabinetLocationDescr}}' أقل ({{actualStock}}) من الحد الأدنى للمخزون ({{minimalStock}}).", null },
                    { 41, "ar-AE", "stock_at_minimum_level", "مخزون الخزانة عاد إلى المستوى الأدنى", "عزيز مدير الخزانة،<br /><br />مخزون {{itemTypeDescription}} في الخزانة '{{cabinetNumber}}, {{cabinetName}}' في الموقع '{{cabinetLocationDescr}}' قد بلغ مستوى الحد الأدنى للمخزون ({{minimalStock}}).", null },
                    { 42, "ar-AE", "item_status_changed_to_defect", "تم الإبلاغ عن عيب في العنصر", "عزيز مدير الخزانة، <br /><br />تم الإبلاغ عن عطل في العنصر '{{itemDescription}}' من نوع '{{itemTypeDescription}}' من قبل المستخدم '{{userName}}' مع رمز الخطأ: '{{errorCodeDescription}}'. <br /><br />يمكنك استلامه من الخزانة '{{cabinetName}}' في الموضع '{{positionAlias}}'.", null },
                    { 43, "ar-AE", "personal_item_status_changed_to_defect", "تم الإبلاغ عن عيب {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}", "تم الإبلاغ عن عيب في العنصر {{putItemDescription}} بتاريخ {{actionDT}} من قبل {{userName}} مع خطأ: {{errorCodeDescription}}. الخزانة {{cabinetName}} في خزانة {{positionAlias}} بالموقع {{cabinetLocationDescr}}. <br /><br />المستخدم {{userName}} لديه الآن عنصر بديل {{takeItemDescription}}.", null },
                    { 44, "ar-AE", "personal_item_status_changed_to_repaired", "تم إصلاح العنصر الشخصي الخاص بك", "عزيز {{userName}}, <br /><br />تم الإبلاغ عن إصلاح العنصر الشخصي '{{itemDescription}}' من نوع '{{itemTypeDescription}}'. <br /><br />يمكنك استبداله مرة أخرى في الخزانة '{{cabinetName}}' على الموضع '{{positionAlias}}'.", null },
                    { 45, "ar-AE", "personal_item_status_changed_to_replaced", "تم استبدال العنصر الشخصي الخاص بك", "عزيز {{userName}}, <br /><br />تم الإبلاغ عن استبدال العنصر الشخصي '{{itemDescription}}' من نوع '{{itemTypeDescription}}'. <br /><br />يمكنك استلامه في الخزانة '{{cabinetName}}' على الموضع '{{positionAlias}}'.", null },
                    { 46, "ar-AE", "personal_item_status_changed_to_swappedback", "تم إرجاع العنصر المؤقت {{putItemTypeDescription}} {{cabinetLocationDescr}} {{errorCodeDescription}}", "قام المستخدم بإعادة تبديل العنصر المؤقت. <br /><br />تم تبديل العنصر المؤقت <span style='font-style:italic'>'{{putItemDescription}}'</span> بمرجع خارجي <span style='font-style:italic'>'{{putItemExternalReferenceID}}'</span> من نوع <span style='font-style:italic'>'{{putItemTypeDescription}}'</span> مرة أخرى في <span style='font-style:italic'>'{{actionDT}}'</span> بواسطة المستخدم <span style='font-style:italic'>'{{userName}}'</span> ويجب فصله عن المستخدم وتحديث حالته إلى ‘متوفر’. هذا العنصر المؤقت له رمز خطأ: <span style='font-style:italic'>'{{errorCodeDescription}}'</span>. <br /><br />المستخدم <span style='font-style:italic'>'{{userName}}'</span> يمتلك منذ <span style='font-style:italic'>'{{actionDT}}'</span> العنصر <span style='font-style:italic'>'{{takeItemDescription}}'</span> بمرجع خارجي <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> من نوع <span style='font-style:italic'>'{{takeItemTypeDescription}}'</span>؛ في CMDB يجب فصل العنصر <span style='font-style:italic'>'{{takeItemDescription}}'</span> بمرجع خارجي <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> عن المستخدم <span style='font-style:italic'>'{{userName}}'</span> وتحديث حالته إلى ‘مستخدم’. <br /><br />يرجى بعد تغيير CMDB تحديث حالة الحادثة للعنصر <span style='font-style:italic'>'{{takeItemDescription}}'</span> بمرجع خارجي <span style='font-style:italic'>'{{takeItemExternalReferenceID}}'</span> إلى ‘تم الحل’.", null },
                    { 47, "ar-AE", "welcome_cabinet_login", "رقمك السري لتسجيل الدخول في الخزانة", "عزيز {{userName}},<br /><br />رقمك السري هو {{pinCode}}.", null },
                    { 48, "ar-AE", "pincode_changed", "إعادة تعيين الرقم السري", "عزيز {{name}},<br /><br />رقمك السري الجديد هو {{pinCode}}<br />", null }
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

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "Communication",
                table: "MailTemplate",
                keyColumn: "ID",
                keyValue: 48);
        }
    }
}
