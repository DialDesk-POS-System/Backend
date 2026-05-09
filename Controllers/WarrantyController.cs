using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarrantyController : ControllerBase
    {
        public readonly IWarrantyService _warrantyService;

        public WarrantyController(IWarrantyService warrantyService)
        {
            _warrantyService = warrantyService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var warranty = await _warrantyService.GetByIdAsync(id);
                if (warranty == null)
                {
                    return NotFound();
                }
                return Ok(warranty);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarrantyAsync(Warranty warranty)
        {
            try
            {
                var create = await _warrantyService.CreateWarrantyAsync(warranty);
                return Ok(create);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("sale/{saleItemId}")]
        public async Task<IActionResult> GetBySaleItemIdAsync(int saleItemId)
        {
            try
            {
                var saleItem = await _warrantyService.GetBySaleItemIdAsync(saleItemId);
                if (saleItem == null)
                {
                    return NotFound();
                }

                return Ok(saleItem);

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
                var watch = await _warrantyService.GetByWatchIdAsync(watchId);
                if (watch == null)
                {
                    return NotFound();
                }
                return Ok(watch);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("claim/{warrantyId}")]
        public async Task<IActionResult> ClaimWarrantyAsync(int warrantyId, DateTime claimDate)
        {
            try
            {
                await _warrantyService.ClaimWarrantyAsync(warrantyId, claimDate);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{warrantyId}")]
        public async Task<IActionResult> DeleteWarrentyAsync(int warrantyId)
        {
            try
            {
                var deleted = await _warrantyService.DeleteWarrentyAsync(warrantyId);
                if(!deleted)
                    return NotFound();

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }


}
