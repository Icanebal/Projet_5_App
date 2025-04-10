namespace Projet_5_App.Models.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateOnly SaleDate { get; set; }
        public int CarForSaleId { get; set; }
        public CarForSale CarForSale { get; set; }
    }
}
