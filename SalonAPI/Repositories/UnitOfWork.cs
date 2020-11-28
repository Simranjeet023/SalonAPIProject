using SalonAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SalonDBContext _context;

        public UnitOfWork(SalonDBContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
