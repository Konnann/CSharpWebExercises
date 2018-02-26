namespace PlanetHunters.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Astronomer
    {
        private string firstName;
        private string lastName;
        public Astronomer()
        {
            this.DiscoveriesMade = new HashSet<Discovery>();
            this.DiscoveriesObserved = new HashSet<Discovery>();
        }

        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                if (value.Length > 50)
                {
                    throw new FormatException();
                }
                else
                {
                    this.firstName = value;
                }
            }
        }

        [Required, MaxLength(50)]
        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                if (value.Length > 50)
                {
                    throw new FormatException();
                }
                else
                {
                    this.lastName = value;
                }
            }
        }

        public virtual ICollection<Discovery> DiscoveriesMade { get; set; }

        public virtual ICollection<Discovery> DiscoveriesObserved { get; set; }
    }
}
