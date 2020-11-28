using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPIClient.Models
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
    }
}
