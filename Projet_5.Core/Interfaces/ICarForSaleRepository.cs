using Projet_5.Core.Models.Entities;

namespace Projet_5.Core.Interfaces
{
    public interface ICarForSaleRepository
    {
        Task<IEnumerable<CarForSale>> GetAllCarsForSaleWithBrandNameAsync();
        Task<CarForSale?> GetCarForSaleByIdAsync(int id);
        Task<List<Brand>> GetAllBrandsAsync();
        Task AddCarForSaleAsync(CarForSale carForSale);
        Task UpdateCarForSaleAsync(CarForSale carForSale);
    }
}
