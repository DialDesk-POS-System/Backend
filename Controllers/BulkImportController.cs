using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BulkImportController : ControllerBase
    {
        public readonly IBulkImportService _bulkImportService;

        public BulkImportController(IBulkImportService bulkImportService)
        {
            _bulkImportService = bulkImportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateImportAsync(BulkImport bulkImport) { 
                try
                {
                    var createdImport = await _bulkImportService.CreateImportAsync(bulkImport);
                    return Ok(createdImport);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var import =await _bulkImportService.GetByIdAsync(id);
                if (import == null)
                {
                    return NotFound();
                }
                return Ok(import);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _bulkImportService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BulkImport bulkImport)
        {
            try
            {
                var updated = await _bulkImportService.UpdateAsync(id, bulkImport);
                if (!updated)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _bulkImportService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("supplier/{supplier}")]
        public async Task<IActionResult> GetBySupplier(string supplier)
        {
            try
            {
                var item = await _bulkImportService.GetBySupplierAsync(supplier);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("daterange")]
        public async Task<IActionResult> GetByDateRange([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                return Ok(await _bulkImportService.GetByDateRangeAsync(from, to));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
