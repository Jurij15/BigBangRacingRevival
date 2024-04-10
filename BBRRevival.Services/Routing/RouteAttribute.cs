namespace BBRRevival.Services.Routing;

public sealed class RouteAttribute : Attribute
{
    public RouteAttribute(string method, string route)
    {
        Method = method;
        Route = route;
    }

    internal RouteAttribute(string method, string route, bool isHidden)
        : this(method, route)
    {
        IsHidden = isHidden;
    }

    public string Method { get; }

    public string Route { get; }

    internal bool IsHidden { get; }
}