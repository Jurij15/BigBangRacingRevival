using BBRRevival.Services.Routing;
using Serilog.Events;
using Serilog;
using BBRRevival.Services;

namespace BBRRevival.Tester;

class Program
{
    static void Main(string[] args)
    {
       var unzippedbytes = FilePacker.UnZipBytes(File.ReadAllBytes("levelTest.txt"));

        Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose)
                .CreateLogger();
        
        APIConfig config = new APIConfig();
        APIService api = new APIService(config, Log.Logger);

        Log.Information("Press any key to stop the server!");

        Console.ReadKey();
    }
}