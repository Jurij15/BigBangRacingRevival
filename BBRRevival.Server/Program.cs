using BBRRevival.Services;
using Serilog.Events;
using Serilog;
using System.Net;

namespace BBRRevival.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //get the host ip
            int port = 4451;
            string ip = "192.168.1.7";

            foreach (string arg in args)
            {
                if (arg.Contains("-port="))
                {
                    port = Convert.ToInt32(arg.Replace("-port=", ""));
                }
            }

            string fullip = $"http://{ip}:{port}/";

            APIConfig config  = new APIConfig();
            config.IP = fullip;

            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Verbose()
                  .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose)
                  .CreateLogger();

            Log.Information("Server logger initialized");
            Log.Information($"Server IP: {fullip}");

            APIService api = new APIService(config, Log.Logger);

            Log.Information("Press enter key to stop the server!");

            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {

            }
        }
    }
}
