using BBRRevival.Services.Managers;
using Serilog;
using System.Net;
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