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

        #region Views
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

        public ActionResult ProductNotFound()
        {
            return View();
        }

        public ActionResult CartIsEmpty()
        {
            return View();
        }


        public ActionResult ReviewCart()
        {
            return View();
        }
        #endregion


        public ActionResult Products_Read([DataSourceRequest] DataSourceRequest request, string productFilter)
        {

            
            if (!string.IsNullOrEmpty(productFilter) && productFilter.Equals("Search products"))
            {
                productFilter = null;
            }

            var result = productHandler.GetProducts(productFilter);
            if(result == null || !result.Any())
                return RedirectToAction("ProductNotFound");
            else
                return Json(result.ToDataSourceResult(request));
        }

        public ActionResult CartItems_Read([DataSourceRequest] DataSourceRequest request)
        {


            string message;
            CartHandler ch = new CartHandler();
            Cart c = ch.GetUserCart(out message);

            if (c != null )
            {
                var result = productHandler.GetCartProducts(c.CartID);
                if (result == null || !result.Any())
                    return RedirectToAction("CartIsEmpty");
                else
                    return Json(result.ToDataSourceResult(request));
            }
            else
            {
                return RedirectToAction("CartIsEmpty");
            }
        }

        #region Shopping cart 

        public ActionResult GetTotalItemsInCart([DataSourceRequest]DataSourceRequest request)
        {
            string message;
            CartHandler ch = new CartHandler();

            
            Cart c = ch.GetUserCart(out message);

            try {
                if (c == null)
                {
                    c = new Cart();
                    c.TotalItems = 0;
                    c.Message = message;
                    c.IsValid = false;

                }
                else
                {
                    c.CartItems = ch.GetCartItems(c.CartID);
                }
            }
            catch(Exception ex)
            {
                logger.Fatal(ex);
            }
            return Json(new { success = true, Cart = c }, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public ActionResult AddItemToCart([DataSourceRequest]DataSourceRequest request, int productID, decimal unitPrice, int quantity)
        {

            
            CartHandler ch = new CartHandler();
            string message;
            Cart c = ch.GetUserCart(out message);

            if(c != null)
            {
                ch.AddCartItem(c.CartID, productID, quantity, unitPrice);
            }

            return Json(new { success = true, message = string.Format("product id is {0}", productID) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveItemFromCart([DataSourceRequest]DataSourceRequest request, int productID)
        {


            CartHandler ch = new CartHandler();
            string message;
            Cart c = ch.GetUserCart(out message);

            if (c != null)
            {
                ch.RemoveCartItem(c.CartID, productID);
            }

            return Json(new { success = true, message = string.Format("product id is {0}", productID) }, JsonRequestBehavior.AllowGet);
        }
        

        [HttpPost]
        public ActionResult SetCartProcessed([DataSourceRequest]DataSourceRequest request)
        {


            CartHandler ch = new CartHandler();
            string message;
            Cart c = ch.GetUserCart(out message);
            ch.SetCartProcessed(c.CartID,out message);

            

            return Json(new { success = true, message = "Check out completed." }, JsonRequestBehavior.AllowGet);
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

        public ActionResult GetSessionInfo([DataSourceRequest]DataSourceRequest request)
        {
            string message = string.Format("System.Web.HttpContext.Current.Session.SessionID: {0}", System.Web.HttpContext.Current.Session.SessionID);


            return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);

        }

        

        public ActionResult GetCartItemsSummary([DataSourceRequest]DataSourceRequest request)
        {
            string message;
            CartHandler ch = new CartHandler();

            Cart c = ch.GetUserCart(out message);
            IEnumerable<CartItems> CartItemSummary = new List<CartItems>();

            if (c != null)
            {
                string sql = string.Format(@"
                    select CartID,p.ProductName,sum(Quantity) as Quantity,max(Price) as Price, 5 as Shipping
                    from dbo.CartItems ci
                    inner join amz.Products p on ci.ProductID = p.ProductID
                    where CartID = {0}
                    group by CartID,ProductName
                ",c.CartID);
                CartItemSummary = ch.GetCartItemsSummary(sql);
            }
            return Json(new { success = true, CartItemSummary = CartItemSummary }, JsonRequestBehavior.AllowGet);

        }



    }
}