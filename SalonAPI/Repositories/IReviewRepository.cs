using SalonAPI.Entities;
using SalonAPI.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Repositories
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> ListAsync();
        Task AddAsync(Review review);
        Task<Review> FindByIdAsync(int id);
        void Update(Review review);
        void Remove(Review review);
    }
}
