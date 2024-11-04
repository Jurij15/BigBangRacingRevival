using BBRRevival.Services.API;
using BBRRevival.Services.Helpers;
using BBRRevival.Services.Routing;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Controllers
{
    public class TeamController : Controller
    {
        //teams are unfinished
        [Route("GET", "/v1/team/suggest")]
        public async void GetTeams()
        {
            byte[] data = null;

            Dictionary<string, object> ParseTeamsList = new Dictionary<string, object>();
            ParseTeamsList.Add("data", new List<object>());

            List<object> Teamdata = ParseTeamsList["data"] as List<object>;
            //not finished yet.
            Dictionary<string, object> TeamDict = new Dictionary<string, object>();
            TeamDict.Add("id", "24567");
            TeamDict.Add("name", "BestTeamEver");
            TeamDict.Add("description", "We hate corrupt admins and we love active players!");
            TeamDict.Add("joinType", "Open");
            TeamDict.Add("requiredTrophies", 100);
            TeamDict.Add("memberList", new List<object>());
            TeamDict.Add("members", new List<object>());
            TeamDict.Add("memberCount", 1);

            Teamdata.Add(TeamDict);

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(ParseTeamsList));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }

        [Route("POST", "/v1/team/update")]
        public async void UpdateTeams()
        {
            byte[] data = null;
            //i dont know why i added this
            Dictionary<string, object> sys = new Dictionary<string, object>();
            sys.Add("joinType", "Open");
            sys.Add("Description", "Haha");
            sys.Add("requiredTrophies", 0);
            sys.Add("name", "Haha");

            data = Encoding.Default.GetBytes(JsonConvert.SerializeObject(sys));

            ResponseHelper.AddContentType(_response);
            ResponseHelper.AddResponseHeaders(data, RawUrl, _response, _request, false);

            await _response.OutputStream.WriteAsync(data, 0, data.Length);

            _response.Close();
        }
    }
}
