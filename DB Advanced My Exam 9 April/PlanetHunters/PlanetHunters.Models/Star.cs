namespace PlanetHunters.Models
{
    using PlanetHunters.Models.CustomAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Star
    {
        private string name;

        private int temperature;

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

        [Required, KelvinTemperature] 
        public int Temperature
        {
            get
            {
                return this.temperature;
            }
            set
            {
                if (value < 2400)
                {
                    throw new FormatException();
                }
                else
                {
                    this.temperature = value;
                }
            }
        }

        public int HostStarSystemId { get; set; }

        [Required]
        public virtual StarSystem HostStarSystem { get; set; }

        public int? DiscoveryId { get; set; }

        public virtual Discovery Discovery { get; set; }
    }
}
