using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalonAPI.Context;
using SalonAPI.Entities;
using SalonAPI.Models.Salon;
using SalonAPI.Services;

namespace SalonAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalonsController : ControllerBase
    {
        private readonly SalonDBContext _context;
        private readonly ISalonService _salonService;
        private readonly IMapper _mapper;

        public SalonsController(SalonDBContext context, ISalonService salonService, IMapper mapper)
        {
            _context = context;
            _salonService = salonService;
            _mapper = mapper;

            if (_context.Salons.Count() == 0)
            {
                _context.Salons.Add(new Salon
                {
                    Name = "NHK beauty Salon Inc",
                    Address = "2480 Eglinton Ave E, Scarborough, ON M1K 2R4",
                    Hours = "Open 8:00AM Close 7:30PM",
                    Phone = "(416) 356-7129",
                    Province = "Ontario"
                });
                _context.Salons.Add(new Salon
                {
                    Name = "Bella Jin Hair Salon",
                    Address = "5931 Yonge St, North York, ON M2M 3V7",
                    Hours = "Open 10:00AM Close 7:30PM",
                    Phone = "(416) 224-9411",
                    Province = "Ontario"
                });
                _context.SaveChanges();
                _salonService = salonService;
            }
        }

        // GET: api/Salons
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<SalonDTO>), 200)]
        //public async Task<IEnumerable<SalonDTO>> ListAsync()
        //{
        //    var salons = await _salonService.ListAsync();

        //    var resources = _mapper.Map<IEnumerable<Salon>, IEnumerable<SalonDTO>>(salons);

        //    return resources;
        //}

        // GET: api/Salons
        [HttpGet("Salon.{format}"), FormatFilter]
        [ProducesResponseType(typeof(IEnumerable<SalonDTO>), 200)]
        public async Task<IEnumerable<SalonDTO>> ListAsync()
        {
            var salons = await _salonService.ListAsync();

            var resources = _mapper.Map<IEnumerable<Salon>, IEnumerable<SalonDTO>>(salons);

            return resources;
        }

        // GET: api/Salons/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Salon), 200)]
        public async Task<ActionResult<SalonDTO>> GetSalon(int id)
        {

            var salon = await _salonService.GetSalonAsync(id);
            var resources = _mapper.Map<Salon, SalonDTO>(salon);
            if (salon == null)
            {
                return NotFound();
            }
            return resources;
        }

        // PUT: api/Salons/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SalonDTO), 200)]
        public async Task<IActionResult> PutSalon(int id, [FromBody] SaveSalonDTO resource)
        {
            var salon = _mapper.Map<SaveSalonDTO, Salon>(resource);
            var result = await _salonService.UpdateAsync(id, salon);

            if (!result.Success)
            {
                return NotFound();
            }

            var salonResource = _mapper.Map<Salon, SalonDTO>(result.Resource);
            return Ok(salonResource);
        }

        // POST: api/Salons
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(typeof(SalonDTO), 201)]
        public async Task<IActionResult> PostSalon([FromBody] SaveSalonDTO resource)
        {
            var salon = _mapper.Map<SaveSalonDTO, Salon>(resource);
            var result = await _salonService.SaveAsync(salon);

            if (!result.Success)
            {
                return NotFound();
            }

            var salonResource = _mapper.Map<Salon, SalonDTO>(result.Resource);
            return Ok(salonResource);
        }

        // DELETE: api/Salon/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SalonDTO), 200)]
        public async Task<IActionResult> DeleteSalon(int id)
        {
            var result = await _salonService.DeleteAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            var salonResource = _mapper.Map<Salon, SalonDTO>(result.Resource);
            return Ok(salonResource);
        }
    }
}
