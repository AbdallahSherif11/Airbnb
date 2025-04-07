using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.PaymentServices.Contract;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Service.Services.PaymentServices
{
    public class PaymentService : IPaymentService
    {
        private readonly IStripeService _stripeService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public PaymentService(IUnitOfWork unitOfWork, IStripeService stripeService, IConfiguration config)
        {
            _stripeService = stripeService;
            _unitOfWork = unitOfWork;

            _config = config;
        }

        public async Task<string> CreatePaymentSessionAsync(int bookingId, decimal amount)
        {
            var (sessionId, url) = await _stripeService.CreateCheckoutSessionAsync(amount, bookingId);

            var payment = new Payment
            {
                BookingId = bookingId,
                TotalPrice = amount,
                StripeSessionId = sessionId,
                Status = false
            };

            await _unitOfWork.PaymentRepository.AddAsync(payment);

            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking != null)
            {
                booking.PaymentId = payment.PaymentId;
            }

            await _unitOfWork.CompleteSaveAsync();

            return url;
        }

        public async Task<bool> HandleStripeWebhookAsync(string json, string stripeSignature)
        {
            var secret = _config["Stripe:WebhookSecret"];
            Event stripeEvent;

            try
            {
                stripeEvent = EventUtility.ConstructEvent(json, stripeSignature, secret);
            }
            catch
            {
                return false;
            }

            if (stripeEvent.Type == "checkout.session.completed")
            {
                var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                var bookingId = int.Parse(session.Metadata["bookingId"]);

                var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
                if (booking == null) return false;

                var payment = await _unitOfWork.PaymentRepository.GetByBookingIdAsync(bookingId);
                if (payment == null) return false;

                payment.StripeSessionId = session.Id;
                payment.StripePaymentIntentId = session.PaymentIntentId;
                payment.Status = true;
                await _unitOfWork.CompleteSaveAsync();


                return true;
            }
            await _unitOfWork.CompleteSaveAsync();


            return false;
        }
    }
}
