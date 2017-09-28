using Mastodon.Slider.Models;
using Microsoft.EntityFrameworkCore;

namespace Mastodon_API.Data
{
    public class APIDbContext : DbContext
    {

        public APIDbContext(DbContextOptions<APIDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Mastodon;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<ClientModel> ClientModel { get; set; }
        public DbSet<ClientsWebsite> ClientsWebsites { get; set; }

    }
}
