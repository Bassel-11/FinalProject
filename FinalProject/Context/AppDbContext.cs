using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.; Database=FinalProject;Trusted_Connection=True;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Food", Description = "For human consumption" },
                new Category { CategoryId = 2, Name = "Drinks", Description = "Various beverages" },
                new Category { CategoryId = 3, Name = "Clothes", Description = "Clothing items" },
                new Category { CategoryId = 4, Name = "Miscellaneous", Description = "Various other items" }
            );

            // Seed products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Title = "Fish", Price = 10, Description = "Fresh fish", Quantity = 100, ImagePath = "fish.jpg", CategoryId = 1 },
                new Product { ProductId = 2, Title = "Meat", Price = 20, Description = "Fresh meat", Quantity = 50, ImagePath = "meat.jpg", CategoryId = 1 },
                new Product { ProductId = 3, Title = "Juice", Price = 5, Description = "Orange juice", Quantity = 200, ImagePath = "juice.jpg", CategoryId = 2 },
                new Product { ProductId = 4, Title = "V7", Price = 3, Description = "Energy drink", Quantity = 150, ImagePath = "v7.jpg", CategoryId = 2 },
                new Product { ProductId = 5, Title = "T-shirt", Price = 15, Description = "Cotton T-shirt", Quantity = 75, ImagePath = "tshirt.jpg", CategoryId = 3 },
                new Product { ProductId = 6, Title = "Pants", Price = 25, Description = "Denim pants", Quantity = 60, ImagePath = "pants.jpg", CategoryId = 3 }
            );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
