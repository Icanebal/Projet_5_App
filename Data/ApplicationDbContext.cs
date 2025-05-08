using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projet_5_App.Models.Identity;
using Projet_5_App.Models.Entities;

namespace Projet_5_App.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<CarForSale> CarsForSale { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<Repair> Repairs { get; set; }
    public DbSet<Brand> Brands { get; set; }

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
            .HasOne(b => b.Brand)
            .WithMany(c => c.CarsForSale)
            .HasForeignKey(b => b.BrandId)
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

        modelBuilder.Entity<Brand>().HasData(
            new Brand { Id = 1, Name = "Mazda" },
            new Brand { Id = 2, Name = "Jeep" },
            new Brand { Id = 3, Name = "Renault" },
            new Brand { Id = 4, Name = "Ford" },
            new Brand { Id = 5, Name = "Honda" },
            new Brand { Id = 6, Name = "Volkswagen" }
        );

        modelBuilder.Entity<CarForSale>().HasData(
            new CarForSale
            {
                Id = 1,
                VinCode = "YVV6DV4S59YH60VVH",
                Model = "Miata",
                BrandId = 1,
                Trim = "LE",
                Year = 2019,
                ImagePath = "/uploads/Mazda_Miata_LE.webp",
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
                Model = "Liberty",
                BrandId = 2,
                Trim = "Sport",
                Year = 2007,
                ImagePath = "/uploads/2007-jeep-liberty-sport.jpg",
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
                Model = "Scénic",
                BrandId = 3,
                Trim = "TCe",
                Year = 2007,
                ImagePath = "/uploads/Renault_Scenic_TCe_2007.jpg",
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
                Model = "Explorer",
                BrandId = 4,
                Trim = "XLT",
                Year = 2017,
                ImagePath = "/uploads/Ford_Explorer_XLT_2017.jpg",
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
                Model = "Civic",
                BrandId = 5,
                Trim = "LX",
                Year = 2008,
                ImagePath = "/uploads/Honda_Civic_LX_2008.jpg",
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
                Model = "GTI",
                BrandId = 6,
                Trim = "S",
                Year = 2016,
                ImagePath = "/uploads/Volkswagen_GTI_S_2016.jpg",
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
                Model = "Edge",
                BrandId = 4,
                Trim = "SEL",
                Year = 2013,
                ImagePath = "/uploads/Ford_Edge_SEL_2013.webp",
                PurchasePrice = 10990.0M,
                PurchaseDate = new(2025, 4, 7),
                IsAvailable = true,
                AvailabilityDate = new(2025, 4, 11),
                SalePrice = 12440.0M
            }
        );
    }
}
