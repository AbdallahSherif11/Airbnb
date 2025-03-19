using Airbnb.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Entities.Models
{
    public class Messages
    {
        public int MessageId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string MessageContent { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;

        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }
    }
}
