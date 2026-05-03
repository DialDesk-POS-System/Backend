using DialDesk.Server.Data;
using DialDesk.Server.Interfaces;

namespace DialDesk.Server.Services
{
    public class WarrantyService : IWarrantyService
    {
        private readonly AppDbContext _db;

        public WarrantyService(AppDbContext db)
        {
            _db = db;
        }
    }
}
