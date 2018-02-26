namespace PlanetHunters.Models
{
    using PlanetHunters.Models.CustomAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Planet
    {
        private string name;

        private decimal mass;

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

        [Required, PositiveDecimal]
        public decimal Mass
        {
            get
            {
                return this.mass;
            }
            set
            {
                if (value <= 0)
                {
                    throw new FormatException();
                }
                else
                {
                    this.mass = value;
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
