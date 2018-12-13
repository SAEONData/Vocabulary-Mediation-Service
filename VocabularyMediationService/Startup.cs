using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Swashbuckle.AspNetCore.Swagger;
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
                          builder.AllowAnyOrigin()
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

            // Add OpenAPI/Swagger document
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info
                {
                    Title = "VMS API",
                    Version = "v1.0-beta",
                    Description = "This API gives you access to all <b>VMS <i>(Vocabulary Mediation Service)</i></b> data. <br/>" +
                    "Access is unauthenticated, but read-only.</i>",
                    Contact = new Contact() { Name = "Contact Us", Url = "http://app01.saeon.ac.za/dev/UI_footside/page_contact.html" },
                    License = new License() { Name = "Data Licences", Url = "https://docs.google.com/document/d/e/2PACX-1vT8ajcogJEEo0ZC9BGIej_jOH2EV8lMFrwOu8LB4K9pDq7Tki94mUoVxU8hGM-J5EL8V3w5o83_TuEl/pub" },
                    TermsOfService = "http://noframe.media.dirisa.org/wiki-1/conditions-of-use?searchterm=conditions"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

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

            // Add OpenAPI/Swagger middlewares
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1.0-beta");
            });

            app
                .UseAuthentication()
                .UseMvc();
        }
    }
}
