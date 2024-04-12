using System.Net;
using System.Text;

namespace BBRRevival.Services.Helpers;

public class ResponseHelper
{
    public static HttpListenerResponse AddContentType(HttpListenerResponse response)
    {
        response.ContentType = "application/octet-stream";
        return response;
    }

    public static HttpListenerResponse AddResponseHeaders(byte[] data, string rawUrl, HttpListenerResponse response,
        HttpListenerRequest request)
    {
        response.AddHeader("PLAY_STATUS", "OK"); //TODO: implement actuall status header
        response.AddHeader("PLAY_HASH", Encoding.UTF8.GetString(data)+request.RawUrl + "bfid3Z53SFib325PJGFasae"); //server encryption key

        return response;
    }

    public static void PrepareRequest(byte[] data, string rawUrl, HttpListenerResponse response, HttpListenerRequest request)
    {
        AddContentType(response);
        AddResponseHeaders(data, rawUrl, response, request);
    }
}