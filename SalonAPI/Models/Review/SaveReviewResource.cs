﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Models.Review
{
    public class SaveReview
    {
        [Required]
        public double Rating { get; set; }
        public string Description { get; set; }
        public int SalonId { get; set; }
    }
}
