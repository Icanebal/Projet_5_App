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

            public async Task<IEnumerable<CarForSale>> GetAllCarsForSaleWithBrandNameAsync()
            {
                return await _context.CarsForSale
                    .Include(c => c.Brand)
                    .ToListAsync();
            }
            public async Task<CarForSale?> GetCarForSaleByIdAsync(int id)
            {
                return await _context.CarsForSale
                .Include(c => c.Repairs)
                .Include(c => c.Sale)
                .Include(c => c.Brand)
                .FirstOrDefaultAsync(c => c.Id == id);
            }
            public async Task AddCarForSaleAsync(CarForSale carForSale)
            {
                _context.CarsForSale.Add(carForSale);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateCarForSaleAsync(CarForSale carForSale)
            {
                _context.CarsForSale.Update(carForSale);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteCarForSaleAsync(CarForSale carForSale)
            {
                 _context.CarsForSale.Remove(carForSale);
                 await _context.SaveChangesAsync();
            }
    }
}

