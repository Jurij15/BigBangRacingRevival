using BBRRevival.Services.Events;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.ControlPanel.Model.Messages
{
    public class RequestCacheMessage :RequestMessage<List<NewRequestReceivedEventArgs>>
    {
        public List<NewRequestReceivedEventArgs> cachedRequests;
    }
}
