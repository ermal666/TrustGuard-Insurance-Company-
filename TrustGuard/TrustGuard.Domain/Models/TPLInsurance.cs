using System.ComponentModel.DataAnnotations;

namespace TrustGuard.Domain.Models;

public class TPLInsurance
{
    [Key]
    public int Id { get; set; }
    public string PolicyNumber { get; set; }
    public string PolicyValidation { get; set; }
    [RegularExpression("^[A-Z0-9]{17}$", ErrorMessage = "VIN number should be 17 characters long and consist of capital letters and numbers.")]
    public string VinNumber { get; set; }
    [Required(ErrorMessage = "Plate is required.")]
    public string Plate { get; set; }
    [Required(ErrorMessage = "Producer is required.")]
    public string Producer { get; set; }
    public int PurchaseDate { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int OfferId { get; set; }
    public Offer Offer { get; set; }
}