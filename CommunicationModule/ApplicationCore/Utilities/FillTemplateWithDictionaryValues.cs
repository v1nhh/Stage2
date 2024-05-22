using CTAM.Core.Exceptions;
using CTAMSharedLibrary.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace CommunicationModule.ApplicationCore.Utilities
{
    public static class FillTemplate
    {
        public static string FillTemplateWithDictionaryValues(this string template, string templateName, Dictionary<string, string> values)
        {
            var valuesRegex = @"\{\{.*?\}\}";
            var expectedValues = Regex.Matches(template, valuesRegex).Select(m => m.ToString());
            var mailBody = template;
            foreach (string expectedValue in expectedValues)
            {
                var valueName = expectedValue.Replace("{{", "").Replace("}}", "");
                if (!values.ContainsKey(valueName))
                {
                    throw new CustomException(HttpStatusCode.NotFound, CloudTranslations.communications_apiExceptions_mailTemplateValueNotFound,
                                                                    new Dictionary<string, string> { { "templateName", templateName }, { "valueName", valueName } });
                }
                mailBody = mailBody.Replace(expectedValue, values[valueName]);
            }
            return mailBody;
        }
    }
}
