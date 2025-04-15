using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Airbnb.Core.Entities.Models;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Core.Services.Contract.WishListService.Contract;

namespace Airbnb.Service.Services.WishListService
{
    public class WishListService : IWishListService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WishListService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddToWishListAsync(string guestId, int houseId)
        {
            var isFav = await _unitOfWork.WishListRepository.IsFavoriteAsync(guestId, houseId);
            if (!isFav)
            {
                var wish = new WishList
                {
                    GuestId = guestId,
                    HouseId = houseId
                };
                await _unitOfWork.WishListRepository.AddToWishListAsync(wish);
                await _unitOfWork.CompleteSaveAsync();
            }
        }

        public async Task RemoveFromWishListAsync(string guestId, int houseId)
        {
            await _unitOfWork.WishListRepository.RemoveFromWishListAsync(guestId, houseId);
            await _unitOfWork.CompleteSaveAsync();
        }

        public async Task<bool> IsFavoriteAsync(string guestId, int houseId)
        {
            return await _unitOfWork.WishListRepository.IsFavoriteAsync(guestId, houseId);
        }

        public async Task<List<WishList>> GetUserWishListAsync(string guestId)
        {
            return await _unitOfWork.WishListRepository.GetUserWishListAsync(guestId);
        }
    }
}
