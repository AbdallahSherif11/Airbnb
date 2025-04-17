using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.DTOs.MessageDtos
{
    public class ConversationResult
    {
        public bool Success { get; }
        public string Message { get; }
        public string HostId { get; }

        public ConversationResult(bool success, string message, string hostId = null)
        {
            Success = success;
            Message = message;
            HostId = hostId;
        }
    }
}
