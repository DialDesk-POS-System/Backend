using DialDesk.Server.Data;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Services;

public class AnalyticsService : IAnalyticsService
{
    private readonly AppDbContext _context;
    private readonly ILogger<AnalyticsService> _logger;

    public AnalyticsService(AppDbContext context, ILogger<AnalyticsService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> GetTotalUnitsAsync()
    {
        try
        {
            return await _context.Watches
            .Where(w => w.Status == Status.Available)
            .CountAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting total units");
            throw;
        }
    }

    public async Task<int> GetTotalModelsAsync()
    {
        try
        {
            return await _context.Models
            .Where(m => m.IsActive)
            .CountAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting total models");
            throw;
        }
    }

    public async Task<int> GetTotalBrandsAsync()
    {
        try
        {
            return await _context.Brands
            .Where(b => b.IsActive)
            .CountAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting total brands");
            throw;
        }
    }

    public async Task<int> GetLowStockCountAsync(int threshold = 5)
    {
        try
        {
            return await _context.Models
            .CountAsync(m =>
                m.Watches.Count(w => w.Status == Status.Available) <= threshold
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting low stock count");
            throw;
        }
    }

    public async Task<decimal> GetTotalStockValueAsync()
    {
        try
        {
            return await _context.Watches
            .Where(w => w.Status == Status.Available)
            .SumAsync(w => w.CostPrice);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting total stock value");
            throw;
        }
    }
}
