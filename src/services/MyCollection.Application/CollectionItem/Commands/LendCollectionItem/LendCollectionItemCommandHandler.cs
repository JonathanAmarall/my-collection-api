using MyCollection.Domain.Commands;
using MyCollection.Core.Contracts;
using MyCollection.Domain.Repositories;
using MyCollection.Core.Models;
using MyCollection.Core.Messages.Commands;

namespace MyCollection.Domain.Handler
{
    public class LendCollectionItemCommandHandler : IHandlerAsync<LendCollectionItemCommand>
    {
        private readonly ICollectionItemRepository _collectionItemRepository;

        public LendCollectionItemCommandHandler(ICollectionItemRepository collectionItemRepository)
        {
            _collectionItemRepository = collectionItemRepository;
        }

        public async Task<ICommandResult> HandleAsync(LendCollectionItemCommand command)
        {
            if (!command.IsValid())
            {
                return new CommandResult(false, "Ops, parece que há algo de errado.", command,
                    command.ValidationResult);
            }

            var item = await _collectionItemRepository.GetByIdAsync(command.CollectionItemId);
            if (item is null)
            {
                return new CommandResult(false, "Item não localizado. Verifique e tente novamente.", command,
                    command.ValidationResult);
            }

            if (!item.CanLend())
            {
                return new CommandResult(false, "Este item não está disponível para ser emprestado.", command,
                    command.ValidationResult);
            }

            var contact = await _collectionItemRepository.GetContactByIdAsync((Guid)command.BorrowerId!);
            if (contact is null)
            {
                return new CommandResult(false, "Contato informado é inválido. Por favor, verifique e tente novamente",
                    command, null);
            }

            item.LendOneItem(contact);

            _collectionItemRepository.Update(item);
            await _collectionItemRepository.UnitOfWork.Commit();

            return new CommandResult(true, "Item emprestado com sucesso.", item, null);
        }
    }
}