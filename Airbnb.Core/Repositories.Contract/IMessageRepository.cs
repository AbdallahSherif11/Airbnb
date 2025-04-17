using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Core.DTOs.MessageDtos;
using Airbnb.Core.Entities.Models;

namespace Airbnb.Core.Repositories.Contract
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Messages>> GetMessagesAsync(string userId1, string userId2);
        Task AddMessageAsync(Messages message);
        Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(string userId);
    }
}
