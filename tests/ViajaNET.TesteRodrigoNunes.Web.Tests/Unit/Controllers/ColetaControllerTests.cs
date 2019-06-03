using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RabbitMQ.Client.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ViajaNET.TesteRodrigoNunes.Web.Commands;
using ViajaNET.TesteRodrigoNunes.Web.Controllers;
using ViajaNET.TesteRodrigoNunes.Web.Models;
using ViajaNET.TesteRodrigoNunes.Web.Repositories;
using ViajaNET.TesteRodrigoNunes.Web.Tests.Unit.Repositories;
using Xunit;

namespace ViajaNET.TesteRodrigoNunes.Web.Tests.Unit.Controllers
{
    public class ColetaControllerTests
    {
        [Fact]
        public async Task coleta_controller_get_should_return_one()
        {
            var queueServiceMock = new Mock<IQueueService>();
            var coletaRepositoryMock = new ColetaRepositoryFake();
            var controller = new ColetaController(coletaRepositoryMock,
                queueServiceMock.Object);

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var result = await controller.Get(string.Empty, "Home") as JsonResult;
            var list = (List<Coleta>)result.Value;
            result.Should().NotBeNull();
            list.Count.Should().BeGreaterOrEqualTo(1);
        }

        [Fact]
        public async Task coleta_controller_get_should_return_all()
        {
            var queueServiceMock = new Mock<IQueueService>();
            var coletaRepositoryMock = new ColetaRepositoryFake();
            var controller = new ColetaController(coletaRepositoryMock,
                queueServiceMock.Object);

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var result = await controller.Get(string.Empty, string.Empty) as JsonResult;
            var list = (List<Coleta>)result.Value;
            result.Should().NotBeNull();
            list.Count.Should().BeGreaterOrEqualTo(3);
        }

        [Fact]
        public async Task coleta_controller_get_should_return_notfound()
        {
            var queueServiceMock = new Mock<IQueueService>();
            var coletaRepositoryMock = new ColetaRepositoryFake();
            var controller = new ColetaController(coletaRepositoryMock,
                queueServiceMock.Object);

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var result = await controller.Get(Guid.NewGuid().ToString(), string.Empty);

            var contentResult = result as NotFoundResult;
            contentResult.Should().NotBeNull();
        }

        [Fact]
        public async Task coleta_controller_post_should_return_accepted()
        {
            var queueServiceMock = new Mock<IQueueService>();
            var coletaRepositoryMock = new Mock<IColetaRepository>();
            var controller = new ColetaController(coletaRepositoryMock.Object,
                queueServiceMock.Object);

            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.Connection.RemoteIpAddress = new System.Net.IPAddress(12312312);

            var command = new ColetaCommand
            {
                IP = "127.0.0.1",
                Browser = "Chrome",
                Pagina = "Home",
                Data = DateTime.UtcNow,
                Parametros = String.Empty
            };

            var result = await controller.Post(command);

            var contentResult = result as AcceptedResult;
            contentResult.Should().NotBeNull();
        }
    }
}
