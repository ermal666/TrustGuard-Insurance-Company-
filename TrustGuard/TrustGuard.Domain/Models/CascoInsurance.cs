using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TrustGuard.Domain.Models;

public class CascoInsurance
{
    [Key]
    public int Id { get; set; }
    
    [RegularExpression("^[A-Z0-9]{17}$", ErrorMessage = "VIN number should be 17 characters long and consist of capital letters and numbers.")]
    public string VinNumber { get; set; }
    [Required(ErrorMessage = "CarModel is required.")]
    public string CarModel { get; set; }
    [Required(ErrorMessage = "Plate is required.")]
    public string Plate { get; set; }
    [Required(ErrorMessage = "Producer is required.")]
    public string Producer { get; set; }
    public string? Color { get; set; }
    [Required(ErrorMessage = "EngineCapacity is required.")]
    public int EngineCapacity { get; set; }
    public int? SeatingCapacity { get; set; }
    [Required(ErrorMessage = "PurchaseDate is required.")]
    public DateTime PurchaseDate { get; set; }
    //[Required(ErrorMessage = "Please choose on offer.")]
    //public string Offer { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int OfferId { get; set; }
    public Offer Offer { get; set; }
    
}