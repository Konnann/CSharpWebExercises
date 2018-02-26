using ExamPrep.Data;

namespace Exam.Import
{
    class Startup
    {
        static void Main(string[] args)
        {

            Utility.InitDB();
            JsonImport.ImportSolarSystems();
            JsonImport.ImportStars();
            JsonImport.ImportPlanets();
            JsonImport.ImportPeople();
            JsonImport.ImportAnomalies();
            JsonImport.ImportVictims();
            XMLImport.ImportAnomalies();
        }
    }
}
