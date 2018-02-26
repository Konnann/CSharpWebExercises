using ExamPrep.Data.DTO;
using ExamPrep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Data.Store
{
    public class StarStore
    {
        public static void AddStars(IEnumerable<StarDTO> stars)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var starDto in stars)
                {
                    if (starDto.Name == null || starDto.SolarSystem == null)
                    {
                        Console.WriteLine("Error: Invalid Data");
                    }
                    else
                    {
                        var solarSystem = SolarSystemsStore.GetSystemByName(starDto.SolarSystem);


                        if (solarSystem == null)
                        {
                            Console.WriteLine("Error: Invalid Data");
                        }
                        else
                        {
                            var star = new Star
                            {
                                Name = starDto.Name,
                                SolarSystemId = solarSystem.Id
                            };
                            context.Stars.Add(star);
                        }
                    }
                }
                context.SaveChanges();              
            }
        }


        public static Star GetStarByName(string name)
        {
            using (var context = new MassDefectEntities())
            {
                return context.Stars.Where(s => s.Name == name).FirstOrDefault();
            }
        }
    }
}
