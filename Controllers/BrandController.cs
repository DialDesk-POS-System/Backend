using AutoMapper;
using DialDesk.Server.DTOs.Brand;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandOutDto>>> GetAll()
        {
            var brands = await _brandService.GetAllBrandsAsync();
            return Ok(_mapper.Map<List<BrandOutDto>>(brands));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<BrandOutDto>> GetById(int id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null) return NotFound();
            return Ok(_mapper.Map<BrandOutDto>(brand));
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<BrandOutDto>>> GetByName([FromQuery] string name)
        {
            var brands = await _brandService.GetBrandByNameAsync(name);
            return Ok(_mapper.Map<List<BrandOutDto>>(brands));
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<BrandOutDto>>> GetActive()
        {
            var brands = await _brandService.GetBrandsActiveAsync();
            return Ok(_mapper.Map<List<BrandOutDto>>(brands));
        }

        [HttpPost]
        public async Task<ActionResult<BrandOutDto>> Create([FromBody] BrandInDto dto)
        {
            var brand = _mapper.Map<Brand>(dto);
            var created = await _brandService.CreateBrandAsync(brand);
            return CreatedAtAction(nameof(GetById), new { id = created!.Id }, _mapper.Map<BrandOutDto>(created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<BrandOutDto>> Update(int id, [FromBody] BrandInDto dto)
        {
            var updated = await _brandService.UpdateBrandAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(_mapper.Map<BrandOutDto>(updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _brandService.DeleteBrandAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
