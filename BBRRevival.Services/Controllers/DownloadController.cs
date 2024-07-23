using BBRRevival.Services.API;
using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class DownloadController : Controller
    {
        [Route("GET", "/downloadFile")]
        public async void DownloadFile()
        {
            Log.Verbose("Received a DownloadFile request!");
            byte[] data = null;

            data = File.ReadAllBytes("Assets/Music/" + _request.Url.Query.Replace("?", "") + ".bank");

            ResponseHelper.AddContentType(_response);
            //ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
