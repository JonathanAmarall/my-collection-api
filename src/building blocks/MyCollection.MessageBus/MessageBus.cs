using Microsoft.Extensions.Options;
using MyCollection.Core.Contracts;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MyCollection.MessageBus
{
    public class MessageBus : IMessageBus, IDisposable
    {
        private readonly MessageBusSettings _messageBrokerSettings;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageBus(IOptions<MessageBusSettings> messageBusSettingsOptions)
        {
            _messageBrokerSettings = messageBusSettingsOptions.Value;

            IConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = _messageBrokerSettings.HostName,
                Port = _messageBrokerSettings.Port,
                UserName = _messageBrokerSettings.UserName,
                Password = _messageBrokerSettings.Password
            };

            _connection = connectionFactory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.QueueDeclare(_messageBrokerSettings.QueueName, false, false, false);
        }

        public void Publish<T>(T message) where T : IIntegrationEvent
        {
            string payload = JsonConvert.SerializeObject(message, typeof(IIntegrationEvent), new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });

            byte[] body = Encoding.UTF8.GetBytes(payload);

            _channel.BasicPublish(string.Empty, _messageBrokerSettings.QueueName, body: body);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _connection?.Dispose();
                _channel?.Dispose();
            }
        }
    }
}