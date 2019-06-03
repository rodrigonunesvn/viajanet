using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViajaNET.TesteRodrigoNunes.Web.Models;
using ViajaNET.TesteRodrigoNunes.Web.Repositories;

namespace ViajaNET.TesteRodrigoNunes.Web.Tests.Unit.Repositories
{
    class ColetaRepositoryFake : IColetaRepository
    {
        private readonly IList<Coleta> _coletas;

        public ColetaRepositoryFake()
        {
            _coletas = new List<Coleta>()
            {
                new Coleta() { IP = "127.0.0.1", Pagina = "Home", Browser  = "Chrome", Data = DateTime.UtcNow },
                new Coleta() { IP = "127.0.0.1", Pagina = "Landing page", Browser  = "Edge", Data = DateTime.UtcNow },
                new Coleta() { IP = "127.0.0.1", Pagina = "Checkout", Browser  = "Safari", Data = DateTime.UtcNow },
            };
        }

        public async Task<IList<Coleta>> GetAsync(string ip, string pagina)
        {
            var result = _coletas.Where(p => (p.IP == ip || String.IsNullOrWhiteSpace(ip)) &&
                (p.Pagina == pagina || String.IsNullOrWhiteSpace(pagina))).ToList();

            return result;
        }
    }
}
