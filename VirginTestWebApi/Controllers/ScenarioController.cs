using Microsoft.AspNetCore.Mvc;
using VirginTestWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VirginTestWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScenarioController : ControllerBase
    {
        const string XMLPATH = "./Data/Data.xml";
        const string XMLROOT = "Data";
        const string XMLCHILD = "Scenario";
        const int INTERNALSERVERERROR = 500;

        private readonly ILogger<ScenarioController> _logger;

        public ScenarioController(ILogger<ScenarioController> logger)
        {
            _logger = logger;
        }


        // GET: <ScenarioController>
        [HttpGet(Name = "GetScenarioList")]
        public ActionResult<IEnumerable<Scenario>> Get()
        {
            try
            {
                var nodes = Helpers.XMLHelper.GetScenariosXMLNode(XMLPATH, XMLROOT, XMLCHILD);
                var scenarioListResults = Helpers.XMLHelper.ScenariosXMLNodeParse(nodes);

                if (scenarioListResults.scenarioListErrorElements is not null && scenarioListResults.scenarioListErrorElements.Any())
                {
                    _logger.LogInformation(DateTime.Now.ToString() + ": The following data has been detected as 'wrong data': \n");
                    _logger.LogInformation(string.Join(",", scenarioListResults.scenarioListErrorElements));
                }

                if (scenarioListResults.scenarioList is null)
                {
                    scenarioListResults.scenarioList = new List<Scenario>();
                }

                var summaryScenarioList = Scenario.GetDistinctScenarios(scenarioListResults.scenarioList);

                return Ok(summaryScenarioList);

            }catch(Exception e)
            {
                return StatusCode(INTERNALSERVERERROR, e.Message);
            }
        }
    }
}
