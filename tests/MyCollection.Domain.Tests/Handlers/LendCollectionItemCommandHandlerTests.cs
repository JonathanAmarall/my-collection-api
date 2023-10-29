using Moq;
using MyCollection.Core.Messages.Commands;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Handler;
using MyCollection.Domain.Repositories;
using MyCollection.Domain.Tests.Commands;
using MyCollection.Domain.ValueObjects;
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

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit(default))
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());
            _collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.BorrowerId!))
                .ReturnsAsync(GenericContact());

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit(default)).ReturnsAsync(true);

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.True(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Once);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Once);
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
            _collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.BorrowerId!))
                .ReturnsAsync(GenericContact());

            var locationRepository = new Mock<ILocationRepository>();
            locationRepository.Setup(c => c.UnitOfWork.Commit(default)).ReturnsAsync(true);

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }

        private static Borrower GenericContact()
        {
            return new Borrower("Maria Doe", "maria@mail.com", Email.Create("johndoe@mail.com"),
                "", new Address("Rua tal", "9846000", "Los Angeles", "312"));
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_CollectionItemIdInvalid_ShouldReturnErrorS()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();
            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit(default))
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(null as CollectionItem);

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_CollectionItemUnavailable_ShouldReturnErrorS()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit(default))
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem(0));

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_ContactInvalid_ShouldReturnErrorS()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandValid();

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit(default))
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());
            _collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.BorrowerId!))
                .ReturnsAsync(null as Borrower);

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }

        [Fact]
        public async Task LendCollectionItemCommandHandler_ContactIdNull_ShouldReturnErrors()
        {
            // Arrange
            var command = _fixture.GenerateLendCollectionItemCommandWithoutContactIdValid();

            _collectionItemRepository.Setup(c => c.UnitOfWork.Commit(default))
                .ReturnsAsync(true);
            _collectionItemRepository.Setup(c => c.GetByIdAsync(command.CollectionItemId))
                .ReturnsAsync(GenericCollectionItem());
            _collectionItemRepository.Setup(c => c.GetContactByIdAsync((Guid)command.BorrowerId!))
                .ReturnsAsync(GenericContact());

            var handler = new LendCollectionItemCommandHandler(_collectionItemRepository.Object);
            // Act
            var result = (CommandResult)await handler.HandleAsync(command);

            //Assert
            Assert.False(result.IsSuccess);
            _collectionItemRepository.Verify(r => r.Update(It.IsAny<CollectionItem>()), Times.Never);
            _collectionItemRepository.Verify(r => r.UnitOfWork.Commit(default), Times.Never);
        }
    }
}