using BBRRevival.Services.API;
using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class IndexController :Controller
    {
        [Route("GET", "/")]
        public async void Index()
        {
            byte[] data = null;

            data = Encoding.UTF8.GetBytes("");

            //ResponseHelper.PrepareRequest(data, RawUrl, _response, _request);
            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
