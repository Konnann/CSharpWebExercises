using ExamPrep.Data.DTO;
using ExamPrep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Data.Store
{
    public class PlanetStore
    {
        public static void AddPlanets(IEnumerable<PlanetDTO> planets)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var planetDto in planets)
                {
                    if (planetDto.Name == null || planetDto.SolarSystem == null || planetDto.Sun == null)
                    {
                        Console.WriteLine("Error: Invalid Data");
                    }
                    else
                    {
                        var solarSystem = SolarSystemsStore.GetSystemByName(planetDto.SolarSystem);
                        var sun = StarStore.GetStarByName(planetDto.Sun);

                        if (solarSystem == null || sun == null)
                        {
                            Console.WriteLine("Error: Invalid Data n");
                        }
                        else
                        {
                            var planet = new Planet
                            {
                                Name = planetDto.Name,
                                SunId = sun.Id,
                                SolarSystemId = solarSystem.Id
                            };
                            Console.WriteLine($"Added planet - {planet.Name}");
                            context.Planets.Add(planet);
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public static Planet GetPlanetByName(string name)
        {
            using (var context = new MassDefectEntities())
            {
                return context.Planets.Where(p => p.Name == name).FirstOrDefault();
            }
        }

        public static IEnumerable<PlanetExportDTO> GetPlanetsWithNoVictims()
        {
            using (var context = new MassDefectEntities())
            {
                var planets = context.Planets
                    .Where(p => p.OriginAnomalies.All(a => a.Victims.Count == 0))
                    .Select(p => new PlanetExportDTO
                    {
                        Name = p.Name
                    })
                    .ToList();
                return planets;
            }
        }
    }
}
