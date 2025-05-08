using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projet_5_App.Models.ViewModel;
using Projet_5_App.Services;

namespace Projet_5_App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CarForSaleController : Controller
    {
        private readonly CarService _carService;
        public CarForSaleController(CarService carService)
        {
            _carService = carService;
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

            carFormViewModel.Brands = await _carService.GetAllBrandsAsSelectListAsync();

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
            return RedirectToAction("Index", "Home", new { area = "" });
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

