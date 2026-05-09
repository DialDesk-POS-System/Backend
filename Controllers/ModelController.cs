using AutoMapper;
using DialDesk.Server.DTOs.Model;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly IMapper _mapper;

        public ModelController(IModelService modelService, IMapper mapper)
        {
            _modelService = modelService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ModelOutDto>>> GetAll()
        {
            var models = await _modelService.GetAllModelsAsync();
            return Ok(_mapper.Map<List<ModelOutDto>>(models));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ModelOutDto>> GetById(int id)
        {
            var model = await _modelService.GetModelByIdAsync(id);
            if (model == null) return NotFound();
            return Ok(_mapper.Map<ModelOutDto>(model));
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<ModelOutDto>>> GetByModelNo([FromQuery] string modelNo)
        {
            var models = await _modelService.GetModelByModelNoAsync(modelNo);
            return Ok(_mapper.Map<List<ModelOutDto>>(models));
        }

        [HttpGet("by-brand/{brandId:int}")]
        public async Task<ActionResult<List<ModelOutDto>>> GetByBrand(int brandId)
        {
            var models = await _modelService.GetModelsByBrandAsync(brandId);
            return Ok(_mapper.Map<List<ModelOutDto>>(models));
        }

        [HttpGet("active")]
        public async Task<ActionResult<List<ModelOutDto>>> GetActive()
        {
            var models = await _modelService.GetModelsActiveAsync();
            return Ok(_mapper.Map<List<ModelOutDto>>(models));
        }

        [HttpPost]
        public async Task<ActionResult<ModelOutDto>> Create([FromBody] ModelInDto dto)
        {
            var model = _mapper.Map<Model>(dto);
            var created = await _modelService.CreateModelAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = created!.Id }, _mapper.Map<ModelOutDto>(created));
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ModelOutDto>> Update(int id, [FromBody] ModelInDto dto)
        {
            var updated = await _modelService.UpdateModelAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(_mapper.Map<ModelOutDto>(updated));
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _modelService.DeleteModelAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
