using Microsoft.AspNetCore.Mvc;
using Projet_5_App.Repositories;
using Projet_5_App.Models.ViewModel;

namespace Projet_5_App.Areas.Admin.Controllers
{
    public class CarForSaleController : Controller
    {
        private readonly ICarForSaleRepository _carForSaleRepository;

        public CarForSaleController(ICarForSaleRepository carForSaleRepository)
        {
            _carForSaleRepository = carForSaleRepository;
        }
        public async Task<IActionResult> Index()
        {
            var cars = await _carForSaleRepository.GetAllCarsForSaleWithBrandNameAsync();
            var availableCars = cars
                .Where(c => c.IsAvailable)
                .Select(c => new CarForPublicViewModel
                {
                    Id = c.Id,
                    BrandName = c.Brand.Name,
                    Model = c.Model,
                    Trim = c.Trim,
                    Year = c.Year,
                    SalePrice = c.SalePrice
                })
                .ToList();
            return View(availableCars);
        }
        public async Task<IActionResult> Details(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null)
            {
                return NotFound();
            }

            return View(carForSale);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _carForSaleRepository.AddCarForSaleFromViewModelAsync(model);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null)
            {
                return NotFound();
            }

            var carForSaleViewModel = new CarCreateViewModel
            {
                Id = carForSale.Id,
                VinCode = carForSale.VinCode,
                BrandId = carForSale.Brand.Id,
                Model = carForSale.Model,
                Trim = carForSale.Trim,
                Year = carForSale.Year,
                PurchasePrice = carForSale.PurchasePrice,
                PurchaseDate = carForSale.PurchaseDate,
                AvailabilityDate = carForSale.AvailabilityDate,
                SalePrice = carForSale.SalePrice,
                IsAvailable = carForSale.IsAvailable
            };

            return View(carForSaleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarCreateViewModel carForSaleViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carForSaleViewModel);
            }

            await _carForSaleRepository.UpdateCarForSaleFromViewModelAsync(carForSaleViewModel.Id, carForSaleViewModel);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleAvailability(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null)
            {
                return NotFound();
            }
            carForSale.IsAvailable = !carForSale.IsAvailable;
            await _carForSaleRepository.UpdateCarForSaleAsync(carForSale);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null)
            {
                return NotFound();
            }

            return View(carForSale);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carForSale = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
            if (carForSale == null)
            {
                return NotFound();
            }

            await _carForSaleRepository.DeleteCarForSaleAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

