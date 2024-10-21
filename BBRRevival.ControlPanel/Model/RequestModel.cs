using BBRRevival.Services.Events;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.ControlPanel.Model
{
    public class RequestModel
    {
        public string RequestMethod { get; set; }
        public List<string> RequestHeaders { get; set; } = new();
        public string RequestBody { get; set; }

        public string RequestRoute { get; set; }

        public string ContentType { get; set; }

        public bool Handled { get; set; }

        public string TimeStamp { get; set; }

        public RequestModel(NewRequestReceivedEventArgs args)
        {
            RequestMethod = args.RequestMethod;

            foreach (var item in args.RequestHeaders)
            {
                RequestHeaders.Add(item.ToString());
            }

            RequestBody = args.RequestBody;
            RequestRoute = args.RequestRoute;
            ContentType = args.ContentType;
            Handled = args.Handled;

            TimeStamp = DateTime.Now.ToString("yyyy:MM:dd, HH:mm");
        }
    }
}
