using Moq;
using Projet_5.Core.Models.Entities;
using Projet_5.Web.Models.ViewModel;
using Projet_5.Web.Services;
using Projet_5.Core.Interfaces;
using Microsoft.AspNetCore.Hosting;

namespace Projet_5_Tests
{
    public class CarServiceTests
    {
        private readonly Mock<ICarForSaleRepository> _carForSaleRepositoryMock;
        private readonly Mock<IWebHostEnvironment> _webHostEnvironment;
        private readonly CarMapper _carMappingService;
        private readonly CarService _carService;
        private readonly FileService _fileService;

        public CarServiceTests()
        {
            _carForSaleRepositoryMock = new Mock<ICarForSaleRepository>();
            _webHostEnvironment = new Mock<IWebHostEnvironment>();
            _carMappingService = new CarMapper();
            _fileService = new FileService(_webHostEnvironment.Object);
            _carService = new CarService(_carForSaleRepositoryMock.Object, _carMappingService, _fileService);
        }

        [Fact]
        public async Task DeleteCarForSaleAsync_ShouldUpdateOnceAndMarkAsDeleted()
        {
            var car = new CarForSale
            {
                Id = 1,
                Deleted = false
            };
            _carForSaleRepositoryMock.Setup(r => r.GetCarForSaleByIdAsync(car.Id)).ReturnsAsync(car);
            _carForSaleRepositoryMock.Setup(r => r.UpdateCarForSaleAsync(car)).Returns(Task.CompletedTask);

            await _carService.DeleteCarForSaleAsync(car.Id);
            Assert.True(car.Deleted);
            _carForSaleRepositoryMock.Verify(r => r.UpdateCarForSaleAsync(car), Times.Once);
        }

        [Fact]
        public void MapToCarForSaleEntity_ShouldCalulateSalePriceCorrectly()
        {
            var carFormViewModel = new CarFormViewModel
            {
                Id = 1,
                VinCode = "12345678901234567",
                BrandId = 1,
                Model = "TestModel",
                Trim = "TestTrim",
                Year = 2023,
                PurchasePrice = 20000,
                RepairCost = 5000,
                IsAvailable = true
            };

            var carForSale = _carMappingService.MapToCarForSaleEntity(carFormViewModel);

            Assert.Equal(25500, carForSale.SalePrice);
        }

        [Fact]
        public void MapToCarForPublicViewModel_ShouldMapBrandNameCorrectly()
        {
            var car = new CarForSale
            {
                Id = 1,
                Model = "Focus",
                Trim = "ST",
                Year = 2020,
                PurchasePrice = 20000,
                RepairCost = 5000,
                Brand = new Brand { Id = 1, Name = "Ford" }
            };

            var result = _carMappingService.MapToCarForPublicViewModel(car);

            Assert.Equal("Ford", result.BrandName);
        }
    }
}
