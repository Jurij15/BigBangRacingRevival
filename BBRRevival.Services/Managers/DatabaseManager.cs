using BBRRevival.Services.Database;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBRRevival.Services.Managers
{
    public class DatabaseManager : DatabaseInteractor
    {
        public DatabaseManager() : base()
        {
            Log.Verbose("Initialized DatabaseManager!");
        }


    }
}
