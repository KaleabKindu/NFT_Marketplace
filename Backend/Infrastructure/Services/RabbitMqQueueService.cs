using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Services
;
public class RabbitMqService
{
    private readonly IConnection _connection;

    public RabbitMqService(IConnection connection)
    {
        _connection = connection;
    }

    public void EnqueueAsync<T>(T message, string queueName)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var jsonMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }

    public T DequeueAsync<T>(string queueName)
    {
        using var channel = _connection.CreateModel();
        channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

        var consumer = new EventingBasicConsumer(channel);
        BasicGetResult result = channel.BasicGet(queueName, autoAck: true);
        
        if (result == null)
        {
            // No message in the queue
            return default;
        }

        var jsonMessage = Encoding.UTF8.GetString(result.Body.ToArray());
        var message = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonMessage);
        
        return message;
    }

    public void Close(){
        _connection.Close();
    }
}