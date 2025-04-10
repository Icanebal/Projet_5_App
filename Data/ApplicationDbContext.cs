using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projet_5_App.Models.Entities;

namespace Projet_5_App.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<CarForSale> CarsForSale { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Repair> Repairs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Sale>()
            .Property(p => p.Price)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Repair>()
            .Property(c => c.Cost)
            .HasPrecision(10, 2);

        modelBuilder.Entity<CarForSale>()
            .Property(p => p.PurchasePrice)
            .HasPrecision(10, 2);

        modelBuilder.Entity<CarForSale>()
            .Property(s => s.SalePrice)
            .HasPrecision(10, 2);

        modelBuilder.Entity<CarForSale>()
            .HasMany(r => r.Repairs)
            .WithOne(c => c.CarForSale)
            .HasForeignKey(c => c.CarForSaleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Sale>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<CarForSale>()
            .HasOne(v => v.Sale)
            .WithOne(c => c.CarForSale)
            .HasForeignKey<Sale>(s => s.CarForSaleId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CarForSale>()
            .Property(v => v.VinCode)
            .IsRequired()
            .HasMaxLength(17);

        modelBuilder.Entity<CarForSale>()
            .Property(p => p.PurchaseDate)
            .HasColumnType("date");

        modelBuilder.Entity<CarForSale>()
            .Property(a => a.AvailabilityDate)
            .HasColumnType("date");

        modelBuilder.Entity<Sale>()
            .Property(s => s.SaleDate)
            .HasColumnType("date");

        modelBuilder.Entity<CarForSale>().HasData(
            new CarForSale
            {
                Id = 1,
                VinCode = "YVV6DV4S59YH60VVH",
                Brand = "Mazda",
                Model = "Miata",
                Trim = "LE",
                Year = 2019,
                PurchasePrice = 1800.0M,
                PurchaseDate = new (2025, 1, 7),
                IsAvailable = true,
                AvailabilityDate = new(2025, 4, 7),
                SalePrice = 9900.0M
            },

            new CarForSale
            {
                Id = 2,
                VinCode = "L5KJ1AE9KZ0NUYHL0",
                Brand = "Jeep",
                Model = "Liberty",
                Trim = "Sport",
                Year = 2007,
                PurchasePrice = 4500.0M,
                PurchaseDate = new(2025, 4, 2),
                IsAvailable = true,
                AvailabilityDate = new(2025, 4, 7),
                SalePrice = 5350.0M
            },

            new CarForSale
            {
                Id = 3,
                VinCode = "HA7NZMD2URDCXCYJ9",
                Brand = "Renault",
                Model = "Scénic",
                Trim = "TCe",
                Year = 2007,
                PurchasePrice = 1800.0M,
                PurchaseDate = new(2025, 4, 4),
                IsAvailable = false,
                AvailabilityDate = null,
                SalePrice = 2990.0M
            },

            new CarForSale
            {
                Id = 4,
                VinCode = "EHT5HDBMUAMR4AJL6",
                Brand = "Ford",
                Model = "Explorer",
                Trim = "XLT",
                Year = 2017,
                PurchasePrice = 24350.0M,
                PurchaseDate = new(2025, 4, 5),
                IsAvailable = false,
                AvailabilityDate = null,
                SalePrice = 25950.0M
            },

            new CarForSale
            {
                Id = 5,
                VinCode = "KSUCFCDJWRH3ZL3MS",
                Brand = "Honda",
                Model = "Civic",
                Trim = "LX",
                Year = 2008,
                PurchasePrice = 4000.0M,
                PurchaseDate = new(2025, 4, 6),
                IsAvailable = true,
                AvailabilityDate = new(2025, 4, 9),
                SalePrice = 4975.0M
            },

            new CarForSale
            {
                Id = 6,
                VinCode = "NXJ2K11E7SR061YVR",
                Brand = "Volkswagen",
                Model = "GTI",
                Trim = "S",
                Year = 2016,
                PurchasePrice = 15250.0M,
                PurchaseDate = new(2025, 4, 6),
                IsAvailable = true,
                AvailabilityDate = new(2025, 4, 10),
                SalePrice = 16190.0M
            },

            new CarForSale
            {
                Id = 7,
                VinCode = "9JVHR4BJ6PC1NUXVJ",
                Brand = "Ford",
                Model = "Edge",
                Trim = "SEL",
                Year = 2013,
                PurchasePrice = 10990.0M,
                PurchaseDate = new(2025, 4, 7),
                IsAvailable = true,
                AvailabilityDate = new(2025, 4, 11),
                SalePrice = 12440.0M
            }
        );
    }
}
