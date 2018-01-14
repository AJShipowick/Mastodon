using OsOEasy_API.Data;
using OsOEasy_API.Responses;
using OsOEasy_API.Responses.CSS;
using OsOEasy_API.Responses.HTML;
using OsOEasy_API.Responses.JS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OsOEasy_API.Services;

namespace OsOEasy_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // Add framework services.
            services.AddDbContext<APIDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddLogging();
            services.AddTransient<IMainJS, MainJS>();
            services.AddTransient<IBasicHTML, BasicHTML>();
            services.AddTransient<IBasicCSS, BasicCSS>();
            services.AddTransient<IBasicJS, BasicJS>();
            services.AddTransient<IPromoService, PromoService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
