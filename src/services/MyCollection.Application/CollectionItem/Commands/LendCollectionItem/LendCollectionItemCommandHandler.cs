using MyCollection.Core.Contracts;
using MyCollection.Core.Messages.Commands;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Domain.Handler
{
    public class LendCollectionItemCommandHandler : IHandlerAsync<LendCollectionItemCommand>
    {
        private readonly ICollectionItemRepository _collectionItemRepository;
        private readonly IBorrowerRepository _borrowerRepository;

        public LendCollectionItemCommandHandler(ICollectionItemRepository collectionItemRepository, IBorrowerRepository borrowerRepository)
        {
            _collectionItemRepository = collectionItemRepository;
            _borrowerRepository = borrowerRepository;
        }

        public async Task<ICommandResult> HandleAsync(LendCollectionItemCommand command)
        {
            if (!command.IsValid())
            {
                return CommandResult<CollectionItem>.Failure("Ops, parece que há algo de errado.", command.ValidationResult);
            }

            var item = await _collectionItemRepository.GetByIdAsync(command.CollectionItemId);
            if (item is null)
            {
                return CommandResult<CollectionItem>.Failure("Item não localizado. Verifique e tente novamente.", command.ValidationResult);
            }

            if (!item.CanLend())
            {
                return CommandResult<CollectionItem>.Failure("Este item não está disponível para ser emprestado.", command.ValidationResult);
            }

            var borrower = await _borrowerRepository.GetByIdAsync(command.BorrowerId);
            if (borrower is null)
            {
                return CommandResult<CollectionItem>.Failure("Contato informado é inválido. Por favor, verifique e tente novamente", null);
            }

            item.LendOneItem(borrower, command.RentQuantity);

            _collectionItemRepository.Update(item);
            await _collectionItemRepository.UnitOfWork.Commit();

            return CommandResult<CollectionItem>.Success("Item emprestado com sucesso.", item);
        }
    }
}