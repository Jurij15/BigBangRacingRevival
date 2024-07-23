using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services
{
    internal class CommonPaths
    {
        public static readonly string BasePath = "ServerSavedData";
        public static readonly string MinigamesRootPath = Path.Combine(BasePath, "Minigames");

        public static void CreateRootDirectories()
        {
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }

            if (!Directory.Exists(MinigamesRootPath))
            {
                Directory.CreateDirectory(MinigamesRootPath);
            }
        }
    }
}
