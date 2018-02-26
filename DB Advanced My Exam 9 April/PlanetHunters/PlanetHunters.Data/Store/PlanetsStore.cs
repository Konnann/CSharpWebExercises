namespace PlanetHunters.Data.Store
{
    using PlanetHunters.Data.DTO;
    using PlanetHunters.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public static class PlanetsStore
    {
        public static void AddPlanets(IEnumerable<PlanetDTO> planets)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var planetDto in planets)
                {
                    if (planetDto.Name == null || planetDto.Mass == null || planetDto.StarSystem == null)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        var starSystem = StarSystemsStore.GetStarSystemByName(planetDto.StarSystem);

                        if(starSystem == null)
                        {
                            starSystem = new StarSystem
                            {
                                Name = planetDto.StarSystem
                            };

                            var newStarSystem = StarSystemsStore.AddStarSystem(starSystem);
                            context.StarSystems.Attach(newStarSystem);
                        }

                        try
                        {
                            var planet = new Planet
                            {
                                Name = planetDto.Name,
                                Mass = (decimal)planetDto.Mass,
                                HostStarSystemId = StarSystemsStore.GetStarSystemByName(planetDto.StarSystem).Id
                            };

                            context.Planets.Add(planet);

                            Console.WriteLine($"Record {planet.Name} successfully imported.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invalid data format.");
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        public static Planet GetPlanetByName(string name)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Planets.FirstOrDefault(p => p.Name == name);
            }
        }
    }
}
