using System;

namespace ViajaNET.TesteRodrigoNunes.Web.Models
{
    public class Coleta
    {
        public string IP { get; set; }        
        public string Pagina { get; set; }        
        public string Browser { get; set; }        
        public string Parametros { get; set; }
        public DateTime Data { get; set; }
    }
}
