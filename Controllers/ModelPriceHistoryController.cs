using AutoMapper;
using DialDesk.Server.DTOs;
using DialDesk.Server.DTOs.ModelPriceRecord;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModelPriceHistoryController : ControllerBase
    {
        private readonly IModelPriceHistoryService _priceHistoryService;
        private readonly IMapper _mapper;

        public ModelPriceHistoryController(IModelPriceHistoryService priceHistoryService, IMapper mapper)
        {
            _priceHistoryService = priceHistoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ModelPriceHistory>>> GetAll()
        {
            var records = await _priceHistoryService.GetAllRecordsAsync();
            return Ok(_mapper.Map<List<ModelHistoryCreateDto>>(records));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ModelPriceHistory>> GetById(int id)
        {
            var record = await _priceHistoryService.GetRecordByIdAsync(id);
            if (record == null) return NotFound();
            return Ok(record);
        }

        [HttpGet("by-model/{modelId:int}")]
        public async Task<ActionResult<List<ModelPriceHistory>>> GetByModel(int modelId)
        {
            var records = await _priceHistoryService.GetRecordsByModelAsync(modelId);
            return Ok(records);
        }

        [HttpPost("search")]
        public async Task<ActionResult<List<ModelPriceHistory>>> Search([FromBody] ModelPriceHistoryRecordSearch filter)
        {
            var records = await _priceHistoryService.SearchRecordsAsync(filter);
            return Ok(records);
        }

        [HttpPost]
        public async Task<ActionResult<ModelPriceHistory>> Create([FromBody] ModelHistoryCreateDto dto)
        {
            var record = _mapper.Map<ModelPriceHistory>(dto);
            var created = await _priceHistoryService.CreateRecordAsync(record);
            return CreatedAtAction(nameof(GetById), new { id = created!.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<ModelPriceHistory>> Update(int id, [FromBody] ModelHistoryUpdateDto dto)
        {
            var updated = await _priceHistoryService.UpdateRecordAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _priceHistoryService.DeleteRecordAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
