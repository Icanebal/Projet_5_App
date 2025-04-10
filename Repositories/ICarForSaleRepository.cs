using Projet_5_App.Models.Entities;
using Projet_5_App.Models.ViewModel;

namespace Projet_5_App.Repositories
{
    public interface ICarForSaleRepository
    {
        Task<IEnumerable<CarForSale>> GetAllCarsForSaleAsync();
        Task<IEnumerable<CarForSale>> GetAllCarsForSaleWithBrandNameAsync();
        Task<CarForSale?> GetCarForSaleByIdAsync(int id);
        Task AddCarForSaleFromViewModelAsync(CarForSaleViewModel carForSaleViewModel);
        Task UpdateCarForSaleFromViewModelAsync(int id, CarForSaleViewModel carForSaleViewModel);
        Task UpdateCarForSaleAsync(CarForSale carForSale);
        Task DeleteCarForSaleAsync(int id);
    }
}
