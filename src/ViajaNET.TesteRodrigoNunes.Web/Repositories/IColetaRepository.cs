using System.Collections.Generic;
using System.Threading.Tasks;
using ViajaNET.TesteRodrigoNunes.Web.Models;

namespace ViajaNET.TesteRodrigoNunes.Web.Repositories
{
    public interface IColetaRepository
    {
        Task<IList<Coleta>> GetAsync(string ip, string pagina);
    }
}
