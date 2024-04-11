using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Managers
{
    public class SessionsManager
    {
        private List<string> SessionIds { get; set; }

        public SessionsManager()
        {
            SessionIds = new List<string>();
        }

        public string AddNewSessionId()
        {
            string id = Guid.NewGuid().ToString();
            SessionIds.Add(id);

            return id;
        }
    }
}
