using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using ViajaNet.TesteRodrigoNunes.Robo.Models;

namespace ViajaNet.TesteRodrigoNunes.Robo
{
    class Program
    {
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
                channel.QueueDeclare(queue: "coletaqueue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var dadosColeta = JsonConvert.DeserializeObject<DadosColeta>(message);

                    Console.WriteLine("Processando requisição da página {0}, IP {1}", dadosColeta.Pagina, dadosColeta.IP);

                    // Insere no Couchbase
                    var ret = _bucket.Insert(Guid.NewGuid().ToString(), dadosColeta);

                    // Insere no SQL Sever
                    var coleta = new tbColeta()
                    {
                        Ip = dadosColeta.IP,
                        Browser = dadosColeta.Browser,
                        Pagina = dadosColeta.Pagina,
                        Parametros = dadosColeta.Pagina,
                        Data = dadosColeta.Data
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
