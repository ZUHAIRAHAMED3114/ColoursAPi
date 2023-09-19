using ColoursAPi.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColoursAPi
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
            var server = Configuration["DBServer"] ?? "localhost";
            var port = Configuration["DBPort"] ?? "1433";
            var Database = Configuration["Database"] ?? "Colours";
            var user= Configuration["DBUser"] ?? "SA";//Your SQL Server login
            var password = Configuration["DBPassword"] ?? "pa55w0rd2019";  // Your SQL Server password
            var PasswordConnection = Configuration["PasswordConnection"] ?? "false";

            string dbServer = Configuration["DB_SERVER"]?? "(localdb)\\MSSQLLocalDB";
            Console.WriteLine(dbServer);
            string dbName = Configuration["DB_NAME"]??"Colours";
            string dbIntegratedSecurity = Configuration["DB_INTEGRATED_SECURITY"]??"True";
            
            string connectionString1 = $"Data Source={dbServer};Initial Catalog={dbName};Integrated Security={dbIntegratedSecurity}";
            string connectionString2 = $"Server={server},{port};Initial Catalog={Database};User ID={user};Password={password}";

            if (PasswordConnection.ToLower() == "true") 
                Console.WriteLine($"Connection String 1 is Sleected" + connectionString2);
            else Console.WriteLine($"Connection String 1 is Sleected"+connectionString1);
            
            
            services.AddDbContext<ColorDbContext>(options =>
            {
                options.UseSqlServer(PasswordConnection=="true"?connectionString2:connectionString1);
            });
            services.AddControllers();
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

            PrepDb.PrepPopulation(app);
        }
    }
}
