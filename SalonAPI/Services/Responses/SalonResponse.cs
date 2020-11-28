using SalonAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Services.Responses
{
    public class SalonResponse : BaseResponse<Salon>
    {
        public SalonResponse(Salon salon) : base(salon)
        { }
        public SalonResponse(string message) : base(message)
        { }
    }
}
