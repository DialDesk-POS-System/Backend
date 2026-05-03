using DialDesk.Server.DTOs;
using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface IModelPriceHistoryService
    {
        Task<List<ModelPriceHistory>> GetAllRecordsAsync();
        Task<ModelPriceHistory?> GetRecordByIdAsync(int id);
        Task<List<ModelPriceHistory>> GetRecordsByModelAsync(int modelId);
        Task<ModelPriceHistory?> CreateRecordAsync(ModelPriceHistory record);
        Task<ModelPriceHistory?> UpdateRecordAsync(int id, ModelPriceHistory record);
        Task<bool> DeleteRecordAsync(int id);
        Task<List<ModelPriceHistory>> SearchRecordsAsync(ModelPriceHistoryRecordSearch filter);
    }
}
