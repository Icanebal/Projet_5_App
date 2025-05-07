namespace Projet_5_App.Models.ViewModel
{
    public class CarForPublicViewModel
    {
        public int Id { get; set; }
        public string BrandName { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Trim { get; set; } = string.Empty;
        public int Year { get; set; }
        public decimal SalePrice { get; set; }
        public bool EffectiveAvailability { get; set; }
        public bool IsAvailable { get; set; }
        public DateOnly? AvailabilityDate { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }

}
