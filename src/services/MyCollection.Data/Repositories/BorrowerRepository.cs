using Microsoft.EntityFrameworkCore;
using MyCollection.Core.Data;
using MyCollection.Core.DTOs;
using MyCollection.Data.Extensions;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Data.Repositories
{
    public class BorrowerRepository : IBorrowerRepository
    {
        private readonly MyCollectionContext _context;

        public BorrowerRepository(MyCollectionContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Borrower> CreateBorrowerAsync(Borrower borrower)
        {
            await _context.Borrowers!.AddAsync(borrower);
            return await Task.FromResult(borrower);
        }

        public async Task<PagedList<Borrower>> GetAllPagedAsync(string? globalFilter, string? sortOrder, string? sortField, int pageNumber = 1, int pageSize = 5)
        {
            var query = _context.Borrowers!.AsQueryable();

            if (!string.IsNullOrWhiteSpace(globalFilter))
            {
                query = query.Where(x =>
                                   x.Email.Value.ToUpper().Contains(globalFilter.ToUpper()) ||
                                   x.FirstName.ToUpper().Contains(globalFilter.ToUpper()) ||
                                   x.LastName.ToUpper().Contains(globalFilter.ToUpper()) ||
                                   x.Phone!.ToUpper().Contains(globalFilter.ToUpper())
                               );
            }

            if (!string.IsNullOrWhiteSpace(sortOrder) && !string.IsNullOrWhiteSpace(sortField))
            {
                query = query.GenericOrderBy(sortField, sortOrder.ToUpper() == "DESC");
            }

            return await query.ToPagedListAsync(pageNumber, pageSize);
        }

        public async Task<Borrower?> GetByIdAsync(Guid borrowerId)
        {
            return await _context.Borrowers!.FirstOrDefaultAsync(x => x.Id == borrowerId);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
        }
    }
}
