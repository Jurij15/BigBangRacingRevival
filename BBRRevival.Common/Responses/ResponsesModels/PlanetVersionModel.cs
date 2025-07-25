using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Common.Responses.ResponsesModels
{
    public class PlanetVersion
    {
        public PlanetVersion(string name, int version)
        {
            planet = name;
            this.version = version;
        }

        public PlanetVersion() { }

        public string planet { get; set; }
        public int version { get; set; }
    }
}
