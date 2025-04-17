using Projet_5_App.Models.ViewModel;
using Projet_5_App.Repositories;

namespace Projet_5_App.Services
{
    public class CarService
    {
        private readonly ICarForSaleRepository _carForSaleRepository;
        private readonly CarMapper _carMappingService;
        public CarService(ICarForSaleRepository carForSaleRepository, CarMapper carMappingService)
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

        public async Task<CarFormViewModel?> GetCarFormViewModelByIdAsync(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null)
            {
                return null;
            }

            return _carMappingService.MapToCarFormViewModel(carForSale);
        }

        public async Task AddCarForSaleFromViewModelAsync(CarFormViewModel carFormViewModel)
        {
            var carForSale = _carMappingService.MapToCarForSaleEntity(carFormViewModel);

            await _carForSaleRepository.AddCarForSaleAsync(carForSale);
        }

        public async Task UpdateCarForSaleFromViewModelAsync(CarFormViewModel carFormViewModel)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(carFormViewModel.Id);
            if (carForSale == null) return;

            var updatedCarForSale = _carMappingService.MapToCarForSaleEntity(carFormViewModel);
            updatedCarForSale.Id = carForSale.Id;
            await _carForSaleRepository.UpdateCarForSaleAsync(updatedCarForSale);
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

