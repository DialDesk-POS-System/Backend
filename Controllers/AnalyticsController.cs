using AutoMapper;
using DialDesk.Server.DTOs.Model;
using DialDesk.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DialDesk.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController : ControllerBase
{
    private readonly IAnalyticsService _analyticsService;
    private readonly IMapper _mapper;
    private readonly ILogger<AnalyticsController> _logger;

    public AnalyticsController(IAnalyticsService analyticsService, IMapper mapper, ILogger<AnalyticsController> logger)
    {
        _analyticsService = analyticsService;
        _mapper = mapper;
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

    [HttpGet("low-stock-models")]
    public async Task<ActionResult<List<ModelOutDto>>> GetLowStockModels()
    {
        try
        {
            var models = await _analyticsService.GetLowStockModels();
            return Ok(_mapper.Map<List<ModelOutDto>>(models));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching low stock models");
            return StatusCode(500, "Error fetching low stock models");
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

    [HttpGet("today-revenue")]
    public async Task<ActionResult<decimal>> GetTodayRevenue([FromQuery] DateTime date)
    {
        try
        {
            return await _analyticsService.GetTodayRevenue(date);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching today's revenue");
            return StatusCode(500, "Error fetching today's revenue");
        }
    }
}
