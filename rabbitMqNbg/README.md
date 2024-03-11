**RabbitMQ  producer/consumer NBG app


1. Needed nuggets
- Newtonsoft.Json
- RabbitMQ.Client


1. Run RabbitMQ containerized

docker run --name myqueue -p 15672:15672 -p 5672:5672 -d rabbitmq:management

