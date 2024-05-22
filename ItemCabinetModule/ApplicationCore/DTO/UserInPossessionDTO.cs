using System;
using System.Collections.Generic;
using System.Text;
using CTAM.Core.Utilities;
using ItemCabinetModule.ApplicationCore.Enums;
using ItemModule.ApplicationCore.DTO;

namespace ItemCabinetModule.ApplicationCore.DTO
{
    public class UserInPossessionDTO
    {
        public Guid ID { get; set; }

        public int ItemID { get; set; }
        public ItemDTO Item { get; set; }

        public string CTAMUserUIDOut { get; set; }

        public string CTAMUserNameOut { get; set; }

        public string CTAMUserEmailOut { get; set; }

        public DateTime? OutDT { get; set; }

        public int? CabinetPositionIDOut { get; set; }

        public string CabinetNumberOut { get; set; }

        public string CabinetNameOut { get; set; }

        public string CTAMUserUIDIn { get; set; }

        public string CTAMUserNameIn { get; set; }

        public string CTAMUserEmailIn { get; set; }

        public DateTime? InDT { get; set; }

        public int? CabinetPositionIDIn { get; set; }

        public string CabinetNumberIn { get; set; }

        public string CabinetNameIn { get; set; }

        public DateTime? ReturnBeforeDT { get; set; }

        public UserInPossessionStatus Status { get; set; }

        public DateTime CreatedDT { get; set; }

        public string ToColumnsString(IEnumerable<UserInPossessionColumn> columns, string seperator)
        {
            var row = new StringBuilder();
            string sep = "";
            foreach (var col in columns)
            {
                row.Append(sep);
                sep = seperator; // only first loop empty
                row.Append("\"");
                string cel = col switch {
                                    UserInPossessionColumn.Status => Status.GetTranslation(),
                                    UserInPossessionColumn.OutCTAMUserName => CTAMUserNameOut,
                                    UserInPossessionColumn.OutCTAMUserEmail => CTAMUserEmailOut,
                                    UserInPossessionColumn.OutDT => OutDT?.ToLocalDateTimeString(),
                                    UserInPossessionColumn.CabinetPositionOutCabinetName => CabinetNameOut,
                                    UserInPossessionColumn.InDT => InDT?.ToLocalDateTimeString(),
                                    UserInPossessionColumn.InCTAMUserName => CTAMUserNameIn,
                                    UserInPossessionColumn.InCTAMUserEmail => CTAMUserEmailIn,
                                    UserInPossessionColumn.CabinetPositionInCabinetName => CabinetNameIn,
                                    UserInPossessionColumn.Item_Description => Item.Description,
                                    UserInPossessionColumn.ItemType_Description => Item.ItemType.Description,
                                    _ => ""
                                } ?? "";
                row.Append(cel.Replace("\"", "\"\"").Replace("\n", "\r"));
                row.Append("\"");
            }

            return row.ToString();
        }
    }
}
