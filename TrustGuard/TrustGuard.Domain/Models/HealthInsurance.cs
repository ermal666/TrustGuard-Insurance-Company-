using TrustGuard.Domain.Enums;

namespace TrustGuard.Domain.Models;

public class HealthInsurance
{
    public int Id { get; set; }
    public PaymentOptions PaymentOption { get; set; }
    public DateTime StartDate { get; set;}
    public DateTime EndDate { get; set; }
    public double Price { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int OfferId { get; set; }
    public Offer Offer { get; set; }
    
}