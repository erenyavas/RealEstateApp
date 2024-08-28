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
    public class NeighborhoodController : ControllerBase
    {
        private readonly INeighborhoodService _neighborhoodService;

        public NeighborhoodController(INeighborhoodService neighborhoodService)
        {
            _neighborhoodService = neighborhoodService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var neighborhoods = await _neighborhoodService.GetAllAsync();
            return Ok(neighborhoods);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var neighborhood = await _neighborhoodService.GetByIdAsync(id);
            if (neighborhood == null)
                return NotFound();
            return Ok(neighborhood);
        }

        [HttpGet("byDistrict/{districtId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNeighborhoodsByDistrictId(int districtId)
        {
            var neighborhoods = await _neighborhoodService.GetNeighborhoodsByDistrictIdAsync(districtId);
            return Ok(neighborhoods);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NeighborhoodDto neighborhoodDto)
        {
            await _neighborhoodService.AddAsync(neighborhoodDto);
            return CreatedAtAction(nameof(Get), new { id = neighborhoodDto.Id }, neighborhoodDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NeighborhoodDto neighborhoodDto)
        {
            neighborhoodDto.Id = id;
            await _neighborhoodService.UpdateAsync(neighborhoodDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _neighborhoodService.DeleteAsync(id);
            return NoContent();
        }
    }
}
