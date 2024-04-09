using System.Text;
using BBRRevival.Services.Helpers;
using Newtonsoft.Json;
using Serilog;

namespace BBRRevival.Services.Controllers;

public class PreloadController : Controller
{
    [Route("GET", "/v1/preload/checkVersion")]
    public async void CheckVersion()
    {
        Log.Information("Received GetVersion request");
        byte[] data = null;
        Dictionary<string, object> dict = new Dictionary<string, object>();

        //TODO: check client version here

        dict.Add("version", "upToDate");
        data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(dict));

        ResponseHelper.AddContentType(_response);
        ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

        await _response.OutputStream.WriteAsync(data);
        
        _response.Close();
    }
}