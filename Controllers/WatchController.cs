using AutoMapper;
using DialDesk.Server.DTOs;
using DialDesk.Server.DTOs.Watch;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using DialDesk.Server.Pagination;
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
        public async Task<ActionResult<PagedResult<WatchOutDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var watches = await _watchService.GetAllWatchesAsync(pageNumber, pageSize);
            return Ok(_mapper.Map<PagedResult<WatchOutDto>>(watches));
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

        [HttpPost("search")]
        public async Task<ActionResult<PagedResult<SearchGroupResultDto>>> Search([FromBody] WatchSearchDto filter, [FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var watches = await _watchService.SearchWatchesAsync(filter);

            var mappedWatches = _mapper.Map<List<WatchOutDto>>(watches);
            if (!string.IsNullOrWhiteSpace(filter.SearchQuery))
            {
                var exactMatch = mappedWatches.FirstOrDefault(w => string.Equals(w.SerialNo, filter.SearchQuery, StringComparison.OrdinalIgnoreCase));
                if (exactMatch != null)
                {
                    var singleResult = new List<SearchGroupResultDto>
                    {
                        new SearchGroupResultDto { Type = "watch", Item = exactMatch }
                    };
                    return Ok(new PagedResult<SearchGroupResultDto> { Items = singleResult, TotalCount = 1, PageNumber = 1, PageSize = pageSize });
                }
            }

            var groups = mappedWatches.GroupBy(w => w.ModelId).ToList();
            var result = new List<SearchGroupResultDto>();

            foreach (var group in groups)
            {
                var list = group.ToList();
                if (list.Count == 1)
                {
                    result.Add(new SearchGroupResultDto { Type = "watch", Item = list[0] });
                }
                else
                {
                    result.Add(new SearchGroupResultDto { Type = "model", Items = list, FirstItem = list[0] });
                }
            }

            return Ok(new PagedResult<SearchGroupResultDto>
            {
                TotalCount = result.Count,
                PageNumber = page,
                PageSize = pageSize,
                Items = result.Skip((page - 1) * pageSize).Take(pageSize).ToList()
            });
        }
    }
}
