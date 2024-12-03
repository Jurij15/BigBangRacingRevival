using BBRRevival.Services.Managers;
using Serilog;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace BBRRevival.Services.API;

public class Controller
{
    protected HttpListenerRequest _request { get; set; }
    protected HttpListenerResponse _response { get; set; }

    protected APIConfig _config { get; set; }
    protected SessionsManager sessionsManager { get; set; }
    protected DatabaseManager databaseManager { get; set; }

    protected string RawUrl { get; set; }
    protected MemoryStream BodyMemoryStream;

    protected async Task<string> RequestBodyAsync()
    {
        Log.Debug("Parsing request body");
        //get request body
        BodyMemoryStream.Position = 0;
        using var reader = new StreamReader(BodyMemoryStream, Encoding.Default, true, 1024, true);
        string body = await reader.ReadToEndAsync();
        Log.Debug("Done parsing request body");
        return body;
    }

    protected async Task<byte[]> RequestBodyBytesAsync()
    {
        //TODO: no real need to copy this to so many arrays here, refactor ASAP
        Log.Debug("Parsing request body as bytes");
        BodyMemoryStream.Position = 0;
        using var memoryStream = new MemoryStream();
        await _request.InputStream.CopyToAsync(memoryStream);
        Log.Debug("Done parsing request body as bytes");
        return BodyMemoryStream.ToArray();
    }

    public void Handle(MethodInfo method, HttpListenerRequest request, HttpListenerResponse response, APIConfig config, MemoryStream bodyBytes,SessionsManager sessionsmanager,
        DatabaseManager dbManager)
    {
        Log.Verbose($"Handling request: {method.Name}");
        _request = request;
        _response = response;
        _config = config;
        BodyMemoryStream = bodyBytes;
        sessionsManager = sessionsmanager;
        databaseManager = dbManager;

        method.Invoke(this, null);
    }
}