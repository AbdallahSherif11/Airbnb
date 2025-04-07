using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.PaymentServices.Contract
{
    public interface IPaymentService
    {
        Task<string> CreatePaymentSessionAsync(int bookingId, decimal amount);
        Task<bool> HandleStripeWebhookAsync(string json, string stripeSignature);
    }
}
