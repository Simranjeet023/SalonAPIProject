using Microsoft.Extensions.Caching.Memory;
using SalonAPI.Entities;
using SalonAPI.Models.Review;
using SalonAPI.Models.Salon;
using SalonAPI.Repositories;
using SalonAPI.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Services.impl
{
    public class SalonService : ISalonService
    {
        private readonly ISalonRepository _salonRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public SalonService(IReviewService reviewRepository, ISalonRepository salonRepository,  IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _salonRepository = salonRepository;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public async Task<IEnumerable<Salon>> ListAsync()
        {
            // get the salons list from the memory cache. If there is no data in cache, the anonymous method will be
            // called, setting the cache to expire one minute ahead and returning the Task that lists the salons from the repository.
            var salons = await _cache.GetOrCreateAsync(CacheKeys.SalonsList, (entry) => {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _salonRepository.ListAsync();
            });

            return salons;
        }

        public async Task<Salon> GetSalonAsync(int id)
        {
            var existingSalon = await _salonRepository.FindByIdAsync(id);
            return existingSalon;
        }

        public async Task<SalonResponse> SaveAsync(Salon salon)
        {
            try
            {
                await _salonRepository.AddAsync(salon);
                await _unitOfWork.CompleteAsync();

                return new SalonResponse(salon);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SalonResponse($"An error occurred when saving the salon: {ex.Message}");
            }
        }

        public async Task<SalonResponse> UpdateAsync(int id, Salon salon)
        {
            var existingSalon = await _salonRepository.FindByIdAsync(id);

            if (existingSalon == null)
                return new SalonResponse("Salon not found.");

            existingSalon.Name = salon.Name;
            existingSalon.Address = salon.Address;
            existingSalon.Hours = salon.Hours;
            existingSalon.Phone = salon.Phone;
            existingSalon.Province = salon.Province;

            try
            {
                await _unitOfWork.CompleteAsync();

                return new SalonResponse(existingSalon);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SalonResponse($"An error occurred when updating the salon: {ex.Message}");
            }
        }

        public async Task<SalonResponse> DeleteAsync(int id)
        {
            var existingSalon = await _salonRepository.FindByIdAsync(id);

            if (existingSalon == null)
                return new SalonResponse("Salon not found.");

            try
            {
                _salonRepository.Remove(existingSalon);
                await _unitOfWork.CompleteAsync();

                return new SalonResponse(existingSalon);
            }
            catch (Exception ex)
            {
                // Do some logging stuff
                return new SalonResponse($"An error occurred when deleting the salon: {ex.Message}");
            }
        }
    }
}
