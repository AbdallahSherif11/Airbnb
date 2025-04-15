using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Core.Entities.Models;

namespace Airbnb.Core.Repositories.Contract
{
    public interface IWishListRepository
    {
        Task AddToWishListAsync(WishList wishList);
        Task RemoveFromWishListAsync(string guestId, int houseId);
        Task<bool> IsFavoriteAsync(string guestId, int houseId);
        Task<List<WishList>> GetUserWishListAsync(string guestId);
    }
}
