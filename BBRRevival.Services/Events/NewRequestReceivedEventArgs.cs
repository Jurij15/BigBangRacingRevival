using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Events
{
    public class NewRequestReceivedEventArgs
    {
        public string RequestMethod;
        public NameValueCollection RequestHeaders;
        public string RequestBody;
        public string RequestQuery;

        public string RequestRoute;

        public string ContentType;

        public bool Handled;
    }
}
