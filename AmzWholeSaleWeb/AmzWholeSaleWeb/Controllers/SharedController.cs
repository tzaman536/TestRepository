using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmzBL.Products;
using AmzModel;
using Kendo.Mvc.Examples.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using log4net;
using Simplex.Tools.Cart;
using System.Net.Mime;
using Microsoft.AspNet.Identity;
using System.Web.SessionState;
using AmzBL.Sections;

namespace AmzWholeSaleWeb.Controllers
{
    public class SharedController : Controller
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(AmzHomeController));
        private AmzProductHandler productHandler;

        public SharedController()
        {
            productHandler = new AmzProductHandler();
        }

        #region Common for all sites

        public ActionResult ProductNotFound()
        {
            return View();
        }

        public ActionResult ReviewCart()
        {
            return View();
        }

        public ActionResult CartIsEmpty()
        {
            return View();
        }

        public ActionResult CartItems_Read([DataSourceRequest] DataSourceRequest request)
        {


            string message;
            CartHandler ch = new CartHandler();
            Cart c = ch.GetUserCart(out message);

            if (c != null)
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

        public ActionResult GetTotalItemsInCart([DataSourceRequest]DataSourceRequest request)
        {
            string message;
            CartHandler ch = new CartHandler();


            Cart c = ch.GetUserCart(out message);

            try
            {
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
            catch (Exception ex)
            {
                logger.Fatal(ex);
            }
            return Json(new { success = true, Cart = c }, JsonRequestBehavior.AllowGet);

        }




        [HttpPost]
        public ActionResult AddItemToCart([DataSourceRequest]DataSourceRequest request, int productID, decimal unitPrice, int quantity, bool addToExisting)
        {


            CartHandler ch = new CartHandler();
            string message;
            Cart c = ch.GetUserCart(out message);

            if (c != null)
            {
                ch.AddCartItem(c.CartID, productID, quantity, unitPrice, addToExisting);
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
            ch.SetCartProcessed(c.CartID, out message);



            return Json(new { success = true, message = "Check out completed." }, JsonRequestBehavior.AllowGet);
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
                    select CartID,p.ProductName,sum(Quantity) as Quantity,max(Price) as Price, 0 as Shipping
                    from dbo.CartItems ci
                    inner join amz.Products p on ci.ProductID = p.ProductID
                    where CartID = {0}
                    group by CartID,ProductName
                ", c.CartID);
                CartItemSummary = ch.GetCartItemsSummary(sql);
            }
            return Json(new { success = true, CartItemSummary = CartItemSummary }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetSections()
        {
            SectionDataHandler sectionHandler = new SectionDataHandler();
            var sections = sectionHandler.GetSections();


            return Json(sections, JsonRequestBehavior.AllowGet);
        }


        public ActionResult GetSessionInfo([DataSourceRequest]DataSourceRequest request)
        {
            string message = string.Format("System.Web.HttpContext.Current.Session.SessionID: {0}", System.Web.HttpContext.Current.Session.SessionID);


            return Json(new { success = true, message = message }, JsonRequestBehavior.AllowGet);

        }

        #endregion

    }
}