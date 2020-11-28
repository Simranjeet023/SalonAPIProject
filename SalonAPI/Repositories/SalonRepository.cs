using Microsoft.EntityFrameworkCore;
using SalonAPI.Context;
using SalonAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Repositories
{
    public class SalonRepository : BaseRepository, ISalonRepository
    {
        public SalonRepository(SalonDBContext context) : base(context) { }

        public async Task<IEnumerable<Salon>> ListAsync()
        {
            return await _context.Salons
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public async Task AddAsync(Salon salon)
        {
            await _context.Salons.AddAsync(salon);
        }

        public async Task<Salon> FindByIdAsync(int id)
        {
            return await _context.Salons.Where(c => c.Id == id).FirstOrDefaultAsync();
        }

        public void Update(Salon salon)
        {
            _context.Salons.Update(salon);
        }

        public void Remove(Salon salon)
        {
            _context.Salons.Remove(salon);
        }
    }
}
