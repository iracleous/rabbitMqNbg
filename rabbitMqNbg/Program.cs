using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using rabbitMqNbg.Services;
using System.Text;

QueueProducer queueProducer    = new QueueProducer();

queueProducer.Produce();

QueueConsumer consumer = new QueueConsumer();
consumer.Consume();


Console.ReadLine();






