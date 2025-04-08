using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Core.Entities.Models;

namespace Airbnb.Core.Services.Contract.MessageService.Contract
{
    public interface IMessageService
    {
        Task SendMessageAsync(string senderId, string receiverId, string content);
        Task<IEnumerable<Messages>> GetConversationAsync(string user1, string user2);
    }

}
