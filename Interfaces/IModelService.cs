using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface IModelService
    {
        Task<List<Model>> GetAllModelsAsync();
        Task<Model?> GetModelByIdAsync(int id);
        Task<List<Model>> GetModelByModelNoAsync(string modelNo);
        Task<List<Model>> GetModelsByBrandAsync(int brandId);
        Task<List<Model>> GetModelsActiveAsync();
        Task<Model?> CreateModelAsync(Model model);
        Task<Model?> UpdateModelAsync(int id, Model model);
        Task<bool> DeleteModelAsync(int id);
    }
}
