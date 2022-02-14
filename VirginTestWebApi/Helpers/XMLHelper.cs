using System;
using System.Text.RegularExpressions;
using System.Xml;
using VirginTestWebApi.Models;

namespace VirginTestWebApi.Helpers
{
    public static class XMLHelper
    {
        public static XmlNodeList? GetScenariosXMLNode(string xmlPath, string root, string element)
        {
            try
            {
                var doc = new XmlDocument();
                doc.Load(xmlPath);


                return doc.SelectNodes($"/{ root }/{ element }");

            }catch(DirectoryNotFoundException)
            {
                throw new DirectoryNotFoundException("The introduced path is incorrect");
            }
        }


        public static ScenarioListResults ScenariosXMLNodeParse (XmlNodeList? nodes)
        {
            var scenarioList = new List<Scenario>();
            var scenarioListErrorElements = new List<Scenario>();

            if (nodes != null)
            {
                //Loop through the selected Nodes.
                foreach (XmlNode node in nodes)
                {
                    try
                    {
                        //Adding innertext to variables
                        var regex = new Regex(@"\r\n?|\n|\t|\s", RegexOptions.Compiled);

                        var scenarioID = node["ScenarioID"]?.InnerText;
                        var name = node["Name"]?.InnerText;
                        var surname = node["Surname"]?.InnerText;
                        var forename = node["Forename"]?.InnerText;
                        var userID = node["UserID"]?.InnerText;
                        var sampleDate = node["SampleDate"]?.InnerText;
                        var creationDate = node["CreationDate"]?.InnerText;
                        var numMonths = node["NumMonths"]?.InnerText;
                        var marketID = node["MarketID"]?.InnerText;
                        var networkLayerID = node["NetworkLayerID"]?.InnerText;

                        var defaultNumber = -1;
                        var defaultString = "Data Error";

                        
                        var scenario = new Scenario
                        {
                            ScenarioID = scenarioID is not null ? int.Parse(scenarioID) : defaultNumber,
                            Name = !string.IsNullOrEmpty(name) ? name : defaultString,
                            Surname = !string.IsNullOrEmpty(surname) ? surname : defaultString,
                            Forename = !string.IsNullOrEmpty(forename) ? forename : defaultString,
                            UserID = userID is not null ? Guid.Parse(regex.Replace(userID, String.Empty)) : Guid.Empty,
                            SampleDate = sampleDate is not null ? DateTime.Parse(sampleDate) : DateTime.MinValue,
                            CreationDate = creationDate is not null ? DateTime.Parse(creationDate) : DateTime.MinValue,
                            NumMonths = numMonths is not null ? int.Parse(numMonths) : defaultNumber,
                            MarketID = marketID is not null ? int.Parse(marketID) : defaultNumber,
                            NetworkLayerID = networkLayerID is not null ? int.Parse(networkLayerID) : defaultNumber
                        };

                        if (scenarioID is not null && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(surname) && !string.IsNullOrEmpty(forename)
                            && userID is not null && sampleDate is not null && creationDate is not null && numMonths is not null && marketID is not null
                            && networkLayerID is not null)
                        {
                            scenarioList.Add(scenario);
                        }
                        else
                        {
                            scenarioListErrorElements.Add(scenario);
                        }
                        

                    }catch(Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }

                return new ScenarioListResults {
                    scenarioList = scenarioList,
                    scenarioListErrorElements = scenarioListErrorElements
                };
            }

            throw new ArgumentNullException();
        } 
    }
}
