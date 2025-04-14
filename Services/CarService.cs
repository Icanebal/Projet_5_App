using Projet_5_App.Models.ViewModel;
using Projet_5_App.Repositories;

namespace Projet_5_App.Services
{
    public class CarService
    {
        private readonly ICarForSaleRepository _carForSaleRepository;
        private readonly CarMappingService _carMappingService;
        public CarService(ICarForSaleRepository carForSaleRepository, CarMappingService carMappingService)
        {
            _carForSaleRepository = carForSaleRepository;
            _carMappingService = carMappingService;
        }
        public async Task<List<CarForPublicViewModel>> GetAvailableCarsForPublicAsync()
        {
            var carsForSale = await _carForSaleRepository.GetAllCarsForSaleWithBrandNameAsync();
            var availableCars = carsForSale.Where(c => c.IsAvailable);
            return _carMappingService.MapToCarForPublicViewModels(availableCars);
        }
        public async Task AddCarForSaleFromViewModelAsync(CarCreateViewModel carCreateViewModel)
        {
            var carForSale = _carMappingService.MapToCarForSaleEntity(carCreateViewModel);

            await _carForSaleRepository.AddCarForSaleAsync(carForSale);
        }

        public async Task UpdateCarForSaleFromViewModelAsync(CarCreateViewModel carCreateViewModel)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(carCreateViewModel.Id);
            if (carForSale == null) return;

            var updatedCarForSale = _carMappingService.MapToCarForSaleEntity(carCreateViewModel);
            updatedCarForSale.Id = carForSale.Id;
            await _carForSaleRepository.UpdateCarForSaleAsync(updatedCarForSale);
        }

        public async Task<CarCreateViewModel?> GetCarCreateViewModelByIdAsync(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null) return null;

            return _carMappingService.MapToCarCreateViewModel(carForSale);
        }
    }
}

