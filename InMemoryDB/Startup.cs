using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using InMemoryDB.Models;
using System.IO;

namespace InMemoryDB
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
            services.AddMvc();

            string port = Configuration.GetValue<string>("port");
            string folder = "node" + port;
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            services.AddSingleton(port);

            string dbFile = folder + @"\data.db";
            if (File.Exists(dbFile))
            {
                services.AddSingleton(new Database(WriteReadToDatabase.Read(dbFile)));
            }
            else
            {
                services.AddSingleton(new Database());
            }
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
