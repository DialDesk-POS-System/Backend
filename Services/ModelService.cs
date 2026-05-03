using DialDesk.Server.Data;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Services
{
    public class ModelService : IModelService
    {
        public readonly AppDbContext _context;
        private readonly ILogger<ModelService> _logger;

        public ModelService(AppDbContext context, ILogger<ModelService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Model>> GetAllModelsAsync()
        {
            try
            {
                return await _context.Models
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching models");
                throw;
            }
        }

        public async Task<Model?> GetModelByIdAsync(int id)
        {
            try
            {
                return await _context.Models.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching model by id");
                throw;
            }
        }

        public async Task<List<Model>> GetModelByModelNoAsync(string modelNo)
        {
            try
            {
                return await _context.Models
                    .Where(m => m.ModelNo.Contains(modelNo))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching model by model number");
                throw;
            }
        }

        public async Task<List<Model>> GetModelsByBrandAsync(int brandId)
        {
            try
            {
                return await _context.Models.Where(m => m.BrandId == brandId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching models by brand");
                throw;
            }
        }

        public async Task<List<Model>> GetModelsActiveAsync()
        {
            try
            {
                return await _context.Models.Where(m => m.IsActive).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching active models");
                throw;
            }
        }

        public async Task<Model?> CreateModelAsync(Model model)
        {
            try
            {
                _context.Models.Add(model);
                await _context.SaveChangesAsync();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating model");
                throw;
            }
        }

        public async Task<Model?> UpdateModelAsync(int id, Model model)
        {
            try
            {
                var existingModel = await _context.Models.FindAsync(id);
                if (existingModel == null)
                {
                    _logger.LogWarning("Model with id {Id} not found for update", id);
                    return null;
                }

                _context.Entry(existingModel).CurrentValues.SetValues(model);
                await _context.SaveChangesAsync();
                return existingModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating model");
                throw;
            }
        }

        public async Task<bool> DeleteModelAsync(int id)
        {
            try
            {
                var model = await _context.Models.FindAsync(id);
                if (model == null)
                {
                    _logger.LogWarning("Model with id {Id} not found for deletion", id);
                    return false;
                }
                _context.Models.Remove(model);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting model");
                throw;
            }
        }
    }
}
