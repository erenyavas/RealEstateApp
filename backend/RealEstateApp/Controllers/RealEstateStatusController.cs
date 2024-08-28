using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.DTOs;
using RealEstateApp.Models;
using RealEstateApp.Services;
using System.Threading.Tasks;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RealEstateStatusController : ControllerBase
    {
        private readonly IRealEstateStatusService _realEstateStatusService;

        public RealEstateStatusController(IRealEstateStatusService realEstateStatusService)
        {
            _realEstateStatusService = realEstateStatusService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var realEstateStatuses = await _realEstateStatusService.GetAllAsync();
            return Ok(realEstateStatuses);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var realEstateStatus = await _realEstateStatusService.GetByIdAsync(id);
            if (realEstateStatus == null)
                return NotFound();
            return Ok(realEstateStatus);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RealEstateStatusDto realEstateStatusDto)
        {
            var realEstateStatus = new RealEstateStatus
            {
                Name = realEstateStatusDto.Name
            };

            await _realEstateStatusService.AddAsync(realEstateStatus);
            return CreatedAtAction(nameof(Get), new { id = realEstateStatus.Id }, realEstateStatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RealEstateStatusDto realEstateStatusDto)
        {
            var existingRealEstateStatus = await _realEstateStatusService.GetByIdAsync(id);
            if (existingRealEstateStatus == null)
                return NotFound();

            existingRealEstateStatus.Name = realEstateStatusDto.Name;

            await _realEstateStatusService.UpdateAsync(existingRealEstateStatus);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var realEstateStatus = await _realEstateStatusService.GetByIdAsync(id);
            if (realEstateStatus == null)
                return NotFound();

            await _realEstateStatusService.DeleteAsync(realEstateStatus);
            return NoContent();
        }
    }
}
