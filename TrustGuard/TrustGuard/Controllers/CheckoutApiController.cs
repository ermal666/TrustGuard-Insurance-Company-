using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Session = Stripe.BillingPortal.Session;
using SessionCreateOptions = Stripe.BillingPortal.SessionCreateOptions;
using SessionService = Stripe.BillingPortal.SessionService;

namespace TrustGuard.Application.Controllers;

[Route("create-checkout-session")]
[ApiController]
public class CheckoutApiController : ControllerBase
{
    [HttpPost]
    public ActionResult Create()
    {
        var domain = "http://localhost:4242";
        var options = new SessionCreateOptions
        {
            //Offer = new List<SessionLineItemOptions>
            //{
            //    new SessionLineItemOptions
            //    {
            //        // Provide the exact Price ID (for example, pr_1234) of the product you want to sell
            //        Price = "{{PRICE_ID}}",
            //        Quantity = 1,
            //    },
            //},
            //Mode = "payment",
            //SuccessUrl = domain + "?success=true",
            //CancelUrl = domain + "?canceled=true",
        };
        var service = new SessionService();
        Session session = service.Create(options);

        Response.Headers.Add("Location", session.Url);
        return new StatusCodeResult(303);
    }
}
    
