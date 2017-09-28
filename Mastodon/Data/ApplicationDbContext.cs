using Mastodon.Models;
using Mastodon.Slider.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mastodon.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Mastodon;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            optionsBuilder.UseSqlServer(connectionString);
        }

        //To update the SQL Server DB scripts and structure run the following commands in Package Manager Console
        //Add-Migration InitialCreate
        //Update-Database
        //or
        //Add-Migration InitialCreate -Context MyContext
        //Update-Database -Context MyContext

        public DbSet<ClientModel> ClientModel { get; set; }
        public DbSet<ClientsWebsite> ClientsWebsites { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
