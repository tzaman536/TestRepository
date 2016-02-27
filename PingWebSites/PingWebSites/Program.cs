using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PingWebSites
{

    class Program
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();
            logger.Info("Starting PingWebSites");


            string[] urlList = new string[] { "http://www.amzwholesale.com" };
            foreach (var url in urlList)
            {

                logger.InfoFormat("Pinging {0}", url);
                try
                {
                    Stopwatch stopWatch = new Stopwatch();
                    stopWatch.Start();
                    // Get the elapsed time as a TimeSpan value.
                    HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                    //optional
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    Stream stream = response.GetResponseStream();
                    stopWatch.Stop();
                    TimeSpan ts = stopWatch.Elapsed;
                    logger.InfoFormat("Website respnded in {0}ms", ts.Milliseconds);
                }
                catch (Exception ex)
                {
                    logger.Fatal(ex);
                }
            }
            logger.Info("Ending PingWebSites");
        }
    }
}
