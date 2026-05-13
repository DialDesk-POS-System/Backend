using AutoMapper;
using DialDesk.Server.DTOs.Watch;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatchController : ControllerBase
    {
        private readonly IWatchService _watchService;
        private readonly IMapper _mapper;

        public WatchController(IWatchService watchService, IMapper mapper)
        {
            _watchService = watchService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<WatchOutDto>>> GetAll()
        {
            var watches = await _watchService.GetAllWatchesAsync();
            return Ok(_mapper.Map<List<WatchOutDto>>(watches));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WatchOutDto>> GetById(Guid id)
        {
            var watch = await _watchService.GetWatchByIdAsync(id);
            if (watch == null) return NotFound();
            return Ok(_mapper.Map<WatchOutDto>(watch));
        }

        [HttpGet("by-model/{modelId:int}")]
        public async Task<ActionResult<List<WatchOutDto>>> GetByModel(int modelId)
        {
            var watches = await _watchService.GetWatchesByModelAsync(modelId);
            return Ok(_mapper.Map<List<WatchOutDto>>(watches));
        }

        [HttpGet("by-serial/{serialNumber}")]
        public async Task<ActionResult<WatchOutDto>> GetBySerial(string serialNumber)
        {
            var watch = await _watchService.GetWatchBySerialNumberAsync(serialNumber);
            if (watch == null) return NotFound();
            return Ok(_mapper.Map<WatchOutDto>(watch));
        }

        [HttpGet("by-status/{status}")]
        public async Task<ActionResult<List<WatchOutDto>>> GetByStatus(Status status)
        {
            var watches = await _watchService.GetWatchesByStatusAsync(status);
            return Ok(_mapper.Map<List<WatchOutDto>>(watches));
        }

        [HttpGet("by-cost-price")]
        public async Task<ActionResult<List<WatchOutDto>>> GetByCostPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
        {
            var watches = await _watchService.GetWatchesBetweenCostPriceAsync(min, max);
            return Ok(_mapper.Map<List<WatchOutDto>>(watches));
        }

        [HttpGet("by-selling-price")]
        public async Task<ActionResult<List<WatchOutDto>>> GetBySellingPriceRange([FromQuery] decimal min, [FromQuery] decimal max)
        {
            var watches = await _watchService.GetWatchesBetweenSellingPriceAsync(min, max);
            return Ok(_mapper.Map<List<WatchOutDto>>(watches));
        }

        [HttpPost]
        public async Task<ActionResult<WatchOutDto>> Create([FromBody] WatchCreateDto dto)
        {
            var watch = _mapper.Map<Watch>(dto);
            var created = await _watchService.CreateWatchAsync(watch);
            return CreatedAtAction(nameof(GetById), new { id = created!.Id }, _mapper.Map<WatchOutDto>(created));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<WatchOutDto>> Update(Guid id, [FromBody] WatchUpdateDto dto)
        {
            var updated = await _watchService.UpdateWatchAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(_mapper.Map<WatchOutDto>(updated));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _watchService.DeleteWatchAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
