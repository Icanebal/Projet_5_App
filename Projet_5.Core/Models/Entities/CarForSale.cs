using System.ComponentModel.DataAnnotations.Schema;
namespace Projet_5.Core.Models.Entities
{
    public class CarForSale
    {
        public int Id { get; set; }
        public string VinCode { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Trim { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateOnly? PurchaseDate { get; set; }
        public decimal SalePrice { get; set; }
        public bool IsAvailable { get; set; } = true;
        public DateOnly? AvailabilityDate { get; set; }
        public bool Deleted { get; set; } = false;
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
        public string ImagePath { get; set; } = string.Empty;

        [NotMapped]
        public bool EffectiveAvailability => IsAvailable && (AvailabilityDate == null || AvailabilityDate <= DateOnly.FromDateTime(DateTime.Today));
    }
}
