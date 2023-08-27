using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext 
    {
        public NZWalksDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed Data for Difficulties
            //Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("19b3811a-5d12-48ee-a4a4-d7a059c0c4e4"),
                    Name = "Easy"

                },

                new Difficulty()
                {
                    Id = Guid.Parse("d5c1231a-c40b-4843-9f73-f0fc7dcb01cd"),
                    Name = "Medium"

                },

                new Difficulty()
                {
                    Id = Guid.Parse("ec716cb8-f1d2-45ab-80bf-33ece903dd4e"),
                    Name = "Hard"

                },


            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);
        }
    }
}
