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
    public void Produce(Item item)
    {
        var factory = new ConnectionFactory { HostName = "localhost", Port=5100 };

        //Create the RabbitMQ connection using connection factory details as i mentioned above
        using var connection = factory.CreateConnection();
        //Here we create channel with session and model
        using var channel = connection.CreateModel();

       channel.ExchangeDeclare("CS1225_FanoutEx", ExchangeType.Fanout);

        channel.QueueDeclare(queue: "messageQueue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);



        channel.QueueDeclare(queue: "Q2",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        channel.QueueDeclare(queue: "Q3",
                             durable: false,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        channel.QueueBind("messageQueue", "CS1225_FanoutEx", "");
        channel.QueueBind("Q2", "CS1225_FanoutEx", "");
        channel.QueueBind("Q3", "CS1225_FanoutEx", "");

        //Serialize the message
        var json = JsonConvert.SerializeObject(item);
        var body = Encoding.UTF8.GetBytes(json);
        //put the data on to the product queue
        channel.BasicPublish(
            exchange: "CS1225_FanoutEx",
            routingKey: "", 
            body: body);

    }




}
