using MyCollection.Domain.Entities;
using System;
using Xunit;

namespace MyCollection.Domain.Tests
{
    [CollectionDefinition(nameof(CollectionItemCollection))]
    public class CollectionItemCollection : ICollectionFixture<CollectionItemTestsFixture>
    {

    }

    public class CollectionItemTestsFixture : IDisposable
    {

        public CollectionItem GenerateCollectionItemValid()
        {
            var item = new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK);
            return item;
        }

        public CollectionItem GenerateCollectionWithLocation()
        {
            var item = new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK);
            var location = new Location("PRT 1", "Prateleira 1", null);
            item.AddLocation(location);

            return item;
        }

        public CollectionItem GenerateCollectionItemUnavailable()
        {
            var item = new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK);
            item.LendOneItem(new Contact("Maria Doe", "maria@mail.com", "49559398564"));
            return item;
        }

        public void Dispose()
        {
        }
    }
}
