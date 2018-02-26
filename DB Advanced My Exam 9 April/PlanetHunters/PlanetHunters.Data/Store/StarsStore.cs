namespace PlanetHunters.Data.Store
{
    using DTO;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Linq;

    public static class StarsStore
    {
        public static void AddStars(IEnumerable<StarDTO> stars)
        {
            using (var context = new PlanetHuntersContext())
            {
                //context.StarSystems.Load();

                foreach (var starDto in stars)
                {
                    if (starDto.Name == null || starDto.Temperature == null || starDto.StarSystem == null)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        var starSystem = StarSystemsStore.GetStarSystemByName(starDto.StarSystem);
                        
                        if (starSystem == null)
                        {
                            starSystem = new StarSystem
                            {
                                Name = starDto.StarSystem
                            };

                            context.StarSystems.Add(starSystem);
                            //var newStarSystem = StarSystemsStore.AddStarSystem(starSystem);
                            //context.StarSystems.Attach(newStarSystem);
                        }

                        try
                        {
                            var star = new Star
                            {
                                Name = starDto.Name,
                                Temperature = int.Parse(starDto.Temperature),
                                HostStarSystem = starSystem

                            };
                            context.Stars.Add(star);
                            Console.WriteLine($"Record {star.Name} successfully imported.");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Invalid data format.");
                        }
                    }
                }
                try
                {
                    context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var item in e.EntityValidationErrors)
                    {
                        foreach (var err in item.ValidationErrors)
                        {
                            Console.WriteLine(err.ErrorMessage);
                        }
                    }
                }
            }
        }

        public static Star GetStarByName(string name)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Stars.FirstOrDefault(s => s.Name == name);
            }
        }
    }
}
