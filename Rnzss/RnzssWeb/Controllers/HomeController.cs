using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimplexInvoiceWeb.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult Naval()
        {
            ViewBag.Message = "Naval.";

            return View();
        }

        public ActionResult ArmoredVehicles()
        {
            ViewBag.Message = "Armored Vehicles";

            return View();
        }

        public ActionResult AeroSpace()
        {
            ViewBag.Message = "Aero Space";

            return View();
        }

        public ActionResult Expertise()
        {
            ViewBag.Message = "Extertise";

            return View();
        }

        public ActionResult Ourfocus()
        {
            ViewBag.Message = "Out Focus";

            return View();
        }

    }
}