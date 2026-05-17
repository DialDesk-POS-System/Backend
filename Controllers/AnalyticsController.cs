using DialDesk.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger)
    {
        _analyticsService = analyticsService;
        _logger = logger;
    }

    [HttpGet("total-units")]
    public async Task<ActionResult<int>> GetTotalUnits()
    {
        try
        {
            return await _analyticsService.GetTotalUnitsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching total units");
            return StatusCode(500, "Error fetching total units");
        }
    }

    [HttpGet("total-models")]
    public async Task<ActionResult<int>> GetTotalModels()
    {
        try
        {
            return await _analyticsService.GetTotalModelsAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching total models");
            return StatusCode(500, "Error fetching total models");
        }
    }

    [HttpGet("low-stock-count")]
    public async Task<ActionResult<int>> GetLowStockCount([FromQuery] int threshold = 5)
    {
        try
        {
            return await _analyticsService.GetLowStockCountAsync(threshold);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching low stock count");
            return StatusCode(500, "Error fetching low stock count");
        }
    }

    [HttpGet("total-stock-value")]
    public async Task<ActionResult<decimal>> GetTotalStockValue()
    {
        try
        {
            return await _analyticsService.GetTotalStockValueAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching total stock value");
            return StatusCode(500, "Error fetching total stock value");
        }
    }
}
