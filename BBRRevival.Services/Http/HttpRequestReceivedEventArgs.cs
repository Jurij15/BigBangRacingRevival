using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Http
{
    public class HttpRequestReceivedEventArgs
    {
        public HttpRequestReceivedEventArgs(HttpListenerResponse resp, HttpListenerRequest req) 
        {
            response = resp;
            request = req;
        }

        public HttpListenerRequest request;
        public HttpListenerResponse response;
    }
}
