namespace PlanetHunters.Data.Store
{
    using PlanetHunters.Models;
    using System.Linq;

    public static  class StarSystemsStore
    {
        public static StarSystem GetStarSystemByName(string name)
        {
            using (var context = new PlanetHuntersContext())
            {
                return context.StarSystems.FirstOrDefault(s => s.Name == name);
            }
        }

        public static StarSystem AddStarSystem(StarSystem starSystem)
        {
            using (var context = new PlanetHuntersContext())
            {
                context.StarSystems.Add(starSystem);

                context.SaveChanges();    
            }
            return GetStarSystemByName(starSystem.Name);
                            
        }

    }
}
