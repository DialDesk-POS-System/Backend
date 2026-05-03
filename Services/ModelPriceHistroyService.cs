using DialDesk.Server.Data;
using DialDesk.Server.DTOs;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Services
{
    public class ModelPriceHistroyService : IModelPriceHistory
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ModelPriceHistroyService> _logger;

        public ModelPriceHistroyService(AppDbContext context, ILogger<ModelPriceHistroyService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ModelPriceHistory>> GetAllRecordsAsync()
        {
            try
            {
                return await _context.ModelPriceHistories
                    .Include(mph => mph.Model)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching model price history records.");
                throw;
            }
        }

        public async Task<ModelPriceHistory?> GetRecordByIdAsync(int id)
        {
            try
            {
                return await _context.ModelPriceHistories
                    .Include(mph => mph.Model)
                    .FirstOrDefaultAsync(mph => mph.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching model price history record by id: {id}");
                throw;
            }
        }

        public async Task<List<ModelPriceHistory>> GetRecordsByModelAsync(int modelId)
        {
            try
            {
                return await _context.ModelPriceHistories
                    .Where(mph => mph.ModelId == modelId)
                    .Include(mph => mph.Model)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching model price history records by model id: {modelId}");
                throw;
            }
        }

        public async Task<ModelPriceHistory?> CreateRecordAsync(ModelPriceHistory record)
        {
            try
            {
                _context.ModelPriceHistories.Add(record);
                await _context.SaveChangesAsync();
                return record;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding model price history record.");
                throw;
            }
        }

        public async Task<ModelPriceHistory?> UpdateRecordAsync(int id, ModelPriceHistory record)
        {
            try
            {
                var existingRecord = await _context.ModelPriceHistories.FindAsync(id);
                if (existingRecord == null)
                {
                    return null;
                }
                _context.Entry(existingRecord).CurrentValues.SetValues(record);
                await _context.SaveChangesAsync();
                return existingRecord;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating model price history record with id: {record.Id}");
                throw;
            }
        }

        public async Task<bool> DeleteRecordAsync(int id)
        {
            try
            {
                var record = await _context.ModelPriceHistories.FindAsync(id);
                if (record == null)
                {
                    return false;
                }
                _context.ModelPriceHistories.Remove(record);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting model price history record with id: {id}");
                throw;
            }
        }

        public async Task<List<ModelPriceHistory>> SearchRecordsAsync(ModelPriceHistoryRecordSearch filter)
        {
            try
            {
                var query = _context.ModelPriceHistories
                    .Include(mph => mph.Model)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(filter.ModelName))
                    query = query.Where(mph => mph.Model.ModelName != null && mph.Model.ModelName.Contains(filter.ModelName));

                if (filter.PurchasePriceMin.HasValue)
                    query = query.Where(mph => mph.PurchasePrice >= filter.PurchasePriceMin.Value);

                if (filter.PurchasePriceMax.HasValue)
                    query = query.Where(mph => mph.PurchasePrice <= filter.PurchasePriceMax.Value);

                if (filter.SellingPriceMin.HasValue)
                    query = query.Where(mph => mph.SellingPrice >= filter.SellingPriceMin.Value);

                if (filter.SellingPriceMax.HasValue)
                    query = query.Where(mph => mph.SellingPrice <= filter.SellingPriceMax.Value);

                if (filter.EffectiveFromStart.HasValue)
                    query = query.Where(mph => mph.EffectiveFrom.Date >= filter.EffectiveFromStart.Value.Date);

                if (filter.EffectiveFromEnd.HasValue)
                    query = query.Where(mph => mph.EffectiveFrom.Date <= filter.EffectiveFromEnd.Value.Date);

                if (filter.EffectiveToStart.HasValue)
                    query = query.Where(mph => mph.EffectiveTo.Date >= filter.EffectiveToStart.Value.Date);

                if (filter.EffectiveToEnd.HasValue)
                    query = query.Where(mph => mph.EffectiveTo.Date <= filter.EffectiveToEnd.Value.Date);

                if (filter.CreatedAtFrom.HasValue)
                    query = query.Where(mph => mph.CreatedAt.Date >= filter.CreatedAtFrom.Value.Date);

                if (filter.CreatedAtTo.HasValue)
                    query = query.Where(mph => mph.CreatedAt.Date <= filter.CreatedAtTo.Value.Date);

                if (!string.IsNullOrWhiteSpace(filter.Notes))
                    query = query.Where(mph => mph.Notes != null && mph.Notes.Contains(filter.Notes));

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching model price history records.");
                throw;
            }
        }
    }
}
