using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KONTAKTOR.DA.Interfaces
{
    public interface IBaseServiceOptions
    {
        public string MainDBConnectionName { get; }
        public bool IsConnectionsEncoded { get;  }
    }

    public static class BaseServiceOptionsRegisterExtension
    {
        public static void AddBaseServiceOptions<T>(this IServiceCollection services, IConfiguration config, string configProperty) where  T: class, IBaseServiceOptions, new()
        {
            var mainOptions = config.GetSection(configProperty);
            T options = new T();
            mainOptions.Bind(options);
            services.AddSingleton<IBaseServiceOptions>(options);
            services.AddSingleton<T>(options);
        }

        public static void AddGenericServiceOptions<T>(this IServiceCollection services, IConfiguration config, string configProperty) where T : class, new()
        {
            var mainOptions = config.GetSection(configProperty);
            T options = new T();
            mainOptions.Bind(options);
            services.AddSingleton<T>(options);
        }
    }
}
