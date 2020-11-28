using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalonAPI.Context;
using SalonAPI.Entities;
using SalonAPI.Models.Review;
using SalonAPI.Services;

namespace SalonAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly SalonDBContext _context;
        private readonly IReviewService _reviewService;
        private readonly IMapper _mapper;

        public ReviewsController(SalonDBContext context, IReviewService reviewService, IMapper mapper)
        {
            _context = context;
            _reviewService = reviewService;
            _mapper = mapper;

            if (_context.Reviews.Count() == 0)
            {
                _context.Reviews.Add(new Review
                {
                    Rating = 4.3,
                    Description = "I went for layered haircut and highlights. Bella and her team was really friendly from the beginning. I showed her a picture which I like and she did a great job. This was my first time coloring my hair and I am really happy about their work.",
                    SalonId = 1,
                });
                _context.Reviews.Add(new Review
                {
                    Rating = 5.0,
                    Description = "I've been to Bella a few times now and love her cuts! I'm quite particular with how I want my hair and she does an amazing job! The price has increased since my first cut which was disappointing to hear but the results are amazing and worth it!",
                    SalonId = 1,
                });
                _context.Reviews.Add(new Review
                {
                    Rating = 3.0,
                    Description = "Hit and miss. Just wants to get haircut done as soon possible so they go back to gossiping and attend to their phones. How about you listen? Or ask the customer what they want? If you are in a pinch maybe come here but I’ve been here multiple times and it’s not worth it. I’m not going back anymore.",
                    SalonId = 2,
                });
                _context.SaveChanges();
            }
        }

        // GET: api/Reviews
        [HttpGet]
        public async Task<IEnumerable<ReviewDTO>> ListAsync()
        {
            var queryResult = await _reviewService.ListAsync();

            var resources = _mapper.Map<IEnumerable<ReviewDTO>>(queryResult);
            return resources;
        }


        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview(int id)
        {
            var review = await _reviewService.GetReviewAsync(id);

            if (review == null)
            {
                return NotFound();
            }
            var Review = _mapper.Map<Review, ReviewDTO>(review.Resource);
            return Ok(Review);
        }

        // PUT: api/Reviews/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ReviewDTO), 201)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveReview resource)
        {
            var review = _mapper.Map<SaveReview, Review>(resource);
            var result = await _reviewService.UpdateAsync(id, review);

            if (!result.Success)
            {
                return NotFound();
            }

            var Review = _mapper.Map<Review, ReviewDTO>(result.Resource);
            return Ok(Review);
        }

        // POST: api/Reviews
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(typeof(ReviewDTO), 201)]
        public async Task<IActionResult> PostAsync([FromBody] SaveReview resource)
        {
            var review = _mapper.Map<SaveReview, Review>(resource);
            var result = await _reviewService.SaveAsync(review);


            if (!result.Success)
            {
                return NotFound();
            }

            var Review = _mapper.Map<Review, ReviewDTO>(result.Resource);
            return Ok(Review);
        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ReviewDTO), 200)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _reviewService.DeleteAsync(id);

            if (!result.Success)
            {
                return NotFound();
            }

            var Review = _mapper.Map<Review, ReviewDTO>(result.Resource);
            return Ok(Review);
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
