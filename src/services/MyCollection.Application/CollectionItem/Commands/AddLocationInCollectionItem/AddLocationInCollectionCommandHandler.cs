using MyCollection.Core.Contracts;
using MyCollection.Core.Messages.Commands;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

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
                return CommandResult<CollectionItem>.Failure("Ops, parece que há algo de errado.", command.ValidationResult);
            }

            var item = await _collectionItemRepository.GetByIdAsync(command.CollectionItemId);
            if (item is null)
            {
                return CommandResult<CollectionItem>.Failure("Item não localizado. Verifique e tente novamente.", command.ValidationResult);
            }

            var location = await _locationRepository.GetByIdAsync(command.LocationId);
            if (location is null)
            {
                return CommandResult<CollectionItem>.Failure("Localização inválida. Verifique e tente novamente.", command.ValidationResult);
            }

            item.AddLocation(location);
            _collectionItemRepository.Update(item);
            await _collectionItemRepository.UnitOfWork.Commit();

            return CommandResult<CollectionItem>.Success("Localização adicionado com sucesso.", item);
        }
    }
}