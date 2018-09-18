using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VocabularyMediationService.Database;

namespace VocabularyMediationService
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
            services.AddCors(options =>
            {
                options.AddPolicy("CORSPolicy",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:8080", "http://app01.saeon.ac.za/vms")
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
            });

            var connectionString = Configuration.GetConnectionString("VMS");
            services.AddDbContext<SQLDBContext>(options =>
            {
                options.UseSqlServer(connectionString, o => {
                    o.UseRowNumberForPaging(); //Backwards compatibility for for SQL 2008 R2
                });
            });

            services.AddMvc();

            services
                .AddAuthentication(GetAuthenticationOptions)
                .AddJwtBearer(GetJwtBearerOptions);
        }

        private static void GetAuthenticationOptions(AuthenticationOptions options)
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }

        private static void GetJwtBearerOptions(JwtBearerOptions options)
        {
            options.Authority = "http://identity.saeon.ac.za";
            options.Audience = "http://identity.saeon.ac.za/resources";
            options.RequireHttpsMetadata = false;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CORSPolicy");

            app
                .UseAuthentication()
                .UseMvc();
        }
    }
}
