using BBRRevival.Services.Managers;
using Serilog;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace BBRRevival.Services;

public class Controller
{
    protected HttpListenerRequest _request { get; set; }
    protected HttpListenerResponse _response { get; set; }

    protected APIConfig _config { get; set; }
    protected SessionsManager sessionsManager { get; set; }
    protected DatabaseManager databaseManager { get; set; }

    protected string RawUrl { get; set; }

    protected async Task<string> RequestBodyAsync()
    {
        Log.Debug("Parsing request body");
        //get request body
        using var reader = new StreamReader(_request.InputStream);
        string body = await reader.ReadToEndAsync();
        Log.Debug("Done parsing request body");
        return body;
    }

    public void Handle(MethodInfo method, HttpListenerRequest request, HttpListenerResponse response, APIConfig config, SessionsManager sessionsmanager,
        DatabaseManager dbManager)
    {
        Log.Verbose($"Handling request: {method.Name}");
        _request = request;
        _response = response;
        _config = config;
        sessionsManager = sessionsmanager;
        databaseManager = dbManager;

        method.Invoke(this, null);
    }
}