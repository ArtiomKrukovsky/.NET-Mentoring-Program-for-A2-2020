using EF_Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EF_Core.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Region>().HasData(
                new Region
                {
                    RegionId = 1,
                    RegionDescription = "This is a beautiful region"
                },
                new Region
                {
                    RegionId = 2,
                    RegionDescription = "This is a not good region for doing business"
                }
            );

            modelBuilder.Entity<Territory>().HasData(
                new Territory
                {
                   TerritoryId = "Gdansk",
                   TerritoryDescription = "This is a beautiful territory",
                   RegionId = 1
                },
                new Territory
                {
                   TerritoryId = "Grodno",
                   TerritoryDescription = "This is a beautiful territory",
                   RegionId = 2
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "Phones",
                    Description = "Tech"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "Cars",
                    Description = "Tesla"
                },
                new Category
                {
                    CategoryId = 3,
                    CategoryName = "Monitors",
                    Description = "Full Hd"
                },
                new Category
                {
                    CategoryId = 4,
                    CategoryName = "CPU",
                    Description = "Intel i9"
                }
            );
        }
    }
}