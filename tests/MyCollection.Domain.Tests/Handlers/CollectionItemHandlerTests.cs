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
    public class CollectionItemHandlerTests
    {
        private readonly CollectionItemCommandTestsFixture _fixture;

        public CollectionItemHandlerTests(CollectionItemCommandTestsFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task CollectionItemHandler_CreateCollectionItemCommand_CreateWithSuccess()
        {
            // Arrange
            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit()).ReturnsAsync(true);

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit()).ReturnsAsync(true);

            var command = _fixture.GenerateCreateCollectionItemCommandValid();

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object);

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

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit()).ReturnsAsync(false);

            var command = _fixture.GenerateCreateCollectionItemCommandInvalid();

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object);

            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            // Assert
            Assert.False(result.Success);
            collectionItemRepository.Verify(r => r.CreateAsync(It.IsAny<CollectionItem>()), Times.Never);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public async Task CollectionItemHandler_LendCollectionItemCommand_CreateWithSuccess()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK));
            collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.ContactId!))
                .ReturnsAsync(new Contact("John Doe", "john@mail.com", "5599398654"));

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit()).ReturnsAsync(true);

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.True(result.Success);
            collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Once);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public async Task CollectionItemHandler_LendCollectionItemCommand_CreateFail()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandInvalid();

            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK));
            collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.ContactId!))
                .ReturnsAsync(new Contact("John Doe", "john@mail.com", "5599398654"));

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit()).ReturnsAsync(true);

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.Success);
            collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

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

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object);

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

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object);

            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.Success);
            collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }
    }
}