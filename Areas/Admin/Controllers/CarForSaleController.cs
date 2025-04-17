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
            var carForSale = await _carService.GetCarFormViewModelByIdAsync(id);
            if (carForSale == null)
            {
                return RedirectToAction("Error", "Home");
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
        public async Task<IActionResult> Create(CarFormViewModel carFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carFormViewModel);
            }

            await _carService.AddCarForSaleFromViewModelAsync(carFormViewModel);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var carFormViewModel = await _carService.GetCarFormViewModelByIdAsync(id);
            if (carFormViewModel == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(carFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CarFormViewModel carFormViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(carFormViewModel);
            }

            await _carService.UpdateCarForSaleFromViewModelAsync(carFormViewModel);
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
                return RedirectToAction("Error", "Home");
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
                return RedirectToAction("Error", "Home");
            }

            await _carService.DeleteCarForSaleAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

