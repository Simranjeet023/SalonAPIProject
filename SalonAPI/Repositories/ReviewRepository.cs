using Microsoft.EntityFrameworkCore;
using SalonAPI.Context;
using SalonAPI.Entities;
using SalonAPI.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Repositories
{
    public class ReviewRepository : BaseRepository, IReviewRepository
	{
		public ReviewRepository(SalonDBContext context) : base(context) { }

		public async Task<IEnumerable<Review>> ListAsync()
		{
			IQueryable<Review> queryable = _context.Reviews
													.Include(p => p.Salon)
													.AsNoTracking();

			return queryable;
		}

		public async Task<Review> FindByIdAsync(int id)
		{
			return await _context.Reviews
								 .Include(p => p.Salon)
								 .FirstOrDefaultAsync(p => p.Id == id); // Since Include changes the method's return type, we can't use FindAsync
		}

		public async Task AddAsync(Review review)
		{
			await _context.Reviews.AddAsync(review);
		}

		public void Update(Review review)
		{
			_context.Reviews.Update(review);
		}

		public void Remove(Review review)
		{
			_context.Reviews.Remove(review);
		}
	}
}
