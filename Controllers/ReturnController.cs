using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnController : ControllerBase
    {
        public readonly IReturnService _returnService;

        public ReturnController(IReturnService returnService)
        {
            _returnService = returnService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _returnService.GetAllAsync());

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpPost]
        public async Task<IActionResult> CreateReturn(Return returnRequest)
        {
            try
            {
                var createdReturn = await _returnService.CreateReturnAsync(returnRequest);
                return Ok(createdReturn);

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
                var  returnItem = await _returnService.GetByIdAsync(id);
                if (returnItem == null)
                {
                    return NotFound();
                }
                return Ok(returnItem);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("sale/{saleItemId}")]
        public async Task<IActionResult> GetBySaleItem(int saleItemId)
        {
            try
            {
                var returnItem = await _returnService.GetBySaleItemAsync(saleItemId);
                if (returnItem == null)
                {
                    return NotFound();
                }

                return Ok(returnItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Return returnItem)
        {
            try
            {
                var item = await _returnService.UpdateAsync(id, returnItem);
                if (!item)
                {
                    return NotFound();
                }   

                return Ok("Return updated successfully");


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
                var deleted = await _returnService.DeleteAsync(id);
                if (!deleted)
                {
                    return NotFound();
                }
                return Ok("Return deleted successfully");   

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
