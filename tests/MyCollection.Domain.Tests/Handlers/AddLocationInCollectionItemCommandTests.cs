using Moq;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Handler;
using MyCollection.Domain.Repositories;
using MyCollection.Domain.Tests.Commands;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyCollection.Domain.Tests.Handlers
{
    [Collection(nameof(CreateCollectionItemCollection))]
    public class AddLocationInCollectionItemCommandTests
    {
        [Fact]
        public async Task CollectionItemHandler_AddLocationInCollectionItemCommand_AddWithSuccess()
        {
            // Arrange
            var command = new AddLocationInCollectionItemCommand(Guid.NewGuid(), Guid.NewGuid());

            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK));

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(l => l.GetByIdAsync(command.LocationId))
                .ReturnsAsync(new Location("CX 1", "Caixa 1", null, 0));

            var handler = new AddLocationInCollectionCommandHandler(locationRepository.Object, collectionItemRepository.Object);

            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.True(result.Success);
            collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Once);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task CollectionItemHandler_AddLocationInCollectionItemCommand_AddWithFail()
        {
            // Arrange
            var command = new AddLocationInCollectionItemCommand(Guid.NewGuid(), Guid.NewGuid());

            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK));

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(l => l.GetByIdAsync(command.LocationId));

            var handler = new AddLocationInCollectionCommandHandler(locationRepository.Object, collectionItemRepository.Object);

            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.Success);
            collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }
    }
}