using Microsoft.EntityFrameworkCore;
using Projet_5_App.Models.Entities;
using Projet_5_App.Data;
using Projet_5_App.Models.ViewModel;

namespace Projet_5_App.Repositories
{
    public class CarForSaleRepository : ICarForSaleRepository
    {
        private readonly ApplicationDbContext _context;

        public CarForSaleRepository(ApplicationDbContext context)
            {
                _context = context;
            }
            public async Task<IEnumerable<CarForSale>> GetAllCarsForSaleAsync()
            {
                return await _context.CarsForSale.ToListAsync();
            }
            public async Task<CarForSale?> GetCarForSaleByIdAsync(int id)
            {
                return await _context.CarsForSale
                .Include(c => c.Repairs)
                .Include(c => c.Sale)
                .FirstOrDefaultAsync(c => c.Id == id);
            }
            public async Task AddCarForSaleFromViewModelAsync(CarForSaleViewModel carForSaleViewModel)
            {
                var car = new CarForSale
                {
                    VinCode = carForSaleViewModel.VinCode,
                    Brand = carForSaleViewModel.Brand,
                    Model = carForSaleViewModel.Model,
                    Trim = carForSaleViewModel.Trim,
                    Year = carForSaleViewModel.Year,
                    PurchasePrice = carForSaleViewModel.PurchasePrice,
                    PurchaseDate = carForSaleViewModel.PurchaseDate,
                    AvailabilityDate = carForSaleViewModel.AvailabilityDate,
                    SalePrice = carForSaleViewModel.SalePrice,
                    IsAvailable = carForSaleViewModel.IsAvailable
                };

                _context.CarsForSale.Add(car);
                await _context.SaveChangesAsync();
            }
            public async Task UpdateCarForSaleFromViewModelAsync(int id, CarForSaleViewModel carForSaleViewModel)
            {
                var car = await _context.CarsForSale.FindAsync(id);
                if (car == null) return;

                car.VinCode = carForSaleViewModel.VinCode;
                car.Brand = carForSaleViewModel.Brand;
                car.Model = carForSaleViewModel.Model;
                car.Trim = carForSaleViewModel.Trim;
                car.Year = carForSaleViewModel.Year;
                car.PurchasePrice = carForSaleViewModel.PurchasePrice;
                car.PurchaseDate = carForSaleViewModel.PurchaseDate;
                car.AvailabilityDate = carForSaleViewModel.AvailabilityDate;
                car.SalePrice = carForSaleViewModel.SalePrice;
                car.IsAvailable = carForSaleViewModel.IsAvailable;

                await _context.SaveChangesAsync();
            }

            public async Task UpdateCarForSaleAsync(CarForSale carForSale)
            {
                _context.CarsForSale.Update(carForSale);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteCarForSaleAsync(int id)
                {
                    var carForSale = await _context.CarsForSale.FindAsync(id);
                        if (carForSale != null)
                        {
                            _context.CarsForSale.Remove(carForSale);
                            await _context.SaveChangesAsync();
                        }
                }
    }
}

