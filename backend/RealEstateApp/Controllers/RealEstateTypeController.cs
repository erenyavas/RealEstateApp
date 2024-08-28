using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Services;
using System.Threading.Tasks;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RealEstateTypeController : ControllerBase
    {
        private readonly IRealEstateTypeService _realEstateTypeService;

        public RealEstateTypeController(IRealEstateTypeService realEstateTypeService)
        {
            _realEstateTypeService = realEstateTypeService;
        }

        [HttpGet]
        [AllowAnonymous]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> GetAll()
        {
            var realEstateTypes = await _realEstateTypeService.GetAllAsync();
            return Ok(realEstateTypes);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var realEstateType = await _realEstateTypeService.GetByIdAsync(id);
            if (realEstateType == null)
                return NotFound();
            return Ok(realEstateType);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RealEstateTypeDto realEstateTypeDto)
        {
            var realEstateType = new RealEstateType
            {
                Name = realEstateTypeDto.Name
            };

            await _realEstateTypeService.AddAsync(realEstateType);
            return CreatedAtAction(nameof(Get), new { id = realEstateType.Id }, realEstateType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RealEstateTypeDto realEstateTypeDto)
        {
            var existingRealEstateType = await _realEstateTypeService.GetByIdAsync(id);
            if (existingRealEstateType == null)
                return NotFound();

            existingRealEstateType.Name = realEstateTypeDto.Name;

            await _realEstateTypeService.UpdateAsync(existingRealEstateType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var realEstateType = await _realEstateTypeService.GetByIdAsync(id);
            if (realEstateType == null)
                return NotFound();

            await _realEstateTypeService.DeleteAsync(realEstateType);
            return NoContent();
        }
    }
}
