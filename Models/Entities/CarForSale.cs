using System.ComponentModel.DataAnnotations.Schema;
namespace Projet_5_App.Models.Entities
{
    public class CarForSale
    {
        public int Id { get; set; }
        public string VinCode { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Trim { get; set; } = string.Empty;
        public int Year { get; set; }

        public decimal PurchasePrice { get; set; }
        public DateOnly PurchaseDate { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateOnly? AvailabilityDate { get; set; }
        public decimal SalePrice { get; set; }

        public Sale Sale { get; set; } = null!;
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
        public ICollection<Repair> Repairs { get; set; } = new List<Repair>();

        [NotMapped]
        public decimal TotalCost => PurchasePrice + Repairs.Sum(r => r.Cost);
    }
}
