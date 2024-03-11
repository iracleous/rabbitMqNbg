using Newtonsoft.Json;
using RabbitMQ.Client;
using rabbitMqNbg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rabbitMqNbg.Services;

public class QueueProducer
{
    public void Produce()
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

       
        var item = new Item
        {
            Name = "product",
            Id = 3
        };

        //Serialize the message
        var json = JsonConvert.SerializeObject(item);
        var body = Encoding.UTF8.GetBytes(json);
        //put the data on to the product queue
        channel.BasicPublish(exchange: string.Empty,
            routingKey: "messageQueue", body: body);

    }




}
