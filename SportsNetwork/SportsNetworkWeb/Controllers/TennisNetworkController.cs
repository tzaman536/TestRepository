using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsNetworkModel;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using PhenixTools.Mail;

namespace SportsNetworkWeb.Controllers
{
    public class TennisNetworkController : Controller
    {

        // GET: TennisNetwork
        public ActionResult Index()
        {
            return View();
        }

        #region Create League
        public ActionResult CreateLeague()
        {

            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            ViewData["LeagueTypes"] = LeagueTypes.GetAll();
            ViewData["LeagueLevels"] = LeagueLevels.GetAll();

            return View();
        }

        public ActionResult League_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            return Json(League.GetAll(User.Identity.Name).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult League_Create([DataSourceRequest] DataSourceRequest request, League lg)
        {

            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            lg.AddUserName = User.Identity.Name;
            
            if (string.IsNullOrEmpty(lg.AddUserName))
            {
                lg.AddUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            }

            League.Add(lg);
            return Json(new[] { lg }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult League_Update([DataSourceRequest] DataSourceRequest request, League lg)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            lg.AddUserName = User.Identity.Name;
            if (string.IsNullOrEmpty(lg.AddUserName))
            {
                lg.AddUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            }

            League.Update(lg);
            return Json(new[] { lg }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult League_Destroy([DataSourceRequest] DataSourceRequest request, League lg)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            League.Delete(lg);
            return Json(new[] { lg }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region Create Player
        public ActionResult CreatePlayer()
        {

            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }

            //ViewData["LeagueTypes"] = LeagueTypes.GetAll();
            //ViewData["LeagueLevels"] = LeagueLevels.GetAll();

            return View();
        }

        public ActionResult Player_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            return Json(Player.GetAll(User.Identity.Name).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Player_Create([DataSourceRequest] DataSourceRequest request, Player o)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            o.AddUserName = User.Identity.Name;
            if (string.IsNullOrEmpty(o.AddUserName))
            {
                o.AddUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            }

            Player.Add(o);
            return Json(new[] { o }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Player_Update([DataSourceRequest] DataSourceRequest request, Player o)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            o.AddUserName = User.Identity.Name;
            if (string.IsNullOrEmpty(o.AddUserName))
            {
                o.AddUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            }

            Player.Update(o);
            return Json(new[] { o }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Player_Destroy([DataSourceRequest] DataSourceRequest request, Player o)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            Player.Delete(o);
            return Json(new[] { o }.ToDataSourceResult(request, ModelState));
        }
        #endregion


    }
}