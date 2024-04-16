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
    public class TokenController : Controller
    {
        [Route("POST", "/v1/push/token/save")]
        public async void SaveToken()
        {
            byte[] data = null;

            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("PLAY_HASH", "");
            dict.Add("PLAY_STATUS", "OK");

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dict));

            Log.Verbose("New Token?");
            Console.WriteLine(RequestBodyAsync().Result);

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
