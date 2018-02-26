using ExamPrep.Data;
using ExamPrep.Data.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Export
{
    public class JsonExport
    {
        public static void ExportPlanets()
        {
            var planets =  PlanetStore.GetPlanetsWithNoVictims();
            var json = JsonConvert.SerializeObject(planets, Formatting.Indented);

            File.WriteAllText("../../../export/planets.json", json);
        }
    }
}
