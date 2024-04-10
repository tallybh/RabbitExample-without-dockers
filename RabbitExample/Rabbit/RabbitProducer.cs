using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitExample.Rabbit
{
    public class RabbitProducer : IRabbitProducer
    {
        public void SendLogMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            var connection = factory.CreateConnection();
            using
            var channel = connection.CreateModel();
            channel.QueueDeclare("log", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "log", body: body);
        }

        public void SendProductMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
            var connection = factory.CreateConnection();
            using
            var channel = connection.CreateModel();
            channel.QueueDeclare("message", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "message", body: body);
        }
    }
}
