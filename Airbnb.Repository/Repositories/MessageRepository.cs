using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Core.DTOs.MessageDtos;
using Airbnb.Core.Entities.Identity;
using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract;
using Airbnb.Repository.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Airbnb.Repository.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AirbnbDbContext _context;

        public MessageRepository(AirbnbDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(string userId)
        {
            return await _context.Messages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .Select(g => new ConversationDto
                {
                    UserId = g.Key,
                    LastMessage = g.OrderByDescending(m => m.TimeStamp).FirstOrDefault().MessageContent,
                    LastMessageTime = g.OrderByDescending(m => m.TimeStamp).FirstOrDefault().TimeStamp,
                    
                })
                .ToListAsync();
        }


        public async Task<IEnumerable<Messages>> GetMessagesAsync(string userId1, string userId2)
        {
            return await _context.Messages
                .Where(m =>
                    (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                    (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.TimeStamp)
                .Select(m => new Messages
                {
                    MessageId = m.MessageId,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    MessageContent = m.MessageContent,
                    IsDeleted = m.IsDeleted,
                    TimeStamp = m.TimeStamp,
                    Sender = new ApplicationUser
                    {
                        Id = m.Sender.Id,
                        FirstName = m.Sender.FirstName,
                        LastName = m.Sender.LastName,
                        ProfilePictureUrl = m.Sender.ProfilePictureUrl
                    },
                    Receiver = new ApplicationUser
                    {
                        Id = m.Receiver.Id,
                        FirstName = m.Receiver.FirstName,
                        LastName = m.Receiver.LastName,
                        ProfilePictureUrl = m.Receiver.ProfilePictureUrl
                    }
                })
                .ToListAsync();
        }

        public async Task AddMessageAsync(Messages message)
        {
            await _context.Messages.AddAsync(message);
        }
    }

}
