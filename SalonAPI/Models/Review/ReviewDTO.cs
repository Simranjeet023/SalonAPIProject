using SalonAPI.Models.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Models.Review
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public SalonDTO Salon { get; set; }
    }
}
