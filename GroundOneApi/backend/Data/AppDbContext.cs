using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data;

namespace backend
{
// firstly DataBase context
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
// now we move to DbSets: Tables/ Entities
        public DbSet<Item> Items { get; set; }
        public DbSet<Compartment> Compartments { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // DatabaseSeeder.Seed(modelBuilder); // Removed due to missing definition

            // Relation configurations
            modelBuilder.Entity<Compartment>()
                .HasOne(c => c.Vehicle)
                .WithMany(v => v.Compartments)
                .HasForeignKey(c => c.VehicleId);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Compartment)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.CompartmentId);
            // thats EF CORE magic - it creates relations in db based on navigation properties in models // normally you have to do it manually with SQL commands
        }
    }
}
