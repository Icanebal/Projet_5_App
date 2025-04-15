using Microsoft.AspNetCore.Mvc;
using Projet_5_App.Models.ViewModel;
using Projet_5_App.Services;

namespace Projet_5_App.Areas.Admin.Controllers
{
    public class CarForSaleController : Controller
    {
        private readonly CarService _carService;

        public CarForSaleController(CarService carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> Index()
        {
            var allCars = await _carService.GetAllCarsForPublicAsync();
            return View(allCars);
        }
        public async Task<IActionResult> Details(int id)
        {
            var carForSale = await _carService.GetCarCreateByIdAsync(id);
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
            await _carService.ToggleAvailabilityAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var carForSale = await _carService.GetCarForPublicByIdAsync(id);
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
            var carForSale = await _carService.GetCarForPublicByIdAsync(id);
            if (carForSale == null)
            {
                return NotFound();
            }

            await _carService.DeleteCarForSaleAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

