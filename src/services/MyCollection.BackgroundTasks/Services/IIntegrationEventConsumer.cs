using MediatR;
using MyCollection.Core.Contracts;

namespace MyCollection.BackgroundTasks.Services;

internal interface IIntegrationEventConsumer
{
    /// <summary>
    /// Consumes the incoming the specified integration event.
    /// </summary>
    void Consume(IIntegrationEvent integrationEvent);
}

public sealed class IntegrationEventConsumer : IIntegrationEventConsumer
{
   private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="IntegrationEventConsumer"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public IntegrationEventConsumer(IMediator mediator) => _mediator = mediator;

        /// <inheritdoc />
        public void Consume(IIntegrationEvent integrationEvent) => _mediator.Publish(integrationEvent).GetAwaiter().GetResult();
   
}
