namespace VirginTestWebApi.Models
{
    public class Scenario
    {
        public long ScenarioID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public Guid UserID { get; set; }
        public DateTime SampleDate { get; set; }
        public DateTime CreationDate { get; set; }
        public int NumMonths { get; set; }
        public int MarketID { get; set; }
        public int NetworkLayerID { get; set; }


        public static IEnumerable<Scenario> GetDistinctScenarios (IEnumerable<Scenario> scenarioList)
        {
            return scenarioList.DistinctBy(e => new { e.Surname, e.Forename, e.UserID, e.SampleDate, e.CreationDate, e.NumMonths,
                e.MarketID, e.NetworkLayerID }).ToList();
        }
    }
}
