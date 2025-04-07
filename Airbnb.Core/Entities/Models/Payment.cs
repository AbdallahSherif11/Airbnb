using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Entities.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int BookingId { get; set; }
        public DateTime CreatedAt { get; set; }



        public string PaymentMethod { get; set; } = "Stripe";
        public string StripeSessionId { get; set; }
        public string? StripePaymentIntentId { get; set; }
        public bool Status { get; set; }


        public decimal TotalPrice { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Booking Booking { get; set; } 
    }
}
