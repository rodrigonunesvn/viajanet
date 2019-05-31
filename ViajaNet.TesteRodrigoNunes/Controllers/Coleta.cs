using Microsoft.AspNetCore.Mvc;
using System;
using ViajaNet.TesteRodrigoNunes.Models;
using ViajaNet.TesteRodrigoNunes.Services;

namespace ViajaNet.TesteRodrigoNunes.Controllers
{
    [Route("api/[controller]")]
    public class Coleta : Controller
    {
        private readonly IColetaService _coletaService;

        public Coleta(IColetaService coletaService)
        {
            _coletaService = coletaService;
        }

        [HttpGet]
        public object Get(string ip, string pagina)
        {            
            return _coletaService.Get(ip, pagina); 
        }

        [HttpPost]
        public void Post([FromBody]DadosColeta dadosColeta)
        {
            var ip = this.HttpContext.Connection.RemoteIpAddress.ToString();
            dadosColeta.IP = ip;
            dadosColeta.Data = DateTime.Now;

            _coletaService.Send(dadosColeta);
        }
    }
}
