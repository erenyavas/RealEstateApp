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
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var currencies = await _currencyService.GetAllAsync();
            return Ok(currencies);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var currency = await _currencyService.GetByIdAsync(id);
            if (currency == null)
                return NotFound();
            return Ok(currency);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CurrencyDto currencyDto)
        {
            await _currencyService.AddAsync(currencyDto);
            return CreatedAtAction(nameof(Get), new { id = currencyDto.Id }, currencyDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CurrencyDto currencyDto)
        {
            currencyDto.Id = id;
            await _currencyService.UpdateAsync(currencyDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _currencyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
