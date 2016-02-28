using AmzBL.Products;
using AmzModel;
using Kendo.Mvc.Examples.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;
using Simplex.Tools.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.SessionState;
using AmzBL.Sections;

namespace AmzWholeSaleWeb.Controllers
{
    public class AmzHomeController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AmzHomeController));
        private AmzProductHandler productHandler;

        public AmzHomeController()
        {
            productHandler = new AmzProductHandler();
        }

        #region AMZ Specific
        public ActionResult Index()
        {
            string currentUser = System.Web.HttpContext.Current.Request.LogonUserIdentity.Name;
            logger.InfoFormat("System.Web.HttpContext.Current.Request.LogonUserIdentity.Name {0}", currentUser);
            currentUser = User.Identity.GetUserId();
            logger.InfoFormat("User.Identity.GetUserId() {0}", currentUser);
            currentUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
            logger.InfoFormat("System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString() {0}", currentUser);
            currentUser = System.Web.HttpContext.Current.Session.SessionID;
            logger.InfoFormat("System.Web.HttpContext.Current.Session.SessionID", currentUser);

            return View(productHandler.GetProducts());


        }

   


        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Products_Read([DataSourceRequest] DataSourceRequest request, string productFilter, string productSection)
        {

            if (string.IsNullOrEmpty(productSection))
            {
                productSection = "Prime";
            }



            if (!string.IsNullOrEmpty(productFilter) && productFilter.Equals("Search products"))
            {
                productFilter = null;
            }

            var result = productHandler.GetProducts(productFilter, productSection);
            if (result == null || !result.Any())
                return RedirectToAction("ProductNotFound", "Shared", new { area = "" });
            else
                return Json(result.ToDataSourceResult(request));
        }
        #endregion




        











    }
}