using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimplexSysWeb.Controllers
{
    public class SimplexSysHomeController : Controller
    {
        // GET: SimplexSysHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ITInfrastructure()
        {
            return View();
        }

        public ActionResult SoftwareConsulting()
        {
            return View();
        }

        public ActionResult CustomSoftware()
        {
            return View();
        }

    }


}