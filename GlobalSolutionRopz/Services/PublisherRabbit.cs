using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GlobalSolutionRopz.Services
{
    public class PublisherRabbit
    {
      

        private readonly ConnectionFactory _factory;

        public PublisherRabbit()
        {
            _factory = new ConnectionFactory()
            {
                //HostName = "4.201.125.90",
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest"
            };
        }

        //public async Task PublicarMensagemAsync(string mensagem)
            public void PublicarMensagemAsync(string mensagem)
        {


            // Aguarde a Task e armazene o resultado no tipo correto
            // IConnection connection = _factory.CreateConnection();
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel(); 

            channel.QueueDeclare(queue: "alertas",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(mensagem);

            channel.BasicPublish(exchange: "",
                                 routingKey: "alertas",
                                 basicProperties: null,
                                 body: body);
        }
    }
}

