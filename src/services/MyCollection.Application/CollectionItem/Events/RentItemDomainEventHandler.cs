using MyCollection.Core.Contracts;
using MyCollection.Domain.Entities;
using MyCollection.Domain.Events;
using MyCollection.Domain.Repositories;

namespace MyCollection.Application.CollectionItem.Events
{
    public class RentItemDomainEventHandler : IDomainEventHandler<RentItemDomainEvent>
    {
        private readonly IRentItemRepository _rentItemRepository;
        public RentItemDomainEventHandler(IRentItemRepository rentItemRepository)
        {
            _rentItemRepository = rentItemRepository;
        }

        public async Task Handle(RentItemDomainEvent notification, CancellationToken cancellationToken)
        {
            var rentItem = new RentItem(notification.RentedQuantity, notification.Item.Id, notification.BorrowerId);
            await _rentItemRepository.CreateAsync(rentItem);

            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine($"O Item {notification.Item.Title} foi alugado para o contato {notification.BorrowerId}");
            Console.BackgroundColor = ConsoleColor.Green;

            await Task.CompletedTask;
        }
    }
}