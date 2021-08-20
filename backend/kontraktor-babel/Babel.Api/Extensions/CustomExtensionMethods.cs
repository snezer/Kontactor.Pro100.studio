using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using Babel.Api.Controllers;
using Babel.Api.Services;
using Babel.Db.Services;
using Babel.Db.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Babel.Api.Extensions
{
    public static class CustomExtensionMethods
    {

        public static IServiceCollection AddCustomServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<BaseDatabaseSettings>(
                configuration.GetSection(nameof(BaseDatabaseSettings))
            );

            services.AddSingleton<IBaseDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<BaseDatabaseSettings>>().Value);


            services.AddSingleton<RoomService>()
                .AddSingleton<EntityService>()
                .AddSingleton<LevelService>()
                .AddSingleton<EntityTypeService>()

                .AddSingleton<NgonbLibraryService>()

                ;

            return services;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvcCore()
                .AddApiExplorer()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = false;
                });

            services.AddRazorPages()
                .AddControllersAsServices();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API",
                    Version = "v1",
                    Description =
                        "api",
                    TermsOfService = new Uri("https://tos"),
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }

    }
}
