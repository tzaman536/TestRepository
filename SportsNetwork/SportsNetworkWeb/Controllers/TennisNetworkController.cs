using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsNetworkWeb.Controllers
{
    public class TennisNetworkController : Controller
    {
        // GET: TennisNetwork
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateLeague()
        {
            return View();
        }

    }
}