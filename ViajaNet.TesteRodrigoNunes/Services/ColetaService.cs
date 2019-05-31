using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using Couchbase.Linq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViajaNet.TesteRodrigoNunes.Models;
using Encoding = System.Text.Encoding;

namespace ViajaNet.TesteRodrigoNunes.Services
{
    public class ColetaService : IColetaService
    {
        private readonly IBucket _bucket;

        public ColetaService(IBucketProvider bucketProvider)
        {
            _bucket = bucketProvider.GetBucket("Coleta");            
        }

        public void Send(DadosColeta dadosColeta)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "52.161.12.37",
                Port = 5672,
                UserName = "user1",
                Password = "Lorena2811"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "coletaqueue",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                string message = JsonConvert.SerializeObject(dadosColeta);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                        routingKey: "coletaqueue",
                                        basicProperties: null,
                                        body: body);
            }
        }

        public object Get(string ip, string pagina)
        {
            var db = new BucketContext(_bucket);
            var listaDadosColeta = new List<DadosColeta>();

            var query = "SELECT * FROM Coleta";
            var where = string.Empty;
               
            if(!string.IsNullOrWhiteSpace(ip))
                where = $"ip = '{ip}' ";

            if(!string.IsNullOrWhiteSpace(pagina))
                where += (!string.IsNullOrEmpty(where) ? " and ": "") + $"pagina = '{pagina}' ";

            if (!string.IsNullOrEmpty(where))
                query += " WHERE " + where;
                
            var result = _bucket.Query<dynamic>(query);
            return result.ToList();
        }
    }
}
