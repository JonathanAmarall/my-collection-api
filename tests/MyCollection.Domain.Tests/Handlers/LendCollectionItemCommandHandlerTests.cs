using Moq;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;
using MyCollection.Domain.Tests.Commands;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MyCollection.Domain.Tests.Handlers
{
    [Collection(nameof(CreateCollectionItemCollection))]
    public class LendCollectionItemCommandHandlerTests
    {
        private readonly CollectionItemCommandTestsFixture _fixture;
        private readonly Mock<ICollectionItemRepository> _collectionItemRepository;

        public LendCollectionItemCommandHandlerTests(CollectionItemCommandTestsFixture fixture)
        {
            _fixture = fixture;
            _collectionItemRepository = new Mock<ICollectionItemRepository>();
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_CommandValid_CreateWithSuccess()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            _collectionItemRepository.Setup<Task<CollectionItem>>(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());
            _collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.ContactId!))
                .ReturnsAsync(GenericContact());

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit()).ReturnsAsync(true);

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.True(result.Success);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Once);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        private static CollectionItem GenericCollectionItem(int quantity = 1)
        {
            return new CollectionItem("Livro Teste", "John Doe", quantity, "Deluxe", EType.BOOK);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_CommandInvalid_ShouldReturnError()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandInvalid();

            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());
            _collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.ContactId!))
                .ReturnsAsync(GenericContact());

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit()).ReturnsAsync(true);

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.Success);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        private static Contact GenericContact()
        {
            return new Contact("John Doe", "john@mail.com", "5599398654");
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_CollectionItemIdInvalid_ShouldReturnErrorS()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();
            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(null as CollectionItem);

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.Success);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_CollectionItemUnavailable_ShouldReturnErrorS()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem(0));

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.Success);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_ContactInvalid_ShouldReturnErrorS()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());
            _collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.ContactId!))
                .ReturnsAsync(null as Contact);

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.Success);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Never);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_ContactIdNull_ShouldReturnErrorS()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandWithoutContactIdValid();

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit())
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());
            _collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.ContactId!))
                .ReturnsAsync(GenericContact());

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.True(result.Success);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Once);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }
    }
}