using Projet_5_App.Models.Entities;
using Projet_5_App.Models.ViewModel;

namespace Projet_5_App.Repositories
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
