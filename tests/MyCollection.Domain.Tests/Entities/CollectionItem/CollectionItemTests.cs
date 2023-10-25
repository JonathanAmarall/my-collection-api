using FluentAssertions;
using MyCollection.Domain.Entities;
using System.Linq;
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
            item.LendOneItem(new Borrower("Maria Doe", "maria@mail.com", "049999398534"));

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
            var numberOfItemsConsideringLoanedItem = 5;
            var numberOriginalOfItens = numberOfItemsConsideringLoanedItem + 1;
            var item = _fixture.GenerateCollectionItemWithLendOneItem(numberOriginalOfItens);
            var contactCopy = item.Contacts!.First();
            // Act
            item.RecoveredItem(contactCopy);

            // Assert
            item.Quantity.Should().Be(numberOriginalOfItens);
            item.Status.Should().Be(ECollectionStatus.AVAILABLE);
            item.UpdateAt.Should().NotBeNull();
            item.Contacts.Should().NotContain(contactCopy);
        }
    }
}