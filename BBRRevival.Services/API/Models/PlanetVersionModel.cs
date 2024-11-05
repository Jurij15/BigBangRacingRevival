using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.API.Models
{
    public class PlanetVersionModel : DictionaryModel
    {
        public string planet;
        public int version;

        public override string ToJson()
        {
            return this.ToJson();
        }
    }
}
