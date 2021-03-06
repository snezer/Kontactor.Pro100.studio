using System;
using System.IO;
using System.Reflection;
using CENTROS.SMSNotifications.Service.Models.Mapping;
using Dapper.Contrib.Extensions;
using DapperExtensions.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using netcoreservice.Service.Discovery;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using KONTAKTOR.DA.Interfaces;
using KONTAKTOR.DA.Models;
using KONTAKTOR.DA.Mongo;
using KONTAKTOR.DA.Mongo.Repository;
using KONTAKTOR.DA.Repository;
using KONTAKTOR.Service.Services;
using KONTAKTOR.Service.Services.ContractTempating;
using Prometheus;
using Prometheus.SystemMetrics;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace netcoreservice.Service
{
    public class Startup
    {
        private readonly IWebHostEnvironment _environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _environment = env;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = _environment.IsDevelopment());
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
                {
                    options.OperationFilter<SwaggerDefaultValues>();
                    
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    if (File.Exists(xmlPath))
                    {
                        options.IncludeXmlComments(xmlPath);
                    }

                    var modelsFile = $"{typeof(Compartment).Assembly.GetName().Name}.xml";
                    var xmlPathModels = Path.Combine(AppContext.BaseDirectory, modelsFile);
                    if (File.Exists(xmlPathModels))
                    {
                        options.IncludeXmlComments(xmlPathModels);
                    }
                }
            );

            services.AddSystemMetrics();
            

            services.AddGenericServiceOptions<MongoConnectionOptions>(Configuration, "MongoDB");
            

            

            services.AddSingleton<CompanyRepository>();
            services.AddSingleton<OccupationRepository>();
            services.AddSingleton<BinaryContentRepository>();
            services.AddSingleton<UserInformationRepository>();
            services.AddSingleton<UserRoleRepository>();
            services.AddSingleton<TenancyRepository>();
            services.AddSingleton<CompartmentRepository>();
            services.AddSingleton<RentFactRepository>();
            services.AddSingleton<MainNewsRepository>();
            services.AddSingleton<ChatRepository>();
            services.AddSingleton<MessageRepository>();
            services.AddSingleton<EmployeeRepository>();

            services.AddSingleton<RolesSeeder>();
            services.AddSingleton<UserSeeder>();
            services.AddSingleton<CompaniesSeeder>();

            services.AddSingleton<DocXGenerationService>();
            services.AddSingleton<ContractGenerationService>();

            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
            services.AddAutoMapper(typeof(CompanyMappingProfile));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(o =>
            {
                o.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(_=>true)
                    .AllowCredentials();
            });
                
            app.UseHttpMetrics();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMetrics();
            });

            app.UseSwagger(opts =>
            {
                opts.SerializeAsV2 = true;
            });


            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });
        }
    }
}
