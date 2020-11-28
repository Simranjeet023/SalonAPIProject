using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Entities
{
    public class Salon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Hours { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }

        public IList<Review> Reviews { get; set; }
            = new List<Review>();
    }
}
