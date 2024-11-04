using BBRRevival.ControlPanel.Model.Messages;
using BBRRevival.ControlPanel.Services.Interfaces;
using BBRRevival.Services;
using BBRRevival.Services.Events;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;

namespace BBRRevival.ControlPanel.Services
{
    public class ServerService : IServerService
    {
        private int ServerProcessPort;

        CustomFormatProvider customFormatProvider { get; }
        CustomLogSink customSink { get; }

        private List<NewRequestReceivedEventArgs> _requestsCache { get; } = new();

        APIService api { get; set; }

        public ServerService()
        {
            customFormatProvider = new();
            customSink = new(customFormatProvider);

            Log.Logger = new LoggerConfiguration()
                  .MinimumLevel.Verbose()
                  .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Verbose)
                  .WriteTo.Sink(customSink)
                  .CreateLogger();

            WeakReferenceMessenger.Default.Register<ServerService, RequestCacheMessage>(this, (r, m) =>
            {
                m.Reply(_requestsCache);
            });
        }

        public async void Start()
        {
            //TODO: CHECK IF ANY PROCESSES ALREADY EXIST
            //MODIFY THIS IP OR ELSE THE UI WONT WORK
            //TODO_: get the host ip 
            int port = 4451;
            string ip = "192.168.1.7";

            string fullip = $"http://{ip}:{port}/";

            APIConfig config = new APIConfig();
            config.IP = fullip;

            Log.Information("Server logger initialized");
            Log.Information($"Server IP: {fullip}");

            api = new APIService(config, Log.Logger);

            api.NewRequestReceived += (sender, args) =>
            {
                _requestsCache.Add(args);
                WeakReferenceMessenger.Default.Send<NewRequestMessage>(new NewRequestMessage() { args = args });
            };
        }

        public void Stop()
        {
            api.Shutdown();

            Log.Write(LogEventLevel.Information, "SERVER STOPPED");
        }
    }
}
