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
    public class DynamicFeatureController : ControllerBase
    {
        private readonly IDynamicFeatureService _dynamicFeatureService;

        public DynamicFeatureController(IDynamicFeatureService dynamicFeatureService)
        {
            _dynamicFeatureService = dynamicFeatureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var features = await _dynamicFeatureService.GetAllAsync();
            return Ok(features);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var feature = await _dynamicFeatureService.GetByIdAsync(id);
            if (feature == null)
                return NotFound();
            return Ok(feature);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DynamicFeatureDto featureDto)
        {
            await _dynamicFeatureService.AddAsync(featureDto);
            return CreatedAtAction(nameof(Get), new { id = featureDto.Id }, featureDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DynamicFeatureDto featureDto)
        {
            featureDto.Id = id;
            await _dynamicFeatureService.UpdateAsync(featureDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _dynamicFeatureService.DeleteAsync(id);
            return NoContent();
        }
    }
}
