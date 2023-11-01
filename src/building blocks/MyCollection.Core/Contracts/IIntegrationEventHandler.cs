using MediatR;

namespace MyCollection.Core.Contracts
{
    /// <summary>
    /// Represents the integration event handler.
    /// </summary>
    /// <typeparam name="TIntegrationEvent">The integration event type.</typeparam>
    public interface IIntegrationEventHandler<in TIntegrationEvent> : INotificationHandler<TIntegrationEvent>
        where TIntegrationEvent : IIntegrationEvent
    {
    }
}