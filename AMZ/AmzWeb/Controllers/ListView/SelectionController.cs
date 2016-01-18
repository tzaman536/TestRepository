using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Examples.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.IO;
using System.Reflection;

namespace Kendo.Mvc.Examples.Controllers
{
    public partial class ListViewController : Controller
    {
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ActionResult Selection()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            logger.Info("Hello there with out tool");
            logger.Info(string.Format("Assembly executing in {0}",directory.ToString()));
            var dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory");
            logger.Info(string.Format("DataDirectory is  {0}", dataDirectory.ToString()));

            return View(GetProducts());
        }        
    }
}