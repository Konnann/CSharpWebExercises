using ExamPrep.Data.DTO;
using ExamPrep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Data.Store
{
    public static class PeopleStore
    {
        public static void AddPeople(IEnumerable<PersonDTO> people)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var personDto in people)
                {
                    if(personDto.Name == null || personDto.HomePlanet == null)
                    {
                        Console.WriteLine("Error: Invalid Data");
                    }
                    else
                    {
                        var planet = PlanetStore.GetPlanetByName(personDto.HomePlanet);

                        if(planet == null)
                        {
                            Console.WriteLine("Error: Invalid Data");
                        }
                        else
                        {
                            var person = new Person
                            {
                                Name = personDto.Name,
                                HomePlanetId = planet.Id
                            };
                            context.People.Add(person);
                            Console.WriteLine($"Successfully Imported Person {person.Name}");
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public static Person GetPersonByName(string name)
        {
            using (var context = new MassDefectEntities())
            {
                return context.People.Where(p => p.Name == name).FirstOrDefault();
            }
        }
    }
}
