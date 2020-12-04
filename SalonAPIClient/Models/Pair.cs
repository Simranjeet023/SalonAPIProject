using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SalonAPIClient.Models
{
    public class Pair
    {
        public Pair()
        {
            Salon = new SalonDTO();
        }
        [System.Text.Json.Serialization.JsonIgnore]
        public SalonDTO Salon { get; set; }
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
    }
}
