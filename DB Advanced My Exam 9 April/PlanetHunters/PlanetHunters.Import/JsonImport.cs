
namespace PlanetHunters.Import
{
    using Data.DTO;
    using Data.Store;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public static class JsonImport
    {
        public static void ImportAstronomers()
        {
            var json = File.ReadAllText("../../../datasets/astronomers.json");
            var astronomers = JsonConvert.DeserializeObject<IEnumerable<AstronomerDTO>>(json);
            AstronomersStore.AddAstronomers(astronomers);
        }

        public static void ImportTelescopes()
        {
            var json = File.ReadAllText("../../../datasets/telescopes.json");
            var telescopes = JsonConvert.DeserializeObject<IEnumerable<TelescopeDTO>>(json);
            TelescopesStore.AddTelescope(telescopes);
        }

        public static void ImportPlanets()
        {
            var json = File.ReadAllText("../../../datasets/planets.json");
            var planets = JsonConvert.DeserializeObject<IEnumerable<PlanetDTO>>(json);
            PlanetsStore.AddPlanets(planets);
        }
    }
}
