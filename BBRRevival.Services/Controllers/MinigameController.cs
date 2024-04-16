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
    public class MinigameController : Controller
    {
        [Route("GET", "/v2/minigame/own")]
        public async void GetOwnMinigames()
        {
            byte[] data = null;

            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("id", "1234");
            map.Add("name", "testname");
            
            List<object> maps = new List<object>();
            maps.Add(map);

            Dictionary<string, object> minigames = new Dictionary<string, object>();
            minigames.Add("publishedMinigameCount", 0);
            minigames.Add("followerCount", 0);
            minigames.Add("totalCoinsEarned", 0);
            minigames.Add("totalLikes", 0);
            minigames.Add("totalSuperLikes", 0);
            minigames.Add("likesSeen", 0);
            minigames.Add("data", maps);

            data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(minigames));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
