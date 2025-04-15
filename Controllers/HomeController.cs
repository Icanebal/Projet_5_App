using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projet_5_App.Models;
using Projet_5_App.Repositories;
using Projet_5_App.Services;

namespace Projet_5_App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly CarService _carService;
    public HomeController(ILogger<HomeController> logger, CarService carService)
    {
        _logger = logger;
        _carService = carService;
    }

    public async Task<IActionResult> Index()
    {
        var allCars = await _carService.GetAllCarsForPublicAsync();
        var availableCars = allCars.Where(c => c.IsAvailable).ToList();
        return View(availableCars);
    }
    public async Task<IActionResult> Details(int id)
    {
        var carForSale = await _carService.GetCarForPublicByIdAsync(id);
        if (carForSale == null)
        {
            return NotFound();
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
