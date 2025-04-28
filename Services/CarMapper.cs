using Projet_5_App.Models.Entities;
using Projet_5_App.Models.ViewModel;

namespace Projet_5_App.Services
{
    public class CarMapper
    {
        public CarFormViewModel MapToCarFormViewModel(CarForSale carForSale)
        {
            return new CarFormViewModel
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
                IsAvailable = carForSale.IsAvailable,
                ImagePath = carForSale.ImagePath
            };
        }

        public CarForSale MapToCarForSaleEntity(CarFormViewModel carFormViewModel)
        {
            return new CarForSale
            {
                Id = carFormViewModel.Id,
                VinCode = carFormViewModel.VinCode,
                BrandId = carFormViewModel.BrandId,
                Model = carFormViewModel.Model,
                Trim = carFormViewModel.Trim,
                Year = carFormViewModel.Year,
                PurchasePrice = carFormViewModel.PurchasePrice,
                PurchaseDate = carFormViewModel.PurchaseDate,
                AvailabilityDate = carFormViewModel.AvailabilityDate,
                SalePrice = carFormViewModel.PurchasePrice + carFormViewModel.RepairCost + 500,
                IsAvailable = carFormViewModel.IsAvailable,
                ImagePath = carFormViewModel.ImagePath
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
                SalePrice = carForSale.SalePrice,
                EffectiveAvailability = carForSale.EffectiveAvailability,
                ImagePath = carForSale.ImagePath
            };
        }

        public List<CarForPublicViewModel> MapToCarForPublicViewModels(IEnumerable<CarForSale> cars)
        {
            return cars.Select(c => MapToCarForPublicViewModel(c)).ToList();
        }
    }
}
