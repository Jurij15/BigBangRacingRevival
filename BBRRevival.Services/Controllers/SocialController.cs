using BBRRevival.Services.API;
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

        [Route("GET", "/v1/player/get/social")]
        public async void GetPlayerSocialInfo()
        {
            byte[] data = null;

            Dictionary<string, object> dict = new Dictionary<string, object>();

            dict.Add("playerId", "12345678");
            dict.Add("developer", true);
            dict.Add("countryCode", "386");
            dict.Add("teamid", "0");
            dict.Add("teamName", "BigTeamNameIDK");
            //dict.Add("teamRole", Enums.TeamRole.Creator.ToString());
            dict.Add("hasJoinedTeam", true);
            dict.Add("youtubeName", "TempYtName");
            dict.Add("youtubeId", "ytid");

            //dict.Add("playerId", Guid.NewGuid().ToString());
            dict.Add("name", "Jurij15");
            dict.Add("tag", "JurijG");
            dict.Add("itemDbVersion", 0);
            dict.Add("AcceptNotifications", true);
            dict.Add("nameChangesDone", 0);
            dict.Add("level", 100);

            dict.Add("mcRank", 4000);
            dict.Add("carRank", 4000);

            dict.Add("coins", 1000000);
            dict.Add("diamonds", 1000000);
            dict.Add("copper", 1000000);
            dict.Add("shards", 1000000);

            dict.Add("xp", 100000);
            dict.Add("totalLikes", 100000);
            dict.Add("totalCoinsEarned", 100000);
            
            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dict));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, true);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
