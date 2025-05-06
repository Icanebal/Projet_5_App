using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projet_5_App.Models.ViewModel
{
    public class CarFormViewModel
    {
            public int Id { get; set; }

            public string VinCode { get; set; } = string.Empty;

            [Required(ErrorMessage = "La marque est obligatoire.")]
            [Display(Name = "Marque")]
            public int BrandId { get; set; }
            public IEnumerable<SelectListItem>? Brands { get; set; }

            [Required(ErrorMessage = "Le modèle est obligatoire.")]
            [Display(Name = "Modèle")]
            public string Model { get; set; } = string.Empty;
            [Required(ErrorMessage = "La finition est obligatoire.")]
            [Display(Name = "Finition")]
            public string Trim { get; set; } = string.Empty;

            [Required(ErrorMessage = "L’année est obligatoire.")]
            [Range(1990, 2100, ErrorMessage = "L’année doit être comprise entre 1990 et 2100.")]
            [Display(Name = "Année")]
            public int Year { get; set; }
            public IEnumerable<SelectListItem> AvailableYears { get; set; } = Enumerable.Range(1990, 111)
            .Select(y => new SelectListItem { Value = y.ToString(), Text = y.ToString() });

        [Required(ErrorMessage = "Le prix d'achat est obligatoire.")]
            [Range(0, 1_000_000, ErrorMessage = "Le prix d'achat doit être compris entre 0 et 1 000 000.")]
            [Display(Name = "Prix d'achat")]
            public decimal PurchasePrice { get; set; }

            public DateOnly? PurchaseDate { get; set; }

            public bool IsAvailable { get; set; } = true;

            public DateOnly? AvailabilityDate { get; set; }
            public decimal SalePrice { get; set; }
            public decimal RepairCost { get; set; }
            public IFormFile? ImageFile { get; set; }
            public string ImagePath { get; set; } = string.Empty;

    }
}
