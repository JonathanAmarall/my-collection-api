using MyCollection.Application.Borrower.Events;
using MyCollection.Core.Contracts;
using MyCollection.Domain.Repositories;

namespace MyCollection.BackgroundTasks.IntegrationEvents.Borrowers
{
    internal class CreatedBorrowerIntegrationEventHandler : IIntegrationEventHandler<CreatedBorrowerIntegrationEvent>
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public CreatedBorrowerIntegrationEventHandler(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public async Task Handle(CreatedBorrowerIntegrationEvent notification, CancellationToken cancellationToken)
        {
            var borrower = await _borrowerRepository.GetByIdAsync(notification.BorrowerId);

            Console.BackgroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enviando email para {0} do Borrower {1}", borrower.Email, borrower.FullName);
        }
    }
}
