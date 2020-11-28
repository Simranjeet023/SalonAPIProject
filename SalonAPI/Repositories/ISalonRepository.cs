using SalonAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Repositories
{
    public interface ISalonRepository
    {
        Task<IEnumerable<Salon>> ListAsync();
        Task AddAsync(Salon category);
        Task<Salon> FindByIdAsync(int id);
        void Update(Salon category);
        void Remove(Salon category);
    }
}
