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
    public class RealEstateTypeFeatureController : ControllerBase
    {
        private readonly IRealEstateTypeFeatureService _realEstateTypeFeatureService;

        public RealEstateTypeFeatureController(IRealEstateTypeFeatureService realEstateTypeFeatureService)
        {
            _realEstateTypeFeatureService = realEstateTypeFeatureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var features = await _realEstateTypeFeatureService.GetAllAsync();
            return Ok(features);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RealEstateTypeFeatureDto featureDto)
        {
            foreach (var featureId in featureDto.FeatureIds)
            {
                var realEstateTypeFeature = new RealEstateTypeFeatureDto
                {
                    RealEstateTypeId = featureDto.RealEstateTypeId,
                    FeatureIds = new List<int> { featureId } 
                };

                await _realEstateTypeFeatureService.AddAsync(realEstateTypeFeature);
            }

            return CreatedAtAction(nameof(GetAll), new { realEstateTypeId = featureDto.RealEstateTypeId }, featureDto);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int realEstateTypeId, int featureId)
        {
            await _realEstateTypeFeatureService.DeleteAsync(realEstateTypeId, featureId);
            return NoContent();
        }
    }
}
