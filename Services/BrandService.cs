using DialDesk.Server.Data;
using DialDesk.Server.DTOs.Brand;
using DialDesk.Server.Interfaces;
using DialDesk.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace DialDesk.Server.Services
{
    public class BrandService : IBrandService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<BrandService> _logger;

        private IQueryable<Brand> BrandWithDetail => _context.Brands
            .Include(b => b.Models);

        public BrandService(AppDbContext context, ILogger<BrandService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            try
            {
                return await BrandWithDetail.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching brands");
                throw;
            }
        }

        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            try
            {
                return await BrandWithDetail.FirstOrDefaultAsync(b => b.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching brand by id");
                throw;
            }
        }

        public async Task<List<Brand>> GetBrandByNameAsync(string name)
        {
            try
            {
                return await BrandWithDetail
                    .Where(b => b.Name.Contains(name))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching brand by name");
                throw;
            }
        }

        public async Task<List<Brand>> GetBrandsActiveAsync()
        {
            try
            {
                return await BrandWithDetail
                    .Where(b => b.IsActive)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching active brands");
                throw;
            }
        }

        public async Task<Brand?> CreateBrandAsync(Brand brand)
        {
            try
            {
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return brand;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating brand");
                throw;
            }
        }

        public async Task<Brand?> UpdateBrandAsync(int id, BrandUpdateDto brand)
        {
            try
            {
                var existingBrand = await _context.Brands.FindAsync(id);
                if (existingBrand == null)
                {
                    _logger.LogWarning("Brand with id {Id} not found for update", id);
                    return null;
                }

                if (brand.Name != null) existingBrand.Name = brand.Name;
                if (brand.ManufacturedCountry != null) existingBrand.ManufacturedCountry = brand.ManufacturedCountry;
                if (brand.LogoUrl != null) existingBrand.LogoUrl = brand.LogoUrl;
                if (brand.IsActive.HasValue) existingBrand.IsActive = brand.IsActive.Value;

                await _context.SaveChangesAsync();
                return existingBrand;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating brand");
                throw;
            }
        }

        public async Task<bool> DeleteBrandAsync(int id)
        {
            try
            {
                var brand = await _context.Brands.FindAsync(id);
                if (brand == null)
                {
                    _logger.LogWarning("Brand with id {Id} not found for deletion", id);
                    return false;
                }
                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting brand");
                throw;
            }
        }
    }
}
