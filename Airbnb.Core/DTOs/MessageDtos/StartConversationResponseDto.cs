using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.MessageDtos
{
    public class StartConversationResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string HostId { get; set; }
    }
}
