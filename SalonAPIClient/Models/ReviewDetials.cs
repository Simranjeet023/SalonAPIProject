using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPIClient.Models
{
    public class ReviewDetials
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public SalonDTO Salon { get; set; }
    }
}
