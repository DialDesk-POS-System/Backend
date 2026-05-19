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

    public async Task<List<Model>> GetLowStockModels()
    {
        try
        {
            return await _context.Models
            .Where(m =>
                m.Watches.Count(w => w.Status == Status.Available) <= m.LowStockThreshold
            )
            .ToListAsync();
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

    public async Task<decimal> GetTodayRevenue(DateTime date)
    {
        try
        {
            return await _context.Sales
            .Where(s => s.SaleDate == date)
            .SumAsync(s => s.TotalAmount);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error getting today's revenue");
            throw;
        }
    }
}
