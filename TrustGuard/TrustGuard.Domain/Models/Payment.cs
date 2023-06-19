namespace TrustGuard.Domain.Models;

public class Payment
{
    public int Id { get; set; }
    public User User { get; set; }
    public double Price { get; set; }
    public int InsuranceType { get; set; }
    public int Status { get; set; }
    public int OfferId { get; set; }
    public Offer Offer { get; set; }
    public DateTime PaymentDate { get; set; }
    public string PayId { get; set; }
}
