namespace Projet_5_App.Models.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CarForSale> CarsForSale { get; set; } = new List<CarForSale>();
    }
}
