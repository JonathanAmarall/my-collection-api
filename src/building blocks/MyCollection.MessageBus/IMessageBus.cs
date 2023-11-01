using MyCollection.Core.Contracts;

namespace MyCollection.MessageBus
{
    public interface IMessageBus
    {
        void Publish<T>(T message) where T : IIntegrationEvent;
    }
}
