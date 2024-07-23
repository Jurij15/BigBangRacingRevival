using BBRRevival.Services.Database;
using BBRRevival.Services.Http;
using BBRRevival.Services.Managers;
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
        private SessionsManager sessionsManager { get; set; }
        private DatabaseManager dbManager { get; set; }

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
            App.PrepareInternalAppServices();
            CommonPaths.CreateRootDirectories();

            sessionsManager = new SessionsManager();
            dbManager = new DatabaseManager();

            router = new Router(config, sessionsManager, dbManager);
            server = new HttpServer(config.IP);

            server.RequestReceived += Server_RequestReceived;
            server.Start();

            Log.Information("API Server Started");
        }

        private void Server_RequestReceived(object? sender, HttpRequestReceivedEventArgs e)
        {
            Log.Verbose($"Request Received to address: {e.request.Url}");
            router.HandleRequest(e);
        }
    }
}
