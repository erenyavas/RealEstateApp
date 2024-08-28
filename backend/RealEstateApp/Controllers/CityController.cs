using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.DTOs;
using RealEstateApp.Services;
using System.Threading.Tasks;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var cities = await _cityService.GetAllAsync();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null)
                return NotFound();
            return Ok(city);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CityDto cityDto)
        {
            await _cityService.AddAsync(cityDto);
            return CreatedAtAction(nameof(Get), new { id = cityDto.Id }, cityDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CityDto cityDto)
        {
            cityDto.Id = id;
            await _cityService.UpdateAsync(cityDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _cityService.DeleteAsync(id);
            return NoContent();
        }
    }
}
