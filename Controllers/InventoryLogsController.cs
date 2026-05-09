using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryLogsController : ControllerBase
    {
        public readonly IInventoryLogService _inventoryLogService;

        public InventoryLogsController(IInventoryLogService inventoryLogService)
        {
            _inventoryLogService = inventoryLogService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLog(InventoryLog log)
        {
            try
            {
                return Ok(await _inventoryLogService.CreateLogAsync(log));

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
                var log = await _inventoryLogService.GetByIdAsync(id);
                if (log == null)
                {
                    return NotFound();
                }

                return Ok(log);

            }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }

            }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
            {

                try
                {
                return Ok(await _inventoryLogService.GetAllAsync());

            }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

        [HttpGet("watch/{watchId}")]
        public async Task<IActionResult> GetByWatchIdAsync(string watchId)
            {
                try
                {
                return Ok(await _inventoryLogService.GetByWatchIdAsync(watchId));

            }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }

        [HttpGet("changeType/{changeType}")]
        public async Task<IActionResult> GetByChangeTypeAsync(ChangeType changeType)
            {

                try
                {
                return Ok(await _inventoryLogService.GetByChangeTypeAsync(changeType));

            }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }

            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
            {

                try
                {
                var deleted = await _inventoryLogService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return Ok("Inventory log deleted successfully");

            }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Internal server error: {ex.Message}");
                }
            }
    }
}
