using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirginTestWebApi.Controllers;
using VirginTestWebApi.Models;
using Xunit;

namespace VirginTestWebAPITests
{
    public class ScenarioControllerTests
    {
        public virtual Mock<ILogger<ScenarioController>> _Logger { get; set; }

        private ScenarioController _controller = null;
        
        public ScenarioControllerTests()
        {
            if (_Logger is null)
            {
                _Logger = new Mock<ILogger<ScenarioController>>();
            }
            if (_controller is null)
            {
                _controller = new ScenarioController(_Logger.Object);
            }
        }

        [Fact]
        public void Should_Return_OK()
        {
            var response = _controller.Get();

            Assert.NotNull(response);
            Assert.IsAssignableFrom<OkObjectResult>(response.Result);
            Assert.True(((IEnumerable<Scenario>)((OkObjectResult)response?.Result)?.Value).Count() == 5);
        }

        [Fact]
        public void Should_Return_InternalServerError()
        {
            var logger = new Logger<ScenarioController>(new Mock<ILoggerFactory>().Object);
            var _controllerFake = new ScenarioController(logger);
            var response = _controllerFake.Get();

            Assert.NotNull(response);
            Assert.IsAssignableFrom<ObjectResult>(response.Result);
            Assert.True(((ObjectResult)response?.Result)?.StatusCode == 500);
        }
    }
}
