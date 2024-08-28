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
    public class FeatureCategoryController : ControllerBase
    {
        private readonly IFeatureCategoryService _featureCategoryService;

        public FeatureCategoryController(IFeatureCategoryService featureCategoryService)
        {
            _featureCategoryService = featureCategoryService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _featureCategoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var category = await _featureCategoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FeatureCategoryDto featureCategoryDto)
        {
            await _featureCategoryService.AddAsync(featureCategoryDto);
            return CreatedAtAction(nameof(Get), new { id = featureCategoryDto.Id }, featureCategoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] FeatureCategoryDto featureCategoryDto)
        {
            featureCategoryDto.Id = id;
            await _featureCategoryService.UpdateAsync(featureCategoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _featureCategoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
