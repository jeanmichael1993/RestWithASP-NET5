using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RestWithASPNET5.Business;
using RestWithASPNET5.Business.Implementations;
using RestWithASPNET5.Model.Context;
using RestWithASPNET5.Repository;
using RestWithASPNET5.Repository.Implementations;
using RestWithASPNET5.Service;
using RestWithASPNET5.Service.Implementations;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET5
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration,IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

     

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          
            services.AddControllers();
            var connection = Configuration["MySQLConnection:MySQLConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            if (Environment.IsDevelopment())
            {
                MigrateDatabase(connection);
            }

            //Inje��o de dependencia...
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            services.AddScoped<IBookRepository, BookRepositoryImplementation>();
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
        private void MigrateDatabase(string connection)
        {
            try
            {
                var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolveConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };
                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Database migration failed", ex);
                throw ex;
            }
        }
    }
}
