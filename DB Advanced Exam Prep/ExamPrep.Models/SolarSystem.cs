using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Models
{
    public class SolarSystem
    {
        public SolarSystem()
        {
            Stars = new HashSet<Star>();
            Planets = new HashSet<Planet>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public virtual ICollection<Star> Stars { get; set; }
        public virtual ICollection<Planet> Planets { get; set; }
    }
}
