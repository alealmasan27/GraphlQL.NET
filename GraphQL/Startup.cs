using System;
using GraphQLServer.Models;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<UniversityContext>(options =>
            {
                options.UseSqlServer(@"Server=<hostname>;Database=University;Trusted_Connection=True;MultipleActiveResultSets=True;");
            });
            services.AddCors();

            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddFiltering()
                .AddSorting();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(cors => cors.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseRouting()
                .UseEndpoints(endpoints => endpoints.MapGraphQL());
            app.UsePlayground();
        }
    }
}
