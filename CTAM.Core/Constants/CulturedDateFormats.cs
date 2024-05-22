using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace CTAM.Core.Constants
{
    public class CountryCulture
    {
        public string Country { get; set; }
        public string DateFormat { get; set; }
        public string ISO639_2_CountryCode { get; set; }
        public string ISO639_2_Lang { get; set; }
        public string ISO_3166_CountryCode { get; set; }
        public string Language { get; set; }
    }

    // Need this class instead of CultureInfo because
    // CultureInfo.CreateSpecificCulture(culture).DateTimeFormat.ShortDatePattern returns different values
    // dependent on OS
    public static class CulturedDateFormats
    {
        private static readonly Dictionary<string, CountryCulture> countriesCultureData;

        static CulturedDateFormats()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "CTAM.Core.Resources.CountriesCultureData.json";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonFile = reader.ReadToEnd();
                    countriesCultureData = JsonConvert.DeserializeObject<Dictionary<string, CountryCulture>>(jsonFile);
                }
            }
        }

        public static string GetDateFormat(string culture)
        {
            try
            {
                return countriesCultureData[culture].DateFormat;
            }
            catch
            {
                throw new CultureNotFoundException($"Culture with name '{culture}' is not found");
            }
        }
    }
}
