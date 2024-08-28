using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.DTOs;
using RealEstateApp.Services;
using System.Threading.Tasks;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RealEstateFeatureController : ControllerBase
    {
        private readonly IRealEstateFeatureService _realEstateFeatureService;

        public RealEstateFeatureController(IRealEstateFeatureService realEstateFeatureService)
        {
            _realEstateFeatureService = realEstateFeatureService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var features = await _realEstateFeatureService.GetAllAsync();
            return Ok(features);
        }

        [HttpGet("{realEstateId}/{featureId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int realEstateId, int featureId)
        {
            var feature = await _realEstateFeatureService.GetByIdAsync(realEstateId, featureId);
            if (feature == null)
                return NotFound();
            return Ok(feature);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RealEstateFeatureDto realEstateFeatureDto)
        {
            await _realEstateFeatureService.AddAsync(realEstateFeatureDto);
            return CreatedAtAction(nameof(Get), new { realEstateId = realEstateFeatureDto.RealEstateId, featureId = realEstateFeatureDto.FeatureId }, realEstateFeatureDto);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulk([FromBody] RealEstateFeatureBulkDto realEstateFeatureBulkDto)
        {
            await _realEstateFeatureService.AddBulkAsync(realEstateFeatureBulkDto);
            return CreatedAtAction(nameof(GetAll), null);
        }

        [HttpDelete("{realEstateId}/{featureId}")]
        public async Task<IActionResult> Delete(int realEstateId, int featureId)
        {
            await _realEstateFeatureService.DeleteAsync(realEstateId, featureId);
            return NoContent();
        }
    }
}
