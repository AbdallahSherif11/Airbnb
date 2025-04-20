using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.PaymentDTOs
{
    public class CreateCheckoutSessionRequest
    {
        public int BookingId { get; set; }
        public decimal Amount { get; set; }
    }
}
