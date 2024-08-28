using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.DTOs;
using RealEstateApp.Services;
using System.Threading.Tasks;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, User")]
    public class RealEstateFeatureValueController : ControllerBase
    {
        private readonly IRealEstateFeatureValueService _realEstateFeatureValueService;

        public RealEstateFeatureValueController(IRealEstateFeatureValueService realEstateFeatureValueService)
        {
            _realEstateFeatureValueService = realEstateFeatureValueService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var featureValues = await _realEstateFeatureValueService.GetAllAsync();
            return Ok(featureValues);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var featureValue = await _realEstateFeatureValueService.GetByIdAsync(id);
            if (featureValue == null)
                return NotFound();
            return Ok(featureValue);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RealEstateFeatureValueDto featureValueDto)
        {
            await _realEstateFeatureValueService.AddAsync(featureValueDto);
            return CreatedAtAction(nameof(Get), new { id = featureValueDto.Id }, featureValueDto);
        }

        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulk([FromBody] RealEstateFeatureValueBulkDto bulkDto)
        {
            await _realEstateFeatureValueService.AddBulkAsync(bulkDto);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RealEstateFeatureValueDto featureValueDto)
        {
            featureValueDto.Id = id;
            await _realEstateFeatureValueService.UpdateAsync(featureValueDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _realEstateFeatureValueService.DeleteAsync(id);
            return NoContent();
        }
    }
}
