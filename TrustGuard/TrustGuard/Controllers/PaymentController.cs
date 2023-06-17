using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrustGuard.Application.Dtos;
using TrustGuard.Domain.Models;
using TrustGuard.Service.Services;

namespace TrustGuard.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost]
        public Task<IActionResult> CreatePaymentIntent([FromBody] CreateOfferDto offerDto)
        {
            var offer = new Offer
            {
                Name = offerDto.Name,
                Price = offerDto.Price,
                InsuranceId = offerDto.InsuranceId
            };
            var paymentIntent = _paymentService.CreatePaymentIntent((decimal)offer.Price, "euro", offer.InsuranceId);
            return Task.FromResult<IActionResult>(Ok(paymentIntent));
        }





    }
}
