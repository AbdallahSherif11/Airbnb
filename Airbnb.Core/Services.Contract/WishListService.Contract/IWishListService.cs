using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Core.Entities.Models;

namespace Airbnb.Core.Services.Contract.WishListService.Contract
{
    public interface IWishListService
    {
        Task AddToWishListAsync(string guestId, int houseId);
        Task RemoveFromWishListAsync(string guestId, int houseId);
        Task<bool> IsFavoriteAsync(string guestId, int houseId);
        Task<List<WishList>> GetUserWishListAsync(string guestId);
    }
}
