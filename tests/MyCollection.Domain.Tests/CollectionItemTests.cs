using MyCollection.Domain.Entities;
using Xunit;

namespace MyCollection.Domain.Tests
{
    public class CollectionItemTests
    {
        [Fact]
        public void DeveCriarEntidadeValida()
        {
            var item = new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.CD);

            Assert.NotNull(item);
        }
    }
}