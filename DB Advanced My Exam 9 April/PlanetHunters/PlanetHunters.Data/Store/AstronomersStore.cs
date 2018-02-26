namespace PlanetHunters.Data.Store
{
    using Models;
    using PlanetHunters.Data.DTO;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class AstronomersStore
    {
        public static void AddAstronomers(IEnumerable<AstronomerDTO> astronomers)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var astronomerDto in astronomers)
                {
                    if(astronomerDto.FirstName == null || astronomerDto.LastName == null)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {                       
                        try
                        {
                            var astronomer = new Astronomer
                            {
                                FirstName = astronomerDto.FirstName,
                                LastName = astronomerDto.LastName
                            };

                            context.Astronomers.Add(astronomer);
                            Console.WriteLine($"Record {astronomer.FirstName} {astronomer.LastName} successfully imported.");
                        }
                       catch (Exception e)
                       {
                           Console.WriteLine("Invalid data.");
                       }
                        
                    }
                }
                context.SaveChanges();
            }
        }

        public static Astronomer GetAstronomerByName(string name)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Astronomers.FirstOrDefault(a => a.LastName + ", " + a.FirstName == name);
            }
        }
    }
}
