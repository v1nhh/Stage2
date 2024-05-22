using CabinetModule.ApplicationCore.Enums;
using CTAM.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using UserRoleModule.ApplicationCore.Enums;

namespace CabinetModule.ApplicationCore.DTO
{
    public class CabinetLogDTO
    {
        public int ID { get; set; }

        public DateTime LogDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public LogLevel Level { get; set; }

        public string CabinetNumber { get; set; }

        public string CabinetName { get; set; }

        public LogSource Source { get; set; }

        public string LogResourcePath { get; set; }
        public string Parameters { get; set; }

        public string Log { get; set; }

        public string ToColumnsString(IEnumerable<CabinetLogColumn> columns, string seperator)
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
                                    CabinetLogColumn.CabinetNumber => CabinetNumber,
                                    CabinetLogColumn.CabinetName => CabinetName,
                                    CabinetLogColumn.Level => Level.GetTranslation(),
                                    CabinetLogColumn.Source => Source.ToString(),
                                    CabinetLogColumn.Log => Log,
                                    CabinetLogColumn.LogDT => LogDT.ToLocalDateTimeString(),
                                    _ => ""
                                } ?? "";

                row.Append(cel.Replace("\"", "\"\"").Replace("\n", "\r"));
                row.Append("\"");
            }

            return row.ToString();
        }
    }
}
