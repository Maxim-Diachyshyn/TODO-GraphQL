using Autofac;
using FilmCatalogue.Api.Web.Rest.Converters;
using FilmCatalogue.Persistence.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmCatalogue.Api.Web.Rest
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
            services.AddMvc()
                .AddJsonOptions(options => 
                {
                    options.SerializerSettings.Converters.Insert(0, new IdConverter());
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new Persistence.EntityFramework.Module());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FilmDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated())
            {
                context.SeedDataAsync().Wait();
            }
        }
    }
}
