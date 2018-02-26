namespace PlanetHunters.Import
{
    using Data.DTO;
    using Data.Store;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    public static class ImportXML
    {
        public static void ImportStars()
        {
            XDocument xml = XDocument.Load("../../../datasets/stars.xml");
            var stars = xml.Root.Elements();
            var result = new List<StarDTO>();

            foreach (var star in stars)
            {
                try
                {
                    var starDto = new StarDTO
                    {
                        Name = star.Element("Name").Value,
                        Temperature = star.Element("Temperature").Value,
                        StarSystem = star.Element("StarSystem").Value
                    };
                    result.Add(starDto);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid data format.");
                }

            }
            StarsStore.AddStars(result);
        }

        public static void ImportDiscoveries()
        {
            XDocument xml = XDocument.Load("../../../datasets/discoveries.xml");
            var discoveries = xml.Root.Elements();
            var result = new List<DiscoveryDTO>();

            foreach (var discovery in discoveries)
            {
                var stuff = discovery.Element("Stars").Elements().Select(e => e.Value).ToList();
                try
                {
                    var discoveryDto = new DiscoveryDTO
                    {
                        DateMade = discovery.Attribute("DateMade").Value,
                        Telescope = discovery.Attribute("Telescope").Value,
                        Planets = discovery.Element("Planets").Elements().Select(e => e.Value).ToList(),
                        Stars = discovery.Element("Stars").Elements().Select(e => e.Value).ToList(),
                        Pioneers = discovery.Element("Pioneers").Elements().Select(e => e.Value).ToList(),
                        Observers = discovery.Element("Observers").Elements().Select(e => e.Value).ToList()
                    };
                    result.Add(discoveryDto);
                    Console.WriteLine($"Added planet");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Invalid data format");
                }

            }
            DiscoveriesStore.AddDiscoveries(result);
        }
    }
}
