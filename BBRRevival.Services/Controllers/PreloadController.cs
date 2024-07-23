using System.Text;
using BBRRevival.Services.API;
using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using Serilog;

namespace BBRRevival.Services.Controllers;

public class PreloadController : Controller
{
    [Route("GET", "/v1/preload/checkVersion")]
    public async void CheckVersion()
    {
        byte[] data = null;
        Dictionary<string, object> Version = new Dictionary<string, object>();

        //TODO: check client version here

        Version.Add("version", "upToDate");
        data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(Version));

        ResponseHelper.AddContentType(_response);
        ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

        await _response.OutputStream.WriteAsync(data);
        
        _response.Close();
    }

    [Route("GET", "/v1/preload/checkFile")]
    public async void CheckFile()
    {
        Log.Verbose("Received CheckFile request");
        byte[] data = null;
        Dictionary<string, object> File = new Dictionary<string, object>();

        try
        {
            string name = _request.Url.Query.Split("&")[1].Remove(0, 5);
            File.Add("PLAY_STATUS", "OK");
            File.Add("name", name);
            File.Add("type", "idkWhatToPutHere");
            Log.Warning("HARDCODED URL AT PreloadController.cs at line 44");
            //File.Add("path", $"{_config.IP}downloadFile?{name}"); //this is the adress to download the music bank, it can be anything
            File.Add("path", $"http://192.168.1.7:4451/downloadFile?{name}"); //this is the adress to download the music bank, it can be anything
            File.Add("version", "0"); //maybe?
        }
        catch (Exception ex)
        {
            Log.Error($"{ex}, query was probably null");
            _response.Close();
            return;
        }

        data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(File));

        ResponseHelper.AddContentType(_response);
        ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

        await _response.OutputStream.WriteAsync(data);

        _response.Close();
    }
}