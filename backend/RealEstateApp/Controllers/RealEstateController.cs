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
    [Authorize(Roles = "Admin, User")]
    public class RealEstateController : ControllerBase
    {
        private readonly IRealEstateService _realEstateService;

        public RealEstateController(IRealEstateService realEstateService)
        {
            _realEstateService = realEstateService;
        }

        [HttpPost("uploadPhotos")]
        public async Task<IActionResult> UploadPhotos(List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
                return BadRequest(new { Status = "Error", Message = "Files not selected" });

            var uploadedFilePaths = new List<string>();

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            foreach (var file in files)
            {
                var filePath = Path.Combine(uploadsFolder, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                uploadedFilePaths.Add(Path.Combine("uploads", file.FileName));
            }

            return Ok(new { Status = "Success", FilePaths = uploadedFilePaths });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] RealEstateQueryParameters queryParameters)
        {
            var realEstates = await _realEstateService.GetPagedRealEstatesAsync(queryParameters);
            return Ok(realEstates);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var realEstate = await _realEstateService.GetByIdAsync(id);
            if (realEstate == null)
                return NotFound();
            return Ok(realEstate);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RealEstateDto realEstateDto)
        {
            var ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            realEstateDto.OwnerId = ownerId;

            await _realEstateService.AddAsync(realEstateDto);
            return CreatedAtAction(nameof(Get), new { id = realEstateDto.Id }, realEstateDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RealEstateEditDto realEstateEditDto)
        {
            realEstateEditDto.Id = id;
            await _realEstateService.UpdateAsync(realEstateEditDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _realEstateService.DeleteAsync(id);
            return NoContent();
        }
    }
}
