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
    }
}