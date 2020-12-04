using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SalonAPI.Entities;
using SalonAPI.Models.Review;
using SalonAPI.Repositories;
using SalonAPI.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Services.impl
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository _reviewRepository;
        private readonly ISalonRepository _salonRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public ReviewService(IReviewRepository reviewRepository, ISalonRepository salonRepository, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _reviewRepository = reviewRepository;
            _salonRepository = salonRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Review>> ListAsync()
        {
            var reviews = await _cache.GetOrCreateAsync(CacheKeys.ReviewsList, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _reviewRepository.ListAsync();
            });

            return reviews;
        }

        public async Task<ReviewResponse> SaveAsync(Review review)
        {
            try
            {
                var existingSalon = await _salonRepository.FindByIdAsync(review.SalonId);
                if (existingSalon == null)
                    return new ReviewResponse("Invalid salon.");

                await _reviewRepository.AddAsync(review);
                await _unitOfWork.CompleteAsync();

                return new ReviewResponse(review);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ReviewResponse($"An error occurred when saving the review: {ex.Message}");
            }
        }

        public async Task<ReviewResponse> UpdateAsync(int id, Review review)
        {
            var existingReview = await _reviewRepository.FindByIdAsync(id);

            if (existingReview == null)
                return new ReviewResponse("Review not found.");

            var existingSalon = await _salonRepository.FindByIdAsync(review.SalonId);
            if (existingSalon == null)
                return new ReviewResponse("Invalid salon.");

            existingReview.Rating = review.Rating;
            existingReview.Description = review.Description;
            existingReview.SalonId = review.SalonId;

            try
            {
                _reviewRepository.Update(existingReview);
                await _unitOfWork.CompleteAsync();

                return new ReviewResponse(existingReview);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ReviewResponse($"An error occurred when updating the review: {ex.Message}");
            }
        }

        public async Task<ReviewResponse> DeleteAsync(int id)
        {
            var existingReview = await _reviewRepository.FindByIdAsync(id);

            if (existingReview == null)
                return new ReviewResponse("Review not found.");

            try
            {
                _reviewRepository.Remove(existingReview);
                await _unitOfWork.CompleteAsync();

                return new ReviewResponse(existingReview);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new ReviewResponse($"An error occurred when deleting the review: {ex.Message}");
            }
        }

        public async Task<ReviewResponse> GetReviewAsync(int id)
        {
            var existingReview = await _reviewRepository.FindByIdAsync(id);
            if (existingReview == null)
                return new ReviewResponse("Review not found.");
            else
            {
                return new ReviewResponse(existingReview);
            }
        }
    }
}
