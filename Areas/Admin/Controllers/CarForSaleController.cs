using Microsoft.AspNetCore.Mvc;
using Projet_5_App.Repositories;
using Projet_5_App.Models.ViewModel;
using Projet_5_App.Services;

namespace Projet_5_App.Areas.Admin.Controllers
{
    public class CarForSaleController : Controller
    {
        private readonly ICarForSaleRepository _carForSaleRepository;
        private readonly CarService _carService;

        public CarForSaleController(ICarForSaleRepository carForSaleRepository, CarService carService)
        {
            _carForSaleRepository = carForSaleRepository;
            _carService = carService;
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
        public async Task<IActionResult> Create(CarCreateViewModel carCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carCreateViewModel);
            }

            await _carService.AddCarForSaleFromViewModelAsync(carCreateViewModel);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var carCreateViewModel = await _carService.GetCarCreateViewModelByIdAsync(id);
            if (carCreateViewModel == null)
            {
                return NotFound();
            }
            return View(carCreateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarCreateViewModel carCreateViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carCreateViewModel);
            }

            await _carService.UpdateCarForSaleFromViewModelAsync(carCreateViewModel);
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

