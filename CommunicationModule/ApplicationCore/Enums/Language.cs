using System;
namespace CommunicationModule.ApplicationCore.Enums
{
    public enum Language
    {
        Dutch = 0,
        EnglishUS = 1,
        EnglishGB = 2,
        Arabic = 3,
        Swedish = 4,
        French = 5,
        German = 6
    }

    public static class LanguageExtension
    {

        public static string GetLanguageCode(this Language language)
        {
            return language switch
            {
                Language.Dutch => "nl-NL",
                Language.EnglishUS => "en-US",
                Language.EnglishGB => "en-GB",
                Language.Arabic => "ar-AE",
                Language.Swedish => "sv-SE",
                Language.French => "fr-FR",
                Language.German => "de-DE",
                _ => throw new NotImplementedException()
            };
        }
    }
}
