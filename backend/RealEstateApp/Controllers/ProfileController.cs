using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.DTOs;
using RealEstateApp.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var profile = await _profileService.GetProfileAsync(userId);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileDto profileDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _profileService.UpdateProfileAsync(userId, profileDto);

            if (!result)
                return StatusCode(500, "An error occurred while updating the profile.");

            return NoContent();
        }
    }
}
