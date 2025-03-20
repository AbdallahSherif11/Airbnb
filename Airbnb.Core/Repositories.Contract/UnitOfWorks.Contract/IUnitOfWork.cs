using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Repositories.Contract.UnitOfWorks.Contract
{
    public interface IUnitOfWork
    {
        public IHouseRepository HouseRepository { get; }

        Task<int> CompleteSaveAsync();

    }
}
