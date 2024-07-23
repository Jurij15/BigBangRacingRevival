using BBRRevival.Services.Internal.Services;
using BBRRevival.Services.Internal.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services
{
    public class App
    {
        public static IServiceProvider Services { get; set; }

        public static void PrepareInternalAppServices()
        {
            Log.Verbose("Starting Internal API Services");

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IMinigameService, MinigameService>();

            Services = serviceCollection.BuildServiceProvider();
        }
    }
}
