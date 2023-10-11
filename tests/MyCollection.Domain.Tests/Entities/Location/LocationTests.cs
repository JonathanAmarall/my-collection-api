using FluentAssertions;
using MyCollection.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MyCollection.Domain.Tests.Entities.Location
{
    //[Collection(nameof(LocationCollection))]
    public class LocationTests
    {
        [Fact]
        public void Location_HasChildren_WithLocationWithChildrenShouldReturnTrue()
        {
            // Arrange
            var location = new Domain.Entities.Location("CXA", "Caixa", null, 0);
            var childrensLocation = new List<Domain.Entities.Location> { new Domain.Entities.Location("CXA", "Caixa", null, 1) };
            location.AddChildrens(childrensLocation);
            // Act
            var hasChildren = location.HasChildren();
            // Assert
            hasChildren.Should().BeTrue();
        }

        [Fact]
        public void Location_HasChildren_WithLocationWithParentShouldReturnTrue()
        {
            // Arrange
            var location = new Domain.Entities.Location("CXA", "Caixa", null, 0);
            var childrensLocation = new List<Domain.Entities.Location> { new Domain.Entities.Location("CXA", "Caixa", null, 1) };
            location.AddChildrens(childrensLocation);
            // Act
            var hasParent = childrensLocation.First().HasParent();
            // Assert
            hasParent.Should().BeTrue();
        }


        [Fact]
        public void Location_HasChildren_LinkCollectionItemWithSuccess()
        {
            // Arrange
            var location = new Domain.Entities.Location("CXA", "Caixa", null, 0);
            var item = new Domain.Entities.CollectionItem("Livro ABC", "John Doe", 10, string.Empty, EType.BOOK);
            // Act
            location.LinkACollectionItem(item);
            var hasItem = location.HasCollectionItem();
            // Assert
            hasItem.Should().BeTrue();
        }
    }
}
