using Microsoft.Extensions.Logging;
using MyCollection.Core.Contracts;
using MyCollection.Domain.Events;

namespace MyCollection.Application.CollectionItem.Events
{
    public class LendItemDomainEventHandler : IDomainEventHandler<LendItemDomainEvent>
    {
        public async Task Handle(LendItemDomainEvent notification, CancellationToken cancellationToken)
        {
           Console.WriteLine($"O Item {notification.Item.Title} foi emprestado para o contato {notification.BorrowerId}");

            await Task.CompletedTask;
        }
    }
}