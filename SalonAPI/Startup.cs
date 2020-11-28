using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SalonAPI.Context;
using SalonAPI.Repositories;
using SalonAPI.Services;
using SalonAPI.Services.impl;

namespace SalonAPI
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

            //var builder = new SqlConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"));
            //builder.UserID = Configuration["DbUser"];
            //builder.Password = Configuration["DbPassword"];
            //var connection = builder.ConnectionString;
            //services.AddDbContext<SalonDBContext>(options => options.UseSqlServer(connection));

            services.AddDbContext<SalonDBContext>(
               options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //to add xml and json format for response
            services.AddMvc(options =>
            {
                options.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
                options.FormatterMappings.SetMediaTypeMappingForFormat("js", "application/json");
            }).AddXmlSerializerFormatters();

            services.AddControllers();

            //to add swagger
            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Salon API", Version = "v1" }); });

            services.AddScoped<ISalonRepository, SalonRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped<ISalonService, SalonService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();
            //Enable middleware to serve swagger-ui, specify the Swagger JSON endpoint
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Salon API V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
