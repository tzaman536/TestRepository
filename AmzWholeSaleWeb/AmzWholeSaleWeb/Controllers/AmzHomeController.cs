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

        public ActionResult Index()
        {
            return View(productHandler.GetProducts());


        }

        public ActionResult Products_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(productHandler.GetProducts().ToDataSourceResult(request));
        }

        #region Shopping cart 

        public ActionResult GetTotalItemsInCart([DataSourceRequest]DataSourceRequest request)
        {
            string message;
            CartHandler ch = new CartHandler();

            Cart c = ch.GetUserCart(out message);

            if (c == null)
            {
                c = new Cart();
                c.TotalItems = 0;
                c.Message = message;
                c.IsValid = false;
            }
            else
            {
                c.CartItems= ch.GetCartItems(c.CartID);
            }
            return Json(new { success = true, Cart = c }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult AddItemToCart([DataSourceRequest]DataSourceRequest request, int productID)
        {

            CartHandler ch = new CartHandler();
            string message;
            Cart c = ch.GetUserCart(out message);



            return Json(new { success = true, message = string.Format("product id is {0}", productID) }, JsonRequestBehavior.AllowGet);
        }
        #endregion 


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




    }
}