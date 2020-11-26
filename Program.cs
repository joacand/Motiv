using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Motiv.Extensions;
using Serilog;
using Serilog.Debugging;
using System;
using System.Threading.Tasks;

namespace Motiv
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            ConfigureLogging();

            try
            {
                var builder = WebAssemblyHostBuilder.CreateDefault(args);
                builder.RootComponents.Add<App>("#app");

                builder.Services.AddServices(builder.HostEnvironment.BaseAddress);

                await builder.Build().RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Exception during main executiong");
                throw;
            }
        }

        private static void ConfigureLogging()
        {
            SelfLog.Enable(message => Console.Error.WriteLine(message));

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.BrowserConsole()
                .CreateLogger();
        }
    }
}
