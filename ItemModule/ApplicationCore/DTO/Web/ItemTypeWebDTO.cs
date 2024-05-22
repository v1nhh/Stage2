namespace ItemModule.ApplicationCore.DTO.Web
{
    public class ItemTypeWebDTO
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int? MaxQtyToPick { get; set; }
        public bool IsChecked { get; set; }
    }
}
