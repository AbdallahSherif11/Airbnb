using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.PaymentServices.Contract
{
    public interface IStripeService
    {
        Task<(string sessionId, string sessionUrl)> CreateCheckoutSessionAsync(decimal amount, int bookingId);
    }
}
