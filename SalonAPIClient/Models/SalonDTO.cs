using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPIClient.Models
{
    public class SalonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Hours { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
    }
}
