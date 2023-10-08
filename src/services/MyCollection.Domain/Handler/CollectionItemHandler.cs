using MyCollection.Domain.Commands;
using MyCollection.Domain.Contracts;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Domain.Handler
{
    public class CollectionItemHandler : IHandlerAsync<CreateCollectionItemCommand>,
        IHandlerAsync<LendCollectionItemCommand>, IHandlerAsync<AddLocationInCollectionItemCommand>
    {
        private readonly ICollectionItemRepository _collectionItemRepository;
        private readonly ILocationRepository _locationRepository;

        public CollectionItemHandler(ICollectionItemRepository collectionItemRepository,
            ILocationRepository locationRepository)
        {
            _collectionItemRepository = collectionItemRepository;
            _locationRepository = locationRepository;
        }

        public async Task<ICommandResult> HandleAsync(CreateCollectionItemCommand command)
        {
            if (!command.IsValid())
            {
                return new CommandResult(false, "Ops, parece que há algo de errado.", command,
                    command.ValidationResult);
            }

            var item = new CollectionItem(command.Title, command.Autor, command.Quantity, command.Edition,
                command.ItemType);

            await _collectionItemRepository.CreateAsync(item);
            await _collectionItemRepository.UnitOfWork.Commit();

            return new CommandResult(true, "Cadastrado com sucesso!", item);
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
                return new CommandResult(false, "Este item não está disponível para ser emprestado.", command,
                    command.ValidationResult);

            var contact = await GetOrCreateContactIfNotExistAsync(command);
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

        private async Task<Contact?> GetOrCreateContactIfNotExistAsync(LendCollectionItemCommand command)
        {
            if (command.ContactId is null)
            {
                return new Contact(command.FullName!, command.Email!, command.Phone!);
            }

            var contact = await _collectionItemRepository.GetContactByIdAsync((Guid)command.ContactId);
            return contact ?? default;
        }
    }
}