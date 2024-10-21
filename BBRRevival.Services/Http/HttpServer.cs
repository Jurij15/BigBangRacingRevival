using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Http
{
    public class HttpServer
    {
        private HttpListener listener;
        public bool isDisposed = false;

        public string IP {  get; set; }

        public event EventHandler<HttpRequestReceivedEventArgs> RequestReceived;

        public HttpServer(string ip)
        {
            listener = new HttpListener();
            listener.Prefixes.Add(ip);

            IP = ip;
        }

        public void Stop()
        {
            listener.Stop();
            Log.Verbose("HTTP Listener Stopped!");
        }

        public void Start()
        {
            listener.Start();
            Log.Verbose("HTTP Server Started!");

            Task.Run(AcceptConnections);
        }

        private async Task AcceptConnections()
        {
            while (listener.IsListening)
            {
                HttpListenerContext ctx = listener.GetContext();

                var task = Task.Run(() =>
                {
                    RequestReceived.Invoke(this, new HttpRequestReceivedEventArgs(ctx.Response, ctx.Request));
                });
            }
        }
    }
}
