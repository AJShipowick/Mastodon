using OsOEasy.Promo.Models;
using OsOEasy.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace OsOEasy_API.Data
{
    public class APIDbContext : DbContext
    {

        public APIDbContext(DbContextOptions<APIDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OsOEasy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Promotion> PromotionModel { get; set; }

    }
}
