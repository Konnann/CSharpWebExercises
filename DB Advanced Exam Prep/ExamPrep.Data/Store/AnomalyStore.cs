using ExamPrep.Data.DTO;
using ExamPrep.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Data.Store
{
    public static class AnomalyStore
    {
        public static void AddAnomalies(IEnumerable<AnomalyDTO> anomalies)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var anomalyDTO in anomalies)
                {
                    if(anomalyDTO.OriginPlanet == null || anomalyDTO.TeleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid Data");
                    }
                    else
                    {
                        var originPlanet = PlanetStore.GetPlanetByName(anomalyDTO.OriginPlanet);
                        var teleportPlanet = PlanetStore.GetPlanetByName(anomalyDTO.TeleportPlanet);
                        
                        if(originPlanet == null || originPlanet == null)
                        {
                            Console.WriteLine("Error: Invalid data");
                        }
                        else
                        {
                            var anomaly = new Anomaly
                            {
                                OriginPlanetId = originPlanet.Id,
                                TeleportPlanetId = teleportPlanet.Id
                            };
                            context.Anomalies.Add(anomaly);

                            Console.WriteLine("Successfully added anomaly");
                        }
                    }
                }
                context.SaveChanges();
            }
        }

        public static void AddAnomaliesWithVictims(List<AnomalyWithVictimsDTO> anomalies
            )
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var anomalyDto in anomalies )
                {
                    var originPlanet = PlanetStore.GetPlanetByName(anomalyDto.OriginPlanet);
                    var teleportPlanet = PlanetStore.GetPlanetByName(anomalyDto.TeleportPlanet);

                    if(originPlanet == null || teleportPlanet == null)
                    {
                        Console.WriteLine("Error: Invalid Data.");
                    }
                    else
                    {
                        var anomaly = new Anomaly
                        {
                            OriginPlanetId = originPlanet.Id,
                            TeleportPlanetId = teleportPlanet.Id
                        };

                        context.Anomalies.Add(anomaly);

                        foreach (var victimName in anomalyDto.Victims)
                        {
                            var victim = context.People.FirstOrDefault(p => p.Name == victimName);

                            if(victim != null)
                            {
                                anomaly.Victims.Add(victim);
                            }
                        }
                    }
                    Console.WriteLine("Added anomaly with victims");
                }
                context.SaveChanges();
            }
        }

        public static Anomaly GetAnomalyById(int Id)
        {
            using (var context = new MassDefectEntities())
            {
                return context.Anomalies.FirstOrDefault(a => a.Id == Id);
            }
        }

        public static void AddVictims(IEnumerable<VictimDTO> victims)
        {
            using (var context = new MassDefectEntities())
            {
                foreach (var victimDto in victims)
                {
                    if (victimDto.Person == null)
                    {
                        Console.WriteLine("Error: Invalid Data");
                    }
                    else
                    {
                        var person = PeopleStore.GetPersonByName(victimDto.Person);
                        var anomaly = GetAnomalyById(victimDto.Id);

                        if (person == null || anomaly == null)
                        {
                            Console.WriteLine("Error: Invalid Data");
                        }
                        else
                        {
                            context.People.Attach(person);
                            context.Anomalies.Attach(anomaly);
                            anomaly.Victims.Add(person);
                            Console.WriteLine($"Successfully Added victim {person.Name} to Anomaly {anomaly.Id}");

                            context.Entry(person).State = EntityState.Detached;
                            context.Entry(anomaly).State = EntityState.Detached;

                        }
                    }
                }

                context.SaveChanges();
            }
        }
    }
}
