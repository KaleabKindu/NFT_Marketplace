using System.Text;
using RabbitMQ.Client;
namespace Infrastructure.Services
;
public class RabbitMqService
{
    private readonly IModel _channel;

    public RabbitMqService(IConnection connection, List<string> queues)
    {
        _channel = connection.CreateModel();
        foreach (string queueName in queues)
        {
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: true, arguments: null);
        }
    }

    public void EnqueueAsync<T>(T message, string queueName)
    {
        var jsonMessage = Newtonsoft.Json.JsonConvert.SerializeObject(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);

        _channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
    }

    public T DequeueAsync<T>(string queueName)
    {
        BasicGetResult result = _channel.BasicGet(queueName, autoAck: true);
        
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
        _channel.Close();
    }
}