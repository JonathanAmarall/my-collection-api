using MyCollection.Domain.Tests.Commands;
using Xunit;

namespace MyCollection.Domain.Tests.Handlers
{

    [Collection(nameof(CreateCollectionItemCollection))]
    public class CreateCollectionItemCommandTests
    {
        private readonly CollectionItemCommandTestsFixture _fixture;

        public CreateCollectionItemCommandTests(CollectionItemCommandTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CreateCollectionItemCommandTests_CreateCollectionItemCommand_Valid()
        {
            // Arrange 
            var command = _fixture.GenerateCreateCollectionItemCommandValid();

            // Act
            bool isValid = command.IsValid();

            // Assert
            Assert.True(isValid);
        }


        [Fact]
        public void CreateCollectionItemCommandTests_CreateCollectionItemCommand_Invalid()
        {
            // Arrange 
            var command = _fixture.GenerateCreateCollectionItemCommandInvalid();

            // Act
            bool isValid = command.IsValid();

            // Assert
            Assert.False(isValid);
        }
    }
}
