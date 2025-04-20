using Airbnb.Core.Services.Contract.PaymentServices.Contract;
using Microsoft.Extensions.Configuration;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Service.Services.PaymentServices
{
    public class StripeService : IStripeService
    {
        private readonly IConfiguration _config;

        public StripeService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<(string sessionId, string sessionUrl)> CreateCheckoutSessionAsync(decimal amount, int bookingId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal = amount * 100,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = $"Booking #{bookingId}"
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                SuccessUrl = _config["Stripe:SuccessUrl"], // Get from appsettings.json
                CancelUrl = _config["Stripe:CancelUrl"],
                Metadata = new Dictionary<string, string>
                {
                    { "bookingId", bookingId.ToString() }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return (session.Id, session.Url);
        }
    }
}
