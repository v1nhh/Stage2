using CabinetModule.ApplicationCore.Entities;
using CloudAPI.ApplicationCore.DTO.Integration;
using CTAM.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudAPI.Integrations.NationalePolitie.Requests
{
    public class IncidentNP
    {
        public string AffectedCI { get; set; }
        public string Contact { get; set; }
        public string ServiceRecipient { get; set; }
        public string[] Description { get; set; }
        public string Location { get; set; }
        public string[] RelatedCI { get; set; }
        public string[] Solution { get; set; }
        public string Title { get; set; }
    }
    public class SwapRequest : GenericRequest
    {
        public IncidentNP IncidentNP { get; set; } = new IncidentNP();

        public override async Task CollectDataAsync()
        {
            var cabinetAction = Context != null && Context.GetType().Equals(typeof(CabinetAction)) ? (CabinetAction)Context : null;
            if (cabinetAction == null) throw new ArgumentException("Not a valid Request Context received.");

            var cabinet = await MainDbContext.Cabinet().Where(c => c.CabinetNumber.Equals(cabinetAction.CabinetNumber)).FirstOrDefaultAsync();
            var putItem = await MainDbContext.Item().Include(i => i.ItemType).Where(c => c.ID.Equals(cabinetAction.PutItemID)).FirstOrDefaultAsync();
            var takeItem = await MainDbContext.Item().Include(i => i.ItemType).Where(c => c.ID.Equals(cabinetAction.TakeItemID)).FirstOrDefaultAsync();
            var errorCode = await MainDbContext.ErrorCode().Where(e => e.Description.Equals(cabinetAction.ErrorCodeDescription)).FirstOrDefaultAsync();

            IncidentNP.AffectedCI = putItem.ExternalReferenceID;
            IncidentNP.Contact = cabinetAction.CTAMUserName;
            IncidentNP.ServiceRecipient = cabinetAction.CTAMUserName;
            IncidentNP.Description = new string[] { $"Item {cabinetAction.PutItemDescription} met CI-nummer {putItem.ExternalReferenceID} van het type {putItem.ItemType.Description} is op {cabinetAction.ActionDT.ToString("dd-MM-yyyy")}, {cabinetAction.ActionDT.ToString("HH:mm")} defect gemeld door gebruiker {cabinetAction.CTAMUserName} met errorcode: {errorCode.Code} {cabinetAction.ErrorCodeDescription}. U kunt het ophalen aan de IBK {cabinet.Name} op locatie {cabinet.LocationDescr} {cabinet.Description}. Gebruiker {cabinetAction.CTAMUserName} heeft nu tijdelijk vervangend item {cabinetAction.TakeItemDescription} met CI-nummer {takeItem.ExternalReferenceID} van het type {takeItem.ItemType.Description} in gebruik." };
            IncidentNP.Location = cabinet.LocationDescr;
            IncidentNP.RelatedCI = new string[] { takeItem.ExternalReferenceID };
            IncidentNP.Solution = new string[] { $"DEFECTE {putItem.Description} IS OMGERUILD VOOR EEN VERVANGENDE" };
            IncidentNP.Title = $"IBK {cabinetAction.CabinetName}: verstoring item {putItem.ItemType.Description} {errorCode.Description}";
        }

        public override string GetJsonBody()
        {
            var body = MergeJsonBodies(JsonSerializer.Serialize(IncidentNP), APISettingBody);
            var rootBody = new Dictionary<string, object>
            {
                { "IncidentNP", JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(body) }
            };
            return JsonSerializer.Serialize(rootBody);
        }
    }
}
