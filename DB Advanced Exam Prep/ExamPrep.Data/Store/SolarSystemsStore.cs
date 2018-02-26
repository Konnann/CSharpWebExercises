using ExamPrep.Data.DTO;
using ExamPrep.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Data.Store
{
    public class SolarSystemsStore
    {
        public static void AddSolarSystem(IEnumerable<SolarSystemDTO> systems)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var star in systems)
                {
                    if (star.Name == null)
                    {
                        Console.WriteLine("Invalid data");
                    }
                    else
                    {
                        context.SolarSystems.Add(new SolarSystem { Name = star.Name });
                        Console.WriteLine($"Successfully imported {star.Name}");

                    }
                }
                context.SaveChanges();
            }
        }
        
        public static SolarSystem GetSystemByName(string name)
        {
            using (var context = new MassDefectEntities())
            {
                var solarSystem = context.SolarSystems
                    .Where(s => s.Name == name)
                    .FirstOrDefault();
                return solarSystem;
            }

        }
    }
}
