using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class SocialController : Controller
    {
        [Route("GET", "/v2/player/friends")]
        public async void GetFriends()
        {
            byte[] data = null;

            Dictionary<string, object> friends = new Dictionary<string, object>();
            friends.Add("followees", new List<object>());
            friends.Add("friends", new List<object>());

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(friends));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
