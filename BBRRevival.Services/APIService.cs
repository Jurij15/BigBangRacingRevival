﻿using BBRRevival.Services.Database;
using BBRRevival.Services.Events;
using BBRRevival.Services.Http;
using BBRRevival.Services.Managers;
using BBRRevival.Services.Routing;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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

        public EventHandler<NewRequestReceivedEventArgs> NewRequestReceived;

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

            router.NewRequestReceived += Router_NewRequestReceived;

            server.RequestReceived += Server_RequestReceived;
            server.Start();

            Log.Information("API Server Started");
        }

        private void Router_NewRequestReceived(object? sender, NewRequestReceivedEventArgs e)
        {
            NewRequestReceived?.Invoke(sender, e);
        }

        public void Shutdown()
        {
            Log.Warning("API Server is shutting down");
            server.Stop();
        }

        private void Server_RequestReceived(object? sender, HttpRequestReceivedEventArgs e)
        {
            Log.Verbose($"Request Received to address: {e.request.Url}");
            router.HandleRequest(e);
        }
    }
}
