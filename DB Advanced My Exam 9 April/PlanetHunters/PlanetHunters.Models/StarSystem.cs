namespace PlanetHunters.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class StarSystem
    {
        private string name;

        public StarSystem()
        {
            this.Stars = new HashSet<Star>();
            this.Planets = new HashSet<Planet>();
        }

        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value.Length > 255)
                {
                    throw new FormatException();
                }
                else
                {
                    this.name = value;
                }
            }
        }

        public virtual ICollection<Star> Stars { get; set; }

        public virtual ICollection<Planet> Planets { get; set; }
    }
}
