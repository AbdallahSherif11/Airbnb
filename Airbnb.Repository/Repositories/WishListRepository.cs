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
    public class WishListRepository: IWishListRepository
    {
        private readonly AirbnbDbContext _context;

        public WishListRepository(AirbnbDbContext context)
        {
            _context = context;
        }

        public async Task AddToWishListAsync(WishList wishList)
        {
            await _context.WishLists.AddAsync(wishList);
        }

        public async Task RemoveFromWishListAsync(string guestId, int houseId)
        {
            var wish = await _context.WishLists
                .FirstOrDefaultAsync(w => w.GuestId == guestId && w.HouseId == houseId);

            if (wish != null)
                _context.WishLists.Remove(wish);
        }

        public async Task<bool> IsFavoriteAsync(string guestId, int houseId)
        {
            return await _context.WishLists.AnyAsync(w => w.GuestId == guestId && w.HouseId == houseId);
        }

        public async Task<List<WishList>> GetUserWishListAsync(string guestId)
        {
            return await _context.WishLists
                .Include(w => w.House)
                .Where(w => w.GuestId == guestId)
                .ToListAsync();
        }
    }
}
