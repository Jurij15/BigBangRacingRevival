using BBRRevival.Services.API;
using BBRRevival.Services.Events;
using BBRRevival.Services.Http;
using BBRRevival.Services.Managers;
using Serilog;
using System.Reflection;
using System.Text;

namespace BBRRevival.Services.Routing;

public class Router
{
    public class Route
    {
        public RouteAttribute Attribute { get; set; }
        public MethodInfo Method { get; set; }

        public Route(RouteAttribute attribute, MethodInfo info)
        {
            Attribute = attribute;
            Method = info;
        }
    }



    private List<Route> _routes;

    private APIConfig _apiConfig;
    private SessionsManager sessionsManager;
    private DatabaseManager databaseManager;

    public event EventHandler<NewRequestReceivedEventArgs> NewRequestReceived;

    public Router(APIConfig config, SessionsManager sessionsmanager, DatabaseManager dbManager)
    {
        build();
        _apiConfig = config;
        sessionsManager = sessionsmanager;
        databaseManager = dbManager;
    }

    private void build()
    {
        Log.Information("Building server...");
        _routes = new List<Route>();

        var controllerMethods = from a in AppDomain.CurrentDomain.GetAssemblies()
                                from t in a.GetTypes()
                                from m in t.GetMethods()
                                where m.GetCustomAttributes(typeof(RouteAttribute), false).Length > 0
                                select m;

        if (LogOptions.logRequestsBuilder)
        {
            Log.Verbose($"Methods Count: {controllerMethods.Count()}");   
            Log.Verbose("Methods Found:");
        }
        
        foreach (var method in controllerMethods)
        {
            RouteAttribute route = (RouteAttribute)method.GetCustomAttributes(typeof(RouteAttribute), false)[0];
            
            if (LogOptions.logRequestsBuilder)
            {
                Log.Verbose($"{route.Route}, {method.Name}");
            }

            _routes.Add(new Route(route, method));
        }
    }

    public async void HandleRequest(HttpRequestReceivedEventArgs args)
    {
        bool requestHandled = false;

        //get event data before object is disposed
        //TODO: MOVE THIS TO THE CONTROLLER, NO NEED TO DO IT THERE TOO
        using var memoryStream = new MemoryStream();
        await args.request.InputStream.CopyToAsync(memoryStream);
        using var reader = new StreamReader(memoryStream, Encoding.UTF8, true, 1024, true);
        memoryStream.Position = 0;
        string body = await reader.ReadToEndAsync();

        NewRequestReceivedEventArgs eventArgs = new();
        eventArgs.RequestMethod = args.request.HttpMethod;
        eventArgs.RequestBody = body;
        eventArgs.RequestHeaders = args.request.Headers;
        eventArgs.RequestRoute = args.request.Url.AbsolutePath;
        eventArgs.ContentType = args.request.ContentType;
        eventArgs.RequestQuery = args.request.Url.Query;

        foreach (var item in _routes)
        {
            if (item.Attribute.Method == args.request.HttpMethod && item.Attribute.Route == args.request.Url.AbsolutePath)
            {
                Controller controller = (Controller)Activator.CreateInstance(item.Method.DeclaringType);
                controller.Handle(item.Method, args.request, args.response, _apiConfig, memoryStream,sessionsManager, databaseManager);
                requestHandled = true;
                break;
            }
        }

        if (!requestHandled)
        {
            Log.Warning($"Request to {args.request.Url.AbsolutePath} from {args.request.RemoteEndPoint.ToString()} with method {args.request.HttpMethod} was not handled!");
        }

        //fire the event
        eventArgs.Handled = requestHandled;

        NewRequestReceived?.Invoke(null, eventArgs);
    }
}