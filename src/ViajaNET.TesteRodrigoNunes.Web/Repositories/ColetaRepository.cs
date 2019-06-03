using Couchbase.Core;
using Couchbase.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViajaNET.TesteRodrigoNunes.Web.Models;

namespace ViajaNET.TesteRodrigoNunes.Web.Repositories
{
    public class ColetaRepository : IColetaRepository
    {
        private readonly IBucket _bucket;

        public ColetaRepository(IBucketProvider bucketProvider)
        {
            _bucket = bucketProvider.GetBucket("Coleta");
        }

        public async Task<IList<Coleta>> GetAsync(string ip, string pagina)
        {
            var query = "SELECT `Coleta`.* FROM `Coleta`";
            var where = string.Empty;

            if (!string.IsNullOrWhiteSpace(ip))
                where = $"ip = '{ip}' ";

            if (!string.IsNullOrWhiteSpace(pagina))
                where += (!string.IsNullOrEmpty(where) ? " and " : "") + $"pagina = '{pagina}' ";

            if (!string.IsNullOrEmpty(where))
                query += " WHERE " + where;

            var result = await _bucket.QueryAsync<Coleta>(query);
            return result.Rows;
        }
    }
}
