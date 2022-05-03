using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using FoodApi.Models;
namespace FoodApi.Models
{
    public class MyFirstAPIDBContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        public MyFirstAPIDBContext(DbContextOptions<MyFirstAPIDBContext> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var connectionString = Configuration.GetConnectionString("MyConnection");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
        public DbSet<Food> Food { get; set; } = null!;
        public DbSet<Ingredient> Ingredient { get; set; } = null!;
    }
}
