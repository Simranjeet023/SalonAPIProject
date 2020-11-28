using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Models.Salon
{
    public class SaveSalonDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Hours { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
    }
}
