using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
