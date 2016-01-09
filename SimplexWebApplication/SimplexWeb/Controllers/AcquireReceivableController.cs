using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace EZFactor1.Controllers
{
    public class AcquireReceivableController : Controller
    {
        //
        // GET: /AcquireReceivable/

        public ActionResult Index()
        {
            if (!WebSecurity.IsAuthenticated){Response.Redirect(Url.Action("LogIn", "Account"));}
            return View();
        }

    }
}
