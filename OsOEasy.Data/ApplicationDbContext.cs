using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OsOEasy.Data.Models;
using System;

namespace OsOEasy.Data
{

    //Not sure why this 1st class is needed, there was an issue with .net core EF Framework....
    //https://stackoverflow.com/questions/49521315/cannot-add-migration-with-asp-net-core-2
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            return new ApplicationDbContext(builder.Options);
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {

            //For local development..... 
            //Set the project/properties/debug environment variable 'OSODBCONNECTION' as:
            //Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=OsOEasy;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;MultipleActiveResultSets=true;

            //For release, 'OSODBCONNECTION' must be set as the correct DB connection string in Azure

            var connectionString = Environment.GetEnvironmentVariable("OSODBCONNECTION");

            builder.UseSqlServer(connectionString);
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
        public DbSet<PaymentActivity> PaymentActivity { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
