using MyCollection.Domain.Commands;
using MyCollection.Core.Contracts;
using MyCollection.Domain.Repositories;
using MyCollection.Core.Models;

namespace MyCollection.Domain.Handler
{
    public class AddLocationInCollectionCommandHandler : IHandlerAsync<AddLocationInCollectionItemCommand>
    {
        private readonly ILocationRepository _locationRepository;
        private readonly ICollectionItemRepository _collectionItemRepository;

        public AddLocationInCollectionCommandHandler(ILocationRepository locationRepository, ICollectionItemRepository collectionItemRepository)
        {
            _locationRepository = locationRepository;
            _collectionItemRepository = collectionItemRepository;
        }

        public async Task<ICommandResult> HandleAsync(AddLocationInCollectionItemCommand command)
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

            var location = await _locationRepository.GetByIdAsync(command.LocationId);
            if (location is null)
            {
                return new CommandResult(false, "Localização inválida. Verifique e tente novamente.", command,
                    command.ValidationResult);
            }

            item.AddLocation(location);
            _collectionItemRepository.Update(item);
            await _collectionItemRepository.UnitOfWork.Commit();

            return new CommandResult(true, "Localização adicionado com sucesso.", item, null);
        }
    }
}