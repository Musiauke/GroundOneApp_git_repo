using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data;

namespace backend.Data
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
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Compartments)
                .WithOne(c => c.Vehicle)
                .HasForeignKey(c => c.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);  

            modelBuilder.Entity<Compartment>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Compartment)
                .HasForeignKey(i => i.CompartmentId)
                .OnDelete(DeleteBehavior.Cascade);      
        }
    }
}
