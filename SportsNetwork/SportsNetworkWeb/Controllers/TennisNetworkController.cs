using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsNetworkModel;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

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

            ViewData["LeagueTypes"] = LeagueTypes.GetAll();
            ViewData["LeagueLevels"] = LeagueLevels.GetAll();

            return View();
        }


        public ActionResult League_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(League.GetAll().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult League_Create([DataSourceRequest] DataSourceRequest request, League lg)
        {
            lg.AddUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            if (string.IsNullOrEmpty(lg.AddUserName))
            {
                lg.AddUserName = Environment.UserName;
            }

            League.Add(lg);
            return Json(new[] { lg }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult League_Update([DataSourceRequest] DataSourceRequest request, League lg)
        {
            lg.AddUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            if (string.IsNullOrEmpty(lg.AddUserName))
            {
                lg.AddUserName = Environment.UserName;
            }

            League.Update(lg);
            return Json(new[] { lg }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult League_Destroy([DataSourceRequest] DataSourceRequest request, League lg)
        {
            League.Delete(lg);
            return Json(new[] { lg }.ToDataSourceResult(request, ModelState));
        }



    }
}