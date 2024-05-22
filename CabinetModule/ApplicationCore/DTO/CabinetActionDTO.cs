using CabinetModule.ApplicationCore.Enums;
using CTAM.Core.Utilities;
using LocalAPI.ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CabinetModule.ApplicationCore.DTO
{
    public class CabinetActionDTO
    {
        public Guid ID { get; set; }

        public string CabinetNumber { get; set; }

        public string CabinetName { get; set; }

        public string PositionAlias { get; set; }

        public string CTAMUserUID { get; set; }

        public string CTAMUserName { get; set; }

        public string CTAMUserEmail { get; set; }

        public DateTime ActionDT { get; set; }

        public DateTime? UpdateDT { get; set; }

        public CabinetActionStatus Action { get; set; }

        public string TakeItemDescription { get; set; }
        public int? TakeItemID { get; set; }

        public string PutItemDescription { get; set; }
        public int? PutItemID { get; set; }

        public string ErrorCodeDescription { get; set; }
        
        public string LogResourcePath { get; set; }

        public string Log { get; set; }

        public CorrectionStatus? CorrectionStatus { get; set; }

        public string ToColumnsString(IEnumerable<CabinetActionLogColumn> columns, string seperator)
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
                                    CabinetActionLogColumn.CabinetNumber => CabinetNumber,
                                    CabinetActionLogColumn.CabinetName => CabinetName,
                                    CabinetActionLogColumn.PositionAlias => PositionAlias,
                                    CabinetActionLogColumn.UserUID => CTAMUserUID,
                                    CabinetActionLogColumn.UserName => CTAMUserName,
                                    CabinetActionLogColumn.UserEmail => CTAMUserEmail,
                                    CabinetActionLogColumn.ActionDT => ActionDT.ToLocalDateTimeString(),
                                    CabinetActionLogColumn.Action => Action.GetTranslation(),
                                    CabinetActionLogColumn.TakeItemDescription => TakeItemDescription,
                                    CabinetActionLogColumn.PutItemDescription => PutItemDescription,
                                    CabinetActionLogColumn.ErrorCodeDescription => ErrorCodeDescription,
                                    _ => ""
                                } ?? "";
                row.Append(cel.Replace("\"", "\"\"").Replace("\n", "\r"));
                row.Append("\"");
            }

            return row.ToString();
        }
    }
}
