using System;
using System.Collections.Generic;

namespace ViajaNET.TesteRodrigoNunes.Robo.Models
{
    public partial class tbColeta
    {
        public int Id { get; set; }
        public string Ip { get; set; }
        public string Pagina { get; set; }
        public string Browser { get; set; }
        public string Parametros { get; set; }
        public DateTime Data { get; set; }
    }
}
