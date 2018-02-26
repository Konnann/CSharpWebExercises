namespace PlanetHunters.Models
{
    using PlanetHunters.Models.CustomAnnotations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Telescope
    {
        private string name;

        private string location;

        private decimal? mirrorDiameter;


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

        [Required, MaxLength(255)]
        public string Location
        {
            get
            {
                return this.location;
            }
            set
            {
                if (value.Length > 255)
                {
                    throw new FormatException();
                }
                else
                {
                    this.location = value;
                }
            }
        }

        [PositiveDecimal]
        public decimal? MirrorDiameter
        {
            get
            {
                return this.mirrorDiameter;
            }
            set
            {
                if (value <= 0)
                {
                    throw new FormatException();
                }
                else
                {
                    this.mirrorDiameter = value;
                }
            }
        }

        public virtual ICollection<Discovery> Discoveries { get; set; }
    }
}
