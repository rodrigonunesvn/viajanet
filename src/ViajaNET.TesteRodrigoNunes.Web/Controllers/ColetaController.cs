using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViajaNET.TesteRodrigoNunes.Web.Commands;
using ViajaNET.TesteRodrigoNunes.Web.Models;
using ViajaNET.TesteRodrigoNunes.Web.Repositories;

namespace ViajaNET.TesteRodrigoNunes.Web.Controllers
{
    [Route("api/[controller]")]
    public class ColetaController : Controller
    {
        private readonly IColetaRepository _repository;
        private readonly IQueueService _queueService;

        public ColetaController(IColetaRepository coletaRepository, IQueueService queueService)
        {
            _repository = coletaRepository;
            _queueService = queueService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string ip, string pagina)
        {
            IList<Coleta> coletas = await _repository.GetAsync(ip, pagina);

            if (coletas == null || coletas.Count == 0)
            {
                return NotFound();
            }

            return Json(coletas);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ColetaCommand command)
        {
            command.Data = DateTime.UtcNow;
            command.IP = this.HttpContext.Connection.RemoteIpAddress.ToString();
            await _queueService.SendAsync<ColetaCommand>(@object: command, exchangeName: "coleta.exchange", routingKey: "coletaqueue");

            return Accepted();
        }
    }
}
