using Autofac;
using TODOGraphQL.Api.GraphQL.Schemas;
using TODOGraphQL.Persistence.EntityFramework;
using GraphiQl;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TODOGraphQL.Api.GraphQL
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
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = true;
                options.ExposeExceptions = Environment.IsDevelopment();
                // TODO: use this for security
                // options.ComplexityConfiguration
                options.SetFieldMiddleware = false;
            })
            // .AddUserContextBuilder(httpContext => new { httpContext.User })
            .AddWebSockets() // Add required services for web socket support
            .AddDataLoader(); // Add required services for DataLoader support

            services.AddHttpContextAccessor();
            services.AddCors();
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

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            if (Environment.IsDevelopment())
            {
                app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()
                {
                    Path = "/ui/playground",
                    GraphQLEndPoint = "/graphql"
                });
                app.UseGraphiQLServer(new GraphiQLOptions
                {
                    GraphiQLPath = "/ui/graphiql",
                    GraphQLEndPoint = "/graphql"
                });
                app.UseGraphQLVoyager(new GraphQLVoyagerOptions()
                {
                    Path = "/ui/voyager",
                    GraphQLEndPoint = "/graphql"
                });
            }

            app.UseCors(options => 
            {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });

            app.UseWebSockets();
            app.UseGraphQLWebSockets<TodoSchema>("/graphql");
            app.UseGraphQL<TodoSchema>("/graphql");

            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated())
            {
                context.SeedDataAsync().Wait();
            }
        }
    }
}
