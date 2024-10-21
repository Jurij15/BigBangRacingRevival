using BBRRevival.Services.API;
using BBRRevival.Services.API.Models;
using BBRRevival.Services.Helpers;
using BBRRevival.Services.Internal.Services.Interfaces;
using BBRRevival.Services.Routing;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

            Dictionary<string, object> testmap = new Dictionary<string, object>();
            testmap.Add("id", "test12345");
            testmap.Add("creatorId", "123456789");
            testmap.Add("name", "Test BBR Level");
            testmap.Add("state", "saved");

            List<object> maps = new List<object>();
            maps.Add(map);
            maps.Add(testmap);

            Dictionary<string, object> minigames = new Dictionary<string, object>();
            minigames.Add("publishedMinigameCount", 1);
            minigames.Add("followerCount", 0);
            minigames.Add("totalCoinsEarned", 10000);
            minigames.Add("totalLikes", 10000000);
            minigames.Add("totalSuperLikes", 10000000);
            minigames.Add("likesSeen", 10);
            minigames.Add("data", maps);

            data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(minigames));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v2/minigame/save")]
        public async void SaveMinigame()
        {
            IMinigameService minigameService = App.Services.GetService<IMinigameService>();

            byte[] data = null;

            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("id", "1234");
            map.Add("name", "testname");

            List<object> maps = new List<object>();
            maps.Add(map);

            data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(map));

            //Console.WriteLine(this.RequestBodyAsync().Result);

            var level = this.RequestBodyBytesAsync().Result;
            var fileSizes = _request.Headers["FILE_SIZES"].ToString();

            await minigameService.SaveMinigame(level, fileSizes);

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
        
        [Route("POST", "/v4/minigame/publish")]
        public async void PublishMinigame()
        {
            IMinigameService minigameService = App.Services.GetService<IMinigameService>();

            byte[] data = null;

            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("id", "1234");
            map.Add("name", "testname");

            List<object> maps = new List<object>();
            maps.Add(map);

            data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(map));

            //Console.WriteLine(this.RequestBodyAsync().Result);

            var level = this.RequestBodyBytesAsync().Result;
            var fileSizes = _request.Headers["FILE_SIZES"].ToString();

            await minigameService.SaveMinigame(level, fileSizes);

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/minigame/meta/find")]
        public async void FindMinigame()
        {
            IMinigameService minigameService = App.Services.GetService<IMinigameService>();
            byte[] data = null;

            var levelId = _request.QueryString["id"];
            //var levelId = "f6f765c6fc064338b4d28560eac2ccbf-workingPub";
            var meta = await minigameService.GetMinigameMetadata(levelId);

            var json = JsonConvert.SerializeObject(meta);
            Console.WriteLine(json);

            //TODO: MOVE THIS WEIRD CONVERSION TO ANOTHER FILE
            Dictionary<string ,object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            Console.WriteLine(JsonConvert.SerializeObject(dict));

            data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dict));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/minigame/data/find")]
        public async void DownloadMinigame()
        {
            byte[] data = null;

            var levelId = _request.QueryString["id"];

            Log.Verbose(levelId);

            data = File.ReadAllBytes(Path.Combine(CommonPaths.MinigamesRootPath, "f6f765c6fc064338b4d28560eac2ccbf-workingPub", "level"));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, false);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        //TODO: maybe put this into its own file
        [Route("GET", "/v1/ghost/bossbattle/get")]
        public async void GhostMinigame()
        {
            byte[] data = null;

            Dictionary<string, object> tosend = new Dictionary<string, object>();
            tosend.Add("playerId", "5425243");
            tosend.Add("name", "TestGhostName");

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(tosend));

            Console.WriteLine(this.RequestBodyAsync().Result);

            _response.Headers.Add("FILE_SIZES", "45");

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, false);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        //TODO: maybe put this into its own file
        [Route("GET", "/v1/trophy/ghostsByTrophies")]
        public async void GetGhostsByTrophies()
        {
            byte[] data = null;

            Dictionary<string, object> tosend = new Dictionary<string, object>();
            tosend.Add("playerId", "5425243");
            tosend.Add("name", "TestGhostName");

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(tosend));

            Console.WriteLine(this.RequestBodyAsync().Result);

            _response.Headers.Add("FILE_SIZES", "45");

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, false);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v1/minigame/backtosaved")]
        public async void MinigameBackToSaved()
        {
            byte[] data = null;

            Dictionary<string, object> tosend = new Dictionary<string, object>();

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(tosend));

            Console.WriteLine(this.RequestBodyAsync().Result);

            _response.Headers.Add("FILE_SIZES", "0");

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, false);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
