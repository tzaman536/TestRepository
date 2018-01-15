using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsNetworkModel;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using PhenixTools.Mail;
using System.Web.Script.Serialization;

namespace RnzssWeb.Controllers
{
    public class Message
    {
        public string AlertMessage { get; set; }
    }

    public class CurrencyUser
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool PlayWithAnyone { get; set; }
        public string PlayerLevel { get; set; }

    }
    public class TennisNetworkController : Controller
    {

        // GET: TennisNetwork
        public ActionResult Index()
        {
            return View();
        }

        #region Reference Data
        [HttpPost]
        public ActionResult GetCurrencyUserInformation([DataSourceRequest]DataSourceRequest request)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var json_serializer = new JavaScriptSerializer();

            var cu = new CurrencyUser() { Email = User.Identity.Name };
            var p = Player.GetPlayer(User.Identity.Name).FirstOrDefault();
            if (p != null)
            {
                cu.Name = p.Name;
                cu.Email = p.Email;
                cu.Phone = p.Phone;
                cu.PlayWithAnyone = p.PlayWithAnyone;
                cu.PlayerLevel = p.PlayerLevel;
            }
            return Json(new { success = true, message = cu }, JsonRequestBehavior.AllowGet);
        }

        #endregion


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

        public ActionResult GetLeagues()
        {

            var result = League.GetAll(User.Identity.Name).ToList();



            return Json(result, JsonRequestBehavior.AllowGet);
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
            return Json(Player.GetAll(User.Identity.Name).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
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

        #region Add Player to League
        public ActionResult AddPlayerToLeague()
        {

            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }

            //ViewData["LeagueTypes"] = LeagueTypes.GetAll();
            //ViewData["LeagueLevels"] = LeagueLevels.GetAll();

            return View();
        }

        public ActionResult PlayerInLeague_Read([DataSourceRequest] DataSourceRequest request, string inputLeagueName)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            return Json(League.GetAllPlayersInLeague(User.Identity.Name, inputLeagueName).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        public ActionResult PlayerNotInLeague_Read([DataSourceRequest] DataSourceRequest request, string inputLeagueName)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            return Json(League.GetAllPlayersNotInLeague(User.Identity.Name, inputLeagueName).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        public ActionResult AddPlayerToLeague_AddPlayer([DataSourceRequest] DataSourceRequest request, string inputLeagueName, int playerId)
        {
            string message = "Saved";

            if (inputLeagueName.Equals("UNSELECTED"))
            {
                message = "Please select a league";
                return Json(message.ToDataSourceResult(request));
            }

            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            League.AddPlayerToLeague(User.Identity.Name, inputLeagueName, playerId);


            return Json(message.ToDataSourceResult(request));
        }
        public ActionResult AddPlayerToLeague_RemovePlayer([DataSourceRequest] DataSourceRequest request, string inputLeagueName, int playerId)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            League.RemovePlayerFromLeague(User.Identity.Name, inputLeagueName, playerId);

            string message = "Saved";
            return Json(message.ToDataSourceResult(request));
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddPlayerToLeague_Create([DataSourceRequest] DataSourceRequest request, Player o)
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
        public ActionResult AddPlayerToLeague_Update([DataSourceRequest] DataSourceRequest request, Player o)
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
        public ActionResult AddPlayerToLeague_Destroy([DataSourceRequest] DataSourceRequest request, Player o)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            Player.Delete(o);
            return Json(new[] { o }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region Create Location
        public ActionResult CreateLocation()
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            return View();
        }

        public ActionResult Location_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            return Json(Location.GetMyLocations(User.Identity.Name).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Location_Create([DataSourceRequest] DataSourceRequest request, Location o)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            o.AddUserName = User.Identity.Name;
            if (string.IsNullOrEmpty(o.AddUserName))
            {
                o.AddUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            }

            Location.Add(o);
            return Json(new[] { o }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Location_Update([DataSourceRequest] DataSourceRequest request, Location o)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            o.AddUserName = User.Identity.Name;
            if (string.IsNullOrEmpty(o.AddUserName))
            {
                o.AddUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            }

            Location.Update(o);
            return Json(new[] { o }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Location_Destroy([DataSourceRequest] DataSourceRequest request, Location o)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            Location.Delete(o);
            return Json(new[] { o }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region Schedule Singles Game
        public ActionResult ScheduleSinglesGame()
        {

            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            ViewData["Teams"] = Team.GetSinglesTeams(User.Identity.Name);
            ViewData["Locations"] = Location.GetAllLocations(User.Identity.Name);

            return View();
        }

        public ActionResult Schedule_Read([DataSourceRequest] DataSourceRequest request, string inputLeagueName)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            return Json(Schedule.GetAll(User.Identity.Name,inputLeagueName).ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Schedule_Create([DataSourceRequest] DataSourceRequest request, Schedule o, string inputLeagueName)
        {

            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            o.AddUserName = User.Identity.Name;

            if (string.IsNullOrEmpty(o.AddUserName))
            {
                o.AddUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            }

            Schedule.Add(o);
            return Json(new[] { o }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Schedule_Update([DataSourceRequest] DataSourceRequest request, League lg, string inputLeagueName)
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
        public ActionResult Schedule_Destroy([DataSourceRequest] DataSourceRequest request, League lg)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            League.Delete(lg);
            return Json(new[] { lg }.ToDataSourceResult(request, ModelState));
        }


        #endregion

        #region LetsPlay
        public ActionResult LetsPlay()
        {

            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            ViewData["Teams"] = Team.GetSinglesTeams(User.Identity.Name);
            ViewData["Locations"] = Location.GetAllLocations(User.Identity.Name);

            return View();
        }

        public ActionResult LetsPlay_Read([DataSourceRequest] DataSourceRequest request, string inputLeagueName)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            return Json(Schedule.GetAll(User.Identity.Name, inputLeagueName).ToDataSourceResult(request));
        }

        public ActionResult AvailablePlayers_Read([DataSourceRequest] DataSourceRequest request)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            return Json(Player.GetPlayWithAnyonePlayers(User.Identity.Name).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        public ActionResult PlayersInRequestQueue_Read([DataSourceRequest] DataSourceRequest request, string inputLeagueName)
        {
            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }
            return Json(League.GetAllPlayersNotInLeague(User.Identity.Name, inputLeagueName).ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Add profile
        public ActionResult AddProfile()
        {

            if (!User.Identity.IsAuthenticated) { return RedirectToAction("Login", "Account"); }

            //ViewData["LeagueTypes"] = LeagueTypes.GetAll();
            //ViewData["LeagueLevels"] = LeagueLevels.GetAll();

            return View();
        }


        [HttpPost]
        public ActionResult AddMyProfile([DataSourceRequest]DataSourceRequest request, string inputPlayer)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            var json_serializer = new JavaScriptSerializer();
            Player p = json_serializer.Deserialize<Player>(inputPlayer);

            p.AddUserName = User.Identity.Name;
            if (string.IsNullOrEmpty(p.AddUserName))
            {
                p.AddUserName = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            }

            var playerExist = Player.GetPlayer(p.Email);
            if (playerExist.Any())
            {
                p.PlayerId = playerExist.FirstOrDefault().PlayerId;
                Player.Update(p);
            }
            else
            {
                Player.Add(p);
            }




            //decimal totalCharges = inputQuantity * inputWeight * 

            return Json(new { success = true, message = "Profile updated" }, JsonRequestBehavior.AllowGet);
        }


        #endregion

    }
}