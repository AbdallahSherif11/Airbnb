﻿using Airbnb.Core.Repositories.Contract;
using Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract;
using Airbnb.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Repository.Repositories.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirbnbDbContext _context;
        private IHouseRepository _houseRepository;

        public UnitOfWork(AirbnbDbContext context)
        {
            _context = context;
            //_houseRepository = new HouseRepository(context);
        }
        public IHouseRepository HouseRepository
        {
            get
            {
                if (_houseRepository == null)
                {
                    _houseRepository = new HouseRepository(_context);
                }
                return _houseRepository;
            }
        }


        public async Task<int> CompleteSaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
