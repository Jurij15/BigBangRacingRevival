using Serilog;
using System.Net;
using System.Reflection;

namespace BBRRevival.Services;

public class Controller
{
    protected HttpListenerRequest _request { get; set; }
    protected HttpListenerResponse _response { get; set; }

    protected APIConfig _config { get; set; }

    protected string RawUrl { get; set; }

    public void Handle(MethodInfo method, HttpListenerRequest request, HttpListenerResponse response, APIConfig config)
    {
        Log.Verbose($"Handling request: {method.Name}");
        _request = request;
        _response = response;
        _config = config;

        method.Invoke(this, null);
    }
}