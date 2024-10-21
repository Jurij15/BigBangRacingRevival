using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.ControlPanel.Model.Messages
{
    public class LogCacheMessage : RequestMessage<List<string>>
    {
        public List<string> Logs { get; set; }
    }
}
