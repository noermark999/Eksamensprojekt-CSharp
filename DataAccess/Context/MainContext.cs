using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class MainContext : DbContext
    {
        public MainContext() : base("Eksamensprojekt") { }
        public DbSet<Ferry> Ferrys { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasKey(c => c.CarID);
            modelBuilder.Entity<Passenger>().HasKey(p => p.PassengerID);
            modelBuilder.Entity<Ferry>().HasKey(f => f.FerryID);

            modelBuilder.Entity<Car>()
                .HasRequired(c => c.Ferry)
                .WithMany(f => f.Cars)
                .HasForeignKey(c => c.FerryID)
                .WillCascadeOnDelete(true);
                

            modelBuilder.Entity<Passenger>()
                .HasRequired(p => p.Ferry)
                .WithMany(f => f.Passengers)
                .HasForeignKey(c => c.FerryID)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Passenger>()
                .HasOptional(p => p.Car)
                .WithMany(c => c.Passengers)
                .HasForeignKey(p => p.CarID)
                .WillCascadeOnDelete(false);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
