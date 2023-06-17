using Stripe;
using TrustGuard.Domain.Models;

namespace TrustGuard.Service.Services
{
    public interface IPaymentService
    {
        Task<PaymentIntent> ConfirmPaymentIntent(string paymentIntentId);
        Task<PaymentIntent> CreatePaymentIntent(decimal price, string currency, int insuranceType);
        Task<Subscription> CreateSubscription(Offer offer);
        Task<PaymentIntent> GetPaymentIntent(string paymentIntentId);

    }
}