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

        public async Task<List<CarForPublicViewModel>> GetAllCarsForPublicAsync()
        {
            var carsForSale = await _carForSaleRepository.GetAllCarsForSaleWithBrandNameAsync();
            var notDeletedCars = carsForSale.Where(c => !c.Deleted);
            return _carMappingService.MapToCarForPublicViewModels(carsForSale);
        }

        public async Task<CarForPublicViewModel?> GetCarForPublicByIdAsync(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null || !carForSale.IsAvailable || carForSale.Deleted)
            {
                return null;
            }

            return _carMappingService.MapToCarForPublicViewModel(carForSale);
        }

        public async Task<CarCreateViewModel?> GetCarCreateByIdAsync(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null)
            {
                return null;
            }

            return _carMappingService.MapToCarCreateViewModel(carForSale);
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

        public async Task ToggleAvailabilityAsync(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null) return;

            carForSale.IsAvailable = !carForSale.IsAvailable;
            await _carForSaleRepository.UpdateCarForSaleAsync(carForSale);
        }

        public async Task DeleteCarForSaleAsync(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale != null)
            {
                carForSale.Deleted = true;
                await _carForSaleRepository.UpdateCarForSaleAsync(carForSale);
            }
        }
    }
}

