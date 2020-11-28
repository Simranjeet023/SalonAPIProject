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
                /*
                 Notice here we have to check if the salon ID is valid before adding the review, to avoid errors.
                 You can create a method into the SalonService class to return the salon and inject the service here if you prefer, but 
                 it doesn't matter given the API scope.
                */
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

        //private string GetCacheKeyForReviewsQuery(ReviewsQuery query)
        //{
        //    string key = CacheKeys.ReviewsList.ToString();

        //    if (query.SalonId.HasValue && query.SalonId > 0)
        //    {
        //        key = string.Concat(key, "_", query.SalonId.Value);
        //    }

        //    key = string.Concat(key, "_", query.Page, "_", query.ReviewsPerPage);
        //    return key;
        //}

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
