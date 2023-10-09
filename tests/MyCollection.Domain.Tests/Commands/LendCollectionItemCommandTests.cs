using Xunit;

namespace MyCollection.Domain.Tests.Commands
{
    [Collection(nameof(CreateCollectionItemCollection))]
    public class LendCollectionItemCommandTests
    {
        private readonly CollectionItemCommandTestsFixture _fixture;

        public LendCollectionItemCommandTests(CollectionItemCommandTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void LendCollectionItemCommandTests_LendCollectionItemCommand_Valid()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            // Act
            bool isValid = command.IsValid();

            // Assert
            Assert.True(isValid);
            Assert.Equal(0, command.ValidationResult?.Errors.Count);
        }

        [Fact]
        public void LendCollectionItemCommandTests_LendCollectionItemCommand_Invalid()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandInvalid();

            // Act
            bool isValid = command.IsValid();

            // Assert
            Assert.False(isValid);
            Assert.NotEqual(0, command.ValidationResult?.Errors.Count);
        }
    }
}
