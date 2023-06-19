using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using TrustGuard.Domain.Enums;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;
using TrustGuard.Service.IServices;

namespace TrustGuard.Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly UserManager<User> _userManager;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IHttpContextAccessor _httpContext;
        private readonly StripeClient _stripeClient;

        public PaymentService(IPaymentRepository paymentRepository, IOfferRepository offerRepository, IHttpContextAccessor httpContext, UserManager<User> userManager)
        {
            _paymentRepository = paymentRepository;
            _stripeClient = new StripeClient(StripeConfiguration.ApiKey);
            _offerRepository = offerRepository;
            _httpContext = httpContext;
            _userManager = userManager;

        }

        public async Task<PaymentIntent> CreatePaymentIntent(Offer offer, string currency = "eur")
        {
            

            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(offer.Price * 100),
                Currency = currency,
                PaymentMethodTypes = new List<string> { "card" }
            };

            var service = new PaymentIntentService(_stripeClient);
            var paymentIntent = await service.CreateAsync(options);
            try
            {
                await _paymentRepository.CreatePaymentAsync(new Payment
                {
                    PaymentDate = DateTime.Now,
                    Price = (double)offer.Price,
                    InsuranceType = offer.InsuranceId,
                    Status = (int)PaymentStatus.Pending,
                    Offer = offer,
                    PayId = paymentIntent.Id,
                    User = GetLoggedUser().Result
                });

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            await _paymentRepository.SaveChangesAsync();

            return paymentIntent;
        }

        public async Task<PaymentIntent> ConfirmPaymentIntent(string paymentIntentId)
        {
            var payment = await _paymentRepository.GetPaymentById(x => x.PayId == paymentIntentId).FirstOrDefaultAsync();
            var options = new PaymentIntentConfirmOptions { };

            var service = new PaymentIntentService(_stripeClient);
            var paymentIntent = await service.ConfirmAsync(paymentIntentId, options);

            payment.Status = (int)PaymentStatus.Succeeded;

            _paymentRepository.Update(payment);
             
            await _paymentRepository.SaveChangesAsync();
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
            var subscription = await service.CreateAsync(options);
            return subscription;
        }

        public async Task<PaymentIntent> CanclePaymentIntent(string paymentIntentId)
        {
            var options = new PaymentIntentCancelOptions 
            {
                CancellationReason = "abandoned",
            };
            var service = new PaymentIntentService(_stripeClient);
            var paymentIntent = await service.CancelAsync(paymentIntentId, options);
            return paymentIntent;
        }

        public async Task<Payment> GetPayment(int id)
        {
            var payment = await _paymentRepository.GetPaymentById(x => x.Id == id && x.User.Id == GetLoggedUser().Result.Id).Include(x => x.Offer).FirstOrDefaultAsync();
            return payment;
        }

        public async Task Init()
        {
        }

        public async Task<Payment> Callback(Event @event)
        {
            PaymentIntent paymentIntent;


            switch (@event.Type)
            {
                case "payment_intent.succeeded":
                    paymentIntent = (PaymentIntent)@event.Data.Object;
                    await ConfirmPaymentIntent(paymentIntent.Id);
                    return await _paymentRepository.GetPaymentById(x => x.PayId == paymentIntent.Id && x.User.Id == GetLoggedUser().Result.Id).FirstOrDefaultAsync();
                case "payment_intent.payment_failed" :
                    paymentIntent = (PaymentIntent)@event.Data.Object;
                    await CanclePaymentIntent(@event.Type);
                    break;  
                case "payment_intent.canceled" :
                    paymentIntent = (PaymentIntent)@event.Data.Object;
                    await CanclePaymentIntent(@event.Type);
                    break;
                case "payment_intent.created" :
                    paymentIntent = (PaymentIntent)@event.Data.Object;
                    await ConfirmPaymentIntent(@event.Type);
                    break;
                case "charge.succeeded":
                    var charge = (Charge)@event.Data.Object;
                    return await _paymentRepository.GetPaymentById(x => x.User.Id == GetLoggedUser().Result.Id).FirstOrDefaultAsync();
                    break;
                default:
                    break;
            }
            return null;
        }

        private async Task<User> GetLoggedUser()
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);

            return user;
        } 
    }
}
