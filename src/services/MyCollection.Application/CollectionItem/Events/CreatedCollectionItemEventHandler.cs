using MyCollection.Core.Contracts;
using MyCollection.Domain.Events;

namespace MyCollection.Application.CollectionItem.Events;

public class CreatedCollectionItemEventHandler : IDomainEventHandler<CreatedCollectionItemEvent>
{
    public async Task Handle(CreatedCollectionItemEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"O Item {notification.Item.Title} foi cadastrado.");

        await Task.CompletedTask;
    }

}
