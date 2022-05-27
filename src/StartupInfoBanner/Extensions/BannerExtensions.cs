using Microsoft.AspNetCore.Builder;
using System.Runtime.InteropServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace StartupInfoBanner.Extensions
{
    public static class BannerExtensions
    {
        public static IApplicationBuilder ShowStartupInfo(
            this IApplicationBuilder app,
            string bannerMessage)
        {
            // The message
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(bannerMessage);
            Console.ResetColor();

            // Server Urls
            var builder = app.ApplicationServices.GetService<IWebHostBuilder>();
            if (builder != null)
            {
                var urls = builder
                    .GetSetting(WebHostDefaults.ServerUrlsKey)
                    ?.Replace(";", " ");
                Console.Write($"    Urls: ");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"{urls}", ConsoleColor.DarkCyan);
                Console.ResetColor();
            }


            Console.WriteLine($"    Runtime: {RuntimeInformation.FrameworkDescription}");
            Console.WriteLine($"   Platform: {RuntimeInformation.OSDescription}");

            // Environment Name
            var env = app.ApplicationServices.GetService<IHostEnvironment>();
            if (env != null)
                Console.WriteLine($"Environment: {env.EnvironmentName}");

            Console.WriteLine();

            return app;
        }
    }
}
