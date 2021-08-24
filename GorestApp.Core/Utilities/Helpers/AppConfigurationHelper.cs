using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GorestApp.Core.Utilities.Helpers
{
    public static class AppConfigurationHelper
    {
        private static IConfigurationRoot ConfigurationRoot 
        { 
            get
            {
                var builder = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                return builder.Build();
            } 
        }

        public static string GetConnectionString()
        {
            return ConfigurationRoot.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
        }

        public static string GetAppVersion()
        {
            return ConfigurationRoot.GetSection("Version").Value;
        }

        public static string GetGorestUserSourceUrl()
        {
            return ConfigurationRoot.GetSection("GorestUserSourceUrl").Value;
        }

    }
}
