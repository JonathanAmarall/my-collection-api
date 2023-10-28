using AutoFixture;
using MyCollection.Core.Data;
using MyCollection.Core.DTOs;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Application.Tests.Mock.Repositories;

public class CollectionItemRepositoryMock : ICollectionItemRepository
{
    public void Dispose()
    {
    }

    public IUnitOfWork UnitOfWork { get; }
    public Task CreateAsync(CollectionItem item)
    {
        throw new NotImplementedException();
    }

    public void Delete(CollectionItem item)
    {
        throw new NotImplementedException();
    }

    public void Update(CollectionItem item)
    {
        throw new NotImplementedException();
    }

    public Task<CollectionItem?> GetByIdAsync(Guid collectionItemId)
    {
        var item = new CollectionItem("Livro ABC", "Jonhn Doe", 10, string.Empty, EType.BOOK);
        return Task.FromResult(item);
    }

    public Task<Borrower?> GetContactByIdAsync(Guid contactId)
    {
        throw new NotImplementedException();
    }

    public Task<PagedList<CollectionItem>> GetAllPagedAsync(string? globalFilter, string? sortOrder, string? sortField, ECollectionStatus? status,
        EType? type, int pageNumber = 1, int pageSize = 5)
    {
        var fixture = new Fixture();
        var itens = fixture.Create<PagedList<CollectionItem>>();
        return Task.FromResult(itens);
    }

    public Task<PagedList<Borrower>> GetAllContactsPagedAsync(string? globalFilter, int pageNumber = 1, int pageSize = 5)
    {
        throw new NotImplementedException();
    }
}