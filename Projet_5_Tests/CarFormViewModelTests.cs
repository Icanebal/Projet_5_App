using System.ComponentModel.DataAnnotations;
using Projet_5.Web.Models.ViewModel;

namespace Projet_5_Tests
{
    public class CarFormViewModelTests
    {
        private bool ValidateModel(CarFormViewModel carFormViewModel, out List<ValidationResult> errorResults)
        {
            var context = new ValidationContext(carFormViewModel);
            errorResults = new List<ValidationResult>();
            return Validator.TryValidateObject(carFormViewModel, context, errorResults, true);
        }

        [Fact]
        public void CarCreateViewModel_ShouldBeValidWithCorrectData()
        {
            var carFormViewModel = new CarFormViewModel
            {
                VinCode = "12345678901234567",
                BrandId = 1,
                Model = "Civic",
                Trim = "Sport",
                Year = 2022,
                PurchasePrice = 15000,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Today),
                AvailabilityDate = DateOnly.FromDateTime(DateTime.Today.AddDays(5)),
                SalePrice = 20500,
                RepairCost = 3000
            };

            var isValid = ValidateModel(carFormViewModel, out var errorResults);

            Assert.True(isValid);
            Assert.Empty(errorResults);
        }

        [Fact]
        public void CarCreateViewModel_ShouldBeInvalidWithWrongYear()
        {
            var carFormViewModel = new CarFormViewModel
            {
                VinCode = "12345678901234567",
                BrandId = 1,
                Model = "Oldie",
                Trim = "Basic",
                Year = 1800,
                PurchasePrice = 800,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Today),
                AvailabilityDate = DateOnly.FromDateTime(DateTime.Today),
                SalePrice = 2000,
                RepairCost = 500
            };

            var isValid = ValidateModel(carFormViewModel, out var errorResults);

            Assert.False(isValid);
            Assert.Contains(errorResults, r => r.ErrorMessage!.Contains("L’année doit être comprise entre 1990 et 2100."));
        }

        [Fact]
        public void WithValidData_ShouldBeInvalidWithEmptyModel()
        {
            var carFormViewModel = new CarFormViewModel
            {
                VinCode = "5XYKT3A17DG398742",
                BrandId = 1,
                Model = "",
                Trim = "Sport",
                Year = 2022,
                PurchasePrice = 10000,
                PurchaseDate = DateOnly.FromDateTime(DateTime.Today),
                AvailabilityDate = DateOnly.FromDateTime(DateTime.Today),
                SalePrice = 15000,
                RepairCost = 3000
            };

            var isValid = ValidateModel(carFormViewModel, out var errorResults);

            Assert.False(isValid);
            Assert.Contains(errorResults, r => r.ErrorMessage!.Contains("Le modèle est obligatoire."));
        }
    }
}
