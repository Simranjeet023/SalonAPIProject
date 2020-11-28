using SalonAPI.Entities;
using SalonAPI.Models.Salon;
using SalonAPI.Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Services
{
    public interface ISalonService
    {
        Task<IEnumerable<Salon>> ListAsync();
        Task<Salon> GetSalonAsync(int salonId);
        Task<SalonResponse> SaveAsync(Salon salon);
        Task<SalonResponse> UpdateAsync(int salonId, Salon salon);
        Task<SalonResponse> DeleteAsync(int salonId);
    }
}
