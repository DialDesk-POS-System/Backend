using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface IBrandService
    {
        Task<List<Brand>> GetAllBrandsAsync();
        Task<Brand?> GetBrandByIdAsync(int id);
        Task<List<Brand>> GetBrandByNameAsync(string name);
        Task<List<Brand>> GetBrandsActiveAsync();
        Task<Brand?> CreateBrandAsync(Brand brand);
        Task<Brand?> UpdateBrandAsync(int id, Brand brand);
        Task<bool> DeleteBrandAsync(int id);
    }
}
