using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Entities
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
        public int SalonId { get; set; }
        public Salon Salon { get; set; }
        //public object Salon { get; internal set; }
    }
}
