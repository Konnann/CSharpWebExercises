namespace PlanetHunters.Data
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PlanetHuntersContext : DbContext
    {
        public PlanetHuntersContext()
            : base("name=PlanetHuntersContext")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<PlanetHuntersContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Astronomer>()
                .HasMany(a => a.DiscoveriesMade)
                .WithMany(d => d.Pioneers)
                .Map(ad =>
               {
                   ad.MapLeftKey("PioneerId");
                   ad.MapRightKey("DiscoveryId");
                   ad.ToTable("PioneersDiscoveries");
               });

            modelBuilder.Entity<Astronomer>()
                .HasMany(a => a.DiscoveriesObserved)
                .WithMany(d => d.Observers)
                .Map(ad =>
                {
                    ad.MapLeftKey("ObserverId");
                    ad.MapRightKey("DiscoveryId");
                    ad.ToTable("ObserversDiscoveries");
                });

            modelBuilder.Entity<Discovery>()
                .HasRequired(d => d.Telescope)
                .WithMany(t => t.Discoveries)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Star>()
                .HasRequired(s => s.HostStarSystem)
                .WithMany(s => s.Stars)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Planet>()
                .HasRequired(p => p.HostStarSystem)
                .WithMany(s => s.Planets);


            base.OnModelCreating(modelBuilder);

        }

        public virtual DbSet<StarSystem> StarSystems { get; set; }
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Telescope> Telescopes { get; set; }
        public virtual DbSet<Discovery> Discoveries { get; set; }
        public virtual DbSet<Astronomer> Astronomers { get; set; }
    }
}