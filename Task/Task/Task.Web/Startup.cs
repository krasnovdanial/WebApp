using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LinqToDB.AspNet;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Task.Web.Db;
using Task.Web.Models;
using Task.Web.Repositories;

namespace Task.Web
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
            services.AddControllers();

            services.AddLinqToDbContext<ShopDbContext>(
                (provider, optionsBuilder) =>
                    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddScoped<DataConnection, ShopDbContext>();

            services.AddScoped<Repository<UserComment>, CommentRepository>();
            services.AddScoped<Repository<User>, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
