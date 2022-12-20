using Moq;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Handler;
using MyCollection.Domain.Queries;
using MyCollection.Domain.Repositories;
using MyCollection.Domain.Tests.Commands;
using System;
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
        public void CollectionItemHandler_CreateCollectionItemCommand_CreateWithSuccess()
        {
            // Arrange
            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit().Result).Returns(true);

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit().Result).Returns(true);

            var collectionItemsQueries = new Mock<ICollectionItemsQueries>();

            var command = _fixture.GenerateCreateCollectionItemCommandValid();

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object, collectionItemsQueries.Object);

            // Act
            var result = (CommandResult)handler.HandleAsync(command).Result;

            // Assert
            Assert.True(result.Success);
            collectionItemRepository.Verify(r => r.CreateAsync(It.IsAny<CollectionItem>()), Times.Once);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public void CollectionItemHandler_CreateCollectionItemCommand_CreateFail()
        {
            // Arrange
            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit().Result).Returns(false);

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit().Result).Returns(false);

            var collectionItemsQueries = new Mock<ICollectionItemsQueries>();
            
            var command = _fixture.GenerateCreateCollectionItemCommandInvalid();

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object, collectionItemsQueries.Object);

            // Act
            var result = (CommandResult)handler.HandleAsync(command).Result;

            // Assert
            Assert.False(result.Success);
            collectionItemRepository.Verify(r => r.CreateAsync(It.IsAny<CollectionItem>()), Times.Never);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public void CollectionItemHandler_LendCollectionItemCommand_CreateWithSuccess()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit().Result)
                .Returns(true);
            collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId).Result)
                .Returns(new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK));
            collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.ContactId!).Result)
                .Returns(new Contact("John Doe", "john@mail.com", "5599398654"));

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit().Result).Returns(true);

            var collectionItemsQueries = new Mock<ICollectionItemsQueries>();

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object, collectionItemsQueries.Object);
            // Act
            var result = (CommandResult)handler.HandleAsync(command).Result;

            //Assert
            Assert.True(result.Success);
            collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Once);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public void CollectionItemHandler_LendCollectionItemCommand_CreateFail()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandInvalid();

            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit().Result)
                .Returns(true);
            collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId).Result)
                .Returns(new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK));
            collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.ContactId!).Result)
                .Returns(new Contact("John Doe", "john@mail.com", "5599398654"));

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit().Result).Returns(true);

            var collectionItemsQueries = new Mock<ICollectionItemsQueries>();

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object, collectionItemsQueries.Object);
            // Act
            var result = (CommandResult)handler.HandleAsync(command).Result;

            //Assert
            Assert.False(result.Success);
            collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public void CollectionItemHandler_AddLocationInCollectionItemCommand_AddWithSuccess()
        {
            // Arrange
            var command = new AddLocationInCollectionItemCommand(Guid.NewGuid(), Guid.NewGuid());

            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit().Result)
               .Returns(true);
            collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId).Result)
               .Returns(new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK));

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(l => l.GetByIdAsync(command.LocationId).Result)
                .Returns(new Location("CX 1", "Caixa 1", null, 0));

            var collectionItemsQueries = new Mock<ICollectionItemsQueries>();

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object, collectionItemsQueries.Object);

            // Act
            var result = (CommandResult)handler.HandleAsync(command).Result;

            //Assert
            Assert.True(result.Success);
            collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Once);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact]
        public void CollectionItemHandler_AddLocationInCollectionItemCommand_AddWithFail()
        {
            // Arrange
            var command = new AddLocationInCollectionItemCommand(Guid.NewGuid(), Guid.NewGuid());

            var collectionItemRepository = new Mock<ICollectionItemRepository>();
            collectionItemRepository.Setup(c => c.UnitOfWork.Commit().Result)
               .Returns(true);
            collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId).Result)
               .Returns(new CollectionItem("Livro Teste", "John Doe", 1, "Deluxe", EType.BOOK));

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(l => l.GetByIdAsync(command.LocationId).Result);

            var collectionItemsQueries = new Mock<ICollectionItemsQueries>();

            var handler = new CollectionItemHandler(collectionItemRepository.Object, locationRepository.Object, collectionItemsQueries.Object);

            // Act
            var result = (CommandResult)handler.HandleAsync(command).Result;

            //Assert
            Assert.False(result.Success);
            collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }
    }
}
