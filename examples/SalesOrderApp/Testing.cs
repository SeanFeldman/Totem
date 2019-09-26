﻿using System.IO;
using MicroservicesMessagingDemo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace SalesOrderApp
{
    public static class Testing
    {
        private static readonly IServiceScopeFactory ScopeFactory;
        public static IConfigurationRoot Configuration { get; }

        static Testing()
        {
            var currentDirectory = Directory.GetCurrentDirectory();

            Configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();

            var startup = new Startup(Configuration);
            var services = new ServiceCollection();

            startup.ConfigureServices(services);

            var rootContainer = services.BuildServiceProvider();
            ScopeFactory = rootContainer.GetService<IServiceScopeFactory>();
        }

        public static TotemSettings GetTotemApplicationConfigurationSettings()
        {
            var totemSettings = new TotemSettings();
            var appSettings = Configuration.GetSection(nameof(TotemSettings));
            appSettings.Bind(totemSettings);

            return totemSettings;
        }

        public static string Json(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
    }
}