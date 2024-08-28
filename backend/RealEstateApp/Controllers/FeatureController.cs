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
    public class FeatureController : ControllerBase
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var features = await _featureService.GetAllAsync();
            return Ok(features);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var feature = await _featureService.GetByIdAsync(id);
            if (feature == null)
                return NotFound();
            return Ok(feature);
        }

        [HttpGet("GetByCategoryId/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByCategoryId(int categoryId)
        {
            var features = await _featureService.GetByCategoryIdAsync(categoryId);
            return Ok(features);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FeatureDto featureDto)
        {
            await _featureService.AddAsync(featureDto);
            return CreatedAtAction(nameof(Get), new { id = featureDto.Id }, featureDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FeatureDto featureDto)
        {
            featureDto.Id = id;
            await _featureService.UpdateAsync(featureDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _featureService.DeleteAsync(id);
            return NoContent();
        }
    }
}
