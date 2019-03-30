using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using FinalProyect.Entities;
using FinalProyect.Repositories;
using Microsoft.AspNetCore.Http;
using FinalProyect.Helpers;

namespace FinalProyect
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
            });

            // register the DbContext on the container, getting the connection string from
            // appSettings (note: use this during development; in a production environment,
            // it's better to store the connection string in an environment variable)
            var connectionString = Configuration["connectionStrings:DBConnectionString"];
            services.AddDbContext<PersonsCollectivesContext>(o => o.UseSqlServer(connectionString));

            // register the repository
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddScoped<ICollectiveRepository, CollectiveRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory, PersonsCollectivesContext personsCollectivesContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unexpected fault happened. Try again later.");

                    });
                });
            }

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Person, Models.PersonDto>()
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src =>
                    $"{src.Name} {src.Surname}"))
                    .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                    src.DateOfBirth.GetCurrentAge()));
                //.ForMember(dest => dest.Collectives, opt => opt.MapFrom(src =>
                //src.Collectives);

                cfg.CreateMap<Models.CreatePersonDto, Entities.Person>();

                cfg.CreateMap<Models.CollectiveDto, Entities.Collective>();

            });


            personsCollectivesContext.EnsureSeedDataForContext();

            app.UseMvcWithDefaultRoute();
        }
    }
}
