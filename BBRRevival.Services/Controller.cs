using System.Net;

namespace BBRRevival.Services;

public class Controller
{
    protected HttpListenerRequest _request { get; set; }
    protected HttpListenerResponse _response { get; set; }

    protected string RawUrl { get; set; }
}