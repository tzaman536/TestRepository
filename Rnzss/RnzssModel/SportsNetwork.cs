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
        public static string SupportEmail;
        static SportsNetwork()
        {
            DefaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SupportEmail = ConfigurationManager.AppSettings["SupportEmailAddress"];  
        }

    }
}
