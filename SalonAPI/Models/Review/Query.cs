using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Models.Review
{
    public class Query
    {
        public int Page { get; protected set; }
        public int ReviewsPerPage { get; protected set; }

        public Query(int page, int reviewsPerPage)
        {
            Page = page;
            ReviewsPerPage = reviewsPerPage;

            if (Page <= 0)
            {
                Page = 1;
            }

            if (ReviewsPerPage <= 0)
            {
                ReviewsPerPage = 10;
            }
        }
    }
}
