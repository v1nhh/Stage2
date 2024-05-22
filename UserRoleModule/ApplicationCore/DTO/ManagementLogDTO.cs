using CTAM.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using UserRoleModule.ApplicationCore.Enums;

namespace UserRoleModule.ApplicationCore.DTO
{
    public class ManagementLogDTO
    {
        public int ID { get; set; }

        public DateTime LogDT { get; set; }

        public LogLevel Level { get; set; }

        public LogSource Source { get; set; }

        public string Log { get; set; }

        public string CTAMUserUID { get; set; }

        public string CTAMUserName { get; set; }

        public string CTAMUserEmail { get; set; }

        public string ToColumnsString(IEnumerable<ManagementLogColumn> columns, string seperator)
        {
            var row = new StringBuilder();
            string sep = "";
            foreach (var col in columns)
            {
                row.Append(sep);
                sep = seperator; // only first loop empty
                row.Append("\"");
                string cel = col switch
                                {
                                    ManagementLogColumn.Source => Source.ToString(),
                                    ManagementLogColumn.Level => Level.GetTranslation(),
                                    ManagementLogColumn.Log => Log,
                                    ManagementLogColumn.LogDT => LogDT.ToLocalDateTimeString(),
                                    ManagementLogColumn.UserUID => CTAMUserUID,
                                    ManagementLogColumn.UserName => CTAMUserName,
                                    ManagementLogColumn.UserEmail => CTAMUserEmail,
                                    _ => ""
                                } ?? "";
                row.Append(cel.Replace("\"", "\"\"").Replace("\n", "\r"));
                row.Append("\"");
            }

            return row.ToString();
        }
    }
}
