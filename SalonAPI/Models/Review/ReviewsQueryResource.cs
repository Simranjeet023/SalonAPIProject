using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Models.Review
{
    public class ReviewsQueryResource : QueryResource
    {
        public int? SalonId { get; set; }
    }
}
