using BBRRevival.Services.Http;
using Serilog;
using System.Reflection;

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
    
    public Router(APIConfig config)
    {
        build();
        _apiConfig = config;
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
        Log.Verbose($"Methods Count: {controllerMethods.Count()}");

        Log.Verbose("Methods Found:");
        foreach (var method in controllerMethods)
        {
            RouteAttribute route = (RouteAttribute)method.GetCustomAttributes(typeof(RouteAttribute), false)[0];
            Log.Verbose($"{route.Route}, {method.Name}");

            _routes.Add(new Route(route, method));
        }
    }

    public void HandleRequest(HttpRequestReceivedEventArgs args)
    {
        foreach (var item in _routes)
        {
            if (item.Attribute.Method == args.request.HttpMethod && item.Attribute.Route == args.request.Url.AbsolutePath)
            {
                Controller controller = (Controller)Activator.CreateInstance(item.Method.DeclaringType);
                controller.Handle(item.Method, args.request, args.response, _apiConfig);
                break;
            }
        }
    }
}