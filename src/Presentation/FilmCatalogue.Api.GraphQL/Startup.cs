using Autofac;
using FilmCatalogue.Api.GraphQL.Schemas;
using FilmCatalogue.Persistence.EntityFramework;
using GraphiQl;
using GraphQL.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FilmCatalogue.Api.GraphQL
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = Environment.IsDevelopment();
            })
            // .AddUserContextBuilder(httpContext => new { httpContext.User })
            .AddWebSockets() // Add required services for web socket support
            .AddDataLoader(); // Add required services for DataLoader support


            services.AddHttpContextAccessor();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new Persistence.Module());
            builder.RegisterModule(new Module());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, FilmDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (Environment.IsDevelopment())
            {
                app.UseGraphiQl("/graphql/ui", "/graphql");
            }

            app.UseWebSockets();
            app.UseGraphQLWebSockets<FilmSchema>("/graphql");
            app.UseGraphQL<FilmSchema>("/graphql");

            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated())
            {
                context.SeedDataAsync().Wait();
            }
        }
    }
}
