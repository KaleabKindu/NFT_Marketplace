using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
;
public class RabbitMqService
{
    private readonly IConnection _connection;
    private readonly string _queueName; // The queue name

    public RabbitMqService(IConnection connection, IConfiguration configuration)
    {
        _connection = connection;
        _queueName = configuration["RabbitMQ:QueueName"];
    }

    public void EnqueueAsync<T>(T message)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var jsonMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
    }

    public T DequeueAsync<T>()
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        BasicGetResult result = channel.BasicGet(_queueName, autoAck: true);

        if (result == null)
        {
            // No message in the queue
            return default(T);
        }

        var jsonMessage = Encoding.UTF8.GetString(result.Body.ToArray());
        var message = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonMessage);

        return message;
    }
}