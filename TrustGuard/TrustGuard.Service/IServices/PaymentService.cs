using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;
using TrustGuard.Service.Services;

namespace TrustGuard.Service.IServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly StripeClient _stripeClient;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
            _stripeClient = new StripeClient(StripeConfiguration.ApiKey);
        }

        public async Task<PaymentIntent> CreatePaymentIntent(decimal price, string currency, int insuranceType)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(price * 100),
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService(_stripeClient);
            var paymentIntent = service.Create(options);
            await _paymentRepository.CreatePaymentAsync(new Payment
            {
                PaymentDate = DateTime.Now,
                Price = (double)price,
                InsuranceType = insuranceType
            });

            return paymentIntent;
        }

        public async Task<PaymentIntent> ConfirmPaymentIntent(string paymentIntentId)
        {
            var options = new PaymentIntentConfirmOptions { };

            var service = new PaymentIntentService(_stripeClient);
            var paymentIntent = service.Confirm(paymentIntentId, options);

            return paymentIntent;
        }

        public async Task<PaymentIntent> GetPaymentIntent(string paymentIntentId)
        {
            var service = new PaymentIntentService(_stripeClient);
            var paymentIntent = service.Get(paymentIntentId);

            return paymentIntent;
        }

        public async Task<Subscription> CreateSubscription(Offer offer)
        {
            var options = new SubscriptionCreateOptions
            {
                Items = new List<SubscriptionItemOptions>
                {
                    new SubscriptionItemOptions
                    {
                        Price = offer.Price.ToString(),
                    },
                },
            };
            var service = new SubscriptionService(_stripeClient);
            var subscription = service.Create(options);
            return subscription;
        }
    }
}
