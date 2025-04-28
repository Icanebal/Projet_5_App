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
        public bool Deleted { get; set; } = false;
        public Sale? Sale { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
        public ICollection<Repair> Repairs { get; set; } = new List<Repair>();
        public string ImagePath { get; set; } = string.Empty;

        [NotMapped]
        public decimal TotalCost => PurchasePrice + Repairs.Sum(r => r.Cost) + 500;

        public bool EffectiveAvailability => IsAvailable && (AvailabilityDate == null || AvailabilityDate <= DateOnly.FromDateTime(DateTime.Today));
    }
}
