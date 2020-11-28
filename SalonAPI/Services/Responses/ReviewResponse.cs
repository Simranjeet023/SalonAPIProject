using SalonAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Services.Responses
{
    public class ReviewResponse : BaseResponse<Review>
    {
        public ReviewResponse(Review review) : base(review) { }

        public ReviewResponse(string message) : base(message) { }
    }
}
