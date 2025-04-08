using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Core.Entities.Models;
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
