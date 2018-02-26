namespace PlanetHunters.Data.Store
{
    using PlanetHunters.Data.DTO;
    using PlanetHunters.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class TelescopesStore
    {
        public static void AddTelescope(IEnumerable<TelescopeDTO> telescopes)
        {
            using (var context = new PlanetHuntersContext())
            {
                foreach (var telescopeDto in telescopes)
                {
                    if(telescopeDto.Name == null || telescopeDto.Location == null)
                    {
                        Console.WriteLine("Invalid data format.");
                    }
                    else
                    {
                        try
                        {
                            var telescope = new Telescope
                            {
                                Name = telescopeDto.Name,
                                Location = telescopeDto.Location,
                                MirrorDiameter = telescopeDto.MirrorDiameter
                            };

                            context.Telescopes.Add(telescope);
                            Console.WriteLine($"Record {telescope.Name} successfully imported.");
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

        public static Telescope GetTelescopeByName(string name)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.Telescopes.FirstOrDefault(t => t.Name == name);
            }
        }
    }
}
