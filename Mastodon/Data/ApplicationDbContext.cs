using Mastodon.Models;
using Mastodon.Promo.Models.DBModels;
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
            var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Mastodon;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        //To update the SQL Server DB scripts and structure run the following commands in Package Manager Console
        //Add-Migration CreateXX
        //Update-Database
        //or
        //Add-Migration CreateXX -Context MyContext
        //Update-Database -Context MyContext

        public DbSet<Promotion> Promotion { get; set; }
        public DbSet<PromotionStats> PromotionStats { get; set; }
        public DbSet<PromotionEntries> PromotionEntries { get; set; }
        public DbSet<AccountActivity> AccountActivity { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
