using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Common.Responses.Preload
{
    public class CheckFileResponse
    {
        public string name { get; set; }
        public string type { get; set; }
        public string path { get; set; }
        public int version { get; set; }
    }
}
