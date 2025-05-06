using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projet_5_App.Models.ViewModel;
using Projet_5_App.Services;

namespace Projet_5_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CarForSaleController : Controller
    {
        private readonly CarService _carService;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public CarForSaleController(CarService carService, IWebHostEnvironment webHostEnvironment)
        {
            _carService = carService;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create()
        {
            var brands = await _carService.GetAllBrandsAsSelectListAsync();

            var carFormViewModel = new CarFormViewModel
            {
                Brands = brands
            };

            return View(carFormViewModel);
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
            return RedirectToAction("CreatedCarSuccess");
        }

        [HttpGet]
        public IActionResult CreatedCarSuccess()
        {
            return View();
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
            var car = await _carService.GetCarForPublicByIdAsync(id);
            await _carService.DeleteCarForSaleAsync(id);
            return View("DeletedCarSuccess", car);
        }

    }
}

