using Moq;
using MyCollection.Core.Messages.Commands;
using MyCollection.Core.Models;
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
        private readonly Mock<ICollectionItemRepository> _collectionItemRepository;
        private readonly Mock<ILocationRepository> _locationRepository;
        public AddLocationInCollectionItemCommandTests()
        {
            _collectionItemRepository = new Mock<ICollectionItemRepository>();
            _locationRepository = new Mock<ILocationRepository>();
        }

        [Fact]
        public async Task AddLocationInCollectionItemCommandHandler_WithValidCommand_AddWithSuccess()
        {
            // Arrange
            var command = new AddLocationInCollectionItemCommand(Guid.NewGuid(), Guid.NewGuid());

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());

            _locationRepository.Setup(l => l.GetByIdAsync(command.LocationId))
                .ReturnsAsync(new Location("CX 1", "Caixa 1", null, 0));

            var handler = new AddLocationInCollectionCommandHandler(_locationRepository.Object, _collectionItemRepository.Object);

            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.True(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Once);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task AddLocationInCollectionItemCommandHandler_WithCollectionItem_ShouldReturnError()
        {
            // Arrange
            var command = new AddLocationInCollectionItemCommand(Guid.NewGuid(), Guid.NewGuid());

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(null as CollectionItem);

            var handler = new AddLocationInCollectionCommandHandler(_locationRepository.Object, _collectionItemRepository.Object);

            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public async Task AddLocationInCollectionItemCommandHandler_WithInvalidCommand_ShouldReturnError()
        {
            // Arrange
            var command = new AddLocationInCollectionItemCommand(Guid.Empty, Guid.Empty);
            _collectionItemRepository.Setup(l => l.GetByIdAsync(command.LocationId));
            var handler = new AddLocationInCollectionCommandHandler(_locationRepository.Object, _collectionItemRepository.Object);

            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public async Task AddLocationInCollectionItemCommandHandler_WithInvalidLocation_ShouldReturnError()
        {
            // Arrange
            var command = new AddLocationInCollectionItemCommand(Guid.NewGuid(), Guid.NewGuid());

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());

            _locationRepository.Setup(l => l.GetByIdAsync(command.LocationId))
                .ReturnsAsync(null as Location);

            var handler = new AddLocationInCollectionCommandHandler(_locationRepository.Object, _collectionItemRepository.Object);

            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        private static CollectionItem GenericCollectionItem()
        {
            return new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK);
        }
    }
}