using Microsoft.EntityFrameworkCore;
using Projet_5.Core.Models.Entities;
using Projet_5.Data.Data;
using Projet_5.Core.Interfaces;

namespace Projet_5.Data.Repositories
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
                .Include(c => c.Brand)
                .FirstOrDefaultAsync(c => c.Id == id);
            }

                public async Task<List<Brand>> GetAllBrandsAsync()
            {
                return await _context.Brands.OrderBy(b => b.Name).ToListAsync();
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
    }
}

