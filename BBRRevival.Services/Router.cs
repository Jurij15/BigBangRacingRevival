using System.Reflection;

namespace BBRRevival.Services;

public class Router
{
    public class Route
    {
        public RouteAttribute Attribute { get; set; }
        public MethodInfo Method { get; set; }

        public void InvokeMethod()
        {
            
        }
    }

    private List<Route> _routes;

    public Router()
    {
        build();
    }

    private void build()
    {
        var controllerMethods = from a in AppDomain.CurrentDomain.GetAssemblies()
            from t in a.GetTypes()
            from m in t.GetMethods()
            where m.GetCustomAttributes(typeof(RouteAttribute), false).Length > 0
            select m;

        foreach (var method in controllerMethods)
        {
            Console.WriteLine(method.GetCustomAttributes());
        }
    }
}