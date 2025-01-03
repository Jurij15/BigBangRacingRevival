using BBRRevival.Services.API;
using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class GhostController : Controller
    {
        [Route("GET", "/v1/ghost/creator/get")]
        public async void GetGhostsForCreator()
        {
            byte[] data = null;
            Dictionary<string, object> ghosts = new Dictionary<string, object>(); //dont know how this works yet

            data = Encoding.ASCII.GetBytes("");

            _response.Headers.Add("GHOST_TIME", "12");
            _response.Headers.Add("GHOST_NAME", "idk");

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data);

            _response.Close();
        }
    }
}
