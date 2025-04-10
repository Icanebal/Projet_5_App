namespace Projet_5_App.Models.Entities
{
    public class Repair
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public int CarForSaleId { get; set; }
        public bool IsRepaired { get; set; } = false;
        public CarForSale CarForSale { get; set; }
    }
}
