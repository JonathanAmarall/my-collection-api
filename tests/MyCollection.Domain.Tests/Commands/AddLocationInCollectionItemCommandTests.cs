using MyCollection.Domain.Commands;
using System;
using Xunit;

namespace MyCollection.Domain.Tests.Commands
{
    public class AddLocationInCollectionItemCommandTests
    {

        [Fact]
        public void AddLocationInCollectionItemCommandTests_AddLocationInCollectionItemCommand_Valid()
        {
            // Arrange
            var command = new AddLocationInCollectionItemCommand(Guid.NewGuid(), Guid.NewGuid());

            // Act
            bool isValid = command.IsValid();

            // Assert
            Assert.True(isValid);
            Assert.Equal(0, command.ValidationResult?.Errors.Count);
        }


        [Fact]
        public void AddLocationInCollectionItemCommandTests_AddLocationInCollectionItemCommand_Invalid()
        {
            // Arrange
            var command = new AddLocationInCollectionItemCommand(Guid.Empty, Guid.NewGuid());

            // Act
            bool isValid = command.IsValid();

            // Assert
            Assert.False(isValid);
            Assert.NotEqual(0, command.ValidationResult?.Errors.Count);
        }
    }
}
