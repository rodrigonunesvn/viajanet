using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using ViajaNET.TesteRodrigoNunes.Robo.Commands;
using ViajaNET.TesteRodrigoNunes.Robo.Models;

namespace ViajaNET.TesteRodrigoNunes.Robo
{
    class Program
    {
        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando leitura da fila...");

            var couchbaseServer = "http://:8091";
            ClusterHelper.Initialize(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri(couchbaseServer) }
            }, new PasswordAuthenticator("", ""));

            var _bucket = ClusterHelper.GetBucket("Coleta");            
            var factory = new ConnectionFactory()
            {
                HostName = "",
                Port = 5672,
                UserName = "",
                Password = ""
            };

            using (var dbContext = new viajanetContext())
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                //channel.ExchangeDeclare("exchange.name", ExchangeType.Direct);
                channel.QueueDeclare(queue: "coletaqueue",
                                        durable: true,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var command = JsonConvert.DeserializeObject<ColetaCommand>(message);

                    Console.WriteLine("{2} Processando requisição da página {0}, IP {1}", command.Pagina, command.IP, command.Data);

                    // Insere no Couchbase
                    var ret = _bucket.Insert(Guid.NewGuid().ToString(), command);

                    // Insere no SQL Sever
                    var coleta = new tbColeta()
                    {
                        Ip = command.IP,
                        Browser = command.Browser,
                        Pagina = command.Pagina,
                        Parametros = command.Pagina,
                        Data = command.Data
                    };

                    dbContext.Add(coleta);
                    dbContext.SaveChanges();
                };

                channel.BasicConsume(queue: "coletaqueue",
                                        autoAck: true,
                                        consumer: consumer);

                Console.WriteLine("Finalizado.");
                Console.ReadLine();
            }
        }
    }
}
