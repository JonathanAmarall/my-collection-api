using MyCollection.Core.Data;
using MyCollection.Core.DTOs;
using MyCollection.Domain.Entities;

namespace MyCollection.Domain.Repositories
{
    public interface IBorrowerRepository : IRepository<Borrower>
    {
        Task<Borrower?> GetByIdAsync(Guid borrowerId);
        Task<PagedList<Borrower>> GetAllPagedAsync(string? globalFilter, int pageNumber = 1, int pageSize = 5);
        Task<Borrower> CreateBorrowerAsync(Borrower borrower);
    }
}
