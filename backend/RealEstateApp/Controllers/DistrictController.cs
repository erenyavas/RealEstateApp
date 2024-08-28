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
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictService _districtService;

        public DistrictController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var districts = await _districtService.GetAllAsync();
            return Ok(districts);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var district = await _districtService.GetByIdAsync(id);
            if (district == null)
                return NotFound();
            return Ok(district);
        }

        [HttpGet("byCity/{cityId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDistrictsByCityId(int cityId)
        {
            var districts = await _districtService.GetDistrictsByCityIdAsync(cityId);
            return Ok(districts);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DistrictDto districtDto)
        {
            await _districtService.AddAsync(districtDto);
            return CreatedAtAction(nameof(Get), new { id = districtDto.Id }, districtDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DistrictDto districtDto)
        {
            districtDto.Id = id;
            await _districtService.UpdateAsync(districtDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _districtService.DeleteAsync(id);
            return NoContent();
        }
    }
}
