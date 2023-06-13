using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TrustGuard.Domain.Models;

public class AccidentInsurance
{
    public int Id { get; set; }
    public string Proffesion { get; set; }
    public string CoverageAmount { get; set; }
    public string? HealthConditions { get; set; }
    public string? Medications { get; set; }
    public DateTime StartDate { get; set;}
    public DateTime EndDate { get; set; }
    
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    [ValidateNever]
    public User User { get; set; }
    
    public int OfferId { get; set; }
    [ForeignKey("OfferId")]
    [ValidateNever]
    public Offer Offer { get; set; }
    
}