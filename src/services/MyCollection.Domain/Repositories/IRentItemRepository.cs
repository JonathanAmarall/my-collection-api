using MyCollection.Core.Data;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Repositories
{
    public interface IRentItemRepository : IRepository<RentItem>
    {
        Task<RentItem> CreateAsync(RentItem item);

        Task<List<RentItem>> GetAllAsync();

        Task<List<RentItem>?> GetExpiredRents(int quantityToBeObtained = 10);

        void UpdateRange(List<RentItem> rentItems);
    }
}
