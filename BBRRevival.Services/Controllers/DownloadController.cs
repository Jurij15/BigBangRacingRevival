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
        [Route("GET", "downloadFile")]
        public async void CheckVersion()
        {
            byte[] data = null;
            Dictionary<string, object> Version = new Dictionary<string, object>();

            //TODO: check client version here

            data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(""));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
