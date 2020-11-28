using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Models.Review
{
    public class ReviewsQuery : Query
    {
        public int? SalonId { get; set; }

        public ReviewsQuery(int? salonId, int page, int reviewsPerPage) : base(page, reviewsPerPage)
        {
            SalonId = salonId;
        }
    }
}
