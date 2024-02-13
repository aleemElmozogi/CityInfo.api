using CityInfo.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.api.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInrest> PointOfInrest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options):base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<City>().HasData(
                new City("New York") { Id = 1,Description = "i hate it "},
                new City("New York") { Id = 2, Description = "i hate it " },
                new City("tripoli York") { Id = 3, Description = "i hate it " },
                new City("bengazi York") { Id = 4, Description = "i hate it " },
                new City("York") { Id = 5, Description = "i hate it " });

            modelBuilder.Entity<PointOfInrest>().HasData(
                new PointOfInrest("Central park") {
                   Id = 1,
                   CityId= 1,
                   Description= "blaalala"
                },

             new PointOfInrest("Central park")
             {
                 Id = 2,
                 CityId = 2,
                 Description = "blaalala"
             }, new PointOfInrest("Central park")
             {
                 Id = 3,
                 CityId = 3,
                 Description = "blaalala"
             }, new PointOfInrest("Central park")
             {
                 Id = 4,
                 CityId = 4,
                 Description = "blaalala"
             }, new PointOfInrest("Central park")
             {
                 Id = 5,
                 CityId = 5,
                 Description = "blaalala"
             }
                );
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
