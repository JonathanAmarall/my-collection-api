using MyCollection.Core.Contracts;
using MyCollection.Core.Models;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Domain.Handler
{
    public class CreateCollectionItemCommandHandler : IHandlerAsync<CreateCollectionItemCommand>
    {
        private readonly ICollectionItemRepository _collectionItemRepository;

        public CreateCollectionItemCommandHandler(ICollectionItemRepository collectionItemRepository)
        {
            _collectionItemRepository = collectionItemRepository;
        }

        public async Task<ICommandResult> HandleAsync(CreateCollectionItemCommand command)
        {
            if (!command.IsValid())
            {
                return new CommandResult(false, "Ops, parece que há algo de errado.", command,
                    command.ValidationResult);
            }

            var item = new CollectionItem(command.Title, command.Autor, command.Quantity, command.Edition, (EType)command.ItemType);

            await _collectionItemRepository.CreateAsync(item);
            await _collectionItemRepository.UnitOfWork.Commit();

            return new CommandResult(true, "Cadastrado com sucesso!", item);
        }
    }
}