using MyCollection.Domain.Commands;
using MyCollection.Domain.Contracts;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Domain.Handler
{
    public class CollectionItemHandler : IHandlerAsync<CreateCollectionItemCommand>
    {
        private readonly ICollectionItemRepository _collectionItemRepository;

        public CollectionItemHandler(ICollectionItemRepository collectionItemRepository)
        {
            _collectionItemRepository = collectionItemRepository;
        }

        public async Task<ICommandResult> HandleAsync(CreateCollectionItemCommand command, int companyId, string userId)
        {
            if (!command.IsValidate())
                return new CommandResult(false, "Ops, parece que há algo de errado.", command, command.ValidationResult);

            var item = new CollectionItem(command.Title, command.Autor, command.Quantity, command.Edition, command.ItemType, command.LocationId);

            await _collectionItemRepository.CreateAsync(item);
            await _collectionItemRepository.UnitOfWork.Commit();

            return new CommandResult(true, "Cadastrado com sucesso!", item);
        }
    }
}
