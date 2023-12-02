using Core.Abstract;
using Core.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class MessageService: IMessageService
    {
        public void SendMessage<RaporStatus>(RaporStatus message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "user",
                Password = "mypass",
                VirtualHost = "/"

            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("rapor", durable: false, exclusive: false);
            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);
            channel.BasicPublish("", "rapors", body: body);




        }
    }
}
