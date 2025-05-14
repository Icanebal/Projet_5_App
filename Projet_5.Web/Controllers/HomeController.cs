using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projet_5.Web.Models;
using Projet_5.Web.Models.ViewModel;
using Projet_5.Web.Services;

namespace Projet_5.Web.Controllers;

public class HomeController : Controller
{
    private readonly CarService _carService;
    public HomeController(ILogger<HomeController> logger, CarService carService)
    {
        _carService = carService;
    }

    public async Task<IActionResult> Index()
    {
        List <CarForPublicViewModel>? allCars = null;
        allCars = await _carService.GetAllCarsForPublicAsync();

        if (User.Identity.IsAuthenticated)
        {
            return View(allCars);
        }
        else
        {
            var availableCars = allCars.Where(c => c.EffectiveAvailability).ToList();
            return View(availableCars);
        }

    }
    public async Task<IActionResult> Details(int id)
    {
        CarForPublicViewModel? carForSale = null;
        
        if (User.Identity.IsAuthenticated)
        {
            carForSale = await _carService.GetCarForAdminByIdAsync(id);           
        }
        else
        {
            carForSale = await _carService.GetCarForPublicByIdAsync(id);
        }
        if (carForSale == null)
        {
            return RedirectToAction("Error");
        }
        return View(carForSale);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
