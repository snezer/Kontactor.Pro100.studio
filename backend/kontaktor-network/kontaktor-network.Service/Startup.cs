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
using KONTAKTOR.DA.Mongo;
using KONTAKTOR.DA.Mongo.Repository;
using KONTAKTOR.DA.Repository;
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
            services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

            services.AddSystemMetrics();
            services.AddHealthChecks()
                .ForwardToPrometheus();

            services.AddGenericServiceOptions<MongoConnectionOptions>(Configuration, "MongoDB");
            

            

            services.AddSingleton<ICompanyRepository, CompanyRepository>();
            services.AddSingleton<IOccupationRepository, OccupationRepository>();

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