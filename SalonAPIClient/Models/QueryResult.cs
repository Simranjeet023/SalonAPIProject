using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPIClient.Models
{
    public class QueryResult<T>
    {
        public List<T> Reviews { get; set; } = new List<T>();
        public int TotalReviews { get; set; } = 0;
    }
}
