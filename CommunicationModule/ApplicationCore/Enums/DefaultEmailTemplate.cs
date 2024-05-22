using System;
namespace CommunicationModule.ApplicationCore.Enums
{
    public enum DefaultEmailTemplate
    {
        WelcomeWebAndCabinetLogin,
        WelcomeWebLogin,
        WelcomeCabinetLogin,
        TemporaryPassword,
        PasswordChanged,
        ForgotPassword,
        UserModified,
        UserDeleted,
        StockBelowMinimum,
        StockAtMinimalStockLevel,
        ItemStatusChangedToDefect,
        PersonalItemStatusChangedToDefect,
        PersonalItemStatusChangedToRepaired,
        PersonalItemStatusChangedToReplaced,
        PersonalItemStatusChangedToSwappedBack,
        PincodeChanged,
        IncorrectReturnClosedUIP,
        IncorrectReturnCreatedUIP,
        UnknownContentPosition,
        DefectPosition,
        MissingContentPosition
    }

    public static class DefaultEmailTemplateExtension
    {

        public static string GetName(this DefaultEmailTemplate emailTemplate)
        {
            return emailTemplate switch
            {
                DefaultEmailTemplate.WelcomeWebAndCabinetLogin => "welcome_web_and_cabinet_login",
                DefaultEmailTemplate.WelcomeWebLogin => "welcome_web_login",
                DefaultEmailTemplate.WelcomeCabinetLogin => "welcome_cabinet_login",
                DefaultEmailTemplate.TemporaryPassword => "temporary_password",
                DefaultEmailTemplate.PasswordChanged => "password_changed",
                DefaultEmailTemplate.ForgotPassword => "forgot_password",
                DefaultEmailTemplate.UserModified => "user_modified",
                DefaultEmailTemplate.UserDeleted => "user_deleted",
                DefaultEmailTemplate.StockBelowMinimum => "stock_below_minimum",
                DefaultEmailTemplate.StockAtMinimalStockLevel => "stock_at_minimum_level",
                DefaultEmailTemplate.ItemStatusChangedToDefect => "item_status_changed_to_defect",
                DefaultEmailTemplate.PersonalItemStatusChangedToDefect => "personal_item_status_changed_to_defect",
                DefaultEmailTemplate.PersonalItemStatusChangedToRepaired => "personal_item_status_changed_to_repaired",
                DefaultEmailTemplate.PersonalItemStatusChangedToReplaced => "personal_item_status_changed_to_replaced",
                DefaultEmailTemplate.PersonalItemStatusChangedToSwappedBack => "personal_item_status_changed_to_swappedback",
                DefaultEmailTemplate.PincodeChanged => "pincode_changed",
                DefaultEmailTemplate.IncorrectReturnClosedUIP => "incorrect_return_closed_uip",
                DefaultEmailTemplate.IncorrectReturnCreatedUIP => "incorrect_return_created_uip",
                DefaultEmailTemplate.UnknownContentPosition => "unknown_content_position",
                DefaultEmailTemplate.DefectPosition => "defect_position",
                DefaultEmailTemplate.MissingContentPosition => "missing_content_position",
                _ => throw new NotImplementedException()
            };
        }
    }
}
