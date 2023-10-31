using MyCollection.Core.Contracts;
using MyCollection.Core.Messages.Commands;
using MyCollection.Domain.Commands;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Repositories;

namespace MyCollection.Domain.Handler
{
    public class CreateLocationCommandHandler : IHandlerAsync<CreateLocationCommand>
    {
        private readonly ILocationRepository _locationRepository;

        public CreateLocationCommandHandler(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<ICommandResult> HandleAsync(CreateLocationCommand command)
        {
            if (!command.IsValid())
            {
                return CommandResult<Location>.Failure("Ops, parece que há algo de errado.", command.ValidationResult);
            }

            int level = 0;
            if (command.ParentId is not null)
            {
                var parent = await _locationRepository.GetByIdAsync((Guid)command.ParentId);
                if (parent is not null)
                {
                    level = parent.Level + 1;
                }
            }

            var location = new Location(command.Initials, command.Description, command.ParentId, level);

            await _locationRepository.CreateAsync(location);
            await _locationRepository.UnitOfWork.Commit();

            return CommandResult<Location>.Success("Localização salva com sucesso.", location);
        }
    }
}
