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
    public class RealEstateTypeFeatureCategoryController : ControllerBase
    {
        private readonly IRealEstateTypeFeatureCategoryService _realEstateTypeFeatureCategoryService;

        public RealEstateTypeFeatureCategoryController(IRealEstateTypeFeatureCategoryService realEstateTypeFeatureCategoryService)
        {
            _realEstateTypeFeatureCategoryService = realEstateTypeFeatureCategoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _realEstateTypeFeatureCategoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{realEstateTypeId}/{featureCategoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int realEstateTypeId, int featureCategoryId)
        {
            var category = await _realEstateTypeFeatureCategoryService.GetByIdAsync(realEstateTypeId, featureCategoryId);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RealEstateTypeFeatureCategoryDto realEstateTypeFeatureCategoryDto)
        {
            await _realEstateTypeFeatureCategoryService.AddAsync(realEstateTypeFeatureCategoryDto);
            return CreatedAtAction(nameof(Get), new { realEstateTypeId = realEstateTypeFeatureCategoryDto.RealEstateTypeId, featureCategoryId = realEstateTypeFeatureCategoryDto.FeatureCategoryId }, realEstateTypeFeatureCategoryDto);
        }

        [HttpPost("AddCategories")]
        public async Task<IActionResult> AddCategories([FromBody] RealEstateTypeFeatureCategoryRequestDto request)
        {
            await _realEstateTypeFeatureCategoryService.AddCategoriesAsync(request.RealEstateTypeId, request.CategoryIds);
            return Ok();
        }

        [HttpDelete("{realEstateTypeId}/{featureCategoryId}")]
        public async Task<IActionResult> Delete(int realEstateTypeId, int featureCategoryId)
        {
            await _realEstateTypeFeatureCategoryService.DeleteAsync(realEstateTypeId, featureCategoryId);
            return NoContent();
        }
    }
}
