using MyCollection.Domain.Commands;
using MyCollection.Domain.Contracts;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Domain.Handler
{
    public class LocationHandler : IHandlerAsync<CreateLocationCommand>
    {
        private readonly ILocationRepository _locationRepository;

        public LocationHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<ICommandResult> HandleAsync(CreateLocationCommand command)
        {
            if (!command.IsValid())
                return new CommandResult(false, "Ops, parece que há algo de errado.", command, command.ValidationResult);

            var location = new Location(command.Initials, command.Description, command.ParentId);

            await _locationRepository.CreateAsync(location);
            await _locationRepository.UnitOfWork.Commit();

            return new CommandResult(true, "Localização salva com sucesso.", location);
        }
    }
}
