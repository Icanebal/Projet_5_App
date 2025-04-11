using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Projet_5_App.Models;
using Projet_5_App.Models.ViewModel;
using Projet_5_App.Repositories;

namespace Projet_5_App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICarForSaleRepository _carForSaleRepository;
    public HomeController(ILogger<HomeController> logger, ICarForSaleRepository carForSaleRepository)
    {
        _logger = logger;
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
        var car = await _carForSaleRepository.GetCarForSaleByIdAsync(id);
        if (car == null || !car.IsAvailable)
        {
            return NotFound();
        }
        return View(car);
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
