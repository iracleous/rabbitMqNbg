using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using rabbitMqNbg.Models;
using rabbitMqNbg.Services;
using System.Text;
using System.Threading.Channels;

var item = new Item
{
    Name = "product",
    Id = 3
};



//QueueProducer queueProducer    = new QueueProducer();
//for (int i= 0; i < 10; i++)
//        queueProducer.Produce(new Item
//        {
//            Name = "product",
//            Id = i
//        });

///////////////////////////////////////////////////
///
QueueConsumer consumer = new QueueConsumer();
//var items = consumer.Consume();
//items.ForEach(item => Console.WriteLine($"id= {item.Id}, name= {item.Name}"));



var items = consumer.ConsumeUsingExchange();
items.ForEach(item => Console.WriteLine($"item= {item}"));



