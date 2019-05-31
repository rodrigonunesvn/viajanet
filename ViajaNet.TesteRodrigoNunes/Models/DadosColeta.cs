using Couchbase;
using System;

namespace ViajaNet.TesteRodrigoNunes.Models
{
    public class DadosColeta
    {
        public string IP { get; set; }        
        public string Pagina { get; set; }        
        public string Browser { get; set; }        
        public string Parametros { get; set; }
        public DateTime Data { get; set; }
    }
}
