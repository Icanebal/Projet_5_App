namespace Projet_5.Core.Models.Entities
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<CarForSale> CarsForSale { get; set; } = new List<CarForSale>();
    }
}
