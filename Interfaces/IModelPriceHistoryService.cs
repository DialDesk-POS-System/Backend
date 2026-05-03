using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface IModelPriceHistoryService
    {
        Task<List<ModelPriceHistory>> GetModelPriceHistoryRecordsAsync();
        Task<ModelPriceHistory> GetModelPriceHistoryRecordByIdAsync(int id);
        Task<List<ModelPriceHistory>> GetModelPriceHistoryRecordsByModelAsync(int modelId);
        Task<ModelPriceHistory> CreateModelPriceHistoryRecordAsync(ModelPriceHistory record);
        Task<ModelPriceHistory> UpdateModelPriceHistoryRecordAsync(int id, ModelPriceHistory record);
        Task<bool> DeleteModelPriceHistoryRecordAsync(int id);
        Task<List<ModelPriceHistory>> SearchRecordsAsync(ModelPriceHistory record);
        Task<List<ModelPriceHistory>> GetModelPriceHistoryNotesAsync(int id);
    }
}
