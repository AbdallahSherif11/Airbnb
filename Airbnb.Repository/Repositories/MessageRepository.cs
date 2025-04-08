using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Messages>> GetMessagesAsync(string userId1, string userId2)
        {
            return await _context.Messages
                .Where(m =>
                    (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                    (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.TimeStamp).ToListAsync();
        }

        public async Task AddMessageAsync(Messages message)
        {
            await _context.Messages.AddAsync(message);
        }
    }

}
