using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Models.Review
{
    public class QueryResultResource<T>
    {
        public int TotalReviews { get; set; } = 0;
        public List<T> Reviews { get; set; } = new List<T>();
    }
}
