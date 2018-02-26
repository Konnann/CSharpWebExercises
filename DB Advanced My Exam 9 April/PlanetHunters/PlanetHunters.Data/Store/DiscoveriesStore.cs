using PlanetHunters.Data.DTO;
using PlanetHunters.Models;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PlanetHunters.Data.Store
{
    public static class DiscoveriesStore
    {
        public static void AddDiscoveries(IEnumerable<DiscoveryDTO> discoveries)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var discoveryDto in discoveries)
                {
                    if(discoveryDto.DateMade == null || discoveryDto.Telescope == null)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        var telescope = TelescopesStore.GetTelescopeByName(discoveryDto.Telescope);
                        bool invalidData = false;

                        var planets = new HashSet<Planet>();
                        var stars = new HashSet<Star>();
                        var observers = new HashSet<Astronomer>();
                        var pioneers = new HashSet<Astronomer>();


                        foreach (var pioneer in discoveryDto.Pioneers)
                        {
                            var astronomer = AstronomersStore.GetAstronomerByName(pioneer);

                            if (astronomer == null)
                            {
                                invalidData = true;
                            }
                            else
                            {
                                pioneers.Add(astronomer);
                            }

                        }

                        foreach (var observer in discoveryDto.Observers)
                        {
                            var astronomer = AstronomersStore.GetAstronomerByName(observer);

                            if (astronomer == null)
                            {
                                invalidData = true;
                            }
                            else
                            {
                                observers.Add(astronomer);
                            }

                        }

                        foreach (var star in discoveryDto.Stars)
                        {
                            var starObj = StarsStore.GetStarByName(star);

                            if ( starObj == null)
                            {
                                invalidData = true;
                            }
                            else
                            {
                                stars.Add(starObj);
                            }

                        }

                        foreach (var planet in discoveryDto.Planets)
                        {
                            var planetObj = PlanetsStore.GetPlanetByName(planet);

                            if (planetObj == null)
                            {
                                invalidData = true;
                            }
                            else
                            {
                                planets.Add(planetObj);
                            }
                        }

                        if (invalidData)
                        {
                            Console.WriteLine("INVALID DATA");                            
                            continue;
                        }

                        try
                        {
                            var discovery = new Discovery
                            {
                                DateMade = DateTime.ParseExact(discoveryDto.DateMade, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                                TelescopeId = telescope.Id,
                                Planets = planets,
                                Stars = stars,
                                Pioneers = pioneers,
                                Observers = observers                                
                            };
                            context.Discoveries.Add(discovery);
                            Console.WriteLine("Added discovery");
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
    }
}
