using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Models
{
    public class Planet
    {
        public Planet()
        {
            People = new HashSet<Person>();
            OriginAnomalies = new HashSet<Anomaly>();
            TargetingAnomalies = new HashSet<Anomaly>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public int SunId { get; set; }
        public virtual Star Sun { get; set; }
        public int SolarSystemId { get; set; }
        public virtual SolarSystem SolarSystem { get; set; }
        public virtual ICollection<Person> People { get; set; }
        public virtual ICollection<Anomaly> OriginAnomalies { get; set; }
        public virtual ICollection<Anomaly> TargetingAnomalies { get; set; }
    }
}
