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

            Dictionary<string, object> testmap = new Dictionary<string, object>();
            testmap.Add("id", "test12345");
            testmap.Add("name", "Test BBR Level");

            List<object> maps = new List<object>();
            maps.Add(map);
            maps.Add(testmap);

            Dictionary<string, object> minigames = new Dictionary<string, object>();
            minigames.Add("publishedMinigameCount", 1);
            minigames.Add("followerCount", 0);
            minigames.Add("totalCoinsEarned", 1000);
            minigames.Add("totalLikes", 10000);
            minigames.Add("totalSuperLikes", 10000);
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
            minigames.Add("published", false);
            minigames.Add("data", maps);

            data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(map));

            Console.WriteLine(this.RequestBodyAsync().Result);
            string level = this.RequestBodyAsync().Result;
            using (StreamWriter sw = File.CreateText("testSave" + new Random().Next().ToString()))
            {
                sw.Write(FilePacker.UnZipBytes(Encoding.Default.GetBytes(level)));
            };

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/minigame/meta/find")]
        public async void FindMinigame()
        {
            byte[] data = null;

            Dictionary<string, object> map = new Dictionary<string, object>();
            map.Add("id", "1234");
            map.Add("name", "testname");

            List<object> maps = new List<object>();
            maps.Add(map);

            data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(map));

            Console.WriteLine(this.RequestBodyAsync().Result);

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("GET", "/v1/minigame/data/find")]
        public async void DownloadMinigame()
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
            minigames.Add("published", false);
            minigames.Add("data", maps);

            data = File.ReadAllBytes("MyLevel");

            Console.WriteLine(this.RequestBodyAsync().Result);

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
