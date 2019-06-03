using System;

namespace ViajaNET.TesteRodrigoNunes.Web.Commands
{
    public class ColetaCommand
    {
        public string IP { get; set; }        
        public string Pagina { get; set; }        
        public string Browser { get; set; }        
        public string Parametros { get; set; }
        public DateTime Data { get; set; }
    }
}
