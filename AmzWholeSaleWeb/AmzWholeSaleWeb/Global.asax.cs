using log4net;
using Simplex.Tools.AppSettings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AmzWholeSaleWeb
{

    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();

            string homeControllerName = AppSettingsHandler.GetAppSettingsValue("HomeController");
            logger.InfoFormat("Starting {0} application",homeControllerName);
            CleanupFiles();

        }

        void Session_Start(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Add("__MyAppSession", string.Empty);
        }

        public void CleanupFiles()
        {
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/UploadedImages");
                DirectoryInfo di = new DirectoryInfo(path);
                if (!di.Exists)
                {
                    di.Create();
                }
                var files = di.GetFiles();
                foreach(var f in files)
                {
                    FileInfo fi = new FileInfo(f.FullName);
                    try
                    {
                        fi.Delete();
                    }
                    catch(Exception ex)
                    {
                        logger.WarnFormat("Failed to delete file. Errro:{0}",ex.Message);
                    }
                }


            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }

        }
    }
}
