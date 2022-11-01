using MyCollection.Domain.Entities;
using Xunit;

namespace MyCollection.Domain.Tests
{
    [Collection(nameof(CollectionItemCollection))]
    public class CollectionItemTests
    {
        private readonly CollectionItemTestsFixture _fixture;

        public CollectionItemTests(CollectionItemTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CollectionItem_LendOneItem_MustDecreaseTheAmount()
        {
            // Arrange
            var item = _fixture.GenerateCollectionItemValid();

            // Act
            item.LendOneItem(new Contact("Maria Doe", "maria@mail.com", "049999398534"));

            // Assert
            Assert.False(item.ICanLend());
            Assert.Equal(0, item.Quantity);
        }


        [Fact]
        public void CollectionItem_GetAbbreviatedLocation_MustDecreaseTheAmount()
        {
            // Arrange
            var item = _fixture.GenerateCollectionWithLocation();

            // Act
            var abbreviatedLocation = item.GetAbbreviatedLocation();

            // Assert
            Assert.True(item.HasLocation());
        }
    }
}