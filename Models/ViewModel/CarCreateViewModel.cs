using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Projet_5_App.Models.ViewModel
{
    public class CarCreateViewModel
    {
            public int Id { get; set; }

            [Required(ErrorMessage = "Le code VIN est obligatoire.")]
            [StringLength(17, ErrorMessage = "Le code VIN ne peut pas dépasser 17 caractères.")]
            [Display(Name = "Code VIN")]
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

            [Required(ErrorMessage = "Le prix d'achat est obligatoire.")]
            [Range(0, 1_000_000, ErrorMessage = "Le prix d'achat doit être compris entre 0 et 1 000 000.")]
            [Display(Name = "Prix d'achat")]
            public decimal PurchasePrice { get; set; }

            [Required(ErrorMessage = "La date d'achat est obligatoire.")]
            [DataType(DataType.Date)]
            [Display(Name = "Date d'achat")]
            public DateOnly PurchaseDate { get; set; }

            [Display(Name = "Disponible ?")]
            public bool IsAvailable { get; set; } = true;

            [Required(ErrorMessage = "La date de disponibilité est obligatoire.")]
            [DataType(DataType.Date)]
            [Display(Name = "Date de disponibilité")]
            public DateOnly? AvailabilityDate { get; set; }

            [Required(ErrorMessage = "Le prix de vente est obligatoire.")]
            [Range(0, 1_000_000, ErrorMessage = "Le prix de vente doit être compris entre 0 et 1 000 000.")]
            [Display(Name = "Prix de vente")]
            public decimal SalePrice { get; set; }
            public decimal RepairCost { get; set; }
    }
}
