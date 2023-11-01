using MyCollection.Domain.Entities;
using MyCollection.Domain.ValueObjects;
using Xunit;

namespace MyCollection.Domain.Tests.Entities.CollectionItem
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
            item.RentItem(new Borrower("Maria Doe", "maria@mail.com", Email.Create("johndoe@mail.com"),
                "", new Address("Rua tal", "9846000", "Los Angeles", "312")),
                1);

            // Assert
            Assert.False(item.CanLend());
            Assert.Equal(0, item.Quantity);
        }


        [Fact]
        public void CollectionItem_GetAbbreviatedLocation_MustReturnLocation()
        {
            // Arrange
            var item = _fixture.GenerateCollectionWithLocation();

            // Act
            var abbreviatedLocation = item.GetAbbreviatedLocation();

            // Assert
            Assert.True(item.HasLocation());
            Assert.False(string.IsNullOrWhiteSpace(abbreviatedLocation));
        }

        [Fact]
        public void CollectionItem_RecoveredItem_MustReturnAddTheAmount()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}