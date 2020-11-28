using SalonAPI.Entities;
using SalonAPI.Models.Review;
using SalonAPI.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> ListAsync();
        Task<ReviewResponse> GetReviewAsync(int id);
        Task<ReviewResponse> SaveAsync(Review review);
        Task<ReviewResponse> UpdateAsync(int id, Review review);
        Task<ReviewResponse> DeleteAsync(int id);
    }
}
