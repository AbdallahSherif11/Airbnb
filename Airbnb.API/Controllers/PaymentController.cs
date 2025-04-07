using Airbnb.Core.Services.Contract.PaymentServices.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _config;

        public PaymentController(IPaymentService paymentService, IConfiguration config)
        {
            _paymentService = paymentService;
            _config = config;
        }

        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession(int bookingId, decimal amount)
        {
            var sessionUrl = await _paymentService.CreatePaymentSessionAsync(bookingId, amount);
            return Ok(new { url = sessionUrl });
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeSignature = Request.Headers["Stripe-Signature"];

            var result = await _paymentService.HandleStripeWebhookAsync(json, stripeSignature);
            return result ? Ok() : BadRequest();
        }
    }
}
