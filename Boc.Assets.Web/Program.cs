
using Boc.Assets.Application.ServiceInterfaces;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Boc.Assets.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            await LoadPermissions(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:5003");

        private static async Task LoadPermissions(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var memoryCache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();
                IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();
                var permissionList = await permissionService.GetAllListAsync();
                memoryCache.Set("permissions", permissionList);
            }
        }
    }
}
