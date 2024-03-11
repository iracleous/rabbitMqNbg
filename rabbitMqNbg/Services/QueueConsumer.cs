using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbitMqNbg.Services;

public class QueueConsumer
{
    public void Consume()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };

        //Create the RabbitMQ connection using connection factory details as i mentioned above
        using var connection = factory.CreateConnection();
        //Here we create channel with session and model
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: "messageQueue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);




        //Set Event object which listen message from chanel which is sent by producer
        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, eventArgs) => {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Product message received: {message}");

        };
        //read the message
        channel.BasicConsume(queue: "messageQueue", autoAck: true, consumer: consumer);
    }
}
