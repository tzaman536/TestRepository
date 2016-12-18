using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsNetworkModel
{
    public class SportsNetwork
    {
        public static string DefaultConnectionString;
        static SportsNetwork()
        {
            DefaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

    }
}
