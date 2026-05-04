using DialDesk.Server.Models;

namespace DialDesk.Server.Interfaces
{
    public interface IReturnService
    {
        Task<IEnumerable<Return>> GetAllAsync();
        Task<Return> CreateReturnAsync(Return returnRequest);
        Task<Return?> GetByIdAsync(int id);
        Task<Return?> GetBySaleItemAsync(int saleItemId);
        Task<bool> UpdateAsync(int id, Return returnItem);
        Task<bool> DeleteAsync(int id);
    }
}
