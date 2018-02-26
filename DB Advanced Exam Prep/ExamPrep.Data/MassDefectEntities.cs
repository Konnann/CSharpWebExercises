namespace ExamPrep.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class MassDefectEntities : DbContext
    {
        public MassDefectEntities()
            : base("name=MassDefectEntities")
        {
           // Database.SetInitializer( new DropCreateDatabaseAlways<MassDefectEntities>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.OriginPlanet)
                .WithMany(p => p.OriginAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Anomaly>()
                .HasRequired(a => a.TeleportPlanet)
                .WithMany(a => a.TargetingAnomalies)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Planet>()
                .HasRequired(p => p.Sun)
                .WithMany(s => s.Planets)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Person>()
                .HasMany(a => a.Anomalies)
                .WithMany(a => a.Victims)
                .Map(av =>
                {
                    av.ToTable("AnomalyVictims");
                    av.MapLeftKey("PersonId");
                    av.MapRightKey("AnomalyId");
                });

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Anomaly> Anomalies { get; set; }
        public virtual DbSet<Star> Stars { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<SolarSystem> SolarSystems { get; set; }

    }
}