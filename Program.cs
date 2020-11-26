using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Motiv.Extensions;
using System.Threading.Tasks;

namespace Motiv
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddServices(builder.HostEnvironment.BaseAddress);

            await builder.Build().RunAsync();
        }
    }
}
