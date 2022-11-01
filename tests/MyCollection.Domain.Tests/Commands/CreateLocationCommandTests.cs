using MyCollection.Domain.Commands;
using System;
using Xunit;

namespace MyCollection.Domain.Tests.Commands
{
    public class CreateLocationCommandTests
    {
        [Fact]
        public void CreateLocationCommandTests_CreateLocationCommand_Valid()
        {
            // Arrange
            var command = new CreateLocationCommand("CX 1", "Caixa 1", Guid.NewGuid());

            // Act
            bool isValid = command.IsValid();

            // Assert
            Assert.True(isValid);
            Assert.Equal(0, command.ValidationResult?.Errors.Count);
        }

        [Fact]
        public void CreateLocationCommandTests_CreateLocationCommand_Invalid()
        {
            // Arrange
            var command = new CreateLocationCommand("CX COM DESCRICAO GRANDE 1", "Caixa 1", Guid.Empty);

            // Act
            bool isValid = command.IsValid();

            // Assert
            Assert.False(isValid);
            Assert.NotEqual(0, command.ValidationResult?.Errors.Count);
        }
    }
}
