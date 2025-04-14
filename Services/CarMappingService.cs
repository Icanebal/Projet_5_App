using Projet_5_App.Models.Entities;
using Projet_5_App.Models.ViewModel;

namespace Projet_5_App.Services
{
    public class CarMappingService
    {
        public CarCreateViewModel MapToCarCreateViewModel(CarForSale carForSale)
        {
            return new CarCreateViewModel
            {
                Id = carForSale.Id,
                VinCode = carForSale.VinCode,
                BrandId = carForSale.BrandId,
                Model = carForSale.Model,
                Trim = carForSale.Trim,
                Year = carForSale.Year,
                PurchasePrice = carForSale.PurchasePrice,
                PurchaseDate = carForSale.PurchaseDate,
                AvailabilityDate = carForSale.AvailabilityDate,
                SalePrice = carForSale.SalePrice,
                IsAvailable = carForSale.IsAvailable
            };
        }

        public CarForSale MapToCarForSaleEntity(CarCreateViewModel carCreateViewModel)
        {
            return new CarForSale
            {
                Id = carCreateViewModel.Id,
                VinCode = carCreateViewModel.VinCode,
                BrandId = carCreateViewModel.BrandId,
                Model = carCreateViewModel.Model,
                Trim = carCreateViewModel.Trim,
                Year = carCreateViewModel.Year,
                PurchasePrice = carCreateViewModel.PurchasePrice,
                PurchaseDate = carCreateViewModel.PurchaseDate,
                AvailabilityDate = carCreateViewModel.AvailabilityDate,
                SalePrice = carCreateViewModel.PurchasePrice + carCreateViewModel.RepairCost + 500,
                IsAvailable = carCreateViewModel.IsAvailable
            };
        }

        public CarForPublicViewModel MapToCarForPublicViewModel(CarForSale carForSale)
        {
            return new CarForPublicViewModel
            {
                Id = carForSale.Id,
                BrandName = carForSale.Brand.Name,
                Model = carForSale.Model,
                Trim = carForSale.Trim,
                Year = carForSale.Year,
                SalePrice = carForSale.SalePrice
            };
        }

        public List<CarForPublicViewModel> MapToCarForPublicViewModels(IEnumerable<CarForSale> cars)
        {
            return cars.Select(c => MapToCarForPublicViewModel(c)).ToList();
        }
    }
}
