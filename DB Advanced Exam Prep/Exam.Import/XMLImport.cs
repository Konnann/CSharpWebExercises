namespace Exam.Import
{
    using ExamPrep.Data.DTO;
    using ExamPrep.Data.Store;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    public class XMLImport
    {
        public static void ImportAnomalies()
        {
            XDocument xml = XDocument.Load("../../../datasets/new-anomalies.xml");
            var anomalies = xml.Root.Elements();
            var result = new List<AnomalyWithVictimsDTO>();

            foreach (var anomaly in anomalies)
            {
                try
                {
                    var anomalyDto = new AnomalyWithVictimsDTO
                    {
                        OriginPlanet = anomaly.Attribute("origin-planet").Value,
                        TeleportPlanet = anomaly.Attribute("teleport-planet").Value,
                        Victims = anomaly.Element("victims").Elements().Select(e => e.Attribute("name").Value).ToList()

                    };
                    result.Add(anomalyDto);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine("Error: Invalid Data");
                }

            }
            AnomalyStore.AddAnomaliesWithVictims(result);
        }
    }
}
