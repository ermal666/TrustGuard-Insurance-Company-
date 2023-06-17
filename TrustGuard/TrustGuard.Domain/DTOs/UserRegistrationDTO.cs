using System.ComponentModel.DataAnnotations;

namespace TrustGuard.Domain.DTOs;

public class UserRegistrationDTO 
{
    [Required]
    [RegularExpression("^[0-9]{10}$", ErrorMessage = "PersonalId must be 10 characters long and contain only integers.")]
    public string PersonalId { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    [Required]
    public string Address { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}