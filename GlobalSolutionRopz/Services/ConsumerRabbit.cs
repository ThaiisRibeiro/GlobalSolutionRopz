using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace GlobalSolutionRopz.Services
{
    public class ConsumerRabbit 
    {
        private readonly ConnectionFactory _factory;

        public ConsumerRabbit()
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
        public string ExecuteAsync()
        {
            using var connection = _factory.CreateConnection();
            using var channel = connection.CreateModel();

            // Declara fila antes de consumir
            channel.QueueDeclare(queue: "alertas",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            //Pegar mensagem imediatamente (sincronamente)
            var result = channel.BasicGet(queue: "alertas", autoAck: true);
            if (result != null)
            {
                return Encoding.UTF8.GetString(result.Body.ToArray());
            }

            string message = "";
            var resetEvent = new ManualResetEventSlim();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                message = Encoding.UTF8.GetString(body);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[RabbitMQ] Mensagem recebida com ID: {message}");
                Console.ResetColor();

                resetEvent.Set(); // sinaliza que recebeu
            };

            channel.BasicConsume(queue: "alertas",
                                 autoAck: true,
                                 consumer: consumer);

            // Espera por até 5 segundos para receber a mensagem
            resetEvent.Wait(TimeSpan.FromSeconds(5));

            return message; // Retorna mensagem recebida ou string vazia
        }


    }
        

    }

