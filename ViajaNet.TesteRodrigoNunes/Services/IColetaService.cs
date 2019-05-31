using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViajaNet.TesteRodrigoNunes.Models;

namespace ViajaNet.TesteRodrigoNunes.Services
{
    public interface IColetaService
    {
        void Send(DadosColeta dadosColeta);
        object Get(string ip, string pagina);
    }
}
