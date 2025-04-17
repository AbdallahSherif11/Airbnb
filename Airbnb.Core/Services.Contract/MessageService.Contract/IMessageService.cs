using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Core.DTOs.MessageDtos;
using Airbnb.Core.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.Core.Services.Contract.MessageService.Contract
{
    public interface IMessageService
    {
        Task SendMessageAsync(string senderId, string receiverId, string content);
        Task<IEnumerable<Messages>> GetConversationAsync(string user1, string user2);
        Task<ConversationResult> StartConversationWithHostAsync(string userId, int houseId);
        //Task<IActionResult> GetUserConversations();
        Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(string userId);
    }

}
