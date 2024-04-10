using BBRRevival.Services.Http;
using BBRRevival.Services.Routing;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services
{
    public class APIService
    {
        private HttpServer? server { get; set; }
        private APIConfig? config { get; set; }
        private Router? router { get; set; }

        public APIService(APIConfig conf, ILogger Logger, bool AutoInit = true)
        {
            config = conf;
            Log.Logger = Logger;

            if (AutoInit)
            {
                Initialize();
            }
        }

        public void Initialize()
        {
            router = new Router(config);
            server = new HttpServer(config.IP);

            server.RequestReceived += Server_RequestReceived;
            server.Start();

            Log.Information("API Started");
        }

        private void Server_RequestReceived(object? sender, HttpRequestReceivedEventArgs e)
        {
            Log.Verbose($"Request Received to address: {e.request.Url}");
            router.HandleRequest(e);
        }
    }
}
