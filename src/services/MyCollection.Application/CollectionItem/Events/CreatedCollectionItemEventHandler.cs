using MyCollection.Core.Contracts;
using MyCollection.Domain.Events;

namespace MyCollection.Application.CollectionItem.Events;

public class CreatedCollectionItemEventHandler : IDomainEventHandler<CreatedCollectionItemDomainEvent>
{
    public async Task Handle(CreatedCollectionItemDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"O Item {notification.Item.Title} foi cadastrado.");

        await Task.CompletedTask;
    }

}
