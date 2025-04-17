using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Core.DTOs.MessageDtos;
using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.MessageService.Contract;

namespace Airbnb.Service.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ConversationResult> StartConversationWithHostAsync(string userId, int houseId)
        {
            var house = await _unitOfWork.HouseRepository.GetAsync(houseId);
            if (house == null || house.IsDeleted)
                return new ConversationResult(false, "House not found");

            if (house.HostId == userId)
                return new ConversationResult(false, "Cannot message yourself");

            // Check if conversation already exists
            var existingMessages = await _unitOfWork.MessageRepository.GetMessagesAsync(userId, house.HostId);
            if (existingMessages.Any())
                return new ConversationResult(true, "Conversation exists", house.HostId);

            // Create initial message
            var message = new Messages
            {
                SenderId = userId,
                ReceiverId = house.HostId,
                MessageContent = "Hello! I'm interested in your property",
                TimeStamp = DateTime.UtcNow
            };

            await _unitOfWork.MessageRepository.AddMessageAsync(message);
            await _unitOfWork.CompleteSaveAsync();

            return new ConversationResult(true, "Conversation started", house.HostId);
        }

        public async Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(string userId)
        {
            return await _unitOfWork.MessageRepository.GetUserConversationsAsync(userId);
        }

        public async Task SendMessageAsync(string senderId, string receiverId, string content)
        {
            var message = new Messages
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                MessageContent = content,
                TimeStamp = DateTime.UtcNow
            };

            await _unitOfWork.MessageRepository.AddMessageAsync(message);
            await _unitOfWork.CompleteSaveAsync();
        }

        public async Task<IEnumerable<Messages>> GetConversationAsync(string user1, string user2)
        {
            return await _unitOfWork.MessageRepository.GetMessagesAsync(user1, user2);
        }
    }
    

}
