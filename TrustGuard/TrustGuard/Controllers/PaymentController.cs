
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Security.Claims;
using TrustGuard.Domain.Models;
using TrustGuard.Persistence.IRepositories;
using TrustGuard.Service.IServices;

namespace TrustGuard.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService, IOfferRepository offerRepository)
        {
            _paymentService = paymentService;
            _offerRepository = offerRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreatePaymentIntent(int offerId)
        {
            var offer = await _offerRepository.GetOfferById(x => x.Id == offerId).FirstOrDefaultAsync();
            var paymentIntent = await _paymentService.CreatePaymentIntent(offer);
            return Ok(new { clientSecret = paymentIntent.ClientSecret });
        }

        [HttpPost]
        public async Task<IActionResult> Callback()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], "whsec_...");


            var result = await _paymentService.Callback(stripeEvent);

            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Payment Failed");
        }
    }
}
