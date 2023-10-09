using Moq;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Handler;
using MyCollection.Domain.Repositories;
using MyCollection.Domain.Tests.Commands;
using System.Threading.Tasks;
using Xunit;

namespace MyCollection.Domain.Tests.Handlers
{
    [Collection(nameof(CreateCollectionItemCollection))]
    public class CreateCollectionItemHandlerTests
    {
        private readonly CollectionItemCommandTestsFixture _fixture;

        public CreateCollectionItemHandlerTests(CollectionItemCommandTestsFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CollectionItemHandler_CreateCollectionItemCommand_CreateWithSuccess()
        {
            // Arrange
            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit()).ReturnsAsync(true);

            var command = _fixture.GenerateCreateCollectionItemCommandValid();

            var handler = new CreateCollectionItemCommandHandler(collectionItemRepository.Object);

            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            // Assert
            Assert.True(result.Success);
            collectionItemRepository.Verify(r => r.CreateAsync(It.IsAny<CollectionItem>()), Times.Once);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task CollectionItemHandler_CreateCollectionItemCommand_CreateFail()
        {
            // Arrange
            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit()).ReturnsAsync(false);

            var command = _fixture.GenerateCreateCollectionItemCommandInvalid();
            var handler = new CreateCollectionItemCommandHandler(collectionItemRepository.Object);

            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            // Assert
            Assert.False(result.Success);
            collectionItemRepository.Verify(r => r.CreateAsync(It.IsAny<CollectionItem>()), Times.Never);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }
    }
}