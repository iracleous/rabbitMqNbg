using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using rabbitMqNbg.Models;
using Newtonsoft.Json;
using System.Threading.Channels;

namespace rabbitMqNbg.Services;

public class QueueConsumer
{
    private ManualResetEvent messageProcessingComplete = new ManualResetEvent(false);

    public List<Item> Consume()
    {
        List<Item> items = new List<Item>();

        var factory = new ConnectionFactory { 
            HostName = "localhost" , 
            Port=5100,
            UserName = "guest",
            Password = "guest",
             
        };

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
          //  Console.WriteLine($"Product message received: {message}");

            Item? item = JsonConvert.DeserializeObject<Item>(message);
            if(item != null ) items.Add(item);

            // Check if all expected messages are processed then Signal that message processing is complete
            messageProcessingComplete.Set();
        };
        //read the message
        channel.BasicConsume(queue: "messageQueue", autoAck: true, consumer: consumer);
        bool messagesProcessed = messageProcessingComplete.WaitOne(TimeSpan.FromSeconds(1)); // Adjust the timeout as needed
        return items;
    }
    public List<string> ConsumeUsingExchange()
    {
        List<string> result = new List<string>();
        var factory = new ConnectionFactory { HostName = "localhost", Port = 5100 };

        //Create the RabbitMQ connection using connection factory details as i mentioned above
        using var connection = factory.CreateConnection();
        //Here we create channel with session and model
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare("CS1225_FanoutEx", ExchangeType.Fanout);

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            result.Add(message);
        };

        channel.BasicConsume("Q2", true, consumer);
        return result;
    }






}
