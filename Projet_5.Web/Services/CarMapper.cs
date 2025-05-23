﻿using Projet_5.Core.Models.Entities;
using Projet_5.Web.Models.ViewModel;

namespace Projet_5.Web.Services
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
                IsAvailable = carForSale.IsAvailable,
                AvailabilityDate = carForSale.AvailabilityDate,
                ImagePath = carForSale.ImagePath
            };
        }

        public void UpdateCarForSaleEntityFromViewModel(CarForSale carForSale, CarFormViewModel carFormViewModel)
        {
            carForSale.VinCode = carFormViewModel.VinCode;
            carForSale.BrandId = carFormViewModel.BrandId;
            carForSale.Model = carFormViewModel.Model;
            carForSale.Trim = carFormViewModel.Trim;
            carForSale.Year = carFormViewModel.Year;
            carForSale.PurchasePrice = carFormViewModel.PurchasePrice;
            carForSale.PurchaseDate = carFormViewModel.PurchaseDate;
            carForSale.AvailabilityDate = carFormViewModel.AvailabilityDate;
            carForSale.SalePrice = carFormViewModel.PurchasePrice + carFormViewModel.RepairCost + 500;
            carForSale.IsAvailable = carFormViewModel.IsAvailable;
            if (!string.IsNullOrWhiteSpace(carFormViewModel.ImagePath))
            {
                carForSale.ImagePath = carFormViewModel.ImagePath;
            }
        }

        public List<CarForPublicViewModel> MapToCarForPublicViewModels(IEnumerable<CarForSale> cars)
        {
            return cars.Select(c => MapToCarForPublicViewModel(c)).ToList();
        }
    }
}
