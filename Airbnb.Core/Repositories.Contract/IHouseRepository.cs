﻿using Airbnb.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Repositories.Contract
{
    public interface IHouseRepository 
    {
        Task<IEnumerable<House>> GetAllAsync();
        Task<IEnumerable<House>> GetHousesByConditionAsync(Expression<Func<House, bool>> predicate);
        Task<House> GetAsync(int id);
        Task<House> GetAsyncForUpdate(int id);

        Task AddAsync(House house);
        void Update(House house);
        Task DeleteAsync(int id);

        Task AddImageAsync(int houseId, Image image);
        Task<Image> GetImageAsync(int imageId);
        Task DeleteImageAsync(int imageId);

        Task<IEnumerable<House>>  GetHousesByCityAsync(string city);
        Task<IEnumerable<House>> GetHousesByViewAsync(string view);
        Task<IEnumerable<House>> GetHousesByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<House>> GetAvailableHousesAsync();
        Task<IEnumerable<House>> SearchHousesAsync(string keyword);
        IQueryable<House> GetQueryable();

        IQueryable<House> GetQueryableWithIncludes();
    }
}
