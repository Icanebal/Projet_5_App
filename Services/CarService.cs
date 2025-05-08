using Microsoft.AspNetCore.Mvc.Rendering;
using Projet_5.Web.Models.ViewModel;
using Projet_5.Core.Interfaces;

namespace Projet_5.Web.Services
{
    public class CarService
    {
        private readonly ICarForSaleRepository _carForSaleRepository;
        private readonly CarMapper _carMappingService;
        private readonly FileService _fileService;
        public CarService(ICarForSaleRepository carForSaleRepository, CarMapper carMappingService, FileService fileService)
        {
            _carForSaleRepository = carForSaleRepository;
            _carMappingService = carMappingService;
            _fileService = fileService;
        }

        public async Task<List<CarForPublicViewModel>> GetAllCarsForPublicAsync()
        {
            var carsForSale = await _carForSaleRepository.GetAllCarsForSaleWithBrandNameAsync();
            var notDeletedCars = carsForSale.Where(c => !c.Deleted);
            return _carMappingService.MapToCarForPublicViewModels(notDeletedCars);
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

        public async Task<CarForPublicViewModel?> GetCarForAdminByIdAsync(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null || carForSale.Deleted)
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

        public async Task<List<SelectListItem>> GetAllBrandsAsSelectListAsync()
        {
            var brands = await _carForSaleRepository.GetAllBrandsAsync();

            return brands.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            }).ToList();
        }

        public async Task AddCarForSaleFromViewModelAsync(CarFormViewModel carFormViewModel)
        {
            if (carFormViewModel.ImageFile != null && carFormViewModel.ImageFile.Length > 0)
            {
                carFormViewModel.ImagePath = await _fileService.SaveImageAsync(carFormViewModel.ImageFile);
            }

            var carForSale = _carMappingService.MapToCarForSaleEntity(carFormViewModel);

            await _carForSaleRepository.AddCarForSaleAsync(carForSale);
        }

        public async Task UpdateCarForSaleFromViewModelAsync(CarFormViewModel carFormViewModel)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(carFormViewModel.Id);
            if (carForSale == null) return;

            if (carFormViewModel.ImageFile != null && carFormViewModel.ImageFile.Length > 0)
            {
                carFormViewModel.ImagePath = await _fileService.SaveImageAsync(carFormViewModel.ImageFile);
            }


            _carMappingService.UpdateCarForSaleEntityFromViewModel(carForSale, carFormViewModel);
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

