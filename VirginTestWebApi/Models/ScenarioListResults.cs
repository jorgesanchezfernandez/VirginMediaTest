namespace VirginTestWebApi.Models
{
    public class ScenarioListResults
    {
        public  IEnumerable<Scenario>? scenarioList { get; set; }
        public  IEnumerable<Scenario>? scenarioListErrorElements { get; set; }
    }
}
