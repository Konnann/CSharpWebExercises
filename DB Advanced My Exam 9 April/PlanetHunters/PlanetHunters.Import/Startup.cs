using PlanetHunters.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetHunters.Import
{
    class Startup
    {
        static void Main(string[] args)
        {
            Utility.InitDB();
            JsonImport.ImportAstronomers();
            JsonImport.ImportTelescopes();
            JsonImport.ImportPlanets();
            ImportXML.ImportStars();
            ImportXML.ImportDiscoveries();
        }
    }
}
