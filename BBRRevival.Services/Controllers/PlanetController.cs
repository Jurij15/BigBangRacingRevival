using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class PlanetController : Controller
    {
        [Route("GET", "/v1/path/db/find")]
        public async void FindPath()
        {
            byte[] data = null;

            Dictionary<string, object> datas2 = new Dictionary<string, object>();
            datas2.Add("nodeType", "9");//undefined
            datas2.Add("itemName", "9");//undefined
            datas2.Add("itemIcon", "9");//undefined
            datas2.Add("itemDescription", "9");//undefined

            Dictionary<string, object> datas = new Dictionary<string, object>();
            datas.Add("id", 0);//undefined
            datas.Add("name", "RacingMotorcycle");//undefined
            datas.Add("x", 24);//undefined
            datas.Add("y", 24);//undefined
            datas.Add("data", datas2);

            Dictionary<string, object> datadict = new Dictionary<string, object>();
            datadict.Add("nodes", new List<object>() { datas});

            string json = JsonConvert.SerializeObject(datadict, Formatting.None);
            //Console.WriteLine(json);

            //string cleanJson  = Regex.Replace(Encoding.Default.GetString(bytes), @"[\x00-\x1F\x7F]", ""); //string pattern = @"[\x00-\x1F\x7F]";

            byte[] bytes = FilePacker.ZipBytes(Encoding.UTF8.GetBytes(json));

            data = bytes;

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, false);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
