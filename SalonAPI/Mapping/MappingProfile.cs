﻿using AutoMapper;
using SalonAPI.Entities;
using SalonAPI.Models.Review;
using SalonAPI.Models.Salon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalonAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SaveSalonDTO, Salon>();
            CreateMap<Salon, SalonDTO>();

            CreateMap<SaveReview, Review>();
            CreateMap<ReviewsQueryResource, ReviewsQuery>();
            CreateMap<ReviewsQuery, ReviewsQueryResource>();
            CreateMap<Review, ReviewDTO>();
            CreateMap<QueryResult<Review>, QueryResultResource<ReviewDTO>>();
        }
    }
}
