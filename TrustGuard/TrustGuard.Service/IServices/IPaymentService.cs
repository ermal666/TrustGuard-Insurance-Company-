using Stripe;
using TrustGuard.Domain.Models;

namespace TrustGuard.Service.IServices
{
    public interface IPaymentService
    {
        Task<PaymentIntent> ConfirmPaymentIntent(string paymentIntentId);
        Task<PaymentIntent> CreatePaymentIntent(Offer offer, string currency = "eur");
        Task<Subscription> CreateSubscription(Offer offer);
        Task<PaymentIntent> GetPaymentIntent(string paymentIntentId);
        Task<Payment> Callback(Event @event);

    }
}