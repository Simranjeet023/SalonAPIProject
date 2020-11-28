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
           // Review = new ReviewDTO();
            Salon = new SalonDTO();
        }

        //[System.Text.Json.Serialization.JsonIgnore]
        //public ReviewDTO Review { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public SalonDTO Salon { get; set; }

        public int Id { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }


        //[JsonProperty(PropertyName = "salon_id")]
        //public int SalonID
        //{
        //    get { return Salon.Id; }
        //    set { Salon.Id = value; }
        //}

        //public string Name { get; set; }
        //public string Address { get; set; }
        //public string Hours { get; set; }
        //public string Phone { get; set; }
        //public string Province { get; set; }

    }
}
