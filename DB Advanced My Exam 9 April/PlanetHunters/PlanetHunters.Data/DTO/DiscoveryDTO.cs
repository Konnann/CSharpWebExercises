using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetHunters.Data.DTO
{
    public class DiscoveryDTO
    {
        public DiscoveryDTO()
        {
            this.Stars = new List<string>();
            this.Planets = new List<string>();
            this.Pioneers = new List<string>();
            this.Observers = new List<string>();
        }

        public string DateMade { get; set; }

        public string Telescope { get; set; }

        public ICollection<string> Stars { get; set; }

        public ICollection<string> Planets { get; set; }

        public ICollection<string> Pioneers { get; set; }

        public ICollection<string> Observers { get; set; }
    }
}
