using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class LoginController : Controller
    {
        [Route("GET", "/v4/player/login?lastPathSync")]
        public async void CheckVersion()
        {
            byte[] data = null;

            string Query = _request.Url.Query;
            string param = Query.Split("&")[0];
            int value = Convert.ToInt32(param.Split("=")[1]);

            string sessionId = sessionsManager.AddNewSessionId();

            Dictionary<string, object> dict = new Dictionary<string, object>();

            if (value == 0)
            {
                Console.WriteLine("New User Created?");


            }

            //send the request
            ResponseHelper.PrepareRequest(data, RawUrl, _response, _request);
            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
