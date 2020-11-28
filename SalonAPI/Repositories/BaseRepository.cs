using SalonAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly SalonDBContext _context;

        public BaseRepository(SalonDBContext context)
        {
            _context = context;
        }
    }
}
